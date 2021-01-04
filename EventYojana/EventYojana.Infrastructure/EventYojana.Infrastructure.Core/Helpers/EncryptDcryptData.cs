using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Core.Helpers
{
    public static class EncryptDcryptData
    {
        public static string EncryptString(string stringData)
        {
            return string.IsNullOrEmpty(stringData) ? string.Empty : CommonEncryptionUtility.ByteArrayToString(CommonEncryptionUtility.ConvertToMD5Hash(stringData));
        }
    }
}
