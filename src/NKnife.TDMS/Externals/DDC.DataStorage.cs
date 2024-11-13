using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValues")]
        public static extern int SetDataValues(IntPtr channel, IntPtr values, UIntPtr numValues);

        /// <summary>
        /// 函数用于为指定的通道设置时间戳数据值。该函数通过传递每个时间戳组件的单独数组来指定时间戳值。通过组合每个单独数组中的相应条目来构造每个完整的时间戳值。<br/><br/>
        /// 参数说明<br/>
        /// • channel(DDCChannelHandle) : 包含新数据值的通道的句柄。指定通道的数据类型必须是 DDC_Timestamp。<br/>
        /// • year(unsigned int[]) : 包含时间戳值的年份组件的数组。该函数将每个年份值与其他时间戳组件数组中的相应值组合以构造单个时间戳值。年份表示本地日历时间。<br/>
        /// • month(unsigned int[]) : 包含时间戳值的月份组件的数组。有效值为 1（1 月）到 12（12 月）。<br/>
        /// • day(unsigned int[]) : 包含时间戳值的日期组件的数组。有效值为 1 到 31。<br/>
        /// • hour(unsigned int[]) : 包含时间戳值的小时组件的数组。有效值为 0 到 23。<br/>
        /// • minute(unsigned int[]) : 包含时间戳值的分钟组件的数组。有效值为 0 到 59。<br/>
        /// • second(unsigned int[]) : 包含时间戳值的秒组件的数组。有效值为 0 到 59。<br/>
        /// • millisecond(double[]) : 包含时间戳值的毫秒组件的数组。只能指定非负值。<br/>
        /// • numberOfValues(size_t) : 每个时间戳组件数组中的值的数量。<br/><br/>
        /// 返回值<br/>
        /// •	status(integer) : 返回值指示函数是否成功执行。零表示成功执行，负数表示错误代码。
        /// </summary>
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesTimestampComponents")]
        public static extern int SetDataValuesTimestampComponents(IntPtr channel,
                                                                  uint[] year,
                                                                  uint[] month,
                                                                  uint[] day,
                                                                  uint[] hour,
                                                                  uint[] minute,
                                                                  uint[] second,
                                                                  double[] milliSecond,
                                                                  UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValues")]
        public static extern int AppendDataValues(IntPtr channel, IntPtr values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesTimestampComponents")]
        public static extern int AppendDataValuesTimestampComponents(IntPtr channel,
                                                                     uint[] year,
                                                                     uint[] month,
                                                                     uint[] day,
                                                                     uint[] hour,
                                                                     uint[] minute,
                                                                     uint[] second,
                                                                     double[] milliSecond,
                                                                     UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValues")]
        public static extern int ReplaceDataValues(IntPtr channel,
                                                   UIntPtr indexOfFirstValueToReplace,
                                                   IntPtr values,
                                                   UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesTimestampComponents")]
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