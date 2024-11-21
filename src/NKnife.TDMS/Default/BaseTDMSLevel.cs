﻿using System;
using System.ComponentModel;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Default
{
    internal abstract class BaseTDMSLevel : ITDMSLevel
    {
        protected IntPtr _SelfPtr;

        internal IntPtr GetPtr()
        {
            return _SelfPtr;
        }

        #region protected abstract
        protected abstract TDMSDataType GetPropertyType(string propertyName);

        protected abstract void UpdateProperty<T>(string propertyName, T value);
        protected abstract void CreateProperty<T>(string propertyName, T value);
        protected abstract T GetProperty<T>(string propertyName);

        protected abstract DateTime GetPropertyTimestampComponents(string propertyName);
        protected abstract void UpdatePropertyTimestampComponents(string propertyName, DateTime dateTime);
        protected abstract void CreatePropertyTimestampComponents(string propertyName, DateTime dateTime);
        #endregion

        #region Implementation of ITDMSLevelPropertyOperation
        /// <inheritdoc />
        public abstract void CreateOrUpdateProperty<T>(string propertyName, T propertyValue);

        /// <inheritdoc />
        public abstract bool PropertyExists(string propertyName);

        /// <inheritdoc />
        public abstract string[] GetPropertyNames();

        /// <inheritdoc />
        public virtual bool TryGetProperty<T>(string propertyName, out T propertyValue)
        {
            if(!PropertyExists(propertyName))
            {
                propertyValue = default;
                return false;
            }

            var dataType = typeof(T).ToDataType();

            switch (dataType)
            {
                case TDMSDataType.Timestamp:
                {
                    var dateTime = GetPropertyTimestampComponents(propertyName);
                    propertyValue = (T)Convert.ChangeType(dateTime, typeof(T));

                    return true;
                }
                case TDMSDataType.String:
                case TDMSDataType.UInt8:
                case TDMSDataType.Int16:
                case TDMSDataType.Int32:
                case TDMSDataType.Double:
                case TDMSDataType.Float:
                {
                    propertyValue = GetProperty<T>(propertyName);
                    return true;
                }
                case TDMSDataType.UnDefine:
                default:
                    throw new InvalidEnumArgumentException();
            }

            return false;
        }
        #endregion

        #region Implementation of ITDMSLevel
        protected void SetNameAndDescription()
        {
            Name        = GetDefaultProperty(Constants.DDC_LEVEL_NAME);
            Description = GetDefaultProperty(Constants.DDC_LEVEL_DESCRIPTION);
        }

        /// <summary> 快速获取层级的默认属性值 </summary>
        protected string GetDefaultProperty(string propertyName)
        {
            var has = TryGetProperty<string>(propertyName, out var propertyValue);

            if(!has) throw new TDMSErrorException($"Failed to retrieve the default '{propertyName}' property.");

            return propertyValue;
        }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public string Description { get; private set; }

        /// <inheritdoc />
        public abstract bool Close();

        /// <inheritdoc />
        public abstract ulong ChildCount { get; }

        /// <inheritdoc />
        public abstract bool Clear();

        /// <inheritdoc />
        public abstract bool Contains(string levelName);

        /// <inheritdoc />
        public abstract bool TryGetItem(string levelName, out ITDMSLevel level);

        /// <inheritdoc />
        public abstract bool Remove(string levelName);

        /// <inheritdoc />
        public abstract bool RemoveAt(int index);
        #endregion

        #region Implementation of IDisposable
        protected bool _IsClosed;

        ~BaseTDMSLevel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(_SelfPtr != IntPtr.Zero)
            {
                _IsClosed = ManualCloseNode();
                _SelfPtr  = IntPtr.Zero;
            }

            if(disposing)
            {
                // 释放托管资源
            }
        }

        /// <summary>
        ///     手动关闭节点相关的资源。本方法会被<see cref="Dispose" />方法调用。
        /// </summary>
        protected abstract bool ManualCloseNode();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}