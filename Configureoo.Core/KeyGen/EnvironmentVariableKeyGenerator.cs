using System;
using Configureoo.Core.Crypto;

namespace Configureoo.Core.KeyGen
{
    public class EnvironmentVariableKeyGenerator
    {
        private readonly ICryptoStrategy _cryptoStrategy;

        public EnvironmentVariableKeyGenerator(ICryptoStrategy cryptoStrategy)
        {
            _cryptoStrategy = cryptoStrategy;
        }

        public string Generate(string environmentVariablePrefix, string keyName, EnvironmentVariableTarget target, out string concatenatedKeyName)
        {
            concatenatedKeyName = environmentVariablePrefix + keyName;
            string key = _cryptoStrategy.GenerateKey();
            Environment.SetEnvironmentVariable(concatenatedKeyName, key, target);
            return key;
        }
    }
}
