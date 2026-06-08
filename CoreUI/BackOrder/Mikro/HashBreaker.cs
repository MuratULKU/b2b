using DataAccess.Migrations.SqliteMigrations;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CoreUI.BackOrder.Mikro
{
   

public static class Md5Helper
    {
        /// <summary>
        /// Verilen string'in MD5 Base64 hash'ini döndürür
        /// </summary>
        public static string ComputeMd5Base64(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }


}
