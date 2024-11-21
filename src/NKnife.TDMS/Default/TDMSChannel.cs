using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    internal class TDMSChannel : BaseTDMSLevel, ITDMSChannel
    {
        private string _unit;

        public TDMSChannel(IntPtr channelPtr)
        {
            _SelfPtr = channelPtr;
            SetNameAndDescription();
            SetUnit();
        }

        private void SetUnit()
        {
            _unit = GetDefaultProperty(Constants.DDC_CHANNEL_UNIT_STRING);
        }

        #region Implementation of ITDMSChannel
        public bool SetData<T>(T[] values) where T : struct
        {
            var success = -1;

            try
            {
                using var data = new Data<T>(values);

                var v = data.GetValues();
                success = DDC.SetDataValues(_SelfPtr, v.Values, v.Length);

                throw new TDMSErrorException(success, "Failed to add data.");
            }
            catch (Exception e)
            {
                if(success == -1)
                    throw new TDMSErrorException("Failed to add data.", e);
            }

            return success == 0;
        }

        /// <inheritdoc />
        public bool AppendData<T>(T[] values) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool UpdateData<T>(int index, T[] values) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T[] GetDataValues<T>(uint startIndex, uint length)
        {
            var values = new T[length];

            // 获取数组的指针
            var valuesPtr = Marshal.UnsafeAddrOfPinnedArrayElement(values, 0);

            var success = DDC.GetDataValues(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, valuesPtr);
            TDMSErrorException.ThrowIfError(success, "Failed to GetDataValues.");

            return values;
        }

        /// <inheritdoc />
        public string Unit => _unit;
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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool Contains(string levelName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool TryGetItem(string levelName, out ITDMSLevel level)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool Remove(string levelName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool ManualCloseNode()
        {
            return Close();
        }

        /// <inheritdoc />
        protected override TDMSDataType GetPropertyType(string propertyName)
        {
            var success = DDC.GetChannelPropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");

            return type;
        }

        /// <inheritdoc />
        protected override T GetProperty<T>(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void UpdateProperty<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void CreateProperty<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        protected uint GetStringPropertyLength(string propertyName)
        {
            var success = DDC.GetChannelStringPropertyLength(_SelfPtr, propertyName, out var length);
            TDMSErrorException.ThrowIfError(success,
                                            $"Failed to get Channel string property length, Key:[{propertyName}]");

            return length;
        }

        protected void GetPropertyInternal(string propertyName, IntPtr result, uint length)
        {
            if(length > 0)//如果长度为0，说明不是字符串类型
                length++; //HACK: 为了兼容字符串类型，长度+1
            var success = DDC.GetChannelProperty(_SelfPtr, propertyName, result, (UIntPtr)length);
            TDMSErrorException.ThrowIfError(success, $"Failed to GetChannelProperty, Key:[{propertyName}]");
        }

        /// <inheritdoc />
        protected override DateTime GetPropertyTimestampComponents(string propertyName)
        {
            var success = DDC.GetChannelPropertyTimestampComponents(_SelfPtr,
                                                                propertyName,
                                                                out var year,
                                                                out var month,
                                                                out var day,
                                                                out var hour,
                                                                out var minute,
                                                                out var second,
                                                                out var milli,
                                                                out var weekDay);
            TDMSErrorException.ThrowIfError(success,
                                            $"Failed to GetChannelPropertyTimestampComponents, Key:[{propertyName}]");
            var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

            return dt.ToDateTime();
        }

        /// <inheritdoc />
        protected override void UpdatePropertyTimestampComponents(string propertyName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void CreatePropertyTimestampComponents(string propertyName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void CreateOrUpdateProperty<T>(string propertyName, T propertyValue)
        {
            throw new NotImplementedException();
        }

        /*
        /// <inheritdoc />
        public override (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            var success = DDC.GetChannelPropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");
            dataType = type;

            switch (type)
            {
                case TDMSDataType.String:
                    {
                        success = DDC.GetChannelStringPropertyLength(_SelfPtr, propertyName, out var length);
                        TDMSErrorException.ThrowIfError(success,
                                                        $"Failed to get Channel string property length, Key:[{propertyName}]");

                        if (length <= 0) //存在属性，但是值为空
                            return (true, string.Empty);

                        var ptr = Marshal.StringToHGlobalAnsi(new string(new char[length + 1]));

                        success = DDC.GetChannelProperty(_SelfPtr, propertyName, ptr, (UIntPtr)(length + 1));
                        TDMSErrorException.ThrowIfError(success, $"Failed to GetChannelProperty, Key:[{propertyName}]");

                        var result = Marshal.PtrToStringAnsi(ptr);

                        if (result == null)
                            return (false, null);

                        return (true, result.TrimEnd('\0'));
                    }
                case TDMSDataType.Timestamp:
                {
                    success = DDC.GetChannelPropertyTimestampComponents(_SelfPtr,
                                                                        propertyName,
                                                                        out var year,
                                                                        out var month,
                                                                        out var day,
                                                                        out var hour,
                                                                        out var minute,
                                                                        out var second,
                                                                        out var milli,
                                                                        out var weekDay);
                    TDMSErrorException.ThrowIfError(success,
                                                    $"Failed to GetChannelPropertyTimestampComponents, Key:[{propertyName}]");
                    var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

                    return (true, dt.ToDateTime());
                }
                case TDMSDataType.UInt8:
                    {
                        var result = Marshal.AllocHGlobal(sizeof(byte));
                        success = DDC.GetChannelProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                        TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                        byte value = Marshal.ReadByte(result);
                        Marshal.FreeHGlobal(result);

                        return (true, value);
                    }
                case TDMSDataType.Int16:
                    {
                        var result = Marshal.AllocHGlobal(sizeof(short));
                        success = DDC.GetChannelProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                        TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                        short value = Marshal.ReadInt16(result);
                        Marshal.FreeHGlobal(result);

                        return (true, value);
                    }
                case TDMSDataType.Int32:
                    {
                        var result = Marshal.AllocHGlobal(sizeof(int));
                        success = DDC.GetChannelProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                        TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                        int value = Marshal.ReadInt32(result);
                        Marshal.FreeHGlobal(result);

                        return (true, value);
                    }

                case TDMSDataType.Float:
                    {
                        var result = Marshal.AllocHGlobal(sizeof(float));
                        success = DDC.GetChannelProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                        TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                        float value = Marshal.PtrToStructure<float>(result);
                        Marshal.FreeHGlobal(result);

                        return (true, value);
                    }

                case TDMSDataType.Double:
                    {
                        var result = Marshal.AllocHGlobal(sizeof(double));
                        success = DDC.GetChannelProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                        TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                        double value = Marshal.PtrToStructure<double>(result);
                        Marshal.FreeHGlobal(result);

                        return (true, value);
                    }
                default: throw new ArgumentOutOfRangeException();
            }

            return (false, null);
        }
        */

        /// <inheritdoc />
        public override bool PropertyExists(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override string[] GetPropertyNames()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}