using System.Collections.Generic;

namespace Configureoo.Core.IO
{
    public class ParsedFile
    {
        public string File { get; }

        public List<Tag> Tags { get; }

        public ParsedFile(string file, List<Tag> tags)
        {
            File = file;
            Tags = tags;
        }
    }
}
