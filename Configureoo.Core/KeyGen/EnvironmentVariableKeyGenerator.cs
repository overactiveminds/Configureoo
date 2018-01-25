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

        public EnvironmentVariableKey Generate(string environmentVariablePrefix, string keyName, EnvironmentVariableTarget target)
        {
            string concatenatedKeyName = environmentVariablePrefix + keyName;
            string key = _cryptoStrategy.GenerateKey();
            Environment.SetEnvironmentVariable(concatenatedKeyName, key, target);
            return new EnvironmentVariableKey(concatenatedKeyName, key);
        }
    }


    public class EnvironmentVariableKey
    {
        public string EnvironmentVariableName { get;  }

        public string Key { get;  }

        public EnvironmentVariableKey(string variableName, string key)
        {
            EnvironmentVariableName = variableName;
            Key = key;
        }
    }
}
