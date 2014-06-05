using CommandLine;

namespace GccSharp.ConsoleApp.Arguments
{
    class NoActivitySubOptions
    {
        [Option('r', "reason", Required = true, HelpText = "Options: Sick, Travelling, UnableToWear")]
        public NoActivityReason NoActivityReason { get; set; }
    }
}
