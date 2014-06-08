using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommandLine;
using GccSharp.ConsoleApp.Arguments;
using GccSharp.ConsoleApp.Confirmation;
using GccSharp.ConsoleApp.Processors;

namespace GccSharp.ConsoleApp
{
    internal class Program
    {
        private static bool isSuccessful;

        internal static Func<Activity, IEnumerable<DateTime>> Processor;
        internal static Func<Activity, bool> Confirmation;

        public static int SuccessExitCode = 0;
        public static int ErrorExitCode = 1;

        internal static int Main(string[] args)
        {
            SetDefaultValues();

            var options = new GccOptions();
            Parser.Default.ParseArguments(args, options, OnVerbCommand);

            var successfulOutput = isSuccessful ? "\r\nSuccess" : "\r\nUnsuccessful";
            Console.WriteLine(successfulOutput);

            Environment.ExitCode = isSuccessful ? SuccessExitCode : ErrorExitCode;
            return Environment.ExitCode;
        }

        private static void OnVerbCommand(string verbName, object verbSubOptions)
        {
            var activity = ExtractActivity(verbName, verbSubOptions);
            var dates = ExecuteActivity(activity);
            isSuccessful = verbSubOptions != null && dates != null;

            if (isSuccessful)
            {
                Console.WriteLine("\r\nRemaining Entries");
                var datesFormatted = dates.Select(d => d.ToShortDateString());
                var missingDatesOutput = string.Join("\r\n", datesFormatted);
                Console.WriteLine(missingDatesOutput);
            }
        }

        private static IEnumerable<DateTime> ExecuteActivity(Activity activity)
        {
            try
            {
                var confirmed = activity == null || Confirmation(activity);
                if (confirmed)
                {
                    return Processor(activity);
                }
                return new DateTime[] { };
            }
            catch (Exception)
            {
                return null;
            }
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
                case "missing":
                    break;
            }
            return activity;
        }

        readonly static DateTime Yesterday = DateTime.Today.AddDays(-1);

        private static Activity ConvertOptions(ActivitySubOptions subOptions)
        {
            if (subOptions == null)
            {
                return null;
            }

            return new Activity
                {
                    Steps = subOptions.WalkingSteps,
                    Bike = subOptions.BikingKilometers,
                    Date = Yesterday,
                    Swim = subOptions.SwimmingMetres
                };
        }

        private static Activity ConvertOptions(NoActivitySubOptions subOptions)
        {
            if (subOptions == null)
            {
                return null;
            }
            return new Activity
                {
                    Steps = 0,
                    Bike = 0,
                    Date = Yesterday,
                    Swim = 0,
                    NoActivityReason = subOptions.NoActivityReason
                };
        }

        private static void SetDefaultValues()
        {
            if (Processor == null)
            {
                Processor = WebProcessor.Processor;
            }
            if (Confirmation == null)
            {
                Confirmation = ConsoleConfirmation.Confirmation;
                //Confirmation = MessageBoxConfirmation.Confirmation;
            }
        }
    }
}
