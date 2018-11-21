
namespace PasswordChecking
{
    public class Hash
    {
        private string _hashValue; // Hash Value
        private int _count; // Number of times the password has been cracked.

        public Hash(string hashValue, int count)
        {
            _hashValue = hashValue;
            _count = count;
        }

        public string HashValue // Hash Value
        {
            get
            {
                return _hashValue;
            }
            private set
            {
                _hashValue = value;
            }
        }
        public int Count // Number of times the password has been cracked.
        {
            get
            {
                return _count;
            }
            private set
            {
                _count = value;
            }
        }

        public override string ToString()
        {
            return _hashValue + ": " + _count;
        }
    }
}
