using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Configureoo.Core.IO
{
    public class Parser : IParser
    {
        private readonly Regex _regEx = new Regex("CONFIGUREOO\\((\"(?'ciphertext'([^\"]|\"\")*)\")((,\\s?(\"(?'keyname'([^ \"]|\"\")*)\"))?)\\)");

        public List<Tag> Parse(string input)
        {
            var tags = new List<Tag>();
            var matches = _regEx.Matches(input);
            foreach (Match match in matches)
            {
                tags.Add(new Tag(match.Index, 
                    match.Length, 
                    match.Groups["keyname"].Success ? match.Groups["keyname"].Value : "default", 
                    match.Groups["ciphertext"].Value)
                );
            }
            return tags;
        }
    }
}
