using System;

namespace PasswordChecking.PasswordValidations
{
    public class Validation
    {
        private Boolean _valid;
        private Boolean _warnUser;

        public Validation(Boolean valid, Boolean warnUser)
        {
            _valid = valid;
            _warnUser = warnUser;
        }

        public Boolean Valid { get; set; }

        public Boolean WarnUser { get; set; }

        // ToString()
    }
}
