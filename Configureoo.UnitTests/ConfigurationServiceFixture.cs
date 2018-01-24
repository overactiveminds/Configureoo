using System;
using Configureoo.Core;
using Configureoo.Core.Crypto.CryptoStrategies;
using Configureoo.Core.KeyGen;
using Configureoo.Core.Parsing;
using Configureoo.KeyStore.EnvironmentVariables;
using NUnit.Framework;

namespace Configureoo.UnitTests
{
    public class ConfigurationServiceFixture
    {
        [Test]
        public void RunForEncryption()
        {
            // Arrange
            const string envVarsPrefix = "CONFIGUREOO_";
            var keyGenerator = new EnvironmentVariableKeyGenerator(new RngRandomKeyGenerator(16));
            var keyName = "somekeyname";
            var key = keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process, out var envVarName);
            string plainText = "CFGRO_PlainText_CFGROKN_somekeyname_CFGRO";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), new AesCryptoStrategyFactory());

            // Act
            string cipherText = sut.Encrypt(plainText);
            string plainTextReturned = sut.Decrypt(cipherText);

            // Assert
            Assert.AreEqual(plainText, plainTextReturned);
        }
    }
}
