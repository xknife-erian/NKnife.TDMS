using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    internal class TDMSChannel : BaseTDMSLevel, ITDMSChannel
    {
        public TDMSChannel(IntPtr channelPtr)
        {
            _SelfPtr         = channelPtr;
            PropertyOperator = new ChannelPropertyOperator(channelPtr);
            SetNameAndDescription();
            SetUnit();
        }

        private void SetUnit()
        {
            Unit = GetDefaultProperty(Constants.DDC_CHANNEL_UNIT_STRING);
        }

        #region Implementation of ITDMSChannel
        public bool SetData<T>(params T[] values)
        {
            if(values.Length <= 0)
                return true;

            var dataType = typeof(T).ToDataType();
            var success  = -1;

            switch (dataType)
            {
                case TDMSDataType.UInt8:
                    var byteArray = values.Cast<byte>().ToArray();
                    success = DDC.SetDataValuesUInt8(_SelfPtr, byteArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesUInt8.");

                    break;

                case TDMSDataType.Int16:
                    var shortArray = values.Cast<short>().ToArray();
                    success = DDC.SetDataValuesInt16(_SelfPtr, shortArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesInt16.");

                    break;

                case TDMSDataType.Int32:
                    var intArray = values.Cast<int>().ToArray();
                    success = DDC.SetDataValuesInt32(_SelfPtr, intArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesInt32.");

                    break;

                case TDMSDataType.Float:
                    var floatArray = values.Cast<float>().ToArray();
                    success = DDC.SetDataValuesFloat(_SelfPtr, floatArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesFloat.");

                    break;

                case TDMSDataType.Double:
                    var doubleArray = values.Cast<double>().ToArray();
                    success = DDC.SetDataValuesDouble(_SelfPtr, doubleArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesDouble.");

                    break;

                case TDMSDataType.String:
                    var stringArray = values.Cast<string>().ToArray();
                    success = DDC.SetDataValuesString(_SelfPtr, stringArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesString.");

                    break;

                case TDMSDataType.Timestamp:
                    var datetimeArray = values.Cast<DateTime>().ToArray();
                    var df            = datetimeArray.Factoring();
                    success = DDC.SetDataValuesTimestampComponents(_SelfPtr,
                                                                   df.Years,
                                                                   df.Months,
                                                                   df.Days,
                                                                   df.Hours,
                                                                   df.Minutes,
                                                                   df.Seconds,
                                                                   df.Milliseconds,
                                                                   (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetDataValuesTimestampComponents.");

                    break;

                case TDMSDataType.UnDefine:
                default: throw new InvalidEnumArgumentException();
            }

            return success == 0;
        }

        /// <inheritdoc />
        public bool AppendData<T>(params T[] values)
        {
            if(values.Length <= 0)
                return true;

            var dataType = typeof(T).ToDataType();
            var success  = -1;

            switch (dataType)
            {
                case TDMSDataType.UInt8:
                    var byteArray = values.Cast<byte>().ToArray();
                    success = DDC.AppendDataValuesUInt8(_SelfPtr, byteArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesUInt8.");

                    break;

                case TDMSDataType.Int16:
                    var shortArray = values.Cast<short>().ToArray();
                    success = DDC.AppendDataValuesInt16(_SelfPtr, shortArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesInt16.");

                    break;

                case TDMSDataType.Int32:
                    var intArray = values.Cast<int>().ToArray();
                    success = DDC.AppendDataValuesInt32(_SelfPtr, intArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesInt32.");

                    break;

                case TDMSDataType.Float:
                    var floatArray = values.Cast<float>().ToArray();
                    success = DDC.AppendDataValuesFloat(_SelfPtr, floatArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesFloat.");

                    break;

                case TDMSDataType.Double:
                    var doubleArray = values.Cast<double>().ToArray();
                    success = DDC.AppendDataValuesDouble(_SelfPtr, doubleArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesDouble.");

                    break;

                case TDMSDataType.String:
                    var stringArray = values.Cast<string>().ToArray();
                    success = DDC.AppendDataValuesString(_SelfPtr, stringArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesString.");

                    break;

                case TDMSDataType.Timestamp:
                    var datetimeArray = values.Cast<DateTime>().ToArray();
                    var df            = datetimeArray.Factoring();
                    success = DDC.AppendDataValuesTimestampComponents(_SelfPtr,
                                                                      df.Years,
                                                                      df.Months,
                                                                      df.Days,
                                                                      df.Hours,
                                                                      df.Minutes,
                                                                      df.Seconds,
                                                                      df.Milliseconds,
                                                                      (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to AppendDataValuesTimestampComponents.");

                    break;

                case TDMSDataType.UnDefine:
                default: throw new InvalidEnumArgumentException();
            }

            return success == 0;
        }

        /// <inheritdoc />
        public bool UpdateData<T>(int index, params T[] values)
        {
            if(values.Length <= 0)
                return true;

            var dataType = typeof(T).ToDataType();
            var success  = -1;

            switch (dataType)
            {
                case TDMSDataType.UInt8:
                    var byteArray = values.Cast<byte>().ToArray();
                    success = DDC.ReplaceDataValuesUInt8(_SelfPtr, (UIntPtr)index, byteArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesUInt8.");

                    break;

                case TDMSDataType.Int16:
                    var shortArray = values.Cast<short>().ToArray();
                    success = DDC.ReplaceDataValuesInt16(_SelfPtr, (UIntPtr)index, shortArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesInt16.");

                    break;

                case TDMSDataType.Int32:
                    var intArray = values.Cast<int>().ToArray();
                    success = DDC.ReplaceDataValuesInt32(_SelfPtr, (UIntPtr)index, intArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesInt32.");

                    break;

                case TDMSDataType.Float:
                    var floatArray = values.Cast<float>().ToArray();
                    success = DDC.ReplaceDataValuesFloat(_SelfPtr, (UIntPtr)index, floatArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesFloat.");

                    break;

                case TDMSDataType.Double:
                    var doubleArray = values.Cast<double>().ToArray();
                    success = DDC.ReplaceDataValuesDouble(_SelfPtr, (UIntPtr)index, doubleArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesDouble.");

                    break;

                case TDMSDataType.String:
                    var stringArray = values.Cast<string>().ToArray();
                    success = DDC.ReplaceDataValuesString(_SelfPtr, (UIntPtr)index, stringArray, (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesString.");

                    break;

                case TDMSDataType.Timestamp:
                    var datetimeArray = values.Cast<DateTime>().ToArray();
                    var df            = datetimeArray.Factoring();
                    success = DDC.ReplaceDataValuesTimestampComponents(_SelfPtr,
                                                                       (UIntPtr)index,
                                                                       df.Years,
                                                                       df.Months,
                                                                       df.Days,
                                                                       df.Hours,
                                                                       df.Minutes,
                                                                       df.Seconds,
                                                                       df.Milliseconds,
                                                                       (UIntPtr)values.Length);
                    TDMSErrorException.ThrowIfError(success, "Failed to ReplaceDataValuesTimestampComponents.");

                    break;

                case TDMSDataType.UnDefine:
                default: throw new InvalidEnumArgumentException();
            }

            return success == 0;
        }

        /// <inheritdoc />
        public T[] GetDataValues<T>(uint startIndex, uint length)
        {
            var dataType = typeof(T).ToDataType();
            var success = -1;

            switch (dataType)
            {
                case TDMSDataType.UInt8:
                    var byteArray = new byte[length];
                    success = DDC.GetDataValuesUInt8(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, byteArray);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesUInt8.");

                    return byteArray.Cast<T>().ToArray();

                case TDMSDataType.Int16:
                    var shortArray = new short[length];
                    success = DDC.GetDataValuesInt16(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, shortArray);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesInt16.");

                    return shortArray.Cast<T>().ToArray();

                case TDMSDataType.Int32:
                    var intArray = new int[length];
                    success = DDC.GetDataValuesInt32(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, intArray);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesInt32.");

                    return intArray.Cast<T>().ToArray();

                case TDMSDataType.Float:
                    var floatArray = new float[length];
                    success = DDC.GetDataValuesFloat(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, floatArray);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesFloat.");

                    return floatArray.Cast<T>().ToArray();

                case TDMSDataType.Double:
                    var doubleArray = new double[length];
                    success = DDC.GetDataValuesDouble(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, doubleArray);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesDouble.");

                    return doubleArray.Cast<T>().ToArray();

                case TDMSDataType.String:
                    var stringArray = new string[length];
                    var stringPointers = new IntPtr[length];
                    success = DDC.GetDataValuesString(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, stringPointers);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesString.");
                    for (int i = 0; i < length; i++)
                    {
                        stringArray[i] = Marshal.PtrToStringAnsi(stringPointers[i]);
                    }

                    return stringArray.Cast<T>().ToArray();

                case TDMSDataType.Timestamp:
                    var df            = new DatetimeFactor((int)length);
                    success = DDC.GetDataValuesTimestampComponents(_SelfPtr,
                                                                   (UIntPtr)startIndex,
                                                                   (UIntPtr)length,
                                                                   df.Years,
                                                                   df.Months,
                                                                   df.Days,
                                                                   df.Hours,
                                                                   df.Minutes,
                                                                   df.Seconds,
                                                                   df.Milliseconds,
                                                                   df.WeekDays);
                    TDMSErrorException.ThrowIfError(success, "Failed to GetDataValuesTimestampComponents.");
                    
                    return df.ToDateTimeArray().Cast<T>().ToArray();

                case TDMSDataType.UnDefine:
                default: throw new InvalidEnumArgumentException();
            }

            return null;
        }

        /// <inheritdoc />
        public string Unit { get; private set; }
        #endregion

        #region Implementation of ITDMSLevel
        /// <inheritdoc />
        public override bool Close()
        {
            if(!_IsClosed)
            {
                var success = DDC.CloseChannel(_SelfPtr);
                TDMSErrorException.ThrowIfError(success, "Failed to CloseChannel.");
            }

            return _IsClosed = true;
        }

        /// <inheritdoc />
        public override ulong ChildCount => DDC.CountDataValues(_SelfPtr, out var numValues) == 0 ? numValues : 0;

        /// <inheritdoc />
        public override bool Clear()
        {
            return false;
        }

        /// <inheritdoc />
        public override bool Contains(string levelName)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool TryGetItem(string levelName, out ITDMSLevel level)
        {
            level = null;

            return false;
        }

        /// <inheritdoc />
        public override bool Remove(string levelName)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool RemoveAt(int index)
        {
            return false;
        }
        #endregion
    }
}