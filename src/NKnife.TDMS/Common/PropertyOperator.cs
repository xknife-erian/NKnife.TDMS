using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Common
{
    internal class ChannelPropertyOperator(IntPtr selfPtr) : PropertyOperator(selfPtr)
    {
        public override TDMSDataType GetDataType(string propertyName)
        {
            var success = DDC.GetChannelPropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, $"Failed to get property type, Key:[{propertyName}]");

            return type;
        }

        public override bool Exists(string propertyName)
        {
            var success = DDC.ChannelPropertyExists(_SelfPtr, propertyName, out var exists);
            TDMSErrorException.ThrowIfError(success, $"Failed to check property exists, Key:[{propertyName}]");

            return exists == 1;
        }

        public override uint GetStringLength(string propertyName)
        {
            var success = DDC.GetChannelStringPropertyLength(_SelfPtr, propertyName, out var length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property length, Key:[{propertyName}]");

            return length;
        }

        public override string[] GetPropertyNames()
        {
            var success = DDC.CountChannelProperties(_SelfPtr, out var count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property count");

            var names = new IntPtr[count];
            success = DDC.GetChannelPropertyNames(_SelfPtr, names, (UIntPtr)count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property names");

            var result = new string[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = Marshal.PtrToStringAnsi(names[i]);
            }

            return result;
        }

        protected override int DDCGetStringValue(string propertyName, char[] chars, uint length)
        {
            var success = DDC.GetChannelPropertyString(_SelfPtr, propertyName, chars, (UIntPtr)length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property value, Key:[{propertyName}]");
            return success;
        }

        public override byte GetByteValue(string propertyName)
        {
            var success = DDC.GetChannelPropertyUInt8(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get byte property value, Key:[{propertyName}]");

            return value;
        }

        public override short GetShortValue(string propertyName)
        {
            var success = DDC.GetChannelPropertyInt16(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get short property value, Key:[{propertyName}]");

            return value;
        }

        public override int GetIntValue(string propertyName)
        {
            var success = DDC.GetChannelPropertyInt32(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get int property value, Key:[{propertyName}]");

            return value;
        }

        public override float GetFloatValue(string propertyName)
        {
            var success = DDC.GetChannelPropertyFloat(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get float property value, Key:[{propertyName}]");

            return value;
        }

        public override double GetDoubleValue(string propertyName)
        {
            var success = DDC.GetChannelPropertyDouble(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get double property value, Key:[{propertyName}]");

            return value;
        }

        public override DateTime GetDateTimeValue(string propertyName)
        {
            var success = DDC.GetChannelPropertyTimestampComponents(_SelfPtr,
                                                                    propertyName,
                                                                    out var year,
                                                                    out var month,
                                                                    out var day,
                                                                    out var hour,
                                                                    out var minute,
                                                                    out var second,
                                                                    out var milliSecond,
                                                                    out var weekDay);
            TDMSErrorException.ThrowIfError(success, $"Failed to get datetime property value, Key:[{propertyName}]");

            return new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second, (int)milliSecond);
        }

        public override void SetStringValue(string propertyName, string propertyValue)
        {
            var success = DDC.SetChannelPropertyString(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set string property value, Key:[{propertyName}]");
        }

        public override void SetByteValue(string propertyName, byte propertyValue)
        {
            var success = DDC.SetChannelPropertyUInt8(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set byte property value, Key:[{propertyName}]");
        }

        public override void SetShortValue(string propertyName, short propertyValue)
        {
            var success = DDC.SetChannelPropertyInt16(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set short property value, Key:[{propertyName}]");
        }

        public override void SetIntValue(string propertyName, int propertyValue)
        {
            var success = DDC.SetChannelPropertyInt32(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set int property value, Key:[{propertyName}]");
        }

        public override void SetFloatValue(string propertyName, float propertyValue)
        {
            var success = DDC.SetChannelPropertyFloat(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set float property value, Key:[{propertyName}]");
        }

        public override void SetDoubleValue(string propertyName, double propertyValue)
        {
            var success = DDC.SetChannelPropertyDouble(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set double property value, Key:[{propertyName}]");
        }

        public override void SetDateTimeValue(string propertyName, DateTime propertyValue)
        {
            var success = DDC.SetChannelPropertyTimestampComponents(_SelfPtr,
                                                                    propertyName,
                                                                    (uint)propertyValue.Year,
                                                                    (uint)propertyValue.Month,
                                                                    (uint)propertyValue.Day,
                                                                    (uint)propertyValue.Hour,
                                                                    (uint)propertyValue.Minute,
                                                                    (uint)propertyValue.Second,
                                                                    propertyValue.Millisecond);
            TDMSErrorException.ThrowIfError(success, $"Failed to set datetime property value, Key:[{propertyName}]");
        }

        public override void CreateStringValue(string propertyName, string propertyValue)
        {
            var success = DDC.CreateChannelPropertyString(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create string property value, Key:[{propertyName}]");
        }

        public override void CreateByteValue(string propertyName, byte propertyValue)
        {
            var success = DDC.CreateChannelPropertyUInt8(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create byte property value, Key:[{propertyName}]");
        }

        public override void CreateShortValue(string propertyName, short propertyValue)
        {
            var success = DDC.CreateChannelPropertyInt16(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create short property value, Key:[{propertyName}]");
        }

        public override void CreateIntValue(string propertyName, int propertyValue)
        {
            var success = DDC.CreateChannelPropertyInt32(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create int property value, Key:[{propertyName}]");
        }

        public override void CreateFloatValue(string propertyName, float propertyValue)
        {
            var success = DDC.CreateChannelPropertyFloat(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create float property value, Key:[{propertyName}]");
        }

        public override void CreateDoubleValue(string propertyName, double propertyValue)
        {
            var success = DDC.CreateChannelPropertyDouble(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create double property value, Key:[{propertyName}]");
        }

        public override void CreateDateTimeValue(string propertyName, DateTime propertyValue)
        {
            var success = DDC.CreateChannelPropertyTimestampComponents(_SelfPtr,
                                                                       propertyName,
                                                                       (uint)propertyValue.Year,
                                                                       (uint)propertyValue.Month,
                                                                       (uint)propertyValue.Day,
                                                                       (uint)propertyValue.Hour,
                                                                       (uint)propertyValue.Minute,
                                                                       (uint)propertyValue.Second,
                                                                       propertyValue.Millisecond);
            TDMSErrorException.ThrowIfError(success, $"Failed to create datetime property value, Key:[{propertyName}]");
        }
    }

    internal class ChannelGroupPropertyOperator(IntPtr selfPtr) : PropertyOperator(selfPtr)
    {
        public override TDMSDataType GetDataType(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, $"Failed to get property type, Key:[{propertyName}]");

            return type;
        }

        public override bool Exists(string propertyName)
        {
            var success = DDC.ChannelGroupPropertyExists(_SelfPtr, propertyName, out var exists);
            TDMSErrorException.ThrowIfError(success, $"Failed to check property exists, Key:[{propertyName}]");

            return exists == 1;
        }

        public override uint GetStringLength(string propertyName)
        {
            var success = DDC.GetChannelGroupStringPropertyLength(_SelfPtr, propertyName, out var length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property length, Key:[{propertyName}]");

            return length;
        }

        public override string[] GetPropertyNames()
        {
            var success = DDC.CountChannelGroupProperties(_SelfPtr, out var count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property count");

            var names = new IntPtr[count];
            success = DDC.GetChannelGroupPropertyNames(_SelfPtr, names, (UIntPtr)count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property names");

            var result = new string[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = Marshal.PtrToStringAnsi(names[i]);
            }

            return result;
        }

        protected override int DDCGetStringValue(string propertyName, char[] chars, uint length)
        {
            var success = DDC.GetChannelGroupPropertyString(_SelfPtr, propertyName, chars, (UIntPtr)length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property value, Key:[{propertyName}]");
            return success;
        }

        public override byte GetByteValue(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyUInt8(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get byte property value, Key:[{propertyName}]");

            return value;
        }

        public override short GetShortValue(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyInt16(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get short property value, Key:[{propertyName}]");

            return value;
        }

        public override int GetIntValue(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyInt32(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get int property value, Key:[{propertyName}]");

            return value;
        }

        public override float GetFloatValue(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyFloat(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get float property value, Key:[{propertyName}]");

            return value;
        }

        public override double GetDoubleValue(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyDouble(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get double property value, Key:[{propertyName}]");

            return value;
        }

        public override DateTime GetDateTimeValue(string propertyName)
        {
            var success = DDC.GetChannelGroupPropertyTimestampComponents(_SelfPtr,
                                                                         propertyName,
                                                                         out var year,
                                                                         out var month,
                                                                         out var day,
                                                                         out var hour,
                                                                         out var minute,
                                                                         out var second,
                                                                         out var milliSecond,
                                                                         out var weekDay);
            TDMSErrorException.ThrowIfError(success, $"Failed to get datetime property value, Key:[{propertyName}]");

            return new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second, (int)milliSecond);
        }

        public override void SetStringValue(string propertyName, string propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyString(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set string property value, Key:[{propertyName}]");
        }

        public override void SetByteValue(string propertyName, byte propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyUInt8(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set byte property value, Key:[{propertyName}]");
        }

        public override void SetShortValue(string propertyName, short propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyInt16(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set short property value, Key:[{propertyName}]");
        }

        public override void SetIntValue(string propertyName, int propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyInt32(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set int property value, Key:[{propertyName}]");
        }

        public override void SetFloatValue(string propertyName, float propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyFloat(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set float property value, Key:[{propertyName}]");
        }

        public override void SetDoubleValue(string propertyName, double propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyDouble(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set double property value, Key:[{propertyName}]");
        }

        public override void SetDateTimeValue(string propertyName, DateTime propertyValue)
        {
            var success = DDC.SetChannelGroupPropertyTimestampComponents(_SelfPtr,
                                                                         propertyName,
                                                                         (uint)propertyValue.Year,
                                                                         (uint)propertyValue.Month,
                                                                         (uint)propertyValue.Day,
                                                                         (uint)propertyValue.Hour,
                                                                         (uint)propertyValue.Minute,
                                                                         (uint)propertyValue.Second,
                                                                         propertyValue.Millisecond);
            TDMSErrorException.ThrowIfError(success, $"Failed to set datetime property value, Key:[{propertyName}]");
        }

        public override void CreateStringValue(string propertyName, string propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyString(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create string property value, Key:[{propertyName}]");
        }

        public override void CreateByteValue(string propertyName, byte propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyUInt8(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create byte property value, Key:[{propertyName}]");
        }

        public override void CreateShortValue(string propertyName, short propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyInt16(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create short property value, Key:[{propertyName}]");
        }

        public override void CreateIntValue(string propertyName, int propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyInt32(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create int property value, Key:[{propertyName}]");
        }

        public override void CreateFloatValue(string propertyName, float propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyFloat(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create float property value, Key:[{propertyName}]");
        }

        public override void CreateDoubleValue(string propertyName, double propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyDouble(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create double property value, Key:[{propertyName}]");
        }

        public override void CreateDateTimeValue(string propertyName, DateTime propertyValue)
        {
            var success = DDC.CreateChannelGroupPropertyTimestampComponents(_SelfPtr,
                                                                            propertyName,
                                                                            (uint)propertyValue.Year,
                                                                            (uint)propertyValue.Month,
                                                                            (uint)propertyValue.Day,
                                                                            (uint)propertyValue.Hour,
                                                                            (uint)propertyValue.Minute,
                                                                            (uint)propertyValue.Second,
                                                                            propertyValue.Millisecond);
            TDMSErrorException.ThrowIfError(success, $"Failed to create datetime property value, Key:[{propertyName}]");
        }
    }

    internal class FilePropertyOperator(IntPtr selfPtr) : PropertyOperator(selfPtr)
    {
        public override TDMSDataType GetDataType(string propertyName)
        {
            var success = DDC.GetFilePropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, $"Failed to get property type, Key:[{propertyName}]");

            return type;
        }

        public override bool Exists(string propertyName)
        {
            var success = DDC.FilePropertyExists(_SelfPtr, propertyName, out var exists);
            TDMSErrorException.ThrowIfError(success, $"Failed to check property exists, Key:[{propertyName}]");

            return exists == 1;
        }

        public override uint GetStringLength(string propertyName)
        {
            var success = DDC.GetFileStringPropertyLength(_SelfPtr, propertyName, out var length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property length, Key:[{propertyName}]");

            return length;
        }

        public override string[] GetPropertyNames()
        {
            var success = DDC.CountFileProperties(_SelfPtr, out var count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property count");

            var names = new IntPtr[count];
            success = DDC.GetFilePropertyNames(_SelfPtr, names, (UIntPtr)count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property names");

            var result = new string[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = Marshal.PtrToStringAnsi(names[i]);
            }

            return result;
        }

        protected override int DDCGetStringValue(string propertyName, char[] chars, uint length)
        {
            var success = DDC.GetFilePropertyString(_SelfPtr, propertyName, chars, (UIntPtr)length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property value, Key:[{propertyName}]");
            return success;
        }

        public override byte GetByteValue(string propertyName)
        {
            var success = DDC.GetFilePropertyUInt8(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get byte property value, Key:[{propertyName}]");

            return value;
        }

        public override short GetShortValue(string propertyName)
        {
            var success = DDC.GetFilePropertyInt16(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get short property value, Key:[{propertyName}]");

            return value;
        }

        public override int GetIntValue(string propertyName)
        {
            var success = DDC.GetFilePropertyInt32(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get int property value, Key:[{propertyName}]");

            return value;
        }

        public override float GetFloatValue(string propertyName)
        {
            var success = DDC.GetFilePropertyFloat(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get float property value, Key:[{propertyName}]");

            return value;
        }

        public override double GetDoubleValue(string propertyName)
        {
            var success = DDC.GetFilePropertyDouble(_SelfPtr, propertyName, out var value);
            TDMSErrorException.ThrowIfError(success, $"Failed to get double property value, Key:[{propertyName}]");

            return value;
        }

        public override DateTime GetDateTimeValue(string propertyName)
        {
            var success = DDC.GetFilePropertyTimestampComponents(_SelfPtr,
                                                                 propertyName,
                                                                 out var year,
                                                                 out var month,
                                                                 out var day,
                                                                 out var hour,
                                                                 out var minute,
                                                                 out var second,
                                                                 out var milliSecond,
                                                                 out var weekDay);
            TDMSErrorException.ThrowIfError(success, $"Failed to get datetime property value, Key:[{propertyName}]");

            return new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second, (int)milliSecond);
        }

        public override void SetStringValue(string propertyName, string propertyValue)
        {
            var success = DDC.SetFilePropertyString(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set string property value, Key:[{propertyName}]");
        }

        public override void SetByteValue(string propertyName, byte propertyValue)
        {
            var success = DDC.SetFilePropertyUInt8(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set byte property value, Key:[{propertyName}]");
        }

        public override void SetShortValue(string propertyName, short propertyValue)
        {
            var success = DDC.SetFilePropertyInt16(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set short property value, Key:[{propertyName}]");
        }

        public override void SetIntValue(string propertyName, int propertyValue)
        {
            var success = DDC.SetFilePropertyInt32(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set int property value, Key:[{propertyName}]");
        }

        public override void SetFloatValue(string propertyName, float propertyValue)
        {
            var success = DDC.SetFilePropertyFloat(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set float property value, Key:[{propertyName}]");
        }

        public override void SetDoubleValue(string propertyName, double propertyValue)
        {
            var success = DDC.SetFilePropertyDouble(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to set double property value, Key:[{propertyName}]");
        }

        public override void SetDateTimeValue(string propertyName, DateTime propertyValue)
        {
            var success = DDC.SetFilePropertyTimestampComponents(_SelfPtr,
                                                                 propertyName,
                                                                 (uint)propertyValue.Year,
                                                                 (uint)propertyValue.Month,
                                                                 (uint)propertyValue.Day,
                                                                 (uint)propertyValue.Hour,
                                                                 (uint)propertyValue.Minute,
                                                                 (uint)propertyValue.Second,
                                                                 propertyValue.Millisecond);
            TDMSErrorException.ThrowIfError(success, $"Failed to set datetime property value, Key:[{propertyName}]");
        }

        public override void CreateStringValue(string propertyName, string propertyValue)
        {
            var success = DDC.CreateFilePropertyString(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create string property value, Key:[{propertyName}]");
        }

        public override void CreateByteValue(string propertyName, byte propertyValue)
        {
            var success = DDC.CreateFilePropertyUInt8(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create byte property value, Key:[{propertyName}]");
        }

        public override void CreateShortValue(string propertyName, short propertyValue)
        {
            var success = DDC.CreateFilePropertyInt16(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create short property value, Key:[{propertyName}]");
        }

        public override void CreateIntValue(string propertyName, int propertyValue)
        {
            var success = DDC.CreateFilePropertyInt32(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create int property value, Key:[{propertyName}]");
        }

        public override void CreateFloatValue(string propertyName, float propertyValue)
        {
            var success = DDC.CreateFilePropertyFloat(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create float property value, Key:[{propertyName}]");
        }

        public override void CreateDoubleValue(string propertyName, double propertyValue)
        {
            var success = DDC.CreateFilePropertyDouble(_SelfPtr, propertyName, propertyValue);
            TDMSErrorException.ThrowIfError(success, $"Failed to create double property value, Key:[{propertyName}]");
        }

        public override void CreateDateTimeValue(string propertyName, DateTime propertyValue)
        {
            var success = DDC.CreateFilePropertyTimestampComponents(_SelfPtr,
                                                                    propertyName,
                                                                    (uint)propertyValue.Year,
                                                                    (uint)propertyValue.Month,
                                                                    (uint)propertyValue.Day,
                                                                    (uint)propertyValue.Hour,
                                                                    (uint)propertyValue.Minute,
                                                                    (uint)propertyValue.Second,
                                                                    propertyValue.Millisecond);
            TDMSErrorException.ThrowIfError(success, $"Failed to create datetime property value, Key:[{propertyName}]");
        }
    }

    internal abstract class PropertyOperator(IntPtr selfPtr) : IDisposable
    {
        protected IntPtr _SelfPtr = selfPtr;

        public abstract TDMSDataType GetDataType(string propertyName);
        public abstract bool Exists(string propertyName);
        public abstract uint GetStringLength(string propertyName);
        public abstract string[] GetPropertyNames();

        public string GetStringValue(string propertyName)
        {
            if (!Exists(propertyName))
                return string.Empty;

            var length = GetStringLength(propertyName);

            if (length <= 0)
                return string.Empty;

            var chars   = new char[length];
            var success = DDCGetStringValue(propertyName, chars, length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get string property value, Key:[{propertyName}]");

            return new string(chars).TrimEnd('\0');
        }
        protected abstract int DDCGetStringValue(string propertyName, char[] chars, uint length);

        public abstract byte GetByteValue(string propertyName);
        public abstract short GetShortValue(string propertyName);
        public abstract int GetIntValue(string propertyName);
        public abstract float GetFloatValue(string propertyName);
        public abstract double GetDoubleValue(string propertyName);
        public abstract DateTime GetDateTimeValue(string propertyName);

        public abstract void SetStringValue(string propertyName, string propertyValue);
        public abstract void SetByteValue(string propertyName, byte propertyValue);
        public abstract void SetShortValue(string propertyName, short propertyValue);
        public abstract void SetIntValue(string propertyName, int propertyValue);
        public abstract void SetFloatValue(string propertyName, float propertyValue);
        public abstract void SetDoubleValue(string propertyName, double propertyValue);
        public abstract void SetDateTimeValue(string propertyName, DateTime propertyValue);

        public abstract void CreateStringValue(string propertyName, string propertyValue);
        public abstract void CreateByteValue(string propertyName, byte propertyValue);
        public abstract void CreateShortValue(string propertyName, short propertyValue);
        public abstract void CreateIntValue(string propertyName, int propertyValue);
        public abstract void CreateFloatValue(string propertyName, float propertyValue);
        public abstract void CreateDoubleValue(string propertyName, double propertyValue);
        public abstract void CreateDateTimeValue(string propertyName, DateTime propertyValue);

        #region Implementation of IDisposable
        ~PropertyOperator()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            _SelfPtr = IntPtr.Zero;

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