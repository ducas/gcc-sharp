using System;

namespace GccSharp.ConsoleApp.Processors
{
    public static class WebProcessor
    {
        internal static bool Processor(Activity activity)
        {
            var successful = true;
            Console.WriteLine("\r\n\tEntering Web Processor \r\n ");
            try
            {
                var clientEmail = GetClientEmail();
                var clientPassword = GetClientPassword();
                using (var client = new Client())
                {
                    client.Login(clientEmail, clientPassword);
                    client.Submit(activity);
                    client.Logout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\r\nError: \r\n" + ex);
                successful = false;
            }
            Console.WriteLine("\r\n\tExiting Web Processor \r\n ");
            return successful;
        }

        private static string GetClientEmail()
        {
            var email = Configuration.ClientEmail;
            if (!string.IsNullOrWhiteSpace(email)) return email;

            Console.WriteLine("Could not find email address.");
            Console.Write("Email: ");
            return Console.ReadLine();
        }

        private static string GetClientPassword()
        {
            var password = Configuration.ClientPassword;
            if (!string.IsNullOrWhiteSpace(password)) return password;

            Console.WriteLine("Could not find password.");
            Console.Write("Password: ");
            return Console.ReadLine();
        }
    }
}
