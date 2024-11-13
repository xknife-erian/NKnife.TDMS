using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : BaseDDC
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetNumDataValues")]
        public static extern int GetNumDataValues(IntPtr channel, out ulong numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValues")]
        public static extern int GetDataValues(IntPtr channel,
                                               UIntPtr indexOfFirstValueToGet,
                                               UIntPtr numValuesToGet,
                                               IntPtr values);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesTimestampComponents")]
        public static extern int GetDataValuesTimestampComponents(IntPtr channel,
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

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataType")]
        public static extern int GetDataType(IntPtr channel, out DataType dataType);
    }
}