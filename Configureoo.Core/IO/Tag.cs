namespace Configureoo.Core.IO
{
    public class Tag
    {
        public int Index { get; }

        public int Length { get; }

        public string KeyName { get; }

        public string CipherText { get; }

        public Tag(int index, int length, string keyname, string ciphertext)
        {
            Index = index;
            Length = length;
            KeyName = keyname;
            CipherText = ciphertext;
        }
    }
}
