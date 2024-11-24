using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    internal class TDMSChannel : BaseTDMSLevel, ITDMSChannel
    {
        private string _unit;

        public TDMSChannel(IntPtr channelPtr)
        {
            _SelfPtr         = channelPtr;
            PropertyOperator = new ChannelPropertyOperator(channelPtr);
            SetNameAndDescription();
            SetUnit();
        }

        private void SetUnit()
        {
            _unit = GetDefaultProperty(Constants.DDC_CHANNEL_UNIT_STRING);
        }

        #region Implementation of ITDMSChannel
        public bool SetData<T>(params T[] values)
        {
            var success = -1;

            try
            {
                using var data = new Data<T>(values);

                var v = data.GetValues();
                success = DDC.SetDataValues(_SelfPtr, v.Values, v.Length);

                throw new TDMSErrorException(success, "Failed to add data.");
            }
            catch (Exception e)
            {
                if(success == -1)
                    throw new TDMSErrorException("Failed to add data.", e);
            }

            return success == 0;
        }

        /// <inheritdoc />
        public bool AppendData<T>(params T[] values)
        {
            var success = -1;
            try
            {
                using var data = new Data<T>(values);

                var v = data.GetValues();
                success = DDC.AppendDataValues(_SelfPtr, v.Values, v.Length);

                throw new TDMSErrorException(success, "Failed to append data.");
            }
            catch (Exception e)
            {
                if (success == -1)
                    throw new TDMSErrorException("Failed to append data.", e);
            }

            return success == 0;
        }

        /// <inheritdoc />
        public bool UpdateData<T>(int index, params T[] values)
        {
            var success = -1;

            try
            {
                using var data = new Data<T>(values);

                var v = data.GetValues();
                success = DDC.ReplaceDataValues(_SelfPtr, (UIntPtr)index, v.Values, v.Length);

                throw new TDMSErrorException(success, "Failed to update data.");
            }
            catch (Exception e)
            {
                if (success == -1)
                    throw new TDMSErrorException("Failed to update data.", e);
            }

            return success == 0;
        }

        /// <inheritdoc />
        public T[] GetDataValues<T>(uint startIndex, uint length)
        {
            var values = new T[length];

            // 获取数组的指针
            var valuesPtr = Marshal.UnsafeAddrOfPinnedArrayElement(values, 0);

            var success = DDC.GetDataValues(_SelfPtr, (UIntPtr)startIndex, (UIntPtr)length, valuesPtr);
            TDMSErrorException.ThrowIfError(success, "Failed to GetDataValues.");

            return values;
        }

        /// <inheritdoc />
        public string Unit => _unit;
        #endregion

        #region Implementation of ITDMSLevel
        /// <inheritdoc />
        public override bool Close()
        {
            if(!_IsClosed)
            {
                var success = DDC.CloseChannel(_SelfPtr);
                TDMSErrorException.ThrowIfError(success, "Failed to CloseChannel.");
            }

            return _IsClosed = true;
        }

        /// <inheritdoc />
        public override ulong ChildCount => DDC.CountDataValues(_SelfPtr, out var numValues) == 0 ? numValues : 0;


        /// <inheritdoc />
        public override bool Clear()
        {
            return false;
        }

        /// <inheritdoc />
        public override bool Contains(string levelName)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool TryGetItem(string levelName, out ITDMSLevel level)
        {
            level = null;
            return false;
        }

        /// <inheritdoc />
        public override bool Remove(string levelName)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool RemoveAt(int index)
        {
            return false;
        }
        #endregion
    }
}