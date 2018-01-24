using Configureoo.Core.KeyGen;
using NUnit.Framework;

namespace Configureoo.UnitTests.KeyGen
{
    public class RngRandomKeyGeneratorFixture
    {
        [Test]
        public void GenerateRandomKey()
        {
            // Arrange
            int expectedLength = 64;
            var sut = new RngRandomKeyGenerator(expectedLength);

            // Act
            var key1 = sut.GenerateRandomKey();
            var key2 = sut.GenerateRandomKey();

            // Assert
            Assert.AreNotEqual(key1, key2);
            Assert.AreEqual(expectedLength, key1.Length);
            Assert.AreEqual(expectedLength, key2.Length);
        }
    }
}
