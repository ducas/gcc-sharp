using Coypu;

namespace GccSharp
{
    internal class LoginAction
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Go(BrowserSession session)
        {
            session.Visit("/");

            session.ClickLink("Login");

            session.FillIn("UserName").With(Email);
            session.FillIn("Password").With(Password);

            session.ClickButton("Login");

            if (!session.FindCss("#account.dropdown").Exists())
                throw new LoginFailedException("Logon failed.");
        }
    }
}
