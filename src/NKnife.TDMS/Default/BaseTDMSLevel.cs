using System;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    abstract class BaseTDMSLevel : ITDMSLevel
    {
        protected IntPtr _SelfPtr;

        internal IntPtr GetPtr()
        {
            return _SelfPtr;
        }

        #region Implementation of ITDMSNodePropertyOperation
        /// <inheritdoc />
        public abstract void AddOrUpdateProperty<T>(string propertyName, T propertyValue);

        /// <inheritdoc />
        public abstract bool PropertyExists(string propertyName);

        /// <inheritdoc />
        public abstract string[] GetPropertyNames();

        /// <inheritdoc />
        public abstract (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType);
        #endregion

        #region Implementation of ITDMSLevel
        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public abstract string Description { get; }

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
        ~BaseTDMSLevel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_SelfPtr != IntPtr.Zero)
            {
                ManualCloseNode();
                _SelfPtr = IntPtr.Zero;
            }

            if (disposing)
            {
                // 释放托管资源
            }
        }

        /// <summary>
        ///    手动关闭节点相关的资源。
        /// </summary>
        protected abstract void ManualCloseNode();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}