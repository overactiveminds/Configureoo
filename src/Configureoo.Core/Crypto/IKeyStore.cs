using System.Collections.Generic;

namespace Configureoo.Core.Crypto
{
    public interface IKeyStore
    {
        IEnumerable<CryptoKey> Get(IEnumerable<string> keys);
    }
}
