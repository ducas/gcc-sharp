using System;
using CommandLine;
using GccSharp.ConsoleApp.Arguments;

namespace GccSharp.ConsoleApp
{
    internal class Program
    {
        private static bool isSuccessful;

        internal static Func<Activity, bool> Processor;
        internal static Func<Activity, bool> Confirmation;

        public static int SuccessExitCode = 0;
        public static int ErrorExitCode = 1;

        internal static int Main(string[] args)
        {
            SetDefaultValues();

            var options = new GccOptions();
            Parser.Default.ParseArguments(args, options, OnVerbCommand);

            var successfulOutput = isSuccessful ? "Processed" : "Not Processed";
            Console.WriteLine(successfulOutput);

            Environment.ExitCode = isSuccessful ? SuccessExitCode : ErrorExitCode;
            return Environment.ExitCode;
        }

        private static void OnVerbCommand(string verbName, object verbSubOptions)
        {
            var activity = ExtractActivity(verbName, verbSubOptions);
            isSuccessful = ExecuteActivity(activity);
        }

        private static bool ExecuteActivity(Activity activity)
        {
            if (activity == null)
            {
                return false;
            }

            try
            {
                var confirmed = Confirmation(activity);
                if (confirmed)
                {
                    return Processor(activity);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        private static bool ConsoleConfirmation(Activity activity)
        {
            Console.WriteLine("Do you wish to process the following entry? (y/n)");
            Console.WriteLine(activity);
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }


        private static bool WebProcessor(Activity activity)
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
            email = Console.ReadLine();
            return email;
        }

        private static string GetClientPassword()
        {
            var email = Configuration.ClientEmail;
            if (!string.IsNullOrWhiteSpace(email)) return email;

            Console.WriteLine("Could not find password.");
            Console.Write("Password: ");
            email = Console.ReadLine();
            return email;
        }

        private static Activity ExtractActivity(string verbName, object verbSubOptions)
        {
            Activity activity = null;
            switch (verbName)
            {
                case "activity":
                    activity = ConvertOptions((ActivitySubOptions)verbSubOptions);
                    break;
                case "noactivity":
                    activity = ConvertOptions((NoActivitySubOptions)verbSubOptions);
                    break;
            }
            return activity;
        }

        private static Activity ConvertOptions(ActivitySubOptions subOptions)
        {
            if (subOptions == null)
            {
                return null;
            }
            var yesterday = DateTime.Now.AddDays(-1);
            return new Activity
                {
                    Steps = subOptions.WalkingSteps,
                    Bike = subOptions.BikeKilometers,
                    Date = yesterday,
                    Swim = subOptions.SwimmingMetres
                };
        }

        private static Activity ConvertOptions(NoActivitySubOptions subOptions)
        {
            if (subOptions == null)
            {
                return null;
            }
            var yesterday = DateTime.Now.AddDays(-1);
            return new Activity
                {
                    Steps = 0,
                    Bike = 0,
                    Date = yesterday,
                    Swim = 0,
                    NoActivityReason = subOptions.NoActivityReason
                };
        }

        private static void SetDefaultValues()
        {
            if (Processor == null)
            {
                Processor = WebProcessor;
            }
            if (Confirmation == null)
            {
                Confirmation = ConsoleConfirmation;
            }
        }

    }
}
