using System;
using CommandLine;
using GccSharp.ConsoleApp.Arguments;

namespace GccSharp.ConsoleApp
{
    internal class Program
    {
        internal static Func<Activity, string> ActivityProcessor;
        internal static Func<Activity, bool> ActivityConfirmation;

        internal static void Main(string[] args)
        {
            SetDefaultValues();
            var options = new GccOptions();            
            Parser.Default.ParseArguments(args, options, OnVerbCommand);            
        }        

        private static void OnVerbCommand(string verbName, object verbSubOptions)
        {
            var activity = ExtractActivity(verbName, verbSubOptions);
            if (activity == null)
            {
                Environment.Exit(Parser.DefaultExitCodeFail);
            }
            ProcessActivity(activity, ActivityProcessor);
        }


        private static void ProcessActivity(Activity activity, Func<Activity, string> processFunc)
        {
            var shouldProcess = ActivityConfirmation(activity);
            if (shouldProcess)
            {
                Console.WriteLine("Processing Activity");
                var result = processFunc(activity);
                Console.WriteLine("Processing Result: " + result);
            }
            else
            {
                Console.WriteLine("Not Processing Activity");
            }
        }

        private static bool ConsoleConfirmation(Activity activity)
        {
            Console.WriteLine("Do you wish to process the following entry? (y/n)");
            Console.WriteLine(activity);
            return Console.ReadKey().Key == ConsoleKey.Y;
        }


        private static string WebProcessor(Activity activity)
        {
            try
            {
                using (var client = new Client())
                {                    
                    client.Login(Configuration.ClientEmail, Configuration.ClientPassword);
                    client.Submit(activity);
                    client.Logout();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex;
            }
            return "Success";
        }

        private static Activity ExtractActivity(string verbName, object verbSubOptions)
        {
            var activity = new Activity();
            switch (verbName)
            {
                case "activity":                    
                    activity = ConvertOptions((ActivitySubOptions) verbSubOptions);
                    break;
                case "noactivity":                    
                    activity = ConvertOptions((NoActivitySubOptions) verbSubOptions);
                    break;
                default:
                    Environment.Exit(Parser.DefaultExitCodeFail);
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
            if (ActivityProcessor == null)
            {
                ActivityProcessor = WebProcessor;
            }
            if (ActivityConfirmation == null)
            {
                ActivityConfirmation = ConsoleConfirmation;
            }
        }

    }
}
