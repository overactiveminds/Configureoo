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
            var crypto = new AesCryptoStrategy();
            var keyGenerator = new EnvironmentVariableKeyGenerator(crypto);
            var keyName = "somekeyname";
            keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process);
            string plainText = "CFGOE(somekeyname,Hello World From Configureoo!!!!)";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), crypto);

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
            var crypto = new AesCryptoStrategy();
            var keyGenerator = new EnvironmentVariableKeyGenerator(crypto);
            var keyName = "somekeyname";
            keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process);
            string plainText = "|CFGO somekeyname|PlainText|/CFGO|";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), crypto);

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
            var crypto = new AesCryptoStrategy();
            var keyGenerator = new EnvironmentVariableKeyGenerator(crypto);
            var keyName = "somekeyname";
            keyGenerator.Generate(envVarsPrefix, keyName, EnvironmentVariableTarget.Process);
            string plainText = "CFGOE(somekeyname,PlainText)";
            string expectedText = "PlainText";
            var sut = new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(envVarsPrefix), crypto);

            // Act
            string cipherText = sut.EncryptForStorage(plainText);
            string plainTextReturned = sut.DecryptForLoad(cipherText);

            // Assert
            Assert.AreEqual(expectedText, plainTextReturned);
        }
    }
}
