using System.Collections.Generic;
using System.Linq;

namespace Configureoo.Core.Crypto
{
    public class InMemoryKeyStore : IKeyStore
    {
        private readonly Dictionary<string, string> _namedKeys;

        public InMemoryKeyStore(Dictionary<string, string> namedKeys)
        {
            _namedKeys = namedKeys;
        }

        public void Add(CryptoKey key)
        {
            _namedKeys.Add(key.Name, key.Key);
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

        public IEnumerable<CryptoKey> GetAll()
        {
            return _namedKeys.Select(x => new CryptoKey(x.Key, true, x.Value));
        }

        public void Delete(CryptoKey key)
        {
            _namedKeys.Remove(key.Key);
        }

        public bool Exists(string key)
        {
            return _namedKeys.ContainsKey(key);
        }
    }
}
