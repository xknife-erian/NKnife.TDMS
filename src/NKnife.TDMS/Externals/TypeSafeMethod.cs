using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal class TypeSafeMethod : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesUInt8(IntPtr channel, byte[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesInt16(IntPtr channel, short[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesInt32(IntPtr channel, int[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesFloat(IntPtr channel, float[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesDouble(IntPtr channel, double[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetDataValuesString(IntPtr channel, string[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesUInt8(IntPtr channel, byte[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesInt16(IntPtr channel, short[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesInt32(IntPtr channel, int[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesFloat(IntPtr channel, float[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesDouble(IntPtr channel, double[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AppendDataValuesString(IntPtr channel, string[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesUInt8(IntPtr channel, UIntPtr indexOfFirstValueToReplace,
            byte[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesInt16(IntPtr channel, UIntPtr indexOfFirstValueToReplace,
            short[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesInt32(IntPtr channel, UIntPtr indexOfFirstValueToReplace,
            int[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesFloat(IntPtr channel, UIntPtr indexOfFirstValueToReplace,
            float[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesDouble(IntPtr channel, UIntPtr indexOfFirstValueToReplace,
            double[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_ReplaceDataValuesString(IntPtr channel, UIntPtr indexOfFirstValueToReplace,
            string[] values, UIntPtr numValues);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesUInt8(IntPtr channel, UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet, byte[] values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesInt16(IntPtr channel, UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet, short[] values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesInt32(IntPtr channel, UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet, int[] values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesFloat(IntPtr channel, UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet, float[] values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesDouble(IntPtr channel, UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet, double[] values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetDataValuesString(IntPtr channel, UIntPtr indexOfFirstValueToGet,
            UIntPtr numValuesToGet, IntPtr[] values);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyInt16(IntPtr file, string property, short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyInt32(IntPtr file, string property, int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyFloat(IntPtr file, string property, float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyDouble(IntPtr file, string property, double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateFilePropertyString(IntPtr file, string property, string value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyInt16(IntPtr file, string property, short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyInt32(IntPtr file, string property, int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyFloat(IntPtr file, string property, float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyDouble(IntPtr file, string property, double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetFilePropertyString(IntPtr file, string property, string value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyUInt8(IntPtr file, string property, out byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyInt16(IntPtr file, string property, out short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyInt32(IntPtr file, string property, out int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyFloat(IntPtr file, string property, out float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyDouble(IntPtr file, string property, out double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyString(IntPtr file, string property, [Out] char[] value,
            UIntPtr valueSize);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyUInt8(IntPtr channelGroup, string property, byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyInt16(IntPtr channelGroup, string property, short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyInt32(IntPtr channelGroup, string property, int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyFloat(IntPtr channelGroup, string property, float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyDouble(IntPtr channelGroup, string property,
            double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelGroupPropertyString(IntPtr channelGroup, string property,
            string value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyUInt8(IntPtr channelGroup, string property, byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyInt16(IntPtr channelGroup, string property, short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyInt32(IntPtr channelGroup, string property, int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyFloat(IntPtr channelGroup, string property, float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyDouble(IntPtr channelGroup, string property, double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelGroupPropertyString(IntPtr channelGroup, string property, string value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyUInt8(IntPtr channelGroup, string property, out byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int
            DDC_GetChannelGroupPropertyInt16(IntPtr channelGroup, string property, out short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyInt32(IntPtr channelGroup, string property, out int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int
            DDC_GetChannelGroupPropertyFloat(IntPtr channelGroup, string property, out float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyDouble(IntPtr channelGroup, string property,
            out double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyString(IntPtr channelGroup, string property,
            [Out] char[] value, UIntPtr valueSize);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyUInt8(IntPtr channel, string property, byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyInt16(IntPtr channel, string property, short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyInt32(IntPtr channel, string property, int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyFloat(IntPtr channel, string property, float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyDouble(IntPtr channel, string property, double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CreateChannelPropertyString(IntPtr channel, string property, string value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyUInt8(IntPtr channel, string property, byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyInt16(IntPtr channel, string property, short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyInt32(IntPtr channel, string property, int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyFloat(IntPtr channel, string property, float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyDouble(IntPtr channel, string property, double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SetChannelPropertyString(IntPtr channel, string property, string value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyUInt8(IntPtr channel, string property, out byte value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyInt16(IntPtr channel, string property, out short value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyInt32(IntPtr channel, string property, out int value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyFloat(IntPtr channel, string property, out float value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyDouble(IntPtr channel, string property, out double value);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyString(IntPtr channel, string property, [Out] char[] value,
            UIntPtr valueSize);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyNameFromIndex(IntPtr file, UIntPtr index, [Out] char[] propertyName,
            UIntPtr propertyNameSize);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetFilePropertyNameLengthFromIndex(IntPtr file, UIntPtr index,
            out UIntPtr propertyNameLength);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyNameFromIndex(IntPtr channelGroup, UIntPtr index,
            [Out] char[] propertyName, UIntPtr propertyNameSize);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroupPropertyNameLengthFromIndex(IntPtr channelGroup, UIntPtr index,
            out UIntPtr propertyNameLength);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyNameFromIndex(IntPtr channel, UIntPtr index,
            [Out] char[] propertyName, UIntPtr propertyNameSize);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelPropertyNameLengthFromIndex(IntPtr channel, UIntPtr index,
            out UIntPtr propertyNameLength);
    }
}