using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Coypu;
using Coypu.Drivers;

namespace GccSharp
{
    public class Client : IDisposable
    {
        private const string BaseUrl = "https://www.gettheworldmoving.com";

        private BrowserSession _session = new BrowserSession(new SessionConfiguration
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
            AssertIsLoggedIn();

            return new GetStepDatesAction()
                .Go(_session);
        }

        private void AssertIsLoggedIn()
        {
            if (!_loggedIn)
                throw new AuthenticationException("Not logged in.");
        }

        public void Submit(Activity activity)
        {
            Submit(new[] { activity });
        }

        public void Submit(IEnumerable<Activity> activities)
        {
            AssertIsLoggedIn();

            new SubmitAction { Activities = activities }
                .Go(_session);
        }

        public void Logout()
        {
            new LogoutAction().Go(_session);

            _loggedIn = false;
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
                _session = null;
            }
        }
    }
}
