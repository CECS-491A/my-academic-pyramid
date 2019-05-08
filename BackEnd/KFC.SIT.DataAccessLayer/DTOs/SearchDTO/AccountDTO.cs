using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.SearchDTO
{
    public class AccountDTO
    {
        public AccountDTO()
        {
            IsStudent = false;
            SchoolId = 0;
        }

        public bool IsStudent { get; set; }
        public int SchoolId { get; set; }
    }
}
