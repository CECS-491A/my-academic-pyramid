using ManagerLayer.BusinessRules;
using DataAccessLayer.PasswordChecking.HashFunctions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ServiceLayer.HttpClients;

namespace ManagerLayer.Logic.PasswordChecking.PasswordValidations
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
            _hashValue = _hashFunction.GetHashValue(password);
            Console.WriteLine("Hash Value: " + _hashValue); // Demo

            // First 5 characters of the hash value
            string prefix = _hashValue.Substring(0, 5);

            // Full URL address
            Uri uri = new Uri(_url + prefix);
            Console.WriteLine("Url: " + uri); // Demo

            // Http GET request
            Task<string> response = client.RequestData(uri);
            string hashListString = response.Result;

            // Find a matching hash within the response
            int hashCount = FindHash(_hashValue.Substring(5), hashListString);
            Console.WriteLine("Count: " + hashCount); // Demo

            // Check business rules
            PasswordStatus status = PasswordCheckingBR.CheckPasswordCount(hashCount);

            return status;
        }

        /// <summary>
        /// Finds the hash value within a string list of hash values
        /// and their counts.
        /// </summary>
        /// <param name="hashValue">The hash value to find</param>
        /// <param name="list">A string list of hashes</param>
        /// <returns>The count of the found hash value, zero if not found,
        /// or -1 if no hash count was found in the list</returns>
        public int FindHash(string hashValue, string list) // FIX: deal with -1 return in BR? or will JSON throw exception
        {
            // The hash value was found in the list
            if (list.Contains(hashValue))
            {
                Console.WriteLine("Hash Value Found."); // Demo

                // Find index of found hash value
                int start = list.IndexOf(hashValue);

                // Find the count after the hash value
                Regex regex = new Regex(@":(\d+)");
                Match countString = regex.Match(list, start);

                // Check that the count found is an integer
                if (int.TryParse(countString.Value.Substring(1), out int count))
                {
                    Console.WriteLine("Hash Count Found."); // Demo
                    // Return the count of the hash value
                    return count;
                }
                Console.WriteLine("Hash Count NOT Found."); // Demo
                // The count was not found
                return -1;
            }
            Console.WriteLine("Hash Value NOT Found."); // Demo
            // The hash value was not found
            return 0;
        }
    }
}