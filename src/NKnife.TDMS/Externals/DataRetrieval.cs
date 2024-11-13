using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    class DataRetrieval : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetNumDataValues(
            IntPtr channel,
            out ulong numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValues(
            IntPtr channel,
            UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet,
            IntPtr values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesTimestampComponents(
            IntPtr channel,
            UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet,
            uint[] year,
            uint[] month,
            uint[] day,
            uint[] hour,
            uint[] minute,
            uint[] second,
            double[] milliSecond,
            uint[] weekDay);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataType(
            IntPtr channel,
            out DataType dataType);
    }
}