namespace Configureoo.Core
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Decrypts a string and returns plain text with tag structure
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string DecryptForEdit(string source);

        /// <summary>
        /// Encrypts a string with tag structure
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string EncryptForStorage(string source);

        /// <summary>
        /// Decrypts a string and returns plain text with no tags
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string DecryptForLoad(string source);
    }
}