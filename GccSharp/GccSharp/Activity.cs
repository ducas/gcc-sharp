using System;

namespace GccSharp
{
    public class Activity
    {
        public DateTime Date { get; set; }
        public int Steps { get; set; }
        public decimal Bike { get; set; }
        public int Swim { get; set; }
        public NoActivityReason? NoActivityReason { get; set; }

        public override string ToString()
        {
            if (NoActivityReason == null)
            {
                return String.Format("{0} : Activities = Steps:{1,5}, Bike KM:{2,2}, Swimming Metres:{3,4}",
                    Date.ToShortDateString(), Steps, Bike, Swim);
            }

            return String.Format("{0} : No Activities = Reason:{1}",
                Date.ToShortDateString(), NoActivityReason.Value);
        }
    }

    public enum NoActivityReason
    {
        Sick,
        Travelling,
        UnableToWear
    }
}