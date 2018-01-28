namespace Configureoo.Core.Crypto
{
    public class CryptoKey
    {
        public string Name { get; }

        public bool Exists { get; }

        public ICryptoStrategy CryptoStrategy { get; }

        public CryptoKey(string name, bool exists, ICryptoStrategy strategy)
        {
            Name = name;
            Exists = exists;
            CryptoStrategy = strategy;
        }
    }
}
