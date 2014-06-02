using CommandLine;

namespace GccSharp.ConsoleApp.Arguments
{
    class ActivitySubOptions
    {
        [Option('w', "walking", Required=true)]
        public int WalkingSteps { get; set; }

        [Option('b', "bike", Required = false)]
        public decimal BikeKilometers { get; set; }

        [Option('s', "swim", Required = false)]
        public int SwimmingMetres { get; set; }
    }
}
