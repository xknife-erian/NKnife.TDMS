using System;
using System.Linq;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    internal class TDMSChannelGroup : BaseTDMSLevel, ITDMSChannelGroup
    {
        public TDMSChannelGroup(IntPtr groupPtr)
        {
            _SelfPtr         = groupPtr;
            PropertyOperator = new ChannelGroupPropertyOperator(groupPtr);
            SetNameAndDescription();
        }

        public ITDMSChannel AddChannel(TDMSDataType dataType,
                                       string channelName,
                                       string unit,
                                       string description = "")
        {
            var success = DDC.AddChannel(_SelfPtr, dataType, channelName, description, unit, out var channelPtr);

            if(success == (int)Error.NoError)
                return new TDMSChannel(channelPtr);

            throw new TDMSErrorException(success, "Failed to add channel.");
        }

        /// <inheritdoc />
        public ITDMSChannel this[int index]
        {
            get
            {
                if(index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");
                var channelsBuf = new IntPtr[ChildCount];
                var success     = DDC.GetChannels(_SelfPtr, channelsBuf, (UIntPtr)ChildCount);
                TDMSErrorException.ThrowIfError(success, "Failed to get channels");

                if(index >= channelsBuf.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index must be less than the number of channels");
                var groupPtr = channelsBuf[index];

                return new TDMSChannel(groupPtr);
            }
        }

        /// <inheritdoc />
        public ITDMSChannel this[string groupName]
        {
            get
            {
                if(string.IsNullOrEmpty(groupName))
                    throw new ArgumentNullException(nameof(groupName), "Group name cannot be null or empty");

                var channelsBuf = new IntPtr[ChildCount];
                var success     = DDC.GetChannels(_SelfPtr, channelsBuf, (UIntPtr)ChildCount);

                TDMSErrorException.ThrowIfError(success, "Failed to get channels");

                foreach (var intPtr in channelsBuf)
                {
                    var group = new TDMSChannel(intPtr);

                    if(group.Name == groupName)
                        return group;
                    group.Dispose();
                }

                return null;
            }
        }

        #region Implementation of ITDMSLevel
        /// <inheritdoc />
        public override bool Close()
        {
            if(!_IsClosed)
            {
                var success = DDC.CloseChannelGroup(_SelfPtr);
                TDMSErrorException.ThrowIfError(success, "Failed to CloseChannelGroup");
            }

            return _IsClosed = true;
        }

        /// <inheritdoc />
        public override ulong ChildCount => DDC.CountChannels(_SelfPtr, out var count) == 0 ? (ulong)count : 0;

        /// <inheritdoc />
        public override bool Clear()
        {
            var channelsBuf = new IntPtr[ChildCount];
            var success     = DDC.GetChannels(_SelfPtr, channelsBuf, (UIntPtr)ChildCount);
            TDMSErrorException.ThrowIfError(success, "Failed to get channels");

            foreach (var ptr in channelsBuf)
            {
                success = DDC.RemoveChannel(ptr);
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel");
            }

            return success == 0;
        }

        /// <inheritdoc />
        public override bool Contains(string channelName)
        {
            var count = ChildCount;

            if(count == 0)
                return false;

            var names = PropertyOperator.GetPropertyNames();

            return names.Any(name => name.Equals(channelName));
        }

        /// <inheritdoc />
        public override bool TryGetItem(string channelName, out ITDMSLevel level)
        {
            var has = Contains(channelName);

            if(has)
            {
                level = this[channelName];

                return true;
            }

            level = null;

            return false;
        }

        /// <inheritdoc />
        public override bool Remove(string channelName)
        {
            if(TryGetItem(channelName, out var channel)
               && channel is TDMSChannel @in)
            {
                var success = DDC.RemoveChannelGroup(@in.GetPtr());
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel");
            }

            return false;
        }

        /// <inheritdoc />
        public override bool RemoveAt(int index)
        {
            if(index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");
            if((ulong)index >= ChildCount)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be less than the number of channels");

            var channel = this[index];

            if(channel is TDMSChannel @in)
            {
                var success = DDC.RemoveChannel(@in.GetPtr());
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel group");
            }

            return false;
        }
        #endregion
    }
}