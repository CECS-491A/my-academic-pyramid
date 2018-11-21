using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class StudentManagement : IStudentManagement
    {
        public void DeleteUserOwnAccount()
        {
            Console.WriteLine("DeleteUserOwnAccount is allowed");
        }
    }
}
