using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : BaseDDC
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "SetDataValues")]
        public static extern int SetDataValues(IntPtr channel, IntPtr values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "SetDataValuesTimestampComponents")]
        public static extern int SetDataValuesTimestampComponents(IntPtr channel,
                                                                  uint[] year,
                                                                  uint[] month,
                                                                  uint[] day,
                                                                  uint[] hour,
                                                                  uint[] minute,
                                                                  uint[] second,
                                                                  double[] milliSecond,
                                                                  UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "AppendDataValues")]
        public static extern int AppendDataValues(IntPtr channel, IntPtr values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "AppendDataValuesTimestampComponents")]
        public static extern int AppendDataValuesTimestampComponents(IntPtr channel,
                                                                     uint[] year,
                                                                     uint[] month,
                                                                     uint[] day,
                                                                     uint[] hour,
                                                                     uint[] minute,
                                                                     uint[] second,
                                                                     double[] milliSecond,
                                                                     UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "ReplaceDataValues")]
        public static extern int ReplaceDataValues(IntPtr channel,
                                                   UIntPtr indexOfFirstValueToReplace,
                                                   IntPtr values,
                                                   UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "ReplaceDataValuesTimestampComponents")]
        public static extern int ReplaceDataValuesTimestampComponents(IntPtr channel,
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