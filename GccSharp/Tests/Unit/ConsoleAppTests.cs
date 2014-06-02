using GccSharp;
using GccSharp.ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unit
{
    [TestClass]
    public class ConsoleAppTests
    {
        [TestMethod]
        public void Program_ShouldReadArguments_WhenValidInput()
        {
            Program.ActivityProcessor = MockSubmit;
            Program.ActivityConfirmation = MockConfirm;
            var programInput = GetProgramInput("activity -w 10000");
            
            Program.Main(programInput);
        }

        [TestMethod]
        public void Program_ShouldReadArguments_WhenInvalidInput()
        {
            Program.ActivityProcessor = MockSubmit;
            Program.ActivityConfirmation = MockConfirm;
            var programInput = GetProgramInput("activity -x invalid");

            Program.Main(programInput);
        }

        private static string[] GetProgramInput(string userInput)
        {            
            var programInput = userInput.Split(' ');
            return programInput;
        }

        private static string MockSubmit(Activity activity)
        {
            return "Success";
        }

        private static bool MockConfirm(Activity activity)
        {
            return true;
        }
    }
}
