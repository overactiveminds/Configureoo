namespace Configureoo.Core.Crypto
{
    public interface ICryptoStrategy
    {
        string Encrypt(string cipherText);

        string Decrypt(string cipherText);

        string GenerateKey();
    }
}
