using CommandLine;

namespace GccSharp.ConsoleApp.Arguments
{
    class ActivitySubOptions
    {
        [Option('w', "walk", Required=true, HelpText = "Steps walked for the previous day")]
        public int WalkingSteps { get; set; }

        [Option('b', "bike", Required = false, HelpText = "Biking kilometers for the previous day")]
        public decimal BikingKilometers { get; set; }

        [Option('s', "swim", Required = false, HelpText = "Swimming metres for the previous day")]
        public int SwimmingMetres { get; set; }
    }
}
