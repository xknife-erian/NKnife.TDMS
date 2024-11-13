using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    class DataStorage : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValues(
            IntPtr channel,
            IntPtr values,
            UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesTimestampComponents(
            IntPtr channel,
            uint[] year,
            uint[] month,
            uint[] day,
            uint[] hour,
            uint[] minute,
            uint[] second,
            double[] milliSecond,
            UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValues(
            IntPtr channel,
            IntPtr values,
            UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesTimestampComponents(
            IntPtr channel,
            uint[] year,
            uint[] month,
            uint[] day,
            uint[] hour,
            uint[] minute,
            uint[] second,
            double[] milliSecond,
            UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValues(
            IntPtr channel,
            UIntPtr indexOfFirstValueToReplace,
            IntPtr values,
            UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesTimestampComponents(
            IntPtr channel,
            UIntPtr indexOfFirstValueToReplace,
            uint[] year,
            uint[] month,
            uint[] day,
            uint[] hour,
            uint[] minute,
            uint[] second,
            double[] milliSecond,
            UIntPtr numValues);
    }
}