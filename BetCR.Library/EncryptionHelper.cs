using System.Security.Cryptography;
using System.Text;

namespace BetCR.Library
{
    public static class EncryptionHelper
    {
        #region Public Methods

        public static string MD5Hash(string textToHash)
        {
            using var md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(textToHash);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        #endregion Public Methods
    }
}