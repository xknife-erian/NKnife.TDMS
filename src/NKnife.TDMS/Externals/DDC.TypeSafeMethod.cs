using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        #region Channel-Data

        #region Set Data

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

        #region Append Data

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

        #region Replace Data

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

        #endregion

        #region FileProperty

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

        #endregion

        #region ChannelGroupProperty

        #region CreateChannelGroupProperty

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyUInt8")]
        public static extern int CreateChannelGroupPropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyInt16")]
        public static extern int CreateChannelGroupPropertyInt16(IntPtr file, string property, short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyInt32")]
        public static extern int CreateChannelGroupPropertyInt32(IntPtr file, string property, int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyFloat")]
        public static extern int CreateChannelGroupPropertyFloat(IntPtr file, string property, float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyDouble")]
        public static extern int CreateChannelGroupPropertyDouble(IntPtr file, string property, double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelGroupPropertyString")]
        public static extern int CreateChannelGroupPropertyString(IntPtr file, string property, string value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyUInt8")]
        public static extern int SetChannelGroupPropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyInt16")]
        public static extern int SetChannelGroupPropertyInt16(IntPtr file, string property, short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyInt32")]
        public static extern int SetChannelGroupPropertyInt32(IntPtr file, string property, int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyFloat")]
        public static extern int SetChannelGroupPropertyFloat(IntPtr file, string property, float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyDouble")]
        public static extern int SetChannelGroupPropertyDouble(IntPtr file, string property, double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelGroupPropertyString")]
        public static extern int SetChannelGroupPropertyString(IntPtr file, string property, string value);

        #endregion

        #region GetChannelGroupProperty

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyUInt8")]
        public static extern int GetChannelGroupPropertyUInt8(IntPtr file, string property, out byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyInt16")]
        public static extern int GetChannelGroupPropertyInt16(IntPtr file, string property, out short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyInt32")]
        public static extern int GetChannelGroupPropertyInt32(IntPtr file, string property, out int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyFloat")]
        public static extern int GetChannelGroupPropertyFloat(IntPtr file, string property, out float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyDouble")]
        public static extern int GetChannelGroupPropertyDouble(IntPtr file, string property, out double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelGroupPropertyString")]
        public static extern int GetChannelGroupPropertyString(IntPtr file, string property, [Out] char[] value, UIntPtr valueSize);

        #endregion

        #endregion

        #region ChannelProperty

        #region CreateChannelProperty

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyUInt8")]
        public static extern int CreateChannelPropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyInt16")]
        public static extern int CreateChannelPropertyInt16(IntPtr file, string property, short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyInt32")]
        public static extern int CreateChannelPropertyInt32(IntPtr file, string property, int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyFloat")]
        public static extern int CreateChannelPropertyFloat(IntPtr file, string property, float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyDouble")]
        public static extern int CreateChannelPropertyDouble(IntPtr file, string property, double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CreateChannelPropertyString")]
        public static extern int CreateChannelPropertyString(IntPtr file, string property, string value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyUInt8")]
        public static extern int SetChannelPropertyUInt8(IntPtr file, string property, byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyInt16")]
        public static extern int SetChannelPropertyInt16(IntPtr file, string property, short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyInt32")]
        public static extern int SetChannelPropertyInt32(IntPtr file, string property, int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyFloat")]
        public static extern int SetChannelPropertyFloat(IntPtr file, string property, float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyDouble")]
        public static extern int SetChannelPropertyDouble(IntPtr file, string property, double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_SetChannelPropertyString")]
        public static extern int SetChannelPropertyString(IntPtr file, string property, string value);

        #endregion

        #region GetChannelProperty

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyUInt8")]
        public static extern int GetChannelPropertyUInt8(IntPtr file, string property, out byte value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyInt16")]
        public static extern int GetChannelPropertyInt16(IntPtr file, string property, out short value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyInt32")]
        public static extern int GetChannelPropertyInt32(IntPtr file, string property, out int value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyFloat")]
        public static extern int GetChannelPropertyFloat(IntPtr file, string property, out float value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyDouble")]
        public static extern int GetChannelPropertyDouble(IntPtr file, string property, out double value);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetChannelPropertyString")]
        public static extern int GetChannelPropertyString(IntPtr file, string property, [Out] char[] value, UIntPtr valueSize);

        #endregion

        #endregion
    }
}