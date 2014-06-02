using GccSharp.ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unit
{
    [TestClass]
    public class ConsoleAppTests
    {        
        [TestMethod]
        public void Program_ShouldReturnSuccessCode_WhenValidActivityInput()
        {
            Program.Processor = activity => true;
            Program.Confirmation = activity => true;
            var programInput = GetProgramInput("activity -w 10000");
            
            var exitCode = Program.Main(programInput);

            Assert.AreEqual(Program.SuccessExitCode, exitCode);
        }

        [TestMethod]
        public void Program_ShouldReturnSuccessCode_WhenValidNoActivityInput()
        {
            Program.Processor = activity => true;
            Program.Confirmation = activity => true;
            var programInput = GetProgramInput("noactivity --reason sick");

            var exitCode = Program.Main(programInput);

            Assert.AreEqual(Program.SuccessExitCode, exitCode);
        }

        [TestMethod]
        public void Program_ShouldReturnErrorCode_WhenInvalidOptionArgument()
        {
            Program.Processor = activity => true;
            Program.Confirmation = activity => true;
            var programInput = GetProgramInput("activity -x invalidOption");

            var exitCode = Program.Main(programInput);

            Assert.AreEqual(Program.ErrorExitCode, exitCode);
        }

        [TestMethod]
        public void Program_ShouldReturnErrorCode_WhenInvalidVerbArgument()
        {
            Program.Processor = activity => true;
            Program.Confirmation = activity => true;
            var programInput = GetProgramInput("invalidVerb");

            var exitCode = Program.Main(programInput);

            Assert.AreEqual(Program.ErrorExitCode, exitCode);
        }

        private static string[] GetProgramInput(string userInput)
        {            
            var programInput = userInput.Split(' ');
            return programInput;
        }

    }
}
