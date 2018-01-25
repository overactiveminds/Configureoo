using System;
using System.CodeDom;
using System.IO;
using Configureoo.Core;
using Configureoo.Core.Crypto.CryptoStrategies;
using Configureoo.Core.KeyGen;
using Configureoo.Core.Parsing;
using Configureoo.KeyStore.EnvironmentVariables;
using Microsoft.Extensions.CommandLineUtils;

namespace Configureoo
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            string EnvironmentVariablePrefix = "CONFIGUREOO_";
            var parser = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(EnvironmentVariablePrefix), new AesCryptoStrategyFactory());

            app.Command("encrypt", c =>
            {
                var files = c.Option("-f", "The list of files to encrypt or decrypt", CommandOptionType.MultipleValue);

                c.OnExecute(() =>
                {
                    if (!files.HasValue())
                    {
                        c.ShowHelp("encrypt");
                        return 1;
                    }

                    foreach (var file in files.Values)
                    {
                        string fileContents = File.ReadAllText(file);
                        string result = parser.EncryptForStorage(fileContents);
                        File.WriteAllText(file, result);
                    }
                    return 0;
                });
            });

            app.Command("decrypt", c =>
            {
                var files = c.Option("-f", "The list of files to encrypt or decrypt", CommandOptionType.MultipleValue);

                c.OnExecute(() =>
                {
                    if (!files.HasValue())
                    {
                        c.ShowHelp("decrypt");
                        return 1;
                    }

                    foreach (var file in files.Values)
                    {
                        string fileContents = File.ReadAllText(file);
                        string result = parser.DecryptForEdit(fileContents);
                        File.WriteAllText(file, result);
                    }
                    return 0;
                });
            });

            app.Command("keygen", c =>
            {
                var keyName = c.Option("-k", "The name of the key to generate", CommandOptionType.SingleValue);

                c.OnExecute(() =>
                {
                    if (!keyName.HasValue())
                    {
                        c.ShowHelp("keygen");
                        return 1;
                    }
                    var generator = new EnvironmentVariableKeyGenerator(new AesCryptoStrategy(string.Empty));
                    var key = generator.Generate(EnvironmentVariablePrefix, keyName.Value(), EnvironmentVariableTarget.User);
                    Console.WriteLine($"Environment Variable: {key.EnvironmentVariableName} set to {key.Key}");
                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
