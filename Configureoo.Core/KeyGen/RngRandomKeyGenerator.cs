using System.Security.Cryptography;
using System.Text;

namespace Configureoo.Core.KeyGen
{
    public class RngRandomKeyGenerator : IRandomKeyGenerator
    {
        private readonly int _length;

        public RngRandomKeyGenerator(int length)
        {
            _length = length;
        }
        public string GenerateRandomKey()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data;
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                data = new byte[_length];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(_length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
