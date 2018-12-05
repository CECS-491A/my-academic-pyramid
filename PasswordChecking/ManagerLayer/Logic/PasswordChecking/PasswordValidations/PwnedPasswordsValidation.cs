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
            try
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

                if(hashCount >= 0) // A hash count was found within the response.
                {
                    // Check business rules
                    PasswordStatus status = PasswordCheckingBR.CheckPasswordCount(hashCount);

                    return status;
                }
            }
            catch (ArgumentOutOfRangeException e) // Invalid hash value
            {
                //Console.WriteLine(e.StackTrace);
                Console.WriteLine("ArgumentOutOfRangeException: Invalid hash value.");
            }
            catch (UriFormatException e) // Invalid URI address
            {
                //Console.WriteLine(e.StackTrace);
                Console.WriteLine("UriFormatException: Invalid URI adress.");
            }
            catch (AggregateException e) // GET request failed.
            {
                //Console.WriteLine(e.StackTrace);
                Console.WriteLine("AggregateException: GET request failed.");
            }
            catch (ArgumentNullException e) // Null values were used.
            {
                //Console.WriteLine(e.StackTrace);
                Console.WriteLine("ArgumentNullException: Null values were used.");
            }
            catch (NullReferenceException e) // Null values were used.
            {
                //Console.WriteLine(e.StackTrace);
                Console.WriteLine("NullReferenceException: Null values were used.");
            }

            // An exception was thrown
            return null;
        }

        /// <summary>
        /// Finds the hash value within a string list of hash values
        /// and their counts.
        /// </summary>
        /// <param name="hashValue">The hash value to find</param>
        /// <param name="list">A string list of hashes</param>
        /// <returns>The count of the found hash value, zero if not found,
        /// or -1 if no hash count was found in the list</returns>
        public int FindHash(string hashValue, string list) // How to check if list is valid response? integration testing?
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

        ///// <summary>
        ///// converts a string of values separated by a newline
        ///// into an array.
        ///// </summary>
        ///// <param name="liststring">the string list of values</param>
        ///// <returns>the string list of values as an array</returns>
        //public string[] StringToArray(string liststring)
        //{
        //    // split each line of the hash list into an array
        //    string[] listarray = liststring.Split('\n');

        //    return listarray;
        //}

        ///// <summary>
        ///// implementation of binary search to find if a value exists
        ///// in an array of values.
        ///// </summary>
        ///// <param name="hashvalue"></param>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public int FindHash(string hashvalue, string[] list)
        //{
        //    // binary search
        //    int min = 0;
        //    int max = list.Length - 1;

        //    while (min <= max)
        //    {
        //        int mid = (min + max) / 2;
        //        // split the hash value from the count
        //        string[] hash = list[mid].Split(':');
        //        // full hash value to match
        //        string hashvaluefound = hash[0];

        //        if (hashvaluefound.CompareTo(hashvalue) == 0) // match found
        //        {
        //            return Convert.ToInt32(hash[1]); // return hash value count
        //        }
        //        else if (hashvaluefound.CompareTo(hashvalue) > 0) // searched hash value precedes compared hash value
        //        {
        //            max = mid - 1;
        //        }
        //        else // compared hash value precedes searched hash value
        //        {
        //            min = mid + 1;
        //        }
        //    }
        //    return 0;
        //}
    }
}