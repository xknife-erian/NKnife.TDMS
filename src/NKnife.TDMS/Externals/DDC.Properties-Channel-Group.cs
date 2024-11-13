using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DDC_SetChannelGroupProperty")]
        public static extern int SetChannelGroupProperty(IntPtr channelGroup, string property, __arglist);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall,
                   EntryPoint = "DDC_SetChannelGroupPropertyTimestampComponents")]
        public static extern int SetChannelGroupPropertyTimestampComponents(IntPtr channelGroup,
                                                                            string property,
                                                                            uint year,
                                                                            uint month,
                                                                            uint day,
                                                                            uint hour,
                                                                            uint minute,
                                                                            uint second,
                                                                            double milliSecond);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyV")]
        public static extern int SetChannelGroupPropertyV(IntPtr channelGroup, string property, IntPtr args);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupProperty")]
        public static extern int GetChannelGroupProperty(IntPtr channelGroup,
                                                         string property,
                                                         IntPtr value,
                                                         UIntPtr valueSizeInBytes);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall,
                   EntryPoint = "DDC_GetChannelGroupPropertyTimestampComponents")]
        public static extern int GetChannelGroupPropertyTimestampComponents(IntPtr channelGroup,
                                                                            string property,
                                                                            out uint year,
                                                                            out uint month,
                                                                            out uint day,
                                                                            out uint hour,
                                                                            out uint minute,
                                                                            out uint second,
                                                                            out double milliSecond,
                                                                            out uint weekDay);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupStringPropertyLength")]
        public static extern int GetChannelGroupStringPropertyLength(IntPtr channelGroup, string property, out uint length);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DDC_CreateChannelGroupProperty")]
        public static extern int CreateChannelGroupProperty(IntPtr channelGroup,
                                                            string property,
                                                            TDMSDataType dataType,
                                                            __arglist);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall,
                   EntryPoint = "DDC_CreateChannelGroupPropertyTimestampComponents")]
        public static extern int CreateChannelGroupPropertyTimestampComponents(IntPtr channelGroup,
                                                                               string property,
                                                                               uint year,
                                                                               uint month,
                                                                               uint day,
                                                                               uint hour,
                                                                               uint minute,
                                                                               uint second,
                                                                               double milliSecond);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyV")]
        public static extern int CreateChannelGroupPropertyV(IntPtr channelGroup,
                                                             string property,
                                                             TDMSDataType dataType,
                                                             IntPtr args);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ChannelGroupPropertyExists")]
        public static extern int ChannelGroupPropertyExists(IntPtr channelGroup, string property, out int exists);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetNumChannelGroupProperties")]
        public static extern int GetNumChannelGroupProperties(IntPtr channelGroup, out uint numProperties);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyNames")]
        public static extern int GetChannelGroupPropertyNames(IntPtr channelGroup,
                                                              IntPtr[] propertyNames,
                                                              UIntPtr numPropertyNames);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyType")]
        public static extern int GetChannelGroupPropertyType(IntPtr channelGroup, string property, out TDMSDataType dataType);
    }
}