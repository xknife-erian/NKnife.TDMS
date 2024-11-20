using System;
using System.Collections;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    class TDMSChannel : BaseTDMSLevel, ITDMSChannel
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

        #region Implementation of ITDMSLevel
        /// <inheritdoc />
        public override string Name { get; }

        /// <inheritdoc />
        public override string Description { get; }

        /// <inheritdoc />
        public override bool Close()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override ulong ChildCount => DDC.CountDataValues(ChannelPtr, out var numValues) == 0 ? (ulong)numValues : 0;

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
        protected override void ManualCloseNode()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void AddOrUpdateProperty<T>(string propertyName, T propertyValue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            throw new NotImplementedException();
        }

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
        #region Implementation of IEnumerable
        /// <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}