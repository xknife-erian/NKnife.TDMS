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


                    var source = new string(new char[length]);
                    var ptr    = Marshal.StringToHGlobalAnsi(source);

                    success = DDC.GetChannelGroupProperty(_selfPtr, propertyName, ptr, (UIntPtr)length);
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