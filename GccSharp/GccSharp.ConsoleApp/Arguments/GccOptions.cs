using CommandLine;
using CommandLine.Text;

namespace GccSharp.ConsoleApp.Arguments
{
    class GccOptions
    {
        [VerbOption("activity", HelpText = "Activity recorded for the previous day")]
        public ActivitySubOptions Activity { get; set; }

        [VerbOption("noactivity", HelpText = "No Activity recorded for the previous day")]
        public NoActivitySubOptions NoActivity { get; set; }

        [VerbOption("missing", HelpText = "Only shows missing entry dates")]
        public MissingSubOptions Missing { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
