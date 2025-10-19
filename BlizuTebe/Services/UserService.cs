using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UserService(IUserRepository userRepository, IMapper mapper, IWebHostEnvironment environment)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _environment = environment;
        }

        public Result<UserDto> Register(UserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            if (newUser == null)
            {
                return Result.Fail<UserDto>("User mapping failed.");
            }

            // Provera da li korisnik već postoji
            var existing = _userRepository.GetAll().FirstOrDefault(u => u.Username == dto.Username);
            if (existing != null)
            {
                return Result.Fail<UserDto>("Username already exists.");
            }

            // Sačuvaj profilnu sliku ako je poslata
            if (dto.Picture != null && dto.Picture.Length > 0)
            {
                newUser.ProfilePicture = SaveImage(dto.Picture);
            }

            newUser.IsVerified = false;
            newUser.Rating = 0.0;
            newUser.Role = UserRole.Member;
            newUser.DateOfBirth = DateTime.SpecifyKind(newUser.DateOfBirth, DateTimeKind.Utc);

            _userRepository.Create(newUser);
            return Result.Ok(_mapper.Map<UserDto>(newUser));
        }

        public Result<UserViewDto> DeleteById(long id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return Result.Fail<UserViewDto>("User not found with ID: " + id);
            }

            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                DeleteImage(user.ProfilePicture);
            }

            _userRepository.Delete(user);
            return Result.Ok(_mapper.Map<UserViewDto>(user));
        }

        public Result<List<UserViewDto>> GetAll()
        {
            var users = _userRepository.GetAll();
            return Result.Ok(_mapper.Map<List<UserViewDto>>(users));
        }

        public Result<UserViewDto> GetById(long id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return Result.Fail("Not found");
            }
            else return Result.Ok(_mapper.Map<UserViewDto>(user));
        }

        public Result<UserViewDto> UpdateUser(long id, UserViewDto dto)
        {
            var userToUpdate = _userRepository.GetById(id);
            if (userToUpdate == null)
            {
                return Result.Fail<UserViewDto>("User not found with ID: " + id);
            }

            _mapper.Map(dto, userToUpdate);


            if (dto.Picture != null && dto.Picture.Length > 0)
            {
                if (!string.IsNullOrEmpty(userToUpdate.ProfilePicture))
                {
                    DeleteImage(userToUpdate.ProfilePicture);
                }
                userToUpdate.ProfilePicture = SaveImage(dto.Picture);
            }
            else if (!string.IsNullOrEmpty(dto.ProfilePicture))
            {
                userToUpdate.ProfilePicture = dto.ProfilePicture;
            }
            userToUpdate.DateOfBirth = DateTime.SpecifyKind(userToUpdate.DateOfBirth, DateTimeKind.Utc);

            _userRepository.Update(userToUpdate);
            return Result.Ok(_mapper.Map<UserViewDto>(userToUpdate));
        }

        public Result<UserViewDto> VerifyUser(long id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return Result.Fail<UserViewDto>("User not found.");
            }

            user.IsVerified = true;
            _userRepository.Update(user);

            return Result.Ok(_mapper.Map<UserViewDto>(user));
        }

        private string SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "users");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        private void DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "images", "users", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    
}
}
