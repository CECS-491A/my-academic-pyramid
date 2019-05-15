using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UADConstants
{
    public static class MongoDBAction
    {
        public static readonly string Login = "Login";
        public static readonly string Logout = "Logout";
        public static readonly string[] Feature = { "Discussion Forum", "Search", "Registration", "Chat", "User Profile" };
        public static readonly string[] Page = { "Discussion Forum Page", "Search Page", "Registration Page", "Chat Page",
                                                    "User Profile Page", "Logging Page", "Dashboard Page" };

    }
}
