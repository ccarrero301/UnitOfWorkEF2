namespace DataModel.Models
{
    using System;
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;

    internal static class UserUtils
    {
        private const string Key = "559d9720ba6642bdab58a5a09c89f75d";

        public static string EncryptPassword(string password)
        {
            var key = Encoding.UTF8.GetBytes(Key);

            using (var aes = Aes.Create())
            {
                using (var encryptor = aes.CreateEncryptor(key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                        sw.Write(password);

                    var iv = aes.IV;

                    var encrypted = ms.ToArray();

                    var result = new byte[iv.Length + encrypted.Length];

                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

                    return Convert.ToBase64String(result);
                }
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            var encryptedPasswordAsByteArray = Convert.FromBase64String(encryptedPassword);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(encryptedPasswordAsByteArray, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(encryptedPasswordAsByteArray, iv.Length, cipher, 0, iv.Length);

            var key = Encoding.UTF8.GetBytes(Key);

            using (var aes = Aes.Create())
            using (var decryptor = aes.CreateDecryptor(key, iv))
            {
                string result;

                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                    result = sr.ReadToEnd();

                return result;
            }
        }
    }
}