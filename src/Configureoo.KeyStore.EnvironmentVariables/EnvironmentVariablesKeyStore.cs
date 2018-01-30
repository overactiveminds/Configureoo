using System;
using System.Collections;
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

        public void Add(CryptoKey key)
        {
            string envName = _namePrefix + key.Name;
            Environment.SetEnvironmentVariable(envName, key.Key, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(envName, key.Key, EnvironmentVariableTarget.User);
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

        public IEnumerable<CryptoKey> GetAll()
        {
            var configureooVariables = new List<CryptoKey>();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                string key = de.Key.ToString();
                if (!key.StartsWith(_namePrefix))
                {
                    continue;
                }
                configureooVariables.Add(new CryptoKey(key.Substring(_namePrefix.Length), true, de.Value.ToString()));
            }
            return configureooVariables;
        }

        public void Delete(CryptoKey key)
        {
            string envName = _namePrefix + key.Name;
            Environment.SetEnvironmentVariable(envName, null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(envName, null, EnvironmentVariableTarget.User);
        }

        public bool Exists(string key)
        {
            string envName = _namePrefix + key;
            return Environment.GetEnvironmentVariable(envName) != null;
        }

        private string FindEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);
        }
    }
}
