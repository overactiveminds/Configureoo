using System.Collections.Generic;
using Configureoo.Core.Identity;
using Configureoo.Core.KeyProvider;

namespace Configureoo.Core
{
    public class ConfigurationBuilder
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IKeyStore _keyStore;

        public ConfigurationBuilder(IIdentityProvider identityProvider, IKeyStore keyStore)
        {
            _identityProvider = identityProvider;
            _keyStore = keyStore;
        }

        public void Run(List<string> fileInputs, List<string> fileOutputs)
        {
            
        }
    }
}
