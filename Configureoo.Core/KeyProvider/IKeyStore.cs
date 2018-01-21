using Configureoo.Core.Identity;

namespace Configureoo.Core.KeyProvider
{
    public interface IKeyStore
    {
        ICryptoKey Get(IIdentity identity, string key);
    }
}
