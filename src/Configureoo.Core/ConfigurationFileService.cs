using System.Collections.Generic;
using System.IO;

namespace Configureoo.Core
{
    public class ConfigurationFileService
    {
        private readonly IConfigurationService _configurationService;
        private readonly ILog _log;

        public ConfigurationFileService(IConfigurationService configurationService, ILog log)
        {
            _configurationService = configurationService;
            _log = log;
        }

        public void Encrypt(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                _log.Debug($"Encrypting: {file}");
                string fileContents = File.ReadAllText(file);
                string result = _configurationService.EncryptForStorage(fileContents);
                File.WriteAllText(file, result);
            }
        }

        public void Decrypt(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                _log.Debug($"Decrypting: {file}");
                string fileContents = File.ReadAllText(file);
                string result = _configurationService.DecryptForEdit(fileContents);
                File.WriteAllText(file, result);
            }
        }
    }
}
