using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    internal class PasswordEncryptService
    {
        public string PasswordEncrypt(string password)
        {
            var ecnryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return ecnryptedPassword;
        }

        public string GenerateRandomPassword(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool EqualPassrods(string inputPassword, string originalPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, originalPassword);
        }
    }
}
