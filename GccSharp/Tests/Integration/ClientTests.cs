using System;
using System.Linq;
using System.Security.Authentication;
using FluentAssertions;
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

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void GetStepDates_ShouldThrowException_WhenNotLoggedIn()
        {
            var client = new Client();
            client.GetStepDates();
        }

        [TestMethod]
        public void GetStepDates_ShouldReturnValues_WhenLoggedIn()
        {
            var client = new Client();
            client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
            client.GetStepDates().Count().Should().NotBe(0);
        }
    }
}
