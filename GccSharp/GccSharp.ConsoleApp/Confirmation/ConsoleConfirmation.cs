using System;

namespace GccSharp.ConsoleApp.Confirmation
{
    public static class ConsoleConfirmation
    {
        internal static bool Confirmation(Activity activity)
        {
            Console.WriteLine(activity);
            Console.WriteLine("Do you wish to process this entry? (y/n)");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }
    }
}
