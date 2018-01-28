namespace Configureoo.Core.Crypto.CryptoStrategies
{
    public class AesCryptoStrategyFactory : ICryptoStrategyFactory
    {
        public ICryptoStrategy CreateFromRawKey(string rawKey)
        {
            return new AesCryptoStrategy(rawKey);
        }
    }
}
