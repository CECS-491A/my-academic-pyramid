using DataAccessLayer.Repository;
namespace DataAccessLayer
{
    public class Claim : IEntity
    {

        public Claim(string value)
        {
            
            Value = value;
        }

        public int Id { get; set; }
        public string Value { get; set; }

  

        
    }
}
