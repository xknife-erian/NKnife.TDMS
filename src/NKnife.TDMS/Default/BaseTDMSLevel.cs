using System;
using System.ComponentModel;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

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

            var success = DDC.GetFilePropertyType(_SelfPtr, propertyName, out var srcDataType);
            TDMSErrorException.ThrowIfError(success, $"Failed to get property type, Key:[{propertyName}]");

            var type = typeof(T).ToDataType();

            if(srcDataType != type)
            {
                var e = new InvalidCastException($"The data type of the property '{propertyName}' is not '{type}', is '{srcDataType}'.");

                throw new TDMSErrorException($"{type} is error input arg", e);
            }

            switch (type)
            {
                case TDMSDataType.String:
                {
                    success = DDC.GetFileStringPropertyLength(_SelfPtr, propertyName, out var length);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get file string property length, Key:[{propertyName}]");

                    var charArray = new char[length];
                    success = DDC.GetFilePropertyString(_SelfPtr, propertyName, charArray, (UIntPtr)length);
                    TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyString, Key:[{propertyName}]");

                    propertyValue = (T)(object)new string(charArray).TrimEnd('\0');

                    return true;
                }
                case TDMSDataType.UInt8:
                {
                    success = DDC.GetFilePropertyUInt8(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    propertyValue = (T)(object)value;

                    return true;
                }
                case TDMSDataType.Int16:
                {
                    success = DDC.GetFilePropertyInt16(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    propertyValue = (T)(object)value;

                    return true;
                }
                case TDMSDataType.Int32:
                {
                    success = DDC.GetFilePropertyInt32(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    propertyValue = (T)(object)value;

                    return true;
                }
                case TDMSDataType.Float:
                {
                    success = DDC.GetFilePropertyFloat(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    propertyValue = (T)(object)value;

                    return true;
                }
                case TDMSDataType.Double:
                {
                    success = DDC.GetFilePropertyDouble(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    propertyValue = (T)(object)value;

                    return true;
                }
                case TDMSDataType.Timestamp:
                {
                    success = DDC.GetFilePropertyTimestampComponents(_SelfPtr,
                                                                     propertyName,
                                                                     out var year,
                                                                     out var month,
                                                                     out var day,
                                                                     out var hour,
                                                                     out var minute,
                                                                     out var second,
                                                                     out var milli,
                                                                     out var weekDay);
                    TDMSErrorException.ThrowIfError(success,
                                                    $"Failed to GetFilePropertyTimestampComponents, Key:[{propertyName}]");
                    var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

                    propertyValue = (T)(object)dt.ToDateTime();

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