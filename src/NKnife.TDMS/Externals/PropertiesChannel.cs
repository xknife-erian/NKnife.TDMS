using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    class PropertiesChannel : DDC
    {


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DDC_SetChannelProperty(
            IntPtr channel,
            string property,
            __arglist);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyTimestampComponents(
            IntPtr channel,
            string property,
            uint year,
            uint month,
            uint day,
            uint hour,
            uint minute,
            uint second,
            double milliSecond);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyV(
            IntPtr channel,
            string property,
            IntPtr args);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelProperty(
            IntPtr channel,
            string property,
            IntPtr value,
            UIntPtr valueSizeInBytes);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyTimestampComponents(
            IntPtr channel,
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
        public static extern int DDC_GetChannelStringPropertyLength(
            IntPtr channel,
            string property,
            out uint length);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DDC_CreateChannelProperty(
            IntPtr channel,
            string property,
            DataType dataType,
            __arglist);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyTimestampComponents(
            IntPtr channel,
            string property,
            uint year,
            uint month,
            uint day,
            uint hour,
            uint minute,
            uint second,
            double milliSecond);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyV(
            IntPtr channel,
            string property,
            DataType dataType,
            IntPtr args);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ChannelPropertyExists(
            IntPtr channel,
            string property,
            out int exists);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetNumChannelProperties(
            IntPtr channel,
            out uint numProperties);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyNames(
            IntPtr channel,
            IntPtr[] propertyNames,
            UIntPtr numPropertyNames);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyType(
            IntPtr channel,
            string property,
            out DataType dataType);
    }
}