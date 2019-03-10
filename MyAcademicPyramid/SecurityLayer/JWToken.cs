using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web;

namespace SecurityLayer
{
    public static class JWToken
    {
        public static string GenerateToken(Dictionary<string, string> payload, string algorithm)
        {
            string token = "";
            string tokenType = "JWT";
            string key = "JOIENFUPBFJESFOIEJGNEOPFENPPFENFBPPNFIEPUBRGHFCMK";
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            
            Dictionary<string, string> header = new Dictionary<string, string>();
            HMACSHA256 hmac = new HMACSHA256(keyByte);
            header.Add("alg", algorithm);
            header.Add("typ", tokenType);
            string payloadJson = JsonConvert.SerializeObject(payload);
            string headerJson = JsonConvert.SerializeObject(header);
            byte[] headerBytes = Encoding.UTF8.GetBytes(headerJson);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
            string headerEncode = HttpServerUtility.UrlTokenEncode(headerBytes);
            string payloadEncode = HttpServerUtility.UrlTokenEncode(payloadBytes);
            byte[] preSign = Encoding.UTF8.GetBytes(headerEncode+payloadEncode);
            byte[] byteSign = hmac.ComputeHash(preSign);
            string signEncode = HttpServerUtility.UrlTokenEncode(byteSign);
            

            Console.Out.WriteLine($"Header: {headerEncode} Payload: {payloadEncode}\n");
            Console.Out.WriteLine($"{headerEncode}.{payloadEncode}.{signEncode}");

            return token;
        }

        private static string UrlBase64DictEncoding(Dictionary<string, string> dict)
        {
            string jsonDict = JsonConvert.SerializeObject(dict);
            byte[] byteJsonDict = Encoding.UTF8.GetBytes(jsonDict);
            string base64EncodedDict = HttpServerUtility.UrlTokenEncode(byteJsonDict);
            return base64EncodedDict;
        }

        public static string GenerateToken(String jsonPayload, string algorithm)
        {
            string token = "";

            return token;
        }
    }
}
