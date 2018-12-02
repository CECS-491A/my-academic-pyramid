using System;

namespace ManagerLayer.Logic
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
