using System;
using System.Security.Authentication;
using Coypu;

namespace GccSharp
{
    public class LogoutAction
    {
        public void Go(BrowserSession session)
        {
            session.FindCss("#account.dropdown .dropdown-toggle").Click();

            session.ClickLink("Logout");

            if (!session.FindCss("#account.dropdown").Missing())
                throw new AuthenticationException("Not able to log out.");
        }
    }
}