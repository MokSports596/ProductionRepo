using System;
using System.Security.Cryptography;
using System.Text;

namespace MokSportsApp.Models
{
    public class User
    {
        public int UserId { get; set; } // Removing identity attribute
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public void SetPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = Convert.ToBase64String(hmac.Key);
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public bool VerifyPassword(string password)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(PasswordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(computedHash) == PasswordHash;
            }
        }
    }
}
