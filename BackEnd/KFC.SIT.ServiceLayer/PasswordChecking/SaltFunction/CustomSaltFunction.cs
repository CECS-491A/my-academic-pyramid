using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ServiceLayer.PasswordChecking.SaltFunction
{
    public class CustomSaltFunction
    {
        public String GetSaltValue(int size)
        {
            RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rncCsp.GetBytes(salt);

            return Convert.ToBase64String(salt);


        }
    }
}