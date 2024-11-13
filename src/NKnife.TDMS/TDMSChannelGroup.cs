using System;
using System.Collections.Generic;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public class TDMSChannelGroup : ITDMSChannelGroup
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
    }
}