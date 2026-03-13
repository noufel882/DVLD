using System;
using System.Security.Cryptography;
using System.Text;


namespace Utils.Cryptography
{   
    public class Hashing
    {
        public static string ComputeHash_SHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
               
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
