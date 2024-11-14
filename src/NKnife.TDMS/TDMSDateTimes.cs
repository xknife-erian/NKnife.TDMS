using System;

namespace NKnife.TDMS
{
    public record TDMSDateTimes
    {
        public TDMSDateTimes(DateTime dateTime)
        {
            Year        = (uint)dateTime.Year;
            Month       = (uint)dateTime.Month;
            Day         = (uint)dateTime.Day;
            Hour        = (uint)dateTime.Hour;
            Minute      = (uint)dateTime.Minute;
            Second      = (uint)dateTime.Second;
            MilliSecond = dateTime.Millisecond;
        }
        public uint Year { get; }
        public uint Month { get; }
        public uint Day { get; }
        public uint Hour { get; }
        public uint Minute { get; }
        public uint Second { get; }
        public double MilliSecond { get; }
    }
}