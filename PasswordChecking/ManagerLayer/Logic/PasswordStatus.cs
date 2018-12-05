namespace ManagerLayer.Logic
{
    /// <summary>
    /// Holds the security of a password.
    /// </summary>
    public class PasswordStatus
    {
        // An integer representation of a password's security
        private int _status;

        public PasswordStatus(int status)
        {
            _status = status;
        }

        public int Status { get; set; }
    }
}
