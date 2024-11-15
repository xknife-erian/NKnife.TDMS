using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.TDMS.Common
{
    internal class Util
    {
        public static TDMSDataType GetTDMSDataType<T>()
        {
            var dataType  = Common.TDMSDataType.String;
            var inputType = typeof(T);
            if (inputType == typeof(byte))
            {
                dataType = Common.TDMSDataType.UInt8;
            }
            else if (inputType == typeof(short))
            {
                dataType = Common.TDMSDataType.Int16;
            }
            else if (inputType == typeof(int))
            {
                dataType = Common.TDMSDataType.Int32;
            }
            else if (inputType == typeof(float))
            {
                dataType = Common.TDMSDataType.Float;
            }
            else if (inputType == typeof(double))
            {
                dataType = Common.TDMSDataType.Double;
            }
            else if (inputType == typeof(DateTime))
            {
                dataType = Common.TDMSDataType.Timestamp;
            }
            else
            {
                throw new TDMSErrorException("Unsupported data type.");
            }

            return dataType;
        }
    }
}
