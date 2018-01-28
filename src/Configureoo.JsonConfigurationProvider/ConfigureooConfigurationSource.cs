using Microsoft.Extensions.Configuration;

namespace Configureoo.JsonConfigurationProvider
{
    public class ConfigureooConfigurationSource: FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new ConfigureooConfigurationProvider(this);
        }
    }
}
