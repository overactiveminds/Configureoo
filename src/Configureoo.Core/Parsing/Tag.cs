namespace Configureoo.Core.Parsing
{
    public class Tag
    {
        public int Index { get; }

        public int Length { get; }

        public string KeyName { get; }

        public bool KeyNameSpecified { get; }

        public string Text { get; }

        public char OpenTag { get; }

        public char CloseTag { get; }

        public string Whitespace { get; }

        public Tag(int index, int length, string keyname, bool keyNameSpecified, string text, char openTag, char closeTag, string whitespace)
        {
            Index = index;
            Length = length;
            KeyName = keyname;
            KeyNameSpecified = keyNameSpecified;
            Text = text;
            OpenTag = openTag;
            CloseTag = closeTag;
            Whitespace = whitespace;
        }
    }
}
