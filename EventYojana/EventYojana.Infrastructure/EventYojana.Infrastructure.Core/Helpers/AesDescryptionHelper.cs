using EventYojana.Infrastructure.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EventYojana.Infrastructure.Core.Helpers
{
    internal static class AesDescryptionHelper
    {
        internal static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create()) 
            {
                encryptor.Key = Encoding.ASCII.GetBytes(CommonConstant.EncryptionKey);
                encryptor.IV = Encoding.ASCII.GetBytes(CommonConstant.EncryptionKey.Substring(0, 16));
                encryptor.Mode = CipherMode.CBC;
                var decryptor = encryptor.CreateDecryptor(encryptor.Key, encryptor.IV);

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(cs))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
