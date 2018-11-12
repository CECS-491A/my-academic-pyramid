
namespace PasswordChecking
{
    class Hash
    {
        private string _hashValue;
        private int _count;

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
