using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace BlizuTebe.Services
{
    public class LocalCommunityService : ILocalCommunityService
    {
        private readonly ILocalCommunityRepository _repository;
        private readonly GeoJsonReader _geoJsonReader;
        private readonly GeoJsonWriter _geoJsonWriter;
        private readonly IUserRepository _userRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ICommunityRequestRepository _communityRequestRepository;
        private readonly IDiscussionRepository _discussionRepository;

        public LocalCommunityService(ILocalCommunityRepository repository, ICommunityRequestRepository communityRequestRepository, IDiscussionRepository discussionRepository, IUserRepository userRepository, IAnnouncementRepository announcementRepository)
        {
            _repository = repository;
            _geoJsonReader = new GeoJsonReader();
            _geoJsonWriter = new GeoJsonWriter();
            _userRepository = userRepository;
            _announcementRepository = announcementRepository;
            _communityRequestRepository = communityRequestRepository;
            _discussionRepository = discussionRepository;
        }

        public Result<LocalCommunityDto> Create(LocalCommunityDto dto)
        {
            // RUČNO mapiranje - NE koristi _mapper.Map()!
            var community = new LocalCommunity
            {
                Name = dto.Name,
                City = dto.City,
                PresidentId = dto.PresidentId,
                PhoneNumber = dto.PhoneNumber,
                Facebook = dto.Facebook
            };

            if (!string.IsNullOrEmpty(dto.Boundary))
            {
                community.Boundary = _geoJsonReader.Read<Geometry>(dto.Boundary);
                community.Boundary.SRID = 4326;
            }

            if (dto.CenterPoint != null && dto.CenterPoint.Length == 2)
            {
                var factory = new GeometryFactory(new PrecisionModel(), 4326);
                community.CenterPoint = factory.CreatePoint(new Coordinate(dto.CenterPoint[0], dto.CenterPoint[1]));
            }

            _repository.Create(community);

            return Result.Ok(MapToDto(community)); // Ručna konverzija
        }

        public Result<LocalCommunityDto> Update(long id, LocalCommunityDto dto)
        {
            var community = _repository.GetById(id);
            if (community == null)
            {
                return Result.Fail<LocalCommunityDto>("Mesna zajednica nije pronađena sa ID: " + id);
            }

            community.Name = dto.Name;
            community.City = dto.City;
            community.PresidentId = dto.PresidentId;
            community.PhoneNumber = dto.PhoneNumber;
            community.Facebook = dto.Facebook;

            if (!string.IsNullOrEmpty(dto.Boundary))
            {
                community.Boundary = _geoJsonReader.Read<Geometry>(dto.Boundary);
                community.Boundary.SRID = 4326;
            }

            if (dto.CenterPoint != null && dto.CenterPoint.Length == 2)
            {
                var factory = new GeometryFactory(new PrecisionModel(), 4326);
                community.CenterPoint = factory.CreatePoint(new Coordinate(dto.CenterPoint[0], dto.CenterPoint[1]));
            }

            _repository.Update(community);

            return Result.Ok(MapToDto(community));
        }

        public Result<LocalCommunityDto> GetById(long id)
        {
            var community = _repository.GetById(id);
            if (community == null)
            {
                return Result.Fail<LocalCommunityDto>("Mesna zajednica nije pronađena sa ID: " + id);
            }

            return Result.Ok(MapToDto(community));
        }

        public Result<List<LocalCommunityDto>> GetAll()
        {
            var communities = _repository.GetAll();
            var dtos = communities.Select(c => MapToDto(c)).ToList();
            return Result.Ok(dtos);
        }

        public Result<LocalCommunityDto> Delete(long id)
        {
            var community = _repository.GetById(id);
            if (community == null)
            {
                return Result.Fail<LocalCommunityDto>("Mesna zajednica nije pronađena sa ID: " + id);
            }

            if (_userRepository.ExistsByCommunityId(id)
            || _discussionRepository.ExistsByCommunityId(id)
            || _communityRequestRepository.ExistsByCommunityId(id)
            || _announcementRepository.ExistsByCommunityId(id))
            {
                return Result.Fail<LocalCommunityDto>("Brisanje nije dozvoljeno jer postoje povezani entiteti.");
            }

            _repository.Delete(community);
            return Result.Ok(MapToDto(community));
        }

        public Result<LocalCommunityDto> GetByLocation(double latitude, double longitude)
        {
            var factory = new GeometryFactory(new PrecisionModel(), 4326);
            var point = factory.CreatePoint(new Coordinate(longitude, latitude));

            var community = _repository.GetAll()
                .FirstOrDefault(c => c.Boundary != null && c.Boundary.Contains(point));

            if (community == null)
            {
                return Result.Fail<LocalCommunityDto>("Lokacija nije u okviru nijedne mesne zajednice");
            }

            return Result.Ok(MapToDto(community));
        }

        private LocalCommunityDto MapToDto(LocalCommunity community)
        {
            return new LocalCommunityDto
            {
                Id = community.Id,
                Name = community.Name,
                City = community.City,
                Boundary = community.Boundary != null ? _geoJsonWriter.Write(community.Boundary) : null,
                CenterPoint = community.CenterPoint != null ? new[] { community.CenterPoint.X, community.CenterPoint.Y } : null,
                PresidentId = community.PresidentId,
                PhoneNumber = community.PhoneNumber,
                Facebook = community.Facebook
            };
        }
    }

}

