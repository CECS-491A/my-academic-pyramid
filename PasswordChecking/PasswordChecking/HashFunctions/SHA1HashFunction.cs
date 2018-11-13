using System.Security.Cryptography;
using System.Text;

namespace PasswordChecking.HashFunctions
{
    public class SHA1HashFunction : IHashFunction
    {
        public string GetHashValue(string input)
        {
            SHA1 sha; // Hash function
            byte[] hash; // Hash value
            StringBuilder sb; // To store hash value

            sha = new SHA1CryptoServiceProvider();
            hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input)); // Get hash in bytes

            // Store hash value as string with uppercase letters.
            sb = new StringBuilder();
            foreach(byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
