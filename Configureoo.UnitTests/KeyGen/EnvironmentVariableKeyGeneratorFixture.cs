using System;
using Configureoo.Core.KeyGen;
using Moq;
using NUnit.Framework;

namespace Configureoo.UnitTests.KeyGen
{
    public class EnvironmentVariableKeyGeneratorFixture
    {
        [Test]
        public void GenerateKey()
        {
            // Arrange
            var expectedEnvTarget = EnvironmentVariableTarget.Process;
            var expectedKeyValue = "Key Value";
            var expectedKeyName = "SomeKey";
            var expectedPrefix = "Prefix";
            var expectedConcatenatedKeyName = "PrefixSomeKey";
            var mockRandomGenerator = new Mock<IRandomKeyGenerator>();
            mockRandomGenerator.Setup(x => x.GenerateRandomKey())
                .Returns(expectedKeyValue);

            var sut = new EnvironmentVariableKeyGenerator(mockRandomGenerator.Object);

            // Act
            var actual = sut.Generate(expectedPrefix, expectedKeyName, expectedEnvTarget, out var actualConcatenatedKeyName);

            // Assert
            Assert.AreEqual(actual, expectedKeyValue);
            Assert.AreEqual(expectedConcatenatedKeyName, actualConcatenatedKeyName);
            Assert.AreEqual(expectedKeyValue, Environment.GetEnvironmentVariable(expectedConcatenatedKeyName));
        }
    }
}
