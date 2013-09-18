using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadicalGeek.Common.Text;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit.HelperTests
{
    /// <summary>
    /// Summary description for StringDecrypterTests
    /// </summary>
    [TestClass]
    public class StringDecrypterTests
    {
        [TestMethod]
        public void GivenANexusEncryptedStringAnUnencryptedStringShouldBeReturned()
        {
            string result = "W06NavqHyXMa0ijRzarjclYkXuszowZl45wF9IolF5I=".Decrypt();

            Assert.AreEqual("this should be decrypted", result);
        }

        [TestMethod]
        public void GivenANonNexusEncryptedStringAnUnencryptedStringShouldBeReturned()
        {
            string result = "0WsJQibneQOuIv8PbXoJgFVKqEHFg7Ux0Qu+OKqQQo0=".Decrypt();

            Assert.AreEqual("this should be decrypted", result);
        }
        
        [TestMethod]
        public void GivenANonEncryptedStringTheSameStringShouldBeReturned()
        {
            const string thisShouldBeTheSame = "this should be the same";
            string result = thisShouldBeTheSame.Decrypt();

            Assert.AreEqual(thisShouldBeTheSame, result);
        }
    }
}
