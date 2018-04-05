using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Configureoo.Core.Parsing
{
    public class Parser : IParser
    {
        private readonly Regex _regEx = new Regex("(?<tagname>CFGOE|CFGOD)\\(((?<keyname>\\w+),)?(?<text>[^\\)]*)\\)");

        public List<Tag> Parse(string input)
        {
            var tags = new List<Tag>();
            var matches = _regEx.Matches(input);
            foreach (Match match in matches)
            {
                string keyName = match.Groups["keyname"].Success ? match.Groups["keyname"].Value : "default";
                string text = match.Groups["text"].Value;
                string tagName = match.Groups["tagname"].Value;

                tags.Add(new Tag(match.Index, 
                    match.Length,
                    keyName,
                    match.Groups["keyname"].Success,
                    text, 
                    tagName,
                    match.Index)
                );
            }
            return tags;
        }
    }
}
