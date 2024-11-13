using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    class PropertiesChannelGroup : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DDC_SetChannelGroupProperty(
            IntPtr channelGroup,
            string property,
            __arglist);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyTimestampComponents(
            IntPtr channelGroup,
            string property,
            uint year,
            uint month,
            uint day,
            uint hour,
            uint minute,
            uint second,
            double milliSecond);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyV(
            IntPtr channelGroup,
            string property,
            IntPtr args);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupProperty(
            IntPtr channelGroup,
            string property,
            IntPtr value,
            UIntPtr valueSizeInBytes);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyTimestampComponents(
            IntPtr channelGroup,
            string property,
            out uint year,
            out uint month,
            out uint day,
            out uint hour,
            out uint minute,
            out uint second,
            out double milliSecond,
            out uint weekDay);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupStringPropertyLength(
            IntPtr channelGroup,
            string property,
            out uint length);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DDC_CreateChannelGroupProperty(
            IntPtr channelGroup,
            string property,
            DataType dataType,
            __arglist);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyTimestampComponents(
            IntPtr channelGroup,
            string property,
            uint year,
            uint month,
            uint day,
            uint hour,
            uint minute,
            uint second,
            double milliSecond);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyV(
            IntPtr channelGroup,
            string property,
            DataType dataType,
            IntPtr args);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ChannelGroupPropertyExists(
            IntPtr channelGroup,
            string property,
            out int exists);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetNumChannelGroupProperties(
            IntPtr channelGroup,
            out uint numProperties);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyNames(
            IntPtr channelGroup,
            IntPtr[] propertyNames,
            UIntPtr numPropertyNames);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyType(
            IntPtr channelGroup,
            string property,
            out DataType dataType);
    }
}