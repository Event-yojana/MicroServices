using System;
using System.Security.Cryptography;
using System.Text;

namespace EventYojana.Infrastructure.Core.Helpers
{
    public static class AuthenticateUtility
    {
        public static string CreatePasswordSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var byteArr = new byte[64];
            rng.GetBytes(byteArr);
            return Convert.ToBase64String(byteArr);
        }

        public static string GeneratePassword(string password, string salt)
        {
            var passwordSalt = string.Concat(password, salt);
            var hasedPwd = passwordSalt == null ? string.Empty : CommonEncryptionUtility.ByteArrayToString(CommonEncryptionUtility.ConvertToMD5Hash(passwordSalt));
            return hasedPwd;
        }
    }
}
