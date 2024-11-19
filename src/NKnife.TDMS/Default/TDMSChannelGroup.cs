using System;
using System.Runtime.InteropServices;
using System.Text;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    class TDMSChannelGroup : ITDMSChannelGroup
    {
        public TDMSChannelGroup(IntPtr groupPtr)
        {
            _selfPtr = groupPtr;
        }

        private IntPtr _selfPtr;

        internal IntPtr GetPtr()
        {
            return _selfPtr;
        }

        public void Dispose()
        {
            if (_selfPtr != IntPtr.Zero)
            {
                DDC.CloseChannelGroup(_selfPtr);
                _selfPtr = IntPtr.Zero;
            }
            GC.SuppressFinalize(this);
        }

        public ITDMSChannel AddChannel(TDMSDataType dataType, string channelName, string unit, string description)
        {
            var success = DDC.AddChannel(_selfPtr, dataType, channelName, description, unit, out var channelPtr);

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

        public ITDMSChannel GetChannel(int i)
        {
            throw new NotImplementedException();
        }

        #region Implementation of ITDMSNode
        /// <inheritdoc />
        public string Name
        {
            get
            {
                var name = GetProperty(Constants.DDC_CHANNEL_GROUP_NAME, out _);
                if (!name.Success)
                    throw new TDMSErrorException("Failed to retrieve the default 'name' property.");

                return name.PropertyValue.ToString();
            }
        }

        /// <inheritdoc />
        public string Description
        {
            get
            {
                var desc = GetProperty(Constants.DDC_CHANNEL_GROUP_DESCRIPTION, out _);
                if (!desc.Success)
                    throw new TDMSErrorException("Failed to retrieve the default 'description' property.");

                return desc.PropertyValue.ToString();
            }
        }

        /// <inheritdoc />
        public ulong ChildCount => DDC.CountChannels(_selfPtr, out var count) == 0 ? (ulong)count : 0;

        /// <inheritdoc />
        public bool Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void AddOrUpdateProperty<T>(string propertyName, T propertyValue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            var success = DDC.GetChannelGroupPropertyType(_selfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");
            dataType = type;

            switch (type)
            {
                case TDMSDataType.String:
                {
                    success = DDC.GetChannelGroupStringPropertyLength(_selfPtr, propertyName, out var length);
                    TDMSErrorException.ThrowIfError(success,
                                                    $"Failed to get ChannelGroup string property length, Key:[{propertyName}]");

                    if(length <= 0) //存在属性，但是值为空
                        return (true, string.Empty);

                    var ptr = Marshal.StringToHGlobalAnsi(new string(new char[length + 1]));

                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, ptr, (UIntPtr)(length + 1));
                    TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyString, Key:[{propertyName}]");

                    var result = Marshal.PtrToStringAnsi(ptr);

                    if(result == null)
                        return (false, null);

                    TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyString, Key:[{propertyName}]");

                    return (true, result.TrimEnd('\0'));
                }
                case TDMSDataType.Timestamp:
                {
                    success = DDC.GetChannelGroupPropertyTimestampComponents(_selfPtr,
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
                                                    $"Failed to GetFilePropertyTimestampComponents, Key:[{propertyName}]");
                    var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

                    return (true, dt.ToDateTime());
                }
                case TDMSDataType.UInt8:
                {
                    var result = Marshal.AllocHGlobal(sizeof(byte));
                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    byte value = Marshal.ReadByte(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                case TDMSDataType.Int16:
                {
                    var result = Marshal.AllocHGlobal(sizeof(short));
                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    short value = Marshal.ReadInt16(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                case TDMSDataType.Int32:
                {
                    var result = Marshal.AllocHGlobal(sizeof(int));
                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    int value = Marshal.ReadInt32(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }

                case TDMSDataType.Float:
                {
                    var result = Marshal.AllocHGlobal(sizeof(float));
                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    float value = Marshal.PtrToStructure<float>(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }

                case TDMSDataType.Double:
                {
                    var result = Marshal.AllocHGlobal(sizeof(double));
                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, result, (UIntPtr)0);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    double value = Marshal.PtrToStructure<double>(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                default: throw new ArgumentOutOfRangeException();
            }

            return (false, null);
        }

        /// <inheritdoc />
        public bool PropertyExists(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string[] GetPropertyNames()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}