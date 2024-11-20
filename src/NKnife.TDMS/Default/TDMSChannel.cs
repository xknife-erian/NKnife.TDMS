﻿using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    internal class TDMSChannel : BaseTDMSLevel, ITDMSChannel
    {
        public TDMSChannel(IntPtr channelPtr)
        {
            _SelfPtr = channelPtr;
            SetNameAndDescription();
        }

        #region Implementation of ITDMSChannel
        public bool SetData<T>(T[] values) where T : struct
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
        public bool AppendData<T>(T[] values) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool UpdateData<T>(int index, T[] values) where T : struct
        {
            throw new NotImplementedException();
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
        public string Unit { get; set; }
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
        protected override bool ManualCloseNode()
        {
            return Close();
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
    }
}