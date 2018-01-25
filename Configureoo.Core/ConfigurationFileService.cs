using System.Collections.Generic;
using System.IO;

namespace Configureoo.Core
{
    public class ConfigurationFileService
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationFileService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public void Encrypt(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                string fileContents = File.ReadAllText(file);
                string result = _configurationService.Encrypt(fileContents);
                File.WriteAllText(file, result);
            }
        }

        public void Decrypt(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                string fileContents = File.ReadAllText(file);
                string result = _configurationService.Decrypt(fileContents);
                File.WriteAllText(file, result);
            }
        }
    }
}
