﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class StringUtils
    {
        /// <summary>
        /// everything is UTF-8 based 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return Encoding.UTF8.GetBytes(str);
        }
        /// <summary>
        /// everything is UTF-8 based 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetString(byte[] bytes)
        {
            if (bytes?.Length > 0)
            {
                return Encoding.UTF8.GetString(bytes);
            }
            return null;
        }

        public static string GetRandomFriendlyString(int len)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnqprstuwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, len)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static byte[] GetRandomBytes(int size = 128)
        {
            byte[] bytes = new byte[size / 8];
            using (RandomNumberGenerator keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return bytes;
            }
        }
        public static string GetRandomString()
        {
            return BitConverter.ToString(GetRandomBytes()).Replace("-", "");
        }
        public static string SubstringLength(this string value,int length)
        {
            if (value.Length > length)
            {
                return value.Substring(length);
            }
            return value;
        }
     
    }
}
