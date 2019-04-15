

using WebAPI.BusinessRules;
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using ServiceLayer.HttpClients;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Logic.PasswordChecking.PasswordValidations
{
    /// <summary>
    /// A password validation implementation using Troy Hunt's Pwned Passwords Service.
    /// <https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/>
    /// </summary>
    public class PwnedPasswordsValidation : IPasswordValidation
    {
        private IHashFunction _hashFunction; // Hash Function
        private string _url; // URL address
        private string _hashValue; // Hash Value Result
        private IHttpClient client = new HttpClientString(); // Http Client

        public PwnedPasswordsValidation(IHashFunction hashFunction, string url)
        {
            _hashFunction = hashFunction;
            _url = url;
        }

        /// <summary>
        /// Validates the security of a password based on the number
        /// of times it has been breached according to Pwned Passwords.
        /// </summary>
        /// <param name="password">A user password</param>
        /// <returns></returns>
        public PasswordStatus Validate(string password)
        {
            // Use the hash function to get the hash value of the password.
            _hashValue = _hashFunction.GetHashValue(password).Hash + _hashFunction.GetHashValue(password).Salt;
            Console.WriteLine("Hash Value: " + _hashValue); // Demo

            // First 5 characters of the hash value
            string prefix = _hashValue.Substring(0, 5);

            // Full URL address
            Uri uri = new Uri(_url + prefix);
            Console.WriteLine("Url: " + uri); // Demo

            // Http GET request
            Task<string> response = client.RequestData(uri); 
            string hashlistString = response.Result;

            // Deserialize json into a dictionary<hashValue,counts>
            Dictionary<string, int> hashes = JsonToDictionary(hashlistString);

            // Find the hashvalue in the dictionary
            int hashCount = FindHash(_hashValue.Substring(5), hashes);

            // Check business rules
            PasswordStatus status = PasswordCheckingBR.CheckPasswordCount(hashCount);

            return status;
        }

        /// <summary>
        /// Deserialize json string into a dictionary.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Dictionary<string,int> JsonToDictionary(string list)
        {
            // Format json string with double quotation marks and commas
            list = list.Replace("\n", ",");
            list = "{" + list + "}";

            // Deserializae as dictionary
            var hashes = JsonConvert.DeserializeObject<Dictionary<string, int>>(list);

            return hashes;
        }

        /// <summary>
        /// Find a hash value within a dictionary.
        /// </summary>
        /// <param name="hashValue">Hash value to find</param>
        /// <param name="hashes">Dictionary of hash hvalues and counts</param>
        /// <returns>Hash value count if found, zero if not found.</returns>
        public int FindHash(string hashValue, Dictionary<string, int> hashes)
        {
            if (hashes.TryGetValue(hashValue, out int count))
            {
                // Return hash count
                return count;
            }
            // Hash value was not found
            return 0;
        }
    }
}