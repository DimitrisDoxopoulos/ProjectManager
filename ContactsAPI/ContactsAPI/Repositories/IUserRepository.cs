using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IUserRepository
    {
        Task<bool> SignUpUserAsync(UserDTO userDTO);
        Task<User> GetUserAsync(string username, string password);
        Task<User> UpdateUsrAsync(int userId, UserUpdateDTO request);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> ChangePassword(PasswordUpdateDTO request);
    }
}
