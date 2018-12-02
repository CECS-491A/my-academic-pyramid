namespace ManagerLayer.Logic.PasswordChecking.PasswordValidations
{
    interface IPasswordValidation
    {
        
        Validation Validate(string password);
    }
}