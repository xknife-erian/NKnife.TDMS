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

        #region Implementation of ITDMSLevelPropertyOperation
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

        private string _name;
        private string _description;

        protected void SetNameAndDescription()
        {
            var result = GetProperty(Constants.DDC_LEVEL_NAME, out _);

            if(!result.Success)
                throw new TDMSErrorException("Failed to retrieve the default 'name' property.");

            _name = result.PropertyValue.ToString();

            result = GetProperty(Constants.DDC_LEVEL_DESCRIPTION, out _);

            if(!result.Success)
                throw new TDMSErrorException("Failed to retrieve the default 'description' property.");
            _description = result.PropertyValue.ToString();
        }

        /// <inheritdoc />
        public string Name => _name;

        /// <inheritdoc />
        public string Description => _description;

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
        protected bool _IsClosed = false;

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
        ///    手动关闭节点相关的资源。本方法会被<see cref="Dispose"/>方法调用。
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