using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LittleCompany.BL
{
    public class Cyptography
    {
        public static string salt { get; set; }


        public static string GenerateHash(string value, string extrasalt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + extrasalt + value);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }




    }
}