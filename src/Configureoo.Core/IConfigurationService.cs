namespace Configureoo.Core
{
    public interface IConfigurationService
    {
        string DecryptForEdit(string source);
        string EncryptForStorage(string source);
    }
}