using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace NKnife.TDMS.Default
{
    internal class TDMSFile : BaseTDMSLevel, ITDMSFile
    {
        #region Implementation of ITDMSFile
        /// <inheritdoc />
        public TDMSFileInfo FileInfo { get; private set; }

        /// <inheritdoc />
        public bool Save()
        {
            var success = DDC.SaveFile(_SelfPtr);
            TDMSErrorException.ThrowIfError(success, "Failed to save file");
            return success == 0;
        }

        /// <inheritdoc />
        public bool Open(string filePath)
        {
            var tdmsFileInfo = new TDMSFileInfo(filePath);
            return Open(tdmsFileInfo);
        }

        /// <inheritdoc />
        public bool Open(TDMSFileInfo fileInfo)
        {
            if(!fileInfo.Exists)
            {
                return Create(fileInfo);
            }
            else
            {
                FileInfo = fileInfo;
                var success = DDC.OpenFile(fileInfo.FilePath, fileInfo.FileType, out var filePtr);
                TDMSErrorException.ThrowIfError(success, $"Failed to open file:{fileInfo.FilePath},Type:{fileInfo.FileType}");

                _SelfPtr = filePtr;

                var result = GetProperty(Constants.DDC_FILE_DATETIME, out _);
                if (result.Success)
                    fileInfo.DateTime = (DateTime)result.PropertyValue;

                result = GetProperty(Constants.DDC_FILE_NAME, out _);

                if(result.Success)
                    fileInfo.Name = (string)result.PropertyValue;

                result = GetProperty(Constants.DDC_FILE_DESCRIPTION, out _);

                if(result.Success)
                    fileInfo.Description = (string)result.PropertyValue;

                result = GetProperty(Constants.DDC_FILE_TITLE, out _);
                if (result.Success)
                    fileInfo.Title = (string)result.PropertyValue;

                result = GetProperty(Constants.DDC_FILE_AUTHOR, out _);
                if (result.Success)
                    fileInfo.Author = (string)result.PropertyValue;

                SetNameAndDescription();
                return success == 0;
            }
        }

        /// <inheritdoc />
        public bool Create(string filePath,
                           string fileType,
                           string name,
                           string description,
                           string title,
                           string author)
        {
            var tdmsFileInfo = new TDMSFileInfo(filePath)
            {
                Name        = name,
                Description = description,
                Title       = title,
                Author      = author
            };
            return Create(tdmsFileInfo);
        }

        /// <inheritdoc />
        public bool Create(TDMSFileInfo fileInfo)
        {
            FileInfo = fileInfo;
            var success = DDC.CreateFile(FileInfo.FilePath,
                                         FileInfo.FileType,
                                         FileInfo.Name,
                                         FileInfo.Description,
                                         FileInfo.Title,
                                         FileInfo.Author,
                                         out var filePtr);
            TDMSErrorException.ThrowIfError(success, $"Failed to create file:[{FileInfo.FilePath}][{FileInfo.FileType}]");
            
            _SelfPtr = filePtr;
            
            SetNameAndDescription();
            AddOrUpdateProperty(Constants.DDC_FILE_DATETIME, FileInfo.DateTime);//即将存储，添加文件创建时间

            var isSave = Save();
            Thread.Sleep(6); //持久化到硬盘需要一些时间，略做等待

            return success == 0 && isSave;
        }

        /// <inheritdoc />
        public ITDMSChannelGroup this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");
                var channelGroupsBuffer = new IntPtr[ChildCount];
                var success = DDC.GetChannelGroups(_SelfPtr, channelGroupsBuffer, (UIntPtr)ChildCount);
                TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

                if (index >= channelGroupsBuffer.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index must be less than the number of channel groups");
                var groupPtr = channelGroupsBuffer[index];

                return new TDMSChannelGroup(groupPtr);
            }
        }

        /// <inheritdoc />
        public ITDMSChannelGroup this[string groupName]
        {
            get
            {
                if (string.IsNullOrEmpty(groupName))
                    throw new ArgumentNullException(nameof(groupName), "Group name cannot be null or empty");

                var channelGroupsBuffer = new IntPtr[ChildCount];
                var success = DDC.GetChannelGroups(_SelfPtr, channelGroupsBuffer, (UIntPtr)ChildCount);

                TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

                foreach (var intPtr in channelGroupsBuffer)
                {
                    var group = new TDMSChannelGroup(intPtr);
                    var name = group.GetProperty(Constants.DDC_FILE_NAME, out var dataType);

                    if (name.Success && (string)name.PropertyValue != groupName)
                    {
                        group.Dispose();
                        continue;
                    }

                    return group;
                }

                return null;
            }
        }

        /// <inheritdoc />
        public IDictionary<string, string> GetDefaultProperties()
        {
            var dict = new Dictionary<string, string>(5);

            GetDefaultPropertyToDictionary(Constants.DDC_FILE_NAME, dict);
            GetDefaultPropertyToDictionary(Constants.DDC_FILE_DESCRIPTION, dict);
            GetDefaultPropertyToDictionary(Constants.DDC_FILE_TITLE, dict);
            GetDefaultPropertyToDictionary(Constants.DDC_FILE_AUTHOR, dict);
            GetDefaultPropertyToDictionary(Constants.DDC_FILE_DATETIME, dict);

            return dict;
        }

        private void GetDefaultPropertyToDictionary(string key, Dictionary<string, string> dict)
        {
            var property = GetProperty(key, out var type);

            if (property.Success)
            {
                if (key != Constants.DDC_FILE_DATETIME)
                {
                    dict.Add(key, property.PropertyValue.ToString());
                }
                else
                {
                    var dt = (DateTime)property.PropertyValue;
                    dict.Add(key, $"{dt:O}");
                }
            }
            else
            {
                throw new TDMSErrorException($"Failed to get default property [{key}]",
                                             new InvalidOperationException("Property not found"));
            }
        }
        #endregion

        #region Implementation of BaseTDMSLevel
        /// <inheritdoc />
        public override bool Close()
        {
            if(!_IsClosed)
            {
                var success = DDC.CloseFile(_SelfPtr);
                TDMSErrorException.ThrowIfError(success, "Failed to CloseFile");
            }

            return _IsClosed = true;
        }

        /// <inheritdoc />
        public override ulong ChildCount
        {
            get
            {
                var success = DDC.CountChannelGroups(_SelfPtr, out var count);
                TDMSErrorException.ThrowIfError(success, "查询通道组数量异常");

                return count;
            }
        }

        /// <inheritdoc />
        public ITDMSChannelGroup AddGroup(string groupName, string description = "")
        {
            if (string.IsNullOrEmpty(groupName))
                throw new TDMSErrorException("Channel group name cannot be null or empty", new ArgumentNullException(nameof(groupName)));

            if (Contains(groupName))
                return null;

            var success = DDC.AddChannelGroup(_SelfPtr, groupName, description, out var groupPtr);
            TDMSErrorException.ThrowIfError(success, "Failed to add channel group");

            return new TDMSChannelGroup(groupPtr);
        }

        #region Implementation of ITDMSLevelPropertyOperation

        /// <inheritdoc />
        public override void AddOrUpdateProperty<T>(string propertyName, T propertyValue)
        {
            if(!PropertyExists(propertyName))
                AddProperty(propertyName, propertyValue);
            else
                UpdateProperty(propertyName, propertyValue);
        }

        private void AddProperty<T>(string propertyName, T propertyValue)
        {
            int success;

            switch (propertyValue)
            {
                case string stringValue:
                    stringValue = $"{stringValue}+";
                    success     = DDC.CreateFilePropertyString(_SelfPtr, propertyName, stringValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyString");

                    break;
                case byte byteValue:
                    success = DDC.CreateFilePropertyUInt8(_SelfPtr, propertyName, byteValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyUInt8");

                    break;      
                case short shortValue:
                    success = DDC.CreateFilePropertyInt16(_SelfPtr, propertyName, shortValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyInt16");

                    break;
                case int intValue:
                    success = DDC.CreateFilePropertyInt32(_SelfPtr, propertyName, intValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyInt32");

                    break;
                case float floatValue:
                    success = DDC.CreateFilePropertyFloat(_SelfPtr, propertyName, floatValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyFloat");

                    break;
                case double doubleValue:
                    success = DDC.CreateFilePropertyDouble(_SelfPtr, propertyName, doubleValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyDouble");

                    break;
                case DateTime dateTimeValue:
                {
                    var dt = new TDMSDateTime(dateTimeValue);
                    success = DDC.CreateFilePropertyTimestampComponents(_SelfPtr,
                                                                        propertyName,
                                                                        dt.Year,
                                                                        dt.Month,
                                                                        dt.Day,
                                                                        dt.Hour,
                                                                        dt.Minute,
                                                                        dt.Second,
                                                                        dt.MilliSecond);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyTimestampComponents");

                    break;
                }
                default:
                    throw new ArgumentException("Unsupported property value type");
            }
        }

        private void UpdateProperty<T>(string propertyName, T propertyValue)
        {
            int success;

            switch (propertyValue)
            {
                case string stringValue:
                    stringValue = $"{stringValue}+";
                    success     = DDC.SetFilePropertyString(_SelfPtr, propertyName, stringValue);
                    TDMSErrorException.ThrowIfError(success, $"Failed to SetFilePropertyString, KEY:[{propertyName}]; VALUE:[{propertyValue}]");

                    break;
                case byte byteValue:
                    success = DDC.SetFilePropertyUInt8(_SelfPtr, propertyName, byteValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyUInt8");

                    break;
                case short shortValue:
                    success = DDC.SetFilePropertyInt16(_SelfPtr, propertyName, shortValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyInt16");

                    break;
                case int intValue:
                    success = DDC.SetFilePropertyInt32(_SelfPtr, propertyName, intValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyInt32");

                    break;
                case float floatValue:
                    success = DDC.SetFilePropertyFloat(_SelfPtr, propertyName, floatValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyFloat");

                    break;
                case double doubleValue:
                    success = DDC.SetFilePropertyDouble(_SelfPtr, propertyName, doubleValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyDouble");

                    break;
                case DateTime dateTimeValue:
                {
                    var dt = new TDMSDateTime(dateTimeValue);
                    success = DDC.SetFilePropertyTimestampComponents(_SelfPtr,
                                                                     propertyName,
                                                                     dt.Year,
                                                                     dt.Month,
                                                                     dt.Day,
                                                                     dt.Hour,
                                                                     dt.Minute,
                                                                     dt.Second,
                                                                     dt.MilliSecond);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyTimestampComponents");

                    break;
                }
                default:
                    throw new ArgumentException("Unsupported property value type");
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override bool PropertyExists(string propertyName)
        {
            var success = DDC.FilePropertyExists(_SelfPtr, propertyName, out var isExists);
            TDMSErrorException.ThrowIfError(success, "Failed to check property exists");

            return isExists == 1;
        }

        /// <inheritdoc />
        protected override TDMSDataType GetPropertyType(string propertyName)
        {
            var success = DDC.GetFilePropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");

            return type;
        }

        /// <inheritdoc />
        protected override uint GetStringPropertyLength(string propertyName)
        {
            var success = DDC.GetFileStringPropertyLength(_SelfPtr, propertyName, out var length);
            TDMSErrorException.ThrowIfError(success, $"Failed to get file string property length, Key:[{propertyName}]");
            return length;
        }

        /// <inheritdoc />
        protected override void GetPropertyInternal(string propertyName, IntPtr result, uint length)
        {
            var success = DDC.GetFileProperty(_SelfPtr, propertyName, result, (UIntPtr)length);
            TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyString, Key:[{propertyName}]");
        }

        /// <inheritdoc />
        protected override DateTime GetPropertyTimestampComponents(string propertyName)
        {
            var success = DDC.GetFilePropertyTimestampComponents(_SelfPtr,
                                                             propertyName,
                                                             out var year,
                                                             out var month,
                                                             out var day,
                                                             out var hour,
                                                             out var minute,
                                                             out var second,
                                                             out var milli,
                                                             out var weekDay);
            TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyTimestampComponents, Key:[{propertyName}]");
            var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

            return dt.ToDateTime();
        }

        /*
        /// <inheritdoc />
        public override (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            var success = DDC.GetFilePropertyType(_SelfPtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");
            dataType = type;

            switch (type)
            {
                case TDMSDataType.String:
                {
                    success = DDC.GetFileStringPropertyLength(_SelfPtr, propertyName, out var length);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get file string property length, Key:[{propertyName}]");

                    if (length <= 0) //存在属性，但是值为空
                        return (true, string.Empty);

                    var source = new char[length];
                    success = DDC.GetFilePropertyString(_SelfPtr, propertyName, source, (UIntPtr)length);
                    TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyString, Key:[{propertyName}]");

                    return (true, new string(source).TrimEnd('\0'));
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
                    TDMSErrorException.ThrowIfError(success, $"Failed to GetFilePropertyTimestampComponents, Key:[{propertyName}]");
                    var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

                    return (true, dt.ToDateTime());
                }
                case TDMSDataType.UInt8:
                {
                    success = DDC.GetFilePropertyUInt8(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    return (true, value);
                }
                case TDMSDataType.Int16:
                {
                    success = DDC.GetFilePropertyInt16(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    return (true, value);
                }
                case TDMSDataType.Int32:
                {
                    success = DDC.GetFilePropertyInt32(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    return (true, value);
                }

                case TDMSDataType.Float:
                {
                    success = DDC.GetFilePropertyFloat(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    return (true, value);
                }

                case TDMSDataType.Double:
                {
                    success = DDC.GetFilePropertyDouble(_SelfPtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, $"Failed to get property value, Key:[{propertyName}]");

                    return (true, value);
                }
                default: throw new ArgumentOutOfRangeException();
            }

            return (false, null);
        }*/

        #endregion

        /// <inheritdoc />
        public override bool Clear()
        {
            var groupsBuffer = new IntPtr[ChildCount];
            var success      = DDC.GetChannelGroups(_SelfPtr, groupsBuffer, (UIntPtr)ChildCount);
            TDMSErrorException.ThrowIfError(success, "Failed to get channel groups");

            foreach (var ptr in groupsBuffer)
            {
                success = DDC.RemoveChannelGroup(ptr);
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel group");
            }

            return success == 0;
        }

        /// <inheritdoc />
        public override bool Contains(string groupName)
        {
            var count = ChildCount;

            if(count == 0)
                return false;

            var channelGroupsBuffer = new IntPtr[count];

            var success = DDC.GetChannelGroups(_SelfPtr, channelGroupsBuffer, (UIntPtr)count);
            TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

            foreach (var intPtr in channelGroupsBuffer)
            {
                using var group = new TDMSChannelGroup(intPtr);

                var propertyGetter = group.GetProperty(Constants.DDC_FILE_NAME, out var dataType);

                if(propertyGetter.Success
                   && (string)propertyGetter.PropertyValue == groupName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public override bool TryGetItem(string groupName, out ITDMSLevel level)
        {
            var has = Contains(groupName);
            if(has)
            {
                level = this[groupName];
                return true;
            }
            level = null;
            return false;
        }

        /// <inheritdoc />
        public override bool Remove(string groupName)
        {
            var group = this[groupName];

            if(group is TDMSChannelGroup groupIn)
            {
                var success = DDC.RemoveChannelGroup(groupIn.GetPtr());
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel group");
            }

            return false;
        }

        /// <inheritdoc />
        public override bool RemoveAt(int index)
        {
            var group = this[index];

            if(group is TDMSChannelGroup groupIn)
            {
                var success = DDC.RemoveChannelGroup(groupIn.GetPtr());
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel group");
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool ManualCloseNode()
        {
            if(!_IsClosed)
            {
                var success = DDC.CloseFile(_SelfPtr);
                TDMSErrorException.ThrowIfError(success, "Failed to close file");
            }

            return _IsClosed = true;
        }

        #endregion
    }
}