using AutoMapper;
using ContactsAPI.DTO;
using ContactsAPI.Models;
using ContactsAPI.Repositories;

namespace ContactsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _unitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> LoginUserAsync(UserLoginDTO request)
        {
            var user = await _unitOfWork.UserRepository.GetUserAsync(request.Username!, request.Password!);
            if (user == null) return null;
            return user;
        }

        public async Task SignUpUserAsync(UserDTO request)
        {
            if (!await _unitOfWork.UserRepository.SignUpUserAsync(request))
                throw new ApplicationException("User Already Exists");
            await _unitOfWork.SaveAsync();
        }

        public async Task<User?> UpdateUserAccountInfoAsync(UserUpdateDTO request, int userId)
        {
            var user = await _unitOfWork.UserRepository.UpdateUsrAsync(userId, request);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User?> ChangePasswordAsync(PasswordUpdateDTO request)
        {
            var user = await _unitOfWork.UserRepository.ChangePassword(request);
            await _unitOfWork.SaveAsync();
            return user;
        }
    }
}
