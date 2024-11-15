using System;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    class TDMSChannelGroup : ITDMSChannelGroup
    {
        public TDMSChannelGroup(IntPtr groupPtr)
        {
            SelfPtr = groupPtr;
        }

        public IntPtr SelfPtr { get; set; }

        public void Dispose()
        {
            if (SelfPtr != IntPtr.Zero)
            {
                DDC.CloseChannelGroup(SelfPtr);
                SelfPtr = IntPtr.Zero;
            }
            GC.SuppressFinalize(this);
        }

        public ITDMSChannel AddChannel(TDMSDataType dataType, string channelName, string unit, string description)
        {
            var success = DDC.AddChannel(SelfPtr, dataType, channelName, description, unit, out var channelPtr);

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
        public ulong ChildCount => DDC.CountChannels(SelfPtr, out var count) == 0 ? (ulong)count : 0;

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