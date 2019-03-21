

using DataAccessLayer.Models;

namespace ServiceLayer.PasswordChecking.HashFunctions
{
    /// <summary>
    /// A hash function that returns a hash value.
    /// </summary>
    public interface IHashFunction
    {
        /// <summary>
        /// Gets a hash value from a hash function
        /// </summary>
        /// <param name="input">Value to be hashed.</param>
        /// <returns>Hash value result as string.</returns>
        HashSalt GetHashValue(string input);
    }
}
