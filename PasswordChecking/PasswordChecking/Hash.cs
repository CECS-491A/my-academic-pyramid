
namespace PasswordChecking
{
    class Hash
    {
        private string _hashValue; // Hash Value
        private int _count; // Number of times the password has been cracked.

        public Hash(string hashValue, int count)
        {
            _hashValue = hashValue;
            _count = count;
        }

        public string HashValue { get; set; }
        public string Count { get; set; }

        public override string ToString()
        {
            return _hashValue + ": " + _count;
        }
    }
}
