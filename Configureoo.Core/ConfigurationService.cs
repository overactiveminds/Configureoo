using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Configureoo.Core.Crypto;
using Configureoo.Core.Parsing;

namespace Configureoo.Core
{
    public class ConfigurationService
    {
        private readonly IParser _parser;
        private readonly IKeyStore _keyStore;
        private readonly ICryptoStrategyFactory _keyFactory;

        public ConfigurationService(IParser parser, IKeyStore keyStore, ICryptoStrategyFactory keyFactory)
        {
            _parser = parser;
            _keyStore = keyStore;
            _keyFactory = keyFactory;
        }

        private string Run(string source, bool encrypt)
        {
            var tags = _parser.Parse(source);

            if (tags.Count == 0)
            {
                return source;
            }

            var keys = _keyStore.Get(tags.Select(y => y.KeyName).Distinct(), _keyFactory)
                .ToDictionary(x => x.Name);

            var missingKeys = keys.Values.Where(x => !x.Exists).Select(x => x.Name).ToList();
            if (missingKeys.Any())
            {
                throw new KeysNotFoundException(missingKeys);
            }

            StringWriter writer = new StringWriter();
            int currentChar = 0;

            foreach (var tag in tags)
            {
                writer.Write(source.Substring(currentChar, tag.Index - currentChar));
                var key = keys[tag.KeyName];
                string text = encrypt ? key.CryptoStrategy.Encrypt(tag.Text) : key.CryptoStrategy.Decrypt(tag.Text);
                writer.Write(GetTag(tag, text));
                currentChar = tag.Index + tag.Length;
            }

            var lastTag = tags.Last();
            int tagEnd = lastTag.Index + lastTag.Length;

            writer.Write(source.Substring(tagEnd, source.Length - tagEnd));

            return writer.ToString();
        }

        public string Decrypt(string source)
        {
            return Run(source, false);
        }

        public string Encrypt(string source)
        {
            return Run(source, true);
        }

        private string GetTag(Tag tag, string text)
        {
            string keyName = tag.KeyNameSpecified ? tag.Whitespace + tag.KeyName : string.Empty;
            return $"{tag.OpenTag}CFGO{keyName}{tag.CloseTag}{text}{tag.OpenTag}/CFGO{tag.CloseTag}";
        }
    }

    public class KeysNotFoundException : Exception
    {
        public string SourceFile { get; set; }

        public List<string> MissingKeys { get; set; }

        public KeysNotFoundException(List<string> missingKeys)
        {
            MissingKeys = missingKeys;
            SourceFile = string.Empty;
        }
    }
}
