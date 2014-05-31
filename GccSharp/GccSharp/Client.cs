using System;
using System.Net;
using System.Text;

namespace GccSharp
{
    public class Client
    {
        private static readonly Uri BaseUri = new Uri("https://www.gettheworldmoving.com");
        private static readonly Uri LoginUri = new Uri(BaseUri, "/log-in");

        private readonly CookieContainer _cookies = new CookieContainer();

        public void Login(string email, string password)
        {
            var content = string.Format("UserName={0}&Password={1}", email, password);
            var bytes = Encoding.UTF8.GetBytes(content);

            var request = WebRequest.CreateHttp(LoginUri);
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            using (var stream = request.GetRequestStream())
                stream.Write(bytes, 0, bytes.Length);

            request.GetResponse();

            var cookies = _cookies.GetCookies(BaseUri);
            var authCookie = cookies[".ASPXAUTH"];
            if (authCookie == null)
                throw new LoginFailedException("Logon failed. The logon attempt is unsuccessful, probably because of a user name or password that is not valid.");
        }
    }

    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message)
            : base(message)
        { }
    }
}
