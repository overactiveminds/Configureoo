using System;
using System.Collections.Generic;
using Configureoo.Core.Crypto;

namespace Configureoo.KeyStore.EnvironmentVariables
{
    public class EnvironmentVariablesKeyStore : IKeyStore
    {
        private readonly string _namePrefix;

        public EnvironmentVariablesKeyStore(string namePrefix)
        {
            _namePrefix = namePrefix;
        }

        public IEnumerable<CryptoKey> Get(IEnumerable<string> keys, ICryptoStrategyFactory factory)
        {
            var allKeys = new List<CryptoKey>();
            foreach (var keyName in keys)
            {
                string envKeyName = _namePrefix + keyName;
                string rawKey = Environment.GetEnvironmentVariable(envKeyName);
                allKeys.Add(rawKey != null
                    ? new CryptoKey(keyName, true, factory.CreateFromRawKey(rawKey))
                    : new CryptoKey(keyName, false, null));
            }
            return allKeys;
        }
    }
}
