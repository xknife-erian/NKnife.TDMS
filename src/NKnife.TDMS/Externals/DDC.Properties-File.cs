using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        /// <summary>
        /// 设置文件属性的值
        /// </summary>
        /// <param name="file">文件句柄</param>
        /// <param name="property">文件属性的名称</param>
        /// <param name="__arglist">可变参数列表，用于传递属性的值</param>
        /// <returns>执行结果。0 表示成功，负数表示错误代码</returns>
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DDC_SetFileProperty")]
        public static extern int SetFileProperty(IntPtr file, string property, __arglist);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyTimestampComponents")]
        public static extern int SetFilePropertyTimestampComponents(IntPtr file,
                                                                    string property,
                                                                    uint year,
                                                                    uint month,
                                                                    uint day,
                                                                    uint hour,
                                                                    uint minute,
                                                                    uint second,
                                                                    double milliSecond);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyV")]
        public static extern int SetFilePropertyV(IntPtr file, string property, IntPtr args);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFileProperty")]
        public static extern int GetFileProperty(IntPtr file,
                                                 string property,
                                                 IntPtr value,
                                                 UIntPtr valueSizeInBytes);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyTimestampComponents")]
        public static extern int GetFilePropertyTimestampComponents(IntPtr file,
                                                                    string property,
                                                                    out uint year,
                                                                    out uint month,
                                                                    out uint day,
                                                                    out uint hour,
                                                                    out uint minute,
                                                                    out uint second,
                                                                    out double milliSecond,
                                                                    out uint weekDay);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFileStringPropertyLength")]
        public static extern int GetFileStringPropertyLength(IntPtr file, string property, out uint length);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DDC_CreateFileProperty")]
        public static extern int CreateFileProperty(IntPtr file,
                                                    string property,
                                                    TDMSDataType dataType,
                                                    __arglist);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyTimestampComponents")]
        public static extern int CreateFilePropertyTimestampComponents(IntPtr file,
                                                                       string property,
                                                                       uint year,
                                                                       uint month,
                                                                       uint day,
                                                                       uint hour,
                                                                       uint minute,
                                                                       uint second,
                                                                       double milliSecond);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyV")]
        public static extern int CreateFilePropertyV(IntPtr file,
                                                     string property,
                                                     TDMSDataType dataType,
                                                     IntPtr args);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_FilePropertyExists")]
        public static extern int FilePropertyExists(IntPtr file, string property, out int exists);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetNumFileProperties")]
        public static extern int GetNumFileProperties(IntPtr file, out uint numProperties);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyNames")]
        public static extern int GetFilePropertyNames(IntPtr file, IntPtr[] propertyNames, UIntPtr numPropertyNames);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyType")]
        public static extern int GetFilePropertyType(IntPtr file, string property, out TDMSDataType dataType);
    }
}