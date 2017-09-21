using Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class Crypto
    {
        public static string Hash(string password, out string salt)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException();
            }
            Blake2B blake2B = new Blake2B();
            blake2B.Salt = StringUtils.GetRandomBytes(8*16);
            blake2B.Key = StringUtils.GetBytes(password);
            salt = EncodingUtils.Base64Encode(blake2B.Salt);
            byte[] data = blake2B.Final();
            return EncodingUtils.Base64Encode(data);
        }


        public static string HashPlain(string password, string salt)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(salt))
            {
                throw new InvalidOperationException();
            }
            Blake2B blake2B = new Blake2B();
            blake2B.Salt = StringUtils.GetBytes(salt);
            blake2B.Key = StringUtils.GetBytes(password);
            byte[] data = blake2B.Final();
            return EncodingUtils.Base64Encode(data);
        }

        public static string HashB64EncodedSalt(string password, string salt)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(salt))
            {
                throw new InvalidOperationException();
            }
            Blake2B blake2B = new Blake2B();
            blake2B.Salt = EncodingUtils.Base64Decode(salt);
            blake2B.Key = StringUtils.GetBytes(password);
            byte[] data = blake2B.Final();
            return EncodingUtils.Base64Encode(data);
        }
        public static string CheckSum(string data)
        {
            return GetSha256(data);
        }

        public static byte[] GetSha256(byte[] value)
        {
            using (SHA256 algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(value);
            }
        }

        public static string GetSha256(string data)
        {
            return StringUtils.GetString(GetSha256(StringUtils.GetBytes(data)));
        }

        public static bool ValidateHash(string password, string hashedPassword, string salt)
        {
            try
            {
                if (String.IsNullOrEmpty(password))
                {
                    return false;
                }
                string hashed = HashB64EncodedSalt(password, salt);
                if (hashed == hashedPassword)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
