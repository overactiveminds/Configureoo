using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Configureoo.Core;
using Configureoo.Core.Crypto.CryptoStrategies;
using Configureoo.Core.Parsing;
using Configureoo.KeyStore.EnvironmentVariables;
using Newtonsoft.Json;

namespace Configureoo.JsonConfigurationProvider
{
    public class ConfigureooConfigurationProvider : Microsoft.Extensions.Configuration.FileConfigurationProvider
    {
        public ConfigureooConfigurationProvider(ConfigureooConfigurationSource source)
            : base(source)
        {

        }

        public override void Load(Stream stream)
        {
            string source;
            using (var reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }

            ConfigurationService service = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(), new AesCryptoStrategy(), new NullLog());
            source = service.DecryptForLoad(source);

            using (var memStream = new MemoryStream(Encoding.Default.GetBytes(source)))
            {
                memStream.Position = 0;
                try
                {
                    Data = JsonConfigurationFileParser.Parse(memStream);
                }
                catch (JsonReaderException e)
                {
                    string errorLine = string.Empty;
                    if (!stream.CanSeek)
                        throw new FormatException(string.Format("Could not parse the JSON file. Error on line number '{0}': '{1}'.", e.LineNumber, errorLine), e);
                    stream.Seek(0, SeekOrigin.Begin);

                    using (var streamReader = new StreamReader(stream))
                    {
                        var fileContent = ReadLines(streamReader);
                        errorLine = RetrieveErrorContext(e, fileContent);
                    }
                    throw new FormatException(string.Format("Could not parse the JSON file. Error on line number '{0}': '{1}'.", e.LineNumber, errorLine), e);
                }
            }
        }

        private static string RetrieveErrorContext(JsonReaderException e, IEnumerable<string> fileContent)
        {
            string errorLine = null;
            if (e.LineNumber >= 2)
            {
                var errorContext = fileContent.Skip(e.LineNumber - 2).Take(2).ToList();
                // Handle situations when the line number reported is out of bounds
                if (errorContext.Count() >= 2)
                {
                    errorLine = errorContext[0].Trim() + Environment.NewLine + errorContext[1].Trim();
                }
            }
            if (string.IsNullOrEmpty(errorLine))
            {
                var possibleLineContent = fileContent.Skip(e.LineNumber - 1).FirstOrDefault();
                errorLine = possibleLineContent ?? string.Empty;
            }
            return errorLine;
        }

        private static IEnumerable<string> ReadLines(StreamReader streamReader)
        {
            string line;
            do
            {
                line = streamReader.ReadLine();
                yield return line;
            } while (line != null);
        }
    }
}
