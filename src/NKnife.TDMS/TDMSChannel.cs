using System;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public class TDMSChannel : ITDMSChannel
    {
        public TDMSChannel(IntPtr channelPtr)
        {
            ChannelPtr = channelPtr;
        }

        public IntPtr ChannelPtr { get; set; }

        public bool AddData<T>(T[] values) where T : struct
        {
            int success = -1;

            try
            {
                using var data = new Data<T>(values);
                var v = data.GetValues();
                success = DDC.SetDataValues(ChannelPtr, v.Values, v.Length);
            }
            catch (Exception ex)
            {
                if(success == -1)
                {
                    throw new TDMSErrorException("Failed to add data.",ex);
                }
                else
                {
                    throw new TDMSErrorException(success, "Failed to add data.", ex);
                }
            }

            return success == 0;
        }

        public void Dispose()
        {
            if (ChannelPtr != IntPtr.Zero)
            {
                DDC.CloseChannel(ChannelPtr);
                ChannelPtr = IntPtr.Zero;
            }
        }
    }
}