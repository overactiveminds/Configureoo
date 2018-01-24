namespace Configureoo.Core.Crypto
{
    public interface ICryptoStrategyFactory
    {
        ICryptoStrategy CreateFromRawKey(string rawKey);
    }
}
