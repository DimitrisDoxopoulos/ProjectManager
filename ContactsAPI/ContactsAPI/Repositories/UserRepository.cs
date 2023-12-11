using ContactsAPI.DTO;
using ContactsAPI.Models;
using ContactsAPI.Security;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> UpdateUsrAsync(int userId, UserDTO request)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstAsync();
            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = EncryptionUtil.Encrypt(request.Password);
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;

            _context.Users.Update(user);
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

            
    }
}
