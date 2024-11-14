using System;
using System.Runtime.InteropServices;
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
                var       v    = data.GetValues();
                success = DDC.SetDataValues(ChannelPtr, v.Values, v.Length);
            }
            catch (Exception e)
            {
                if(success == -1)
                {
                    throw new TDMSErrorException("Failed to add data.", e);
                }
                else
                {
                    throw new TDMSErrorException(success, "Failed to add data.", e);
                }
            }

            return success == 0;
        }

        public T[] GetDataValues<T>(uint startIndex, uint length)
        {
            T[] values         = new T[length];

            // 获取数组的指针
            IntPtr valuesPtr = Marshal.UnsafeAddrOfPinnedArrayElement(values, 0);

            int result = DDC.GetDataValues(ChannelPtr, (UIntPtr)startIndex, (UIntPtr)length, valuesPtr);

            // 检查返回值并进行相应的处理
            if(result == 0)
            {
                // 执行成功
                // 在values数组中获取到了指定通道中的数据值
            }
            else
            {
                // 发生了错误
                // 根据返回值进行相应的错误处理
            }

            return values;
        }

        public ulong Count => DDC.CountDataValues(ChannelPtr, out var numValues) == 0 ? (ulong)numValues : 0;

        public void Dispose()
        {
            if(ChannelPtr != IntPtr.Zero)
            {
                DDC.CloseChannel(ChannelPtr);
                ChannelPtr = IntPtr.Zero;
            }
        }
    }
}