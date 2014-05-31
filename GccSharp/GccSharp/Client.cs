using System;
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

        public void Login(string email, string password)
        {
            _session.Visit("/");

            _session.ClickLink("Login");

            _session.FillIn("UserName").With(email);
            _session.FillIn("Password").With(password);

            _session.ClickButton("Login");

            if (!_session.FindCss("#account.dropdown").Exists())
                throw new LoginFailedException("Logon failed.");
        }
    }

    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message)
            : base(message)
        { }
    }
}
