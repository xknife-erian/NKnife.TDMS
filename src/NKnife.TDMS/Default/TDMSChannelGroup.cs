using System;
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

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        #region Implementation of ITDMSNode
        /// <inheritdoc />
        public ulong ChildCount => DDC.CountChannels(_selfPtr, out var count) == 0 ? (ulong)count : 0;

        /// <inheritdoc />
        public void SetProperty(string propertyName, string propertyValue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            throw new NotImplementedException();
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