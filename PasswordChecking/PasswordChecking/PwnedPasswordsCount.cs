using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordChecking.HashFunctions
{
    public class PwnedPasswordsCount
    {
        private IHashFunction _hashFunction; // Hash Function
        private string _url; // URL address
        private string _hashValue; // Hash Value Result

        public PwnedPasswordsCount(IHashFunction hashFunction, string url)
        {
            _hashFunction = hashFunction;
            _url = url;
        }

        /// <summary>
        /// Calls the functions to hash the password,
        /// request the hash, and find a matching hash
        /// in the response.
        /// </summary>
        /// <returns>The hash value count.</returns>
        public int GetCount(string password)
        {

            try
            {
                // Use the hash function to get the hash value of the password.
                _hashValue = _hashFunction.GetHashValue(password);

                // First 5 characters of the hash value
                string prefix = _hashValue.Substring(0, 5);

                // Full URL address
                Uri uri = new Uri(_url + prefix);

                // GET request
                Task<string> response = HttpClientMethods.RequestData(uri);
                string hashListString = response.Result;

                // Find a matching hash within the response
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
            // The hash value was found in the list
            if (list.Contains(hashValue))
            {
                // Find index of found hash value
                int start = list.IndexOf(hashValue);

                // Find the count after the hash value
                Regex regex = new Regex(@":(\d+)");
                Match countString = regex.Match(list, start);

                // Check that the count found is an integer
                if (int.TryParse(countString.Value.Substring(1), out int count))
                {
                    // Return the count of the hash value
                    return count;
                }

                // The count was not found
                return -1;
            }

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