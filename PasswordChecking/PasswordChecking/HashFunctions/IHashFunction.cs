

namespace PasswordChecking.HashFunctions
{
    public interface IHashFunction
    {
        /// <summary>
        /// Implementation of a hash function.
        /// </summary>
        /// <param name="input">Value to be hashed.</param>
        /// <returns>Hash value result as string.</returns>
        string GetHashValue(string input);
    }
}
