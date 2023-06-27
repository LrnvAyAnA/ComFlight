using System.Security.Cryptography;
using System.Text;

namespace ComFlight.Helpers
{
    public class HashPasswordHelper
    {
        
        public static string HashPassword(string pass)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                var hash = BitConverter.ToString(hashbytes).Replace("-", "").ToLower();
                return hash;
            }

        }
    }
}
