using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EventYojana.Infrastructure.Core.Helpers
{
    internal static class CommonEncryptionUtility
    {
        internal static byte[] ConvertToMD5Hash(string strInput)
        {
            if (strInput == null)
            {
                return new byte[0];
            }
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var encoder = new UTF8Encoding();
                var hasedDataBytes = md5.ComputeHash(encoder.GetBytes(strInput));
                return hasedDataBytes;
            }
        }

        internal static string ByteArrayToString(byte[] arrInput)
        {
            if (arrInput == null)
            {
                return string.Empty;
            }

            int i;
            var sOutPut = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutPut.Append(arrInput[i].ToString("X2"));
            }

            return sOutPut.ToString();
        }
    }
}
