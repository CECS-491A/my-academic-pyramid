using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Models; 

namespace ManagerLayer.Constants
{
    public static class RedirectUserUtility
    {
        public static string GetUrlAddress(string category)
        {
            string destinationUrl = null;
            switch(category)
            {
                case "Student":
                    destinationUrl = "sefjio";
                    break;
                case "NonStudent":
                    destinationUrl = "sioj";
                    break;
                case "NewUser":
                    destinationUrl = "https://myacademicpyramid/#/Registration";
                    break;
            }
            return destinationUrl;
        }
    }
}