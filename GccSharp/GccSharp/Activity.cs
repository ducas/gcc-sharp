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
    }

    public enum NoActivityReason
    {
        Sick,
        Travelling,
        UnableToWear
    }
}