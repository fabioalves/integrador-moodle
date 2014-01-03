using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace integrador_moodle.Utils
{
    public class SecurityUtil
    {
        public static String CalculateMd5Hash(String input)
        {
            // Primeiro passo, calcular o MD5 hash a partir da string
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Segundo passo, converter o array de bytes em uma string haxadecimal
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}