using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class GiftService : IGiftService
    {
        private readonly IMapper _mapper;
        private readonly IGiftRepository giftRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserRepository userRepository;

        public GiftService(IMapper mapper, IGiftRepository
            repository, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
        {
            _mapper = mapper;
            giftRepository = repository;
            this.webHostEnvironment = webHostEnvironment;
            this.userRepository = userRepository;
        }

        public Result<GiftDto> Create(GiftUpdateDto dto)
        {
            var newGift = _mapper.Map<Gift>(dto);

            newGift.PostDate = DateTime.UtcNow;
            newGift.ExpireDate = newGift.PostDate.AddMonths(1);
            newGift.Status = GiftStatus.Pending;

            if (dto.Attachment != null && dto.Attachment.Length > 0)
            {
                newGift.Attachment = SaveImage(dto.Attachment);
            }

            giftRepository.Create(newGift);
            return Result.Ok(_mapper.Map<GiftDto>(newGift));

        }

        public Result<GiftDto> Update(GiftUpdateDto dto)
        {
            var gift = giftRepository.GetById(dto.Id);
            if (gift == null)
            {
                return Result.Fail<GiftDto>("Gift not found with ID: " + dto.Id);
            }
            _mapper.Map(dto, gift);
            if (dto.Attachment != null && dto.Attachment.Length > 0)
            {
                if (!string.IsNullOrEmpty(gift.Attachment))
                {
                    DeleteImage(gift.Attachment);
                }
                gift.Attachment = SaveImage(dto.Attachment);
            }
            gift.Status = dto.Status;
            giftRepository.Update(gift);
            return Result.Ok(_mapper.Map<GiftDto>(gift));
        }

        public Result<GiftDto> Delete(long id)
        {
            var gift = giftRepository.GetById(id);
            if (gift == null)
            {
                return Result.Fail<GiftDto>("Gift not found with id: " + id);
            }
            //TODO: ovde je opet onaj cudan deo delete image, proveri sta to znaci
            if (!string.IsNullOrEmpty(gift.Attachment))
            {
                DeleteImage(gift.Attachment);
            }

            giftRepository.Delete(id);
            return Result.Ok(_mapper.Map<GiftDto>(gift));
        }

        private PagedResult<Gift> GetAllInternal(int page, int size, GiftCategory? category, GiftStatus? status)
        {
            var gifts = giftRepository.GetAll(page, size, category, status);
            UpdateExpiredGifts(gifts.Results);

            return gifts;
        }

        public Result<PagedResult<GiftDto>> GetAll(int page, int size, GiftCategory? category)
        {
            var gifts = GetAllInternal(page, size, category, null);

            var result = new PagedResult<GiftDto>(
                _mapper.Map<List<GiftDto>>(gifts.Results),
                gifts.TotalCount
            );

            return Result.Ok(result);
        }

        public Result<GiftDto> GetById(long id)
        {
            var gift = giftRepository.GetById(id);
            if (gift == null)
            {
                return Result.Fail("Not Found");
            }

            else
            {
                if (gift.ExpireDate < DateTime.UtcNow && gift.Status == GiftStatus.Pending)
                {
                    gift.Status = GiftStatus.Expired;
                    giftRepository.Update(gift);
                }
            }
            return Result.Ok(_mapper.Map<GiftDto>(gift));
        }

        public Result<PagedResult<GiftDto>> GetPending(int page, int size)
        {
            var gifts = GetAllInternal(page, size, null, GiftStatus.Pending);

            var result = new PagedResult<GiftDto>(
                _mapper.Map<List<GiftDto>>(gifts.Results),
                gifts.TotalCount
            );

            return Result.Ok(result);
        }

        public Result<PagedResult<GiftDto>> GetCompleted(int page, int size)
        {
            var gifts = GetAllInternal(page, size, null, GiftStatus.Completed);

            var result = new PagedResult<GiftDto>(
                 _mapper.Map<List<GiftDto>>(gifts.Results),
                 gifts.TotalCount
             );

            return Result.Ok(result);
        }

/*        public Result<PagedResult<GiftDto>> GetByCategory(int page, int size, GiftCategory giftCategory)
        {
            var res = giftRepository.GetByCategory(helpType, helpCategory).ToList();
            return Result.Ok(_mapper.Map<List<GiftDto>>(res));
        }*/



        //public Result<PagedResult<GiftDto>> GetMyExpired(HelpType helpType, long id)
        //{
        //    var res = GetFilteredInternal(helpType, HelpStatus.Expired).Where(x => x.UserId == id).ToList();
        //    return Result.Ok(res);
        //}

        private string SaveImage(IFormFile file)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "images", "Gifts");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        private void DeleteImage(string fileName)
        {
            var path = Path.Combine(webHostEnvironment.WebRootPath, "images", "Gifts", fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void UpdateExpiredGifts(List<Gift> gifts)
        {
            foreach (var gift in gifts)
            {
                if (gift.ExpireDate < DateTime.UtcNow && gift.Status == GiftStatus.Pending)
                {
                    gift.Status = GiftStatus.Expired;
                    giftRepository.Update(gift);
                }
            }
        }
    }
}
