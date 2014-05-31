using System;

namespace GccSharp
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message)
            : base(message)
        { }
    }
}