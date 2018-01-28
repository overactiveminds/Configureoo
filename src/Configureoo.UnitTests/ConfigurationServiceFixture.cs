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
        public void EncryptForStorageTags()
        {
            // Arrange
            const string envVarsPrefix = "CONFIGUREOO_";
            var keyGenerator = new EnvironmentVariableKeyGenerator(new AesCryptoStrategy(string.Empty));
            var keyName = "somekeyname";
            keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process);
            string plainText = "<CFGO somekeyname>Hello World From Configureoo!!!!</CFGO>";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), new AesCryptoStrategyFactory());

            // Act
            string cipherText = sut.EncryptForStorage(plainText);
            string plainTextReturned = sut.DecryptForEdit(cipherText);

            // Assert
            Assert.AreEqual(plainText, plainTextReturned);
        }

        [Test]
        public void EncryptForStoragePipes()
        {
            // Arrange
            const string envVarsPrefix = "CONFIGUREOO_";
            var keyGenerator = new EnvironmentVariableKeyGenerator(new AesCryptoStrategy(string.Empty));
            var keyName = "somekeyname";
            keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process);
            string plainText = "|CFGO somekeyname|PlainText|/CFGO|";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), new AesCryptoStrategyFactory());

            // Act
            string cipherText = sut.EncryptForStorage(plainText);
            string plainTextReturned = sut.DecryptForEdit(cipherText);

            // Assert
            Assert.AreEqual(plainText, plainTextReturned);
        }

        [Test]
        public void DecryptForLoad()
        {
            // Arrange
            const string envVarsPrefix = "CONFIGUREOO_";
            var keyGenerator = new EnvironmentVariableKeyGenerator(new AesCryptoStrategy(string.Empty));
            var keyName = "somekeyname";
            keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process);
            string plainText = "<CFGO somekeyname>PlainText</CFGO>";
            string expectedText = "PlainText";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), new AesCryptoStrategyFactory());

            // Act
            string cipherText = sut.EncryptForStorage(plainText);
            string plainTextReturned = sut.DecryptForLoad(cipherText);

            // Assert
            Assert.AreEqual(expectedText, plainTextReturned);
        }
    }
}
