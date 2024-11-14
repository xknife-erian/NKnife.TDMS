using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetNumDataValues")]
        public static extern int GetNumDataValues(IntPtr channel, out ulong numValues);

        /// <summary>
        /// 获取指定通道中的数据值
        /// </summary>
        /// <param name="channel">通道句柄</param>
        /// <param name="indexOfFirstValueToGet">要获取的第一个数据值的从零开始的索引</param>
        /// <param name="numValuesToGet">要获取的数据值的数量，从指定的索引开始</param>
        /// <param name="values">
        /// - 接收指定通道数据值的数组。此数组的类型必须与指定通道的数据类型匹配。<br/>
        /// - 可以调用<see cref="GetDataType"/>来获取通道的数据类型。<br/>
        /// - 这个数组必须足够大，至少可以容纳numberOfValuesToGet参数指定的值的数量。<br/>
        /// - 如果指定通道的数据类型是字符串，则必须传递字符指针（char*）类型的数组。这个函数用这个函数分配的字符串指针填充数组。<br/>
        /// - 如果指定通道的数据类型是DDC_Timestamp，则必须传递CVIAbsoluteTime类型的数组。有关此类型的更多信息，请参阅实用程序库中的绝对时间函数。<br/>
        /// - 如果指定通道的数据类型是DDC_Timestamp，则必须调用DDC_GetDataValuesTimestampComponents而不是此函数。<br/>
        /// </param>
        /// <returns>是否成功。除非另有说明，否则零表示执行成功，负数表示错误代码。</returns>
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
        public static extern int GetDataType(IntPtr channel, out TDMSDataType dataType);
    }
}