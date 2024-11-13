using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    class PropertiesFile : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DDC_SetFileProperty(
            IntPtr file,
            string property,
            __arglist);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyTimestampComponents(
            IntPtr file,
            string property,
            uint year,
            uint month,
            uint day,
            uint hour,
            uint minute,
            uint second,
            double milliSecond);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyV(
            IntPtr file,
            string property,
            IntPtr args);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFileProperty(
            IntPtr file,
            string property,
            IntPtr value,
            UIntPtr valueSizeInBytes);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyTimestampComponents(
            IntPtr file,
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
        public static extern int DDC_GetFileStringPropertyLength(
            IntPtr file,
            string property,
            out uint length);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DDC_CreateFileProperty(
            IntPtr file,
            string property,
            DataType dataType,
            __arglist);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyTimestampComponents(
            IntPtr file,
            string property,
            uint year,
            uint month,
            uint day,
            uint hour,
            uint minute,
            uint second,
            double milliSecond);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyV(
            IntPtr file,
            string property,
            DataType dataType,
            IntPtr args);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_FilePropertyExists(
            IntPtr file,
            string property,
            out int exists);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetNumFileProperties(
            IntPtr file,
            out uint numProperties);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyNames(
            IntPtr file,
            IntPtr[] propertyNames,
            UIntPtr numPropertyNames);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyType(
            IntPtr file,
            string property,
            out DataType dataType);
    }
}