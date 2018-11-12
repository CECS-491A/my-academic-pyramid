using System;
using System.Net;

namespace PasswordChecking.HashFunctions
{
    class PasswordValidation
    {
        private IHashFunction _hashFunction; // Hash Function
        private string _password; // Password
        private string _url; // URL
        private string _hashValue; // Hash Value Result
        WebClient client = new WebClient();

        public PasswordValidation(IHashFunction hashFunction, string password, string url)
        {
            _hashFunction = hashFunction;
            _password = password;
            _url = url;
        }

        /// <summary>
        /// Calls the functions to hash the password,
        /// request the hash, and find a matching hash
        /// in the response.
        /// </summary>
        /// <returns>The matching hash if found, null if not found</returns>
        public Hash Run()
        {
            // Use the hash function to get the hash value of the password.
            _hashValue = _hashFunction.GetHashValue(_password);

            // First 5 character of the hash value
            string prefix = _hashValue.Substring(0, 5);

            // GET request
            string response = client.DownloadString(_url + prefix);

            // Find a matching hash
            Hash hash = FindHash(_hashValue, prefix, response);

            // Return hash
            return hash;
        }

        /// <summary>
        /// Implemenatation of Binary Search to find matching hash value.
        /// </summary>
        /// <param name="hashValue">Hash Value to find.</param>
        /// <param name="prefix">5 character prefix of hash value.</param>
        /// <param name="list">All hash values with matching prefixes and their counts</param>
        /// <returns>Matching hash value if found, null if not found</returns>
        public Hash FindHash(string hashValue, string prefix, string list)
        {
            // Split each line of the hash list into an array
            string[] hashesString = list.Split('\n');

            // Binary search
            int min = 0;
            int max = hashesString.Length - 1;

            while(min <= max)
            {
                int mid = (min + max) / 2;
                // Split the hash value from the count
                string[] hash = hashesString[mid].Split(':');
                // Full hash value to match
                string value = prefix + hash[0];

                if (value.CompareTo(hashValue) == 0) // Match found
                {
                    return new Hash(value, Convert.ToInt32(hash[1])); // Return matching hash value
                }
                else if(value.CompareTo(hashValue) > 0) // Searched hash value precedes compared hash value
                {
                    max = mid - 1;
                }
                else // Compared hash value precedes searched hash value
                {
                    min = mid + 1;
                }
            }
            return null;
        }
    }
}
