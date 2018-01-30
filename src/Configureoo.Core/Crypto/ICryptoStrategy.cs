namespace Configureoo.Core.Crypto
{
    public interface ICryptoStrategy
    {
        string Encrypt(string cipherText, string key);

        string Decrypt(string cipherText, string key);

        string GenerateKey();
    }
}
