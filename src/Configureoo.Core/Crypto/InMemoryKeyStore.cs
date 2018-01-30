using System.Collections.Generic;

namespace Configureoo.Core.Crypto
{
    public class InMemoryKeyStore : IKeyStore
    {
        private readonly Dictionary<string, string> _namedKeys;

        public InMemoryKeyStore(Dictionary<string, string> namedKeys)
        {
            _namedKeys = namedKeys;
        }

        public IEnumerable<CryptoKey> Get(IEnumerable<string> keys)
        {
            var allKeys = new List<CryptoKey>();
            foreach (var key in keys)
            {
                allKeys.Add(_namedKeys.TryGetValue(key, out var storedKey)
                    ? new CryptoKey(key, true, storedKey)
                    : new CryptoKey(key, false, null));
            }
            return allKeys;
        }
    }
}
