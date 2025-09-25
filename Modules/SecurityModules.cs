using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace POS_System.Modules
{
    internal static class SecurityModules
    {
        public static string HashPassword(string password) 
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword (string inputPassword, string storedPassword)
        {
            return HashPassword(inputPassword) == storedPassword;
                
        }
    }
}


