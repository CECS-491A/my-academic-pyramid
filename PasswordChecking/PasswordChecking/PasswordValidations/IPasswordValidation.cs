
namespace PasswordChecking.PasswordValidations
{
    interface IPasswordValidation
    {
        /// <summary>
        /// Calls functions needed to find if a password
        /// exists in a library of commonly used passwords.
        /// </summary>
        /// <param name="password">A password</param>
        /// <returns>The number of times a password
        /// has been breached.</returns>
        int GetCount(string password);
    }
}