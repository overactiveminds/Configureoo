using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Configureoo.Core.Parsing
{
    public class Parser : IParser
    {
        private readonly Regex _regEx = new Regex("CFGRO_([^_CFGRO](?<ciphertext>.*))_CFGRO((KN_(<keyname>*.)_CFGRO)?)");

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
