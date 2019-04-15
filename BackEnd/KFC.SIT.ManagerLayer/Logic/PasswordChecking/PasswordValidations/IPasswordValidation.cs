namespace WebAPI.Logic.PasswordChecking.PasswordValidations
{
    /// <summary>
    /// A validation method to test the security
    /// of a user's password.
    /// </summary>
    interface IPasswordValidation
    {
        /// <summary>
        /// Validates the security of a password.
        /// </summary>
        /// <param name="password">A user password</param>
        /// <returns>The password's security status</returns>
        PasswordStatus Validate(string password);
    }
}