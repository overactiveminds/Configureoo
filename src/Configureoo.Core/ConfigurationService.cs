﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Configureoo.Core.Crypto;
using Configureoo.Core.Parsing;

namespace Configureoo.Core
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IParser _parser;
        private readonly IKeyStore _keyStore;
        private readonly ICryptoStrategy _cryptoStrategy;

        public ConfigurationService(IParser parser, IKeyStore keyStore, ICryptoStrategy cryptoStrategy)
        {
            _parser = parser;
            _keyStore = keyStore;
            _cryptoStrategy = cryptoStrategy;
        }

        /// <summary>
        /// Decrypts a config file for editing
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string DecryptForEdit(string source)
        {
            return Run(source, false, true);
        }

        /// <summary>
        /// Encrypts a config file for storing
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string EncryptForStorage(string source)
        {
            return Run(source, true, true);
        }

        /// <summary>
        /// Decrypts a config file and removes all Configureoo tags.  Use this merthod when loading the configuration for use by the application
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string DecryptForLoad(string source)
        {
            return Run(source, false, false);
        }

        private string Run(string source, bool encrypt, bool includeTags)
        {
            var tags = _parser.Parse(source);

            if (tags.Count == 0)
            {
                return source;
            }

            var keys = _keyStore.Get(tags.Select(y => y.KeyName).Distinct())
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
                string text;
                bool isCipherText;
                if (encrypt)
                {
                    isCipherText = true;
                    text = tag.Text;
                    // We've been asked to encrypt this value, check the tag type
                    if (tag.TagName == "CFGOE")
                    {
                        // We have a tag as plain text
                        text = _cryptoStrategy.Encrypt(tag.Text, key.Key);
                    }
                }
                else
                {
                    isCipherText = false;
                    text = tag.Text;
                    // We've been asked to decrypt this value, check the tag type
                    if (tag.TagName == "CFGOD")
                    {
                        // We have a tag containing cipher text
                        text = _cryptoStrategy.Decrypt(tag.Text, key.Key);
                    }
                }
                writer.Write(GetTag(tag, text, includeTags, isCipherText));
                currentChar = tag.Index + tag.Length;
            }

            var lastTag = tags.Last();
            int tagEnd = lastTag.Index + lastTag.Length;

            writer.Write(source.Substring(tagEnd, source.Length - tagEnd));

            return writer.ToString();
        }

        private string GetTag(Tag tag, string text, bool includeTags, bool isCipherText)
        {
            if (!includeTags)
            {
                return text;
            }
            string tagName = isCipherText ? "CFGOD" : "CFGOE";
            string keyName = tag.KeyNameSpecified ? tag.KeyName + "," : string.Empty;
            return $"{tagName}({keyName}{text})";
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
