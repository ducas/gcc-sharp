﻿using System.Configuration;

namespace Tests
{
    public static class Configuration
    {
        public static string ClientEmail
        {
            get { return ConfigurationManager.AppSettings["Client.Email"]; }
        }

        public static string ClientPassword
        {
            get { return ConfigurationManager.AppSettings["Client.Password"]; }
        }
    }
}
