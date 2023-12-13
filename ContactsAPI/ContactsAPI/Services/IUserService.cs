using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IUserService
    {
        Task SignUpUserAsync(UserDTO request);
        Task<User?> LoginUserAsync(UserLoginDTO request);
        Task<User?> UpdateUserAccountInfoAsync(UserUpdateDTO request, int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> ChangePasswordAsync(PasswordUpdateDTO request);
    }
}
