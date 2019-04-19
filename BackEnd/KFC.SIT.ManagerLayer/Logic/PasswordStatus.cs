namespace WebAPI.Logic
{
    /// <summary>
    /// Holds the security of a password.
    /// </summary>
    public class PasswordStatus
    {
        // An integer representation of a password's security
        public int status { get; private set; }

        public PasswordStatus(int s)
        {
            status = s;
        }
    }
}
