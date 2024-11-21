using System;
using System.Runtime.InteropServices;
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
        public virtual bool TryGetProperty<T>(string propertyName, out T propertyValue, out TDMSDataType dataType)
        {
            var has = PropertyExists(propertyName);

            if(has)
            {
                propertyValue = default(T);
                dataType      = TDMSDataType.UnDefine;

                return false;
            }

            dataType = GetPropertyType(propertyName);

            switch (dataType)
            {
                case TDMSDataType.String:
                {
                    var result = GetPropertyInternal<T>(propertyName);
                    propertyValue = result;

                    return true;

                }
                case TDMSDataType.Timestamp:
                {
                    var dateTime = GetPropertyTimestampComponents(propertyName);

                    return (true, dateTime);
                }
                case TDMSDataType.UInt8:
                {
                    var result = Marshal.AllocHGlobal(sizeof(byte));
                    GetPropertyInternal(propertyName, result, 0);

                    byte value = Marshal.ReadByte(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                case TDMSDataType.Int16:
                {
                    var result = Marshal.AllocHGlobal(sizeof(short));
                    GetPropertyInternal(propertyName, result, 0);

                    short value = Marshal.ReadInt16(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                case TDMSDataType.Int32:
                {
                    var result = Marshal.AllocHGlobal(sizeof(int));
                    GetPropertyInternal(propertyName, result, 0);

                    int value = Marshal.ReadInt32(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }

                case TDMSDataType.Float:
                {
                    var result = Marshal.AllocHGlobal(sizeof(float));
                    GetPropertyInternal(propertyName, result, 0);

                    float value = Marshal.PtrToStructure<float>(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }

                case TDMSDataType.Double:
                {
                    var result = Marshal.AllocHGlobal(sizeof(double));
                    GetPropertyInternal(propertyName, result, 0);

                    double value = Marshal.PtrToStructure<double>(result);
                    Marshal.FreeHGlobal(result);

                    return (true, value);
                }
                default: throw new ArgumentOutOfRangeException();
            }

            return (false, null);
        }
        #endregion

        #region Implementation of ITDMSLevel

        private string _name;
        private string _description;

        protected void SetNameAndDescription()
        {
            _name = GetDefaultProperty(Constants.DDC_LEVEL_NAME);
            _description = GetDefaultProperty(Constants.DDC_LEVEL_DESCRIPTION);
        }

        /// <summary>
        ///    快速获取层级的默认属性值
        /// </summary>
        protected string GetDefaultProperty(string propertyName)
        {
            var propertyResult = TryGetProperty(propertyName, out TODO, out _);

            if(!propertyResult.Success)
                throw new TDMSErrorException($"Failed to retrieve the default '{propertyName}' property.");
            return propertyResult.PropertyValue.ToString();
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

        protected abstract TDMSDataType GetPropertyType(string propertyName);

        protected abstract T GetPropertyInternal<T>(string propertyName);
        protected abstract void UpdatePropertyInternal<T>(string propertyName, T value);
        protected abstract void CreatePropertyInternal<T>(string propertyName, T value);

        protected abstract DateTime GetPropertyTimestampComponents(string propertyName);
        protected abstract void UpdatePropertyTimestampComponents(string propertyName, DateTime dateTime);
        protected abstract void CreatePropertyTimestampComponents(string propertyName, DateTime dateTime);
    }
}