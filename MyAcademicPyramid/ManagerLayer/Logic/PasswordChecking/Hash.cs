namespace ManagerLayer.Logic.PasswordChecking
{
    public class Hash
    {
        public Hash(string hashValue, int count)
        {
            HashValue = hashValue;
            Count = count;
        }

        public string HashValue { get; set; }
        public int Count { get; set; }
    }
}