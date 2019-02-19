using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Server_Films
{
    public class GetHeshMd5
    {
        private MD5 _md5Hash;
        public string GetHesh(string source)
        {
            using (_md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(source);

                if (VerifyMd5Hash(source, hash))
                {
                    return hash;
                }
                else
                {
                    throw new Exception("Hesh is not verify!");
                }
            }



        }
        private string GetMd5Hash(string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = _md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckHeshMD5(string firstHash, string secondHash)
        {
            if (firstHash == secondHash)
            {
                return true;
            }

            return false;
        }
    }
}