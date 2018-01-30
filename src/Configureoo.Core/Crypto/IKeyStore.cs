using System.Collections.Generic;

namespace Configureoo.Core.Crypto
{
    public interface IKeyStore
    {
        void Add(CryptoKey key);

        IEnumerable<CryptoKey> Get(IEnumerable<string> keys);

        IEnumerable<CryptoKey> GetAll();

        void Delete(CryptoKey key);

        bool Exists(string key);
    }
}
