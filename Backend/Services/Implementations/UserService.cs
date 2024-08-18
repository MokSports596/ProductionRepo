using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MokSportsApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !user.VerifyPassword(password))
            {
                return null;
            }
            return user;
        }

        public async Task AddUser(User user)
        {
            // Manually assign a UserId
            user.UserId = await GetNextUserId();

            // Implement password hashing here
            user.PasswordSalt = GenerateSalt();
            user.PasswordHash = HashPassword(user.PasswordHash, user.PasswordSalt);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsername(int userId, string username)
        {
            var user = await GetUserById(userId);
            if (user != null)
            {
                user.Username = username;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<int> GetNextUserId()
        {
            var lastUser = await _context.Users.OrderByDescending(u => u.UserId).FirstOrDefaultAsync();
            return lastUser != null ? lastUser.UserId + 1 : 1;
        }

        public async Task<List<League>> GetUserLeaguesAsync(int userId)
        {
            return await _context.UserLeagues
                                .Where(ul => ul.UserId == userId)
                                .Select(ul => ul.League)
                                .ToListAsync();
        }


        private string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        private string HashPassword(string password, string salt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(salt)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
