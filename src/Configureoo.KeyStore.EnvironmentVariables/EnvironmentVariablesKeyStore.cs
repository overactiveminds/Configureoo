using System;
using System.Collections.Generic;
using Configureoo.Core.Crypto;

namespace Configureoo.KeyStore.EnvironmentVariables
{
    public class EnvironmentVariablesKeyStore : IKeyStore
    {
        private readonly string _namePrefix;

        public EnvironmentVariablesKeyStore(string namePrefix = "CONFIGUREOO_")
        {
            _namePrefix = namePrefix;
        }

        public IEnumerable<CryptoKey> Get(IEnumerable<string> keys)
        {
            var allKeys = new List<CryptoKey>();
            foreach (var keyName in keys)
            {
                string envKeyName = _namePrefix + keyName;
                string rawKey = FindEnvironmentVariable(envKeyName);
                allKeys.Add(rawKey != null
                    ? new CryptoKey(keyName, true, rawKey)
                    : new CryptoKey(keyName, false, null));
            }
            return allKeys;
        }

        private string FindEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ??
                   Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User) ??
                   Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        }
    }
}
