
namespace PasswordChecking.PasswordValidations
{
    interface IPasswordValidation
    {
        
        Validation Validate(string password);
    }
}