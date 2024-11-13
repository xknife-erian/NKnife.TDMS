using System;

namespace NKnife.TDMS
{
    public class TDMSFileErrorException : Exception
    {
        public TDMSFileErrorException(string message) : base(message) { }
    }
}