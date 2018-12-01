using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authorization.Interfaces
{
    public interface IUser
    {
            
         String UserName { get;  set; }
         List<String> userClaims { get; set; }




    }




}
