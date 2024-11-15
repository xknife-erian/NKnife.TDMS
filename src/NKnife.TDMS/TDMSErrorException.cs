using System;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public class TDMSErrorException : Exception
    {
        public TDMSErrorException(string message) : base(message) { }
        public TDMSErrorException(string message, Exception e) : base(message, e) { }

        public TDMSErrorException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }   
        public TDMSErrorException(int errorCode, string message, Exception e) : base(message, e)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }

        public static void ThrowIfError(int success, string message)
        {
            if(success < 0)
            {
                var errorDesc = DDC.GetErrorDescription(success);
                throw new TDMSErrorException(success, $"<{message}> [{success}]:{errorDesc}");
            }
        }
    }
}