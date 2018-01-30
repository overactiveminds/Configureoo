namespace Configureoo.Core.Crypto
{
    public class CryptoKey
    {
        public string Name { get; }

        public bool Exists { get; }

        public string Key { get; }

        public CryptoKey(string name, bool exists, string key)
        {
            Name = name;
            Exists = exists;
            Key = key;
        }
    }
}
