using System;

namespace Configureoo.Core.KeyGen
{
    public class EnvironmentVariableKeyGenerator
    {
        public IRandomKeyGenerator RandomKeyGenerator { get; }

        public EnvironmentVariableKeyGenerator(IRandomKeyGenerator randomKeyGenerator)
        {
            RandomKeyGenerator = randomKeyGenerator;
        }

        public string Generate(string environmentVariablePrefix, string keyName, EnvironmentVariableTarget target, out string concatenatedKeyName)
        {
            concatenatedKeyName = environmentVariablePrefix + keyName;
            string key = RandomKeyGenerator.GenerateRandomKey();
            Environment.SetEnvironmentVariable(concatenatedKeyName, key, target);
            return key;
        }
    }
}
