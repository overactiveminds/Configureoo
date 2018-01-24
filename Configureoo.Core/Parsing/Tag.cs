namespace Configureoo.Core.Parsing
{
    public class Tag
    {
        public int Index { get; }

        public int Length { get; }

        public string KeyName { get; }

        public string Text { get; }

        public Tag(int index, int length, string keyname, string text)
        {
            Index = index;
            Length = length;
            KeyName = keyname;
            Text = text;
        }
    }
}
