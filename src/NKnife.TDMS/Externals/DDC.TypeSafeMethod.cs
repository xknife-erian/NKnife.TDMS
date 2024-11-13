using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        #region SetData

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesUInt8")]
        public static extern int SetDataValuesUInt8(IntPtr channel, byte[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesInt16")]
        public static extern int SetDataValuesInt16(IntPtr channel, short[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesInt32")]
        public static extern int SetDataValuesInt32(IntPtr channel, int[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesFloat")]
        public static extern int SetDataValuesFloat(IntPtr channel, float[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesDouble")]
        public static extern int SetDataValuesDouble(IntPtr channel, double[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetDataValuesString")]
        public static extern int SetDataValuesString(IntPtr channel, string[] values, UIntPtr numValues);

        #endregion

        #region Append

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesUInt8")]
        public static extern int AppendDataValuesUInt8(IntPtr channel, byte[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesInt16")]
        public static extern int AppendDataValuesInt16(IntPtr channel, short[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesInt32")]
        public static extern int AppendDataValuesInt32(IntPtr channel, int[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesFloat")]
        public static extern int AppendDataValuesFloat(IntPtr channel, float[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesDouble")]
        public static extern int AppendDataValuesDouble(IntPtr channel, double[] values, UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_AppendDataValuesString")]
        public static extern int AppendDataValuesString(IntPtr channel, string[] values, UIntPtr numValues);

        #endregion

        #region Replace

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesUInt8")]
        public static extern int ReplaceDataValuesUInt8(IntPtr channel,
                                                        UIntPtr indexOfFirstValueToReplace,
                                                        byte[] values,
                                                        UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesInt16")]
        public static extern int ReplaceDataValuesInt16(IntPtr channel,
                                                        UIntPtr indexOfFirstValueToReplace,
                                                        short[] values,
                                                        UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesInt32")]
        public static extern int ReplaceDataValuesInt32(IntPtr channel,
                                                        UIntPtr indexOfFirstValueToReplace,
                                                        int[] values,
                                                        UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesFloat")]
        public static extern int ReplaceDataValuesFloat(IntPtr channel,
                                                        UIntPtr indexOfFirstValueToReplace,
                                                        float[] values,
                                                        UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesDouble")]
        public static extern int ReplaceDataValuesDouble(IntPtr channel,
                                                         UIntPtr indexOfFirstValueToReplace,
                                                         double[] values,
                                                         UIntPtr numValues);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_ReplaceDataValuesString")]
        public static extern int ReplaceDataValuesString(IntPtr channel,
                                                         UIntPtr indexOfFirstValueToReplace,
                                                         string[] values,
                                                         UIntPtr numValues);

        #endregion

        #region GetDataValues

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesUInt8")]
        public static extern int GetDataValuesUInt8(IntPtr channel,
                                                    UIntPtr indexOfFirstValueToGet,
                                                    UIntPtr numValuesToGet,
                                                    byte[] values);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesInt16")]
        public static extern int GetDataValuesInt16(IntPtr channel,
                                                    UIntPtr indexOfFirstValueToGet,
                                                    UIntPtr numValuesToGet,
                                                    short[] values);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesInt32")]
        public static extern int GetDataValuesInt32(IntPtr channel,
                                                    UIntPtr indexOfFirstValueToGet,
                                                    UIntPtr numValuesToGet,
                                                    int[] values);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesFloat")]
        public static extern int GetDataValuesFloat(IntPtr channel,
                                                    UIntPtr indexOfFirstValueToGet,
                                                    UIntPtr numValuesToGet,
                                                    float[] values);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesDouble")]
        public static extern int GetDataValuesDouble(IntPtr channel,
                                                     UIntPtr indexOfFirstValueToGet,
                                                     UIntPtr numValuesToGet,
                                                     double[] values);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetDataValuesString")]
        public static extern int GetDataValuesString(IntPtr channel,
                                                     UIntPtr indexOfFirstValueToGet,
                                                     UIntPtr numValuesToGet,
                                                     IntPtr[] values);

        #endregion

        #region CreateFileProperty

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyUInt8")]
        public static extern int CreateFilePropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyInt16")]
        public static extern int CreateFilePropertyInt16(IntPtr file, string property, short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyInt32")]
        public static extern int CreateFilePropertyInt32(IntPtr file, string property, int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyFloat")]
        public static extern int CreateFilePropertyFloat(IntPtr file, string property, float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyDouble")]
        public static extern int CreateFilePropertyDouble(IntPtr file, string property, double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateFilePropertyString")]
        public static extern int CreateFilePropertyString(IntPtr file, string property, string value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyUInt8")]
        public static extern int SetFilePropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyInt16")]
        public static extern int SetFilePropertyInt16(IntPtr file, string property, short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyInt32")]
        public static extern int SetFilePropertyInt32(IntPtr file, string property, int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyFloat")]
        public static extern int SetFilePropertyFloat(IntPtr file, string property, float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyDouble")]
        public static extern int SetFilePropertyDouble(IntPtr file, string property, double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetFilePropertyString")]
        public static extern int SetFilePropertyString(IntPtr file, string property, string value);

        #endregion

        #region GetFileProperty

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyUInt8")]
        public static extern int GetFilePropertyUInt8(IntPtr file, string property, out byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyInt16")]
        public static extern int GetFilePropertyInt16(IntPtr file, string property, out short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyInt32")]
        public static extern int GetFilePropertyInt32(IntPtr file, string property, out int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyFloat")]
        public static extern int GetFilePropertyFloat(IntPtr file, string property, out float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyDouble")]
        public static extern int GetFilePropertyDouble(IntPtr file, string property, out double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetFilePropertyString")]
        public static extern int GetFilePropertyString(IntPtr file, string property, [Out] char[] value, UIntPtr valueSize);

        #endregion
    }
}