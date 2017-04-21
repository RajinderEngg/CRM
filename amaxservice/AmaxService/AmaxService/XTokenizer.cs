using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace AmaxService
{
    static class XTokenizer
    {
        private const string SecuritySalt = "ofjX!kBw!q3o0is0cHfol9oH2U7/$5~1";
        private static Random rand = new Random();
        private const int tokenExpiryDelay = 3600*12*7;

        public static string Tokenize(object payload)
        {
            var header = new Dictionary<string, object>()
            {
                { "typ", "JWT" },
                { "alg", "MD5" },
                { "exp", (((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds) + tokenExpiryDelay)},
                { "seris", rand.Next(1,999) }

            };

            string encodedHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header)));
            string encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload)));

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            string tkn = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes((encodedHeader + "." + encodedData) + SecuritySalt)));

            return string.Concat(encodedHeader, ".", encodedData, ".", tkn);
        }

        public static Dictionary<string, object> ValidateToken(string token, bool throwExceptions = false)
        {
            Dictionary<string, object> payload = null;
            string[] tokenData = token.Split('.');

            //Validating Token fFormate
            if (tokenData.Length != 3)
                if (throwExceptions)
                    throw new Exception("Invalid Token formate.");
                else
                    return payload;

            //Expory check
            try
            {
                var tokenHeader = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Convert.FromBase64String(tokenData[0])));
                if (Convert.ToInt32(tokenHeader["exp"]) < ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds))
                    throw new Exception("Token expired");
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                {
                    throw ex;
                }
                else
                {
                    return payload;
                }
            }

            //Authorizing Token
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            string tkn = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes((tokenData[0] + "." + tokenData[1]) + SecuritySalt)));

            if (!tkn.Equals(tokenData[2]))
                if (throwExceptions)
                    throw new Exception("Unauthorized token");
                else
                    return payload;

            //converting data
            payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Convert.FromBase64String(tokenData[1])));

            return payload;
        }
    }
}
