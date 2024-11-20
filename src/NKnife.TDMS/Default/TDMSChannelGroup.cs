using System;
using System.Runtime.InteropServices;
using System.Text;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    class TDMSChannelGroup : BaseTDMSLevel, ITDMSChannelGroup
    {
        public TDMSChannelGroup(IntPtr groupPtr)
        {
            _SelfPtr = groupPtr;
            SetNameAndDescription();
        }

        public ITDMSChannel AddChannel(TDMSDataType dataType, string channelName, string unit, string description)
        {
            var success = DDC.AddChannel(_SelfPtr, dataType, channelName, description, unit, out var channelPtr);

            if(success == (int)Error.NoError)
            {
                var channel = new TDMSChannel(channelPtr);

                return channel;
            }
            else
            {
                throw new TDMSErrorException(success, "Failed to add channel.");
            }
        }

        /// <inheritdoc />
        public ITDMSChannel this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITDMSChannel this[string groupName]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        #region Implementation of ITDMSLevel

        /// <inheritdoc />
        public override bool Close()
        {
            if (!_IsClosed)
            {
                var success = DDC.CloseChannelGroup(_SelfPtr);
                TDMSErrorException.ThrowIfError(success, $"Failed to CloseChannelGroup");
            }

            return _IsClosed = true;
        }

        /// <inheritdoc />
        public override ulong ChildCount => DDC.CountChannels(_SelfPtr, out var count) == 0 ? (ulong)count : 0;

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
            var success = DDC.GetChannelGroupPropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");

            return type;
        }

        /// <inheritdoc />
        protected override uint GetStringPropertyLength(string propertyName)
        {
            var success = DDC.GetChannelGroupStringPropertyLength(_SelfPtr, propertyName, out var length);
            TDMSErrorException.ThrowIfError(success,
                                            $"Failed to get ChannelGroup string property length, Key:[{propertyName}]");

            return length;
        }

        /// <inheritdoc />
        protected override void GetPropertyInternal(string propertyName, IntPtr result, uint length)
        {
            var success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, result, (UIntPtr)length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");
        }

        /// <inheritdoc />
        protected override DateTime GetPropertyTimestampComponents(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyTimestampComponents(_SelfPtr,
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
                                            $"Failed to GetChannelGroupPropertyTimestampComponents, Key:[{propertyName}]");
            var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);
            return dt.ToDateTime();
        }

        /// <inheritdoc />
        public override void AddOrUpdateProperty<T>(string propertyName, T propertyValue)
        {
            throw new NotImplementedException();
        }

        /*
        /// <inheritdoc />
        public override (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            var success = DDC.GetChannelGroupPropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");
            dataType = type;

            switch (type)
            {
                case TDMSDataType.String:
                {
                    success = DDC.GetChannelGroupStringPropertyLength(_SelfPtr, propertyName, out var length);
                    TDMSErrorException.ThrowIfError(success,
                                                    $"Failed to get ChannelGroup string property length, Key:[{propertyName}]");

                    if(length <= 0) //存在属性，但是值为空
                        return (true, string.Empty);

                    var ptr = Marshal.StringToHGlobalAnsi(new string(new char[length + 1]));

                    success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, ptr, (UIntPtr)(length + 1));
                    TDMSErrorException.ThrowIfError(success, $"Failed to GetChannelGroupProperty, Key:[{propertyName}]");

                    var result = Marshal.PtrToStringAnsi(ptr);

                    if(result == null)
                        return (false, null);

                    return (true, result.TrimEnd('\0'));
                }
                case TDMSDataType.Timestamp:
                {
                    success = DDC.GetChannelGroupPropertyTimestampComponents(_SelfPtr,
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
                                                    $"Failed to GetChannelGroupPropertyTimestampComponents, Key:[{propertyName}]");
                    var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

                    return (true, dt.ToDateTime());
                }
                case TDMSDataType.UInt8:
                {
                    var result = Marshal.AllocHGlobal(sizeof(byte));
                    success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    byte value = Marshal.ReadByte(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                case TDMSDataType.Int16:
                {
                    var result = Marshal.AllocHGlobal(sizeof(short));
                    success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    short value = Marshal.ReadInt16(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                case TDMSDataType.Int32:
                {
                    var result = Marshal.AllocHGlobal(sizeof(int));
                    success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    int value = Marshal.ReadInt32(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }

                case TDMSDataType.Float:
                {
                    var result = Marshal.AllocHGlobal(sizeof(float));
                    success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    float value = Marshal.PtrToStructure<float>(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }

                case TDMSDataType.Double:
                {
                    var result = Marshal.AllocHGlobal(sizeof(double));
                    success = DDC.GetChannelGroupProperty(_SelfPtr, propertyName, result, (UIntPtr)0);
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