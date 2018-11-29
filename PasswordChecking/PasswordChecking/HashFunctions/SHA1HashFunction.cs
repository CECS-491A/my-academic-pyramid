using System.Security.Cryptography;
using System.Text;

namespace PasswordChecking.HashFunctions
{
    public class SHA1HashFunction : IHashFunction
    {
        public string GetHashValue(string input)
        {
            SHA1 sha = new SHA1Cng(); // Hash function
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input)); // Get hash in bytes

            // Store hash value as string with uppercase letters.
            StringBuilder sb = new StringBuilder(); // To store the hash value
            foreach(byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
