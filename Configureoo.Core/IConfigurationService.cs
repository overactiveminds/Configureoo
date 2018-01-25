namespace Configureoo.Core
{
    public interface IConfigurationService
    {
        string Decrypt(string source);
        string Encrypt(string source);
    }
}