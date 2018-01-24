using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Configureoo.Core.Parsing
{
    public class Parser : IParser
    {
        private readonly Regex _regEx = new Regex("<CFGO((?<whitespace>\\s)(?<keyname>\\w+))?>(?<text>.*)</CFGO>|\\|CFGO((?<whitespacepipe>\\s)(?<keynamepipe>\\w+))?\\|(?<textpipe>.*)\\|/CFGO\\|");

        public List<Tag> Parse(string input)
        {
            var tags = new List<Tag>();
            var matches = _regEx.Matches(input);
            foreach (Match match in matches)
            {
                char openCharacter;
                char closeCharacter;
                string keyName;
                string text;
                bool hasKey;
                string whitespace;
                
                if (match.Groups["text"].Success)
                {
                    openCharacter = '<';
                    closeCharacter = '>';
                    keyName = match.Groups["keyname"].Value;
                    text = match.Groups["text"].Value;
                    hasKey = match.Groups["keyname"].Success;
                    whitespace = match.Groups["whitespace"].Value;
                }
                else
                {
                    openCharacter = closeCharacter = '|';
                    keyName = match.Groups["keynamepipe"].Value;
                    text = match.Groups["textpipe"].Value;
                    hasKey = match.Groups["keynamepipe"].Success;
                    whitespace = match.Groups["whitespacepipe"].Value;
                }

                tags.Add(new Tag(match.Index, 
                    match.Length, 
                    hasKey ? keyName : "default",
                    hasKey,
                    text, 
                    openCharacter, 
                    closeCharacter,
                    whitespace)
                );
            }
            return tags;
        }
    }
}
