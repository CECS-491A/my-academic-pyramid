using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PasswordQA : IEntity
    {
        public PasswordQA(String Q1, String A1, String Q2, String A2, String Q3, String A3)
        {
            Question1 = Q1;
            Answer1 = A1;
            Question2 = Q2;
            Answer2 = A2;
            Question3 = Q3;
            Answer3 = A3;
        }

        public int Id { get; set; }
        public String Question1 { get; set; }
        public String Answer1 { get; set; }
        
        public String Question2 { get; set; }
        public String Answer2 { get; set; }

        public String Question3 { get; set; }
        public String Answer3 { get; set; }
    }
}
