using ServiceLayer.PasswordChecking.SaltFunction;
using System.Security.Cryptography;
using System.Text;

namespace ServiceLayer.PasswordChecking.HashFunctions
{
    public class SHA256HashFunction:IHashFunction
    {
        public string GetHashValue(string input)
        {
            CustomSaltFunction saltFunc = new CustomSaltFunction();
            string salt = saltFunc.GetSaltValue(32);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);

            SHA256 sha = new SHA256Cng(); // Hash function
            byte[] hash = sha.ComputeHash(bytes); // Generate hash in bytes

            // Store the hash value as string with uppercase letters.
            StringBuilder sb = new StringBuilder(); // To store the hash value
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}