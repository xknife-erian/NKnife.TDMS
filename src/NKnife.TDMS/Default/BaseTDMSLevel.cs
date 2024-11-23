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

        public PropertyOperator PropertyOperator { get; protected internal set; }

        #region Implementation of ITDMSLevelPropertyOperation
        /// <inheritdoc />
        public virtual void CreateOrUpdateProperty<T>(string propertyName, T propertyValue)
        {
            if (!PropertyExists(propertyName))
                CreateProperty(propertyName, propertyValue);
            else
                UpdateProperty(propertyName, propertyValue);
        }

        protected virtual void CreateProperty<T>(string propertyName, T propertyValue)
        {
            int success;

            switch (propertyValue)
            {
                case string stringValue:
                    PropertyOperator.CreateStringValue(propertyName, stringValue); break;
                case byte byteValue:
                    PropertyOperator.CreateByteValue(propertyName, byteValue); break;
                case short shortValue:
                    PropertyOperator.CreateShortValue(propertyName, shortValue); break;
                case int intValue:
                    PropertyOperator.CreateIntValue(propertyName, intValue); break;
                case float floatValue:
                    PropertyOperator.CreateFloatValue(propertyName, floatValue); break;
                case double doubleValue:
                    PropertyOperator.CreateDoubleValue(propertyName, doubleValue); break;
                case DateTime dateTimeValue:
                    PropertyOperator.CreateDateTimeValue(propertyName, dateTimeValue); break;
                default:
                    throw new ArgumentException("Unsupported property value type");
            }
        }

        protected virtual void UpdateProperty<T>(string propertyName, T propertyValue)
        {
            int success;

            switch (propertyValue)
            {
                case string stringValue:
                    PropertyOperator.SetStringValue(propertyName, stringValue); break;
                case byte byteValue:
                    PropertyOperator.SetByteValue(propertyName, byteValue); break;
                case short shortValue:
                    PropertyOperator.SetShortValue(propertyName, shortValue); break;
                case int intValue:
                    PropertyOperator.SetIntValue(propertyName, intValue); break;
                case float floatValue:
                    PropertyOperator.SetFloatValue(propertyName, floatValue); break;
                case double doubleValue:
                    PropertyOperator.SetDoubleValue(propertyName, doubleValue); break;
                case DateTime dateTimeValue:
                    PropertyOperator.SetDateTimeValue(propertyName, dateTimeValue); break;
                default:
                    throw new ArgumentException("Unsupported property value type");
            }
        }

        /// <inheritdoc />
        public virtual bool PropertyExists(string propertyName)
        {
            return PropertyOperator.Exists(propertyName);
        }

        /// <inheritdoc />
        public string[] GetPropertyNames()
        {
            return PropertyOperator.GetPropertyNames();
        }

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
                    propertyValue = (T)(object)PropertyOperator.GetStringValue(propertyName);
                    return true;
                }
                case TDMSDataType.UInt8:
                {
                    propertyValue = (T)(object)PropertyOperator.GetByteValue(propertyName);
                    return true;
                }
                case TDMSDataType.Int16:
                {
                    propertyValue = (T)(object)PropertyOperator.GetShortValue(propertyName);
                    return true;
                }
                case TDMSDataType.Int32:
                {
                    propertyValue = (T)(object)PropertyOperator.GetIntValue(propertyName);
                    return true;
                }
                case TDMSDataType.Float:
                {
                    propertyValue = (T)(object)PropertyOperator.GetFloatValue(propertyName);
                    return true;
                }
                case TDMSDataType.Double:
                {
                    propertyValue = (T)(object)PropertyOperator.GetDoubleValue(propertyName);
                    return true;
                }
                case TDMSDataType.Timestamp:
                {
                    propertyValue = (T)(object)PropertyOperator.GetDateTimeValue(propertyName);
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
            PropertyOperator.Dispose();
            if(_SelfPtr != IntPtr.Zero)
            {
                _IsClosed = Close();
                _SelfPtr  = IntPtr.Zero;
            }

            if(disposing)
            {
                // 释放托管资源
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}