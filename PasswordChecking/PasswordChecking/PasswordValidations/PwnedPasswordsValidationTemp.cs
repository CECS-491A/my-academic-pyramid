using PasswordChecking.PasswordValidations;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;

namespace PasswordChecking.HashFunctions
{
    public class PwnedPasswordsValidation2
    {
        private string _url; // URL address
        private string _hashValue; // Hash Value Result

        public PwnedPasswordsValidation2(string input, string url)
        {
            _url = url;
            _hashValue = GetHashValue(input);
        }

        private string GetHashValue(string input)
        {
            SHA1 sha = new SHA1Cng(); // Hash function
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input)); // Get hash in bytes

            // Store hash value as string with uppercase letters.
            StringBuilder sb = new StringBuilder(); // To store the hash value
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calls the functions to hash the password,
        /// request the hash, and find a matching hash
        /// in the response.
        /// </summary>
        /// <returns>The hash value count.</returns>
        public int GetCount()
        {

            try
            {
                // First 5 characters of the hash value
                string prefix = _hashValue.Substring(0, 5);

                // Full URL address
                Uri uri = new Uri(_url + prefix);

                // GET request
                Task<string> response = HttpClientMethods.RequestData(uri);
                string hashListString = response.Result;

                // Find a matching hash within the string
                int hashCount = FindHash(_hashValue.Substring(5), hashListString);

                //// Split the string list of hashes into an array.
                //string[] hashListArray = StringToArray(hashListString);

                //// Find a matching hash within the array
                //int hashCount = FindHash(_hashValue.Substring(5), hashListArray);

                // Return hash count
                return hashCount;
            }
            catch (ArgumentOutOfRangeException e) // Invalid hash value
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (UriFormatException e) // Invalid URI address
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (AggregateException e) // GET request failed.
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (ArgumentNullException e) // Null values were used.
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (NullReferenceException e) // Null values were used.
            {
                Console.WriteLine(e.StackTrace);
            }

            // An exception was thrown
            return -1;
        }

        /// <summary>
        /// Finds the hash value within a string list of hash values
        /// and their counts.
        /// </summary>
        /// <param name="hashValue">The hash value to find</param>
        /// <param name="list">A string list of hashes</param>
        /// <returns>The count of the found hash value, or zero if not found.</returns>
        public int FindHash(string hashValue, string list)
        {
            int start = 0;
            try
            {
                // Find index of found hash value
                start = list.IndexOf(hashValue);
            }
            catch (Exception)
            {
                // The hash value was not found
                return 0;
            }
            // Find the count after the hash value
            Regex regex = new Regex(@":(\d+)");
            Match countString = regex.Match(list, start);

            // Check that the count found is an integer
            if (int.TryParse(countString.Value.Substring(1), out int count))
            {
                // Return the count of the hash value
                return count;
            }
            else
            {
                // The count was not found
                return -1;
            }
        }
    }
}
