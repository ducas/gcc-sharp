using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Coypu;
using Coypu.Drivers;

namespace GccSharp
{
    public class Client
    {
        private const string BaseUrl = "https://www.gettheworldmoving.com";

        private readonly BrowserSession _session = new BrowserSession(new SessionConfiguration
        {
            AppHost = BaseUrl,
            Browser = Browser.PhantomJS,
            SSL = true
        });

        private bool _loggedIn;

        public void Login(string email, string password)
        {
            new LoginAction { Email = email, Password = password }
                .Go(_session);

            _loggedIn = true;
        }

        public IEnumerable<DateTime> GetStepDates()
        {
            if (!_loggedIn)
                throw new AuthenticationException("Not logged in.");

            return new GetStepDatesAction()
                .Go(_session);
        }
    }
}
