using System;

namespace NKnife.TDMS
{
    public class TDMSDataErrorException : Exception
    {
        public TDMSDataErrorException(string message) : base(message) { }
    }
}