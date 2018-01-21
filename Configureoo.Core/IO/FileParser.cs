using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Configureoo.Core.IO
{
    public class FileParser
    {
        private readonly IParser _parser;

        public FileParser(IParser parser)
        {
            _parser = parser;
        }

        public List<ParsedFile> Parse(List<string> inputFiles)
        {
            return (from inputFile in inputFiles
                    let input = File.ReadAllText(inputFile)
                    let tags = _parser.Parse(input)
                    select new ParsedFile(inputFile, tags)
                    ).ToList();
        }
    }
}
