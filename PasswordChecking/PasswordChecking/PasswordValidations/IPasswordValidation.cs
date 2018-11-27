
namespace PasswordChecking.PasswordValidations
{
    interface IPasswordValidation
    {
        /// <summary>
        /// Calls functions needed to find if a
        /// password exists in a library of common
        /// passwords.
        /// </summary>
        /// <returns>The number of times the
        /// password has been breached,
        /// or zero if not found</returns>
        int Run();
    }
}
