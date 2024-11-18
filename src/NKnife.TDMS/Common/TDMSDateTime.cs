using System;

namespace NKnife.TDMS.Common
{
    record TDMSDateTime
    {
        public TDMSDateTime(DateTime dateTime)
        {
            Year = (uint)dateTime.Year;
            Month = (uint)dateTime.Month;
            Day = (uint)dateTime.Day;
            Hour = (uint)dateTime.Hour;
            Minute = (uint)dateTime.Minute;
            Second = (uint)dateTime.Second;
            MilliSecond = dateTime.Millisecond;
        }

        public TDMSDateTime(uint year,
                            uint month,
                            uint day,
                            uint hour,
                            uint minute,
                            uint second,
                            double millisecond)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
            MilliSecond = millisecond;
        }

        public uint Year { get; }
        public uint Month { get; }
        public uint Day { get; }
        public uint Hour { get; }
        public uint Minute { get; }
        public uint Second { get; }
        public double MilliSecond { get; }

        public DateTime ToDateTime()
        {
            return new DateTime((int)Year, (int)Month, (int)Day, (int)Hour, (int)Minute, (int)Second, (int)MilliSecond);
        }
    }
}