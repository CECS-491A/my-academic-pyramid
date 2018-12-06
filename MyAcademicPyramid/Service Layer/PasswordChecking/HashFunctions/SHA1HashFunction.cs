using System.Security.Cryptography;
using System.Text;

namespace ServiceLayer.PasswordChecking.HashFunctions
{
    /// <summary>
    /// SHA1 implementation of a hash function.
    /// </summary>
    public class SHA1HashFunction : IHashFunction
    {
        /// <summary>
        /// Gets a hash value using a SHA1 hash function.
        /// </summary>
        /// <param name="input">Value to be hashed</param>
        /// <returns>The hash value</returns>
        public string GetHashValue(string input)
        {
            SHA1 sha = new SHA1Cng(); // Hash function
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input)); // Generate hash in bytes

            // Store the hash value as string with uppercase letters.
            StringBuilder sb = new StringBuilder(); // To store the hash value
            foreach(byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
