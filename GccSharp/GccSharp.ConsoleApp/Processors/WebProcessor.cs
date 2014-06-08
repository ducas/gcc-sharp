using System;
using System.Collections.Generic;
using System.Linq;

namespace GccSharp.ConsoleApp.Processors
{
    public static class WebProcessor
    {
        internal static DateTime[] Processor(Activity activity)
        {
            DateTime[] dates = null;
            Console.WriteLine("\r\n\tEntering Web Processor \r\n ");
            try
            {
                var clientEmail = GetClientEmail();
                var clientPassword = GetClientPassword();
                using (var client = new Client())
                {
                    client.Login(clientEmail, clientPassword);
                    if (activity != null)
                    {
                        Console.WriteLine("Activity found");
                        Console.WriteLine("Submitting Activity: " + activity);
                        client.Submit(activity);
                    }
                    else
                    {
                        Console.WriteLine("No activity found");
                    }
                    Console.WriteLine("Getting Missing Steps");
                    dates = client.GetStepDates().ToArray();
                    Console.WriteLine("Steps Found:" + dates.Count());
                    client.Logout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\r\nProcessor Error: \r\n" + ex);                
            }
            Console.WriteLine("\r\n\tExiting Web Processor \r\n ");
            return dates;
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
