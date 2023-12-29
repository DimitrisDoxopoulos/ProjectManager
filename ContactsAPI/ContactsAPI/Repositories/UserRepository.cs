using ContactsAPI.DTO;
using ContactsAPI.Models;
using ContactsAPI.Security;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ContactsAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ContactsAppContext context) : base(context) { }

        public async Task<bool> SignUpUserAsync(UserDTO request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x  => x.Email == request.Email);
            if (existingUser != null) return false;

            var user = new User()
            {
                Username = request.Username,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Password = EncryptionUtil.Encrypt(request.Password)
            };

            await _context.Users.AddAsync(user);
            return true;
        }

        public async Task<User> UpdateUsrAsync(int userId, UserUpdateDTO request)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstAsync();
            user.Username = request.Username;
            user.Email = request.Email;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;

            _context.Users.Update(user);
            return user;
        }

        public async Task<User?> ChangePassword(PasswordUpdateDTO request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) return null;
            if (!request.NewPassword!.Equals(request.NewPasswordConfirm) || !IsPasswordValid(request.NewPassword)) return null;
            if (!request.NewPassword!.Equals(request.NewPasswordConfirm)) return null;
            user.Password = EncryptionUtil.Encrypt(request.NewPassword);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.Where(x => x.Username == username || x.Email == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username || x.Email == username);
            if (user is null) return null;
            if (!EncryptionUtil.IsValidPassword(password, user.Password)) return null;
            return user;
        }

        private bool IsPasswordValid(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$";
            return !string.IsNullOrEmpty(password) && Regex.IsMatch(password, pattern);
        }
    }
}
