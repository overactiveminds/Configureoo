namespace Configureoo.Core.Parsing
{
    public class Tag
    {
        public int Index { get; }

        public int Length { get; }

        public string KeyName { get; }

        public bool KeyNameSpecified { get; }

        public string Text { get; }
        public string TagName { get; }

        public Tag(int index, int length, string keyname, bool keyNameSpecified, string text, string tagName)
        {
            Index = index;
            Length = length;
            KeyName = keyname;
            KeyNameSpecified = keyNameSpecified;
            Text = text;
            TagName = tagName;
        }
    }
}
