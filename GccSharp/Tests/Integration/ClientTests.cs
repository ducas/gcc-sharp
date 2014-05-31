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
            using (var client = new Client())
            {
                client.Login("aaa", "bbb");
            }
        }

        [TestMethod]
        public void Login_ShouldNotThrowExecption_WhenCorrectCredsProvided()
        {
            using (var client = new Client())
            {
                client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void GetStepDates_ShouldThrowException_WhenNotLoggedIn()
        {
            using (var client = new Client())
            {
                client.GetStepDates();
            }
        }

        [TestMethod]
        public void GetStepDates_ShouldReturnValues_WhenLoggedIn()
        {
            using (var client = new Client())
            {
                client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
                client.GetStepDates().Count().Should().NotBe(0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void Submit_ShouldThrowException_WhenNotLoggedIn()
        {
            using (var client = new Client())
            {
                client.Submit(null as Activity);
            }
        }

        [TestMethod]
        public void Submit_ShouldSubmitSingle_WhenLoggedIn()
        {
            using (var client = new Client())
            {
                client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
                client.Submit(new Activity
                {
                    NoActivityReason = NoActivityReason.Sick,
                    Steps = 1000,
                    Bike = 10.5M,
                    Date = DateTime.Today.AddDays(-1),
                    Swim = 500
                });
                client.GetStepDates().Should().NotContain(DateTime.Today.AddDays(-1));
            }
        }

        [TestMethod]
        public void Submit_ShouldSubmitMultiple_WhenLoggedIn()
        {
            using (var client = new Client())
            {
                client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
                client.Submit(new[]
                {
                    new Activity
                    {
                        Steps = 16852,
                        Bike = 4.6M,
                        Date = new DateTime(2014, 05, 30),
                        Swim = 0
                    },
                    new Activity
                    {
                        Steps = 13124,
                        Bike = 15.5M,
                        Date = new DateTime(2014, 05, 29),
                        Swim = 0
                    },
                    new Activity
                    {
                        Steps = 6051,
                        Bike = 5.5M,
                        Date = new DateTime(2014, 05, 28),
                        Swim = 0
                    },
                });
                client.GetStepDates().Should().BeEmpty();
            }
        }

        [TestMethod]
        public void Logout_ShouldLogOutUser_WhenLoggedIn()
        {
            using (var client = new Client())
            {
                client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
                client.Logout();
            }
        }
    }
}
