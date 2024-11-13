using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DDC_SetChannelProperty")]
        public static extern int SetChannelProperty(IntPtr channel, string property, __arglist);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyTimestampComponents")]
        public static extern int SetChannelPropertyTimestampComponents(IntPtr channel,
                                                                       string property,
                                                                       uint year,
                                                                       uint month,
                                                                       uint day,
                                                                       uint hour,
                                                                       uint minute,
                                                                       uint second,
                                                                       double milliSecond);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyV")]
        public static extern int SetChannelPropertyV(IntPtr channel, string property, IntPtr args);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelProperty")]
        public static extern int GetChannelProperty(IntPtr channel,
                                                    string property,
                                                    IntPtr value,
                                                    UIntPtr valueSizeInBytes);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyTimestampComponents")]
        public static extern int GetChannelPropertyTimestampComponents(IntPtr channel,
                                                                       string property,
                                                                       out uint year,
                                                                       out uint month,
                                                                       out uint day,
                                                                       out uint hour,
                                                                       out uint minute,
                                                                       out uint second,
                                                                       out double milliSecond,
                                                                       out uint weekDay);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelStringPropertyLength")]
        public static extern int GetChannelStringPropertyLength(IntPtr channel, string property, out uint length);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DDC_CreateChannelProperty")]
        public static extern int CreateChannelProperty(IntPtr channel,
                                                       string property,
                                                       TDMSDataType dataType,
                                                       __arglist);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall,
                   EntryPoint = "DDC_CreateChannelPropertyTimestampComponents")]
        public static extern int CreateChannelPropertyTimestampComponents(IntPtr channel,
                                                                          string property,
                                                                          uint year,
                                                                          uint month,
                                                                          uint day,
                                                                          uint hour,
                                                                          uint minute,
                                                                          uint second,
                                                                          double milliSecond);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyV")]
        public static extern int CreateChannelPropertyV(IntPtr channel,
                                                        string property,
                                                        TDMSDataType dataType,
                                                        IntPtr args);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ChannelPropertyExists")]
        public static extern int ChannelPropertyExists(IntPtr channel, string property, out int exists);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetNumChannelProperties")]
        public static extern int GetNumChannelProperties(IntPtr channel, out uint numProperties);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyNames")]
        public static extern int GetChannelPropertyNames(IntPtr channel, IntPtr[] propertyNames, UIntPtr numPropertyNames);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyType")]
        public static extern int GetChannelPropertyType(IntPtr channel, string property, out TDMSDataType dataType);
    }
}