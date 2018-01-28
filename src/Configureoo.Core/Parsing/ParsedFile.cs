using System.Collections.Generic;

namespace Configureoo.Core.Parsing
{
    public class ParsedFile
    {
        public string FileName { get; }

        public List<Tag> Tags { get; }

        public string Source { get; }

        public ParsedFile(string fileName, List<Tag> tags, string source)
        {
            FileName = fileName;
            Tags = tags;
            Source = source;
        }
    }
}
