using GccSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Integration
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        [ExpectedException(typeof(LoginFailedException))]
        public void Login_ShouldThrowExecption_WhenIncorrectCredsProvided()
        {
            var client = new Client();
            client.Login("aaa", "bbb");
        }

        [TestMethod]
        public void Login_ShouldNotThrowExecption_WhenCorrectCredsProvided()
        {
            var client = new Client();
            client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
        }
    }
}
