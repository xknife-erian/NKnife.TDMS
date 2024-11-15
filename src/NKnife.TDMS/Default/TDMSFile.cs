﻿using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Default
{
    internal class TDMSFile : ITDMSFile
    {
        private IntPtr _filePtr;

        internal IntPtr GetPtr()
        {
            return _filePtr;
        }

        #region Implementation of IDisposable
        /// <inheritdoc />
        public void Dispose()
        {
            Close();
        }
        #endregion

        #region Implementation of ITDMSFile
        /// <inheritdoc />
        public TDMSFileInfo FileInfo { get; private set; }

        /// <inheritdoc />
        public void Save()
        {
            var success = DDC.SaveFile(_filePtr);
            TDMSErrorException.ThrowIfError(success, "Failed to save file");
        }

        /// <inheritdoc />
        public void Open(string filePath)
        {
            var tdmsFileInfo = new TDMSFileInfo { FilePath = filePath };
            Open(tdmsFileInfo);
        }

        /// <inheritdoc />
        public void Open(TDMSFileInfo fileInfo)
        {
            if(!fileInfo.Exists)
            {
                Create(fileInfo);
            }
            else
            {
                FileInfo = fileInfo;
                var success = DDC.OpenFile(fileInfo.FilePath, Constants.DDC_FILE_TYPE_TDM, out var filePtr);
                TDMSErrorException.ThrowIfError(success, "Failed to open file");
                _filePtr = filePtr;
            }
        }

        /// <inheritdoc />
        public void Create(string filePath,
                           string fileType,
                           string name,
                           string description,
                           string title,
                           string author)
        {
            var tdmsFileInfo = new TDMSFileInfo
            {
                FilePath    = filePath,
                FileType    = fileType,
                Name        = name,
                Description = description,
                Title       = title,
                Author      = author
            };
            Create(tdmsFileInfo);
        }

        /// <inheritdoc />
        public void Create(TDMSFileInfo fileInfo)
        {
            FileInfo = fileInfo;
            var success = DDC.CreateFile(FileInfo.FilePath,
                                         FileInfo.FileType,
                                         FileInfo.Name,
                                         FileInfo.Description,
                                         FileInfo.Title,
                                         FileInfo.Author,
                                         out var filePtr);
            TDMSErrorException.ThrowIfError(success, "Failed to create file");
            _filePtr = filePtr;
            Save();
        }

        /// <inheritdoc />
        public void Close()
        {
            DDC.CloseFile(_filePtr);
        }

        /// <inheritdoc />
        public ulong ChildCount
        {
            get
            {
                var success = DDC.CountChannelGroups(_filePtr, out var count);
                TDMSErrorException.ThrowIfError(success, "查询通道组数量异常");

                return count;
            }
        }

        /// <inheritdoc />
        public ITDMSChannelGroup this[int index]
        {
            get
            {
                if(index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");
                var channelGroupsBuffer = new IntPtr[ChildCount];
                var success             = DDC.GetChannelGroups(_filePtr, channelGroupsBuffer, (UIntPtr)ChildCount);
                TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

                if(index >= channelGroupsBuffer.Length)
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
                if(string.IsNullOrEmpty(groupName))
                    throw new ArgumentNullException(nameof(groupName), "Group name cannot be null or empty");

                var channelGroupsBuffer = new IntPtr[ChildCount];
                var success             = DDC.GetChannelGroups(_filePtr, channelGroupsBuffer, (UIntPtr)ChildCount);

                TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

                foreach (var intPtr in channelGroupsBuffer)
                {
                    var group = new TDMSChannelGroup(intPtr);
                    var name  = group.GetProperty(Constants.DDC_FILE_NAME);

                    if(name != groupName)
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
        public ITDMSChannelGroup Add(string groupName, string description = "")
        {
            var success = DDC.AddChannelGroup(_filePtr, groupName, description, out var groupPtr);
            TDMSErrorException.ThrowIfError(success, "Failed to add channel group");

            return new TDMSChannelGroup(groupPtr);
        }

        /// <inheritdoc />
        public void AddOrUpdateProperty<T>(string propertyName, T propertyValue)
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
                    success     = DDC.CreateFilePropertyString(_filePtr, propertyName, stringValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyString");

                    break;
                case byte byteValue:
                    success = DDC.CreateFilePropertyUInt8(_filePtr, propertyName, byteValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyUInt8");

                    break;      
                case short shortValue:
                    success = DDC.CreateFilePropertyInt16(_filePtr, propertyName, shortValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyInt16");

                    break;
                case int intValue:
                    success = DDC.CreateFilePropertyInt32(_filePtr, propertyName, intValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyInt32");

                    break;
                case float floatValue:
                    success = DDC.CreateFilePropertyFloat(_filePtr, propertyName, floatValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyFloat");

                    break;
                case double doubleValue:
                    success = DDC.CreateFilePropertyDouble(_filePtr, propertyName, doubleValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to CreateFilePropertyDouble");

                    break;
                case DateTime dateTimeValue:
                {
                    var dt = new TDMSDateTime(dateTimeValue);
                    success = DDC.CreateFilePropertyTimestampComponents(_filePtr,
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
                    success     = DDC.SetFilePropertyString(_filePtr, propertyName, stringValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyString");

                    break;
                case byte byteValue:
                    success = DDC.SetFilePropertyUInt8(_filePtr, propertyName, byteValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyUInt8");

                    break;
                case short shortValue:
                    success = DDC.SetFilePropertyInt16(_filePtr, propertyName, shortValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyInt16");

                    break;
                case int intValue:
                    success = DDC.SetFilePropertyInt32(_filePtr, propertyName, intValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyInt32");

                    break;
                case float floatValue:
                    success = DDC.SetFilePropertyFloat(_filePtr, propertyName, floatValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyFloat");

                    break;
                case double doubleValue:
                    success = DDC.SetFilePropertyDouble(_filePtr, propertyName, doubleValue);
                    TDMSErrorException.ThrowIfError(success, "Failed to SetFilePropertyDouble");

                    break;
                case DateTime dateTimeValue:
                {
                    var dt = new TDMSDateTime(dateTimeValue);
                    success = DDC.SetFilePropertyTimestampComponents(_filePtr,
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
        public (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType)
        {
            var success = DDC.GetFilePropertyType(_filePtr, propertyName, out var type);
            TDMSErrorException.ThrowIfError(success, "Failed to get property type");
            dataType = type;

            switch (type)
            {
                case TDMSDataType.UInt8:
                {
                    success = DDC.GetFilePropertyUInt8(_filePtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, value);
                }
                case TDMSDataType.Int16:
                {
                    success = DDC.GetFilePropertyInt16(_filePtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, value);
                }
                case TDMSDataType.Int32:
                {
                    success = DDC.GetFilePropertyInt32(_filePtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, value);
                }

                case TDMSDataType.Float:
                {
                    success = DDC.GetFilePropertyFloat(_filePtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, value);
                }

                case TDMSDataType.Double:
                {
                    success = DDC.GetFilePropertyDouble(_filePtr, propertyName, out var value);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, value);
                }
                case TDMSDataType.String:
                {
                    success = DDC.GetFileStringPropertyLength(_filePtr, propertyName, out var length);
                    TDMSErrorException.ThrowIfError(success, "Failed to get file string property length");

                    var source = new char[length];
                    success = DDC.GetFilePropertyString(_filePtr, propertyName, source, (UIntPtr)length);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, new string(source).TrimEnd('\0'));
                }
                case TDMSDataType.Timestamp:
                {
                    success = DDC.GetFilePropertyTimestampComponents(_filePtr,
                                                                     propertyName,
                                                                     out var year,
                                                                     out var month,
                                                                     out var day,
                                                                     out var hour,
                                                                     out var minute,
                                                                     out var second,
                                                                     out var milli,
                                                                     out var weekDay);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");
                    var dt = new TDMSDateTime(year, month, day, hour, minute, second, milli);

                    return (true, dt.ToDateTime());
                }

                default: throw new ArgumentOutOfRangeException();
            }

            return (false, null);
        }

        /// <inheritdoc />
        public bool PropertyExists(string propertyName)
        {
            var success = DDC.FilePropertyExists(_filePtr, propertyName, out var isExists);
            TDMSErrorException.ThrowIfError(success, "Failed to check property exists");

            return isExists == 1;
        }

        /// <inheritdoc />
        public string[] GetPropertyNames()
        {
            var success = DDC.CountFileProperties(_filePtr, out var count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property count");

            var names = new IntPtr[count];
            success = DDC.GetFilePropertyNames(_filePtr, names, (UIntPtr)count);
            TDMSErrorException.ThrowIfError(success, "Failed to get property names");

            var result = new string[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = Marshal.PtrToStringAnsi(names[i]);
            }

            return result;
        }

        /// <inheritdoc />
        public void Clear()
        {
            var groupsBuffer = new IntPtr[ChildCount];
            var success      = DDC.GetChannelGroups(_filePtr, groupsBuffer, (UIntPtr)ChildCount);
            TDMSErrorException.ThrowIfError(success, "Failed to get channel groups");

            foreach (var ptr in groupsBuffer)
            {
                success = DDC.RemoveChannelGroup(ptr);
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel group");
            }
        }

        /// <inheritdoc />
        public bool Contains(string groupName)
        {
            var channelGroupsBuffer = new IntPtr[ChildCount];
            var success             = DDC.GetChannelGroups(_filePtr, channelGroupsBuffer, (UIntPtr)ChildCount);
            TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

            foreach (var intPtr in channelGroupsBuffer)
            {
                using var group = new TDMSChannelGroup(intPtr);
                var       name  = group.GetProperty(Constants.DDC_FILE_NAME);

                if(name == groupName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool Remove(string groupName)
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
        public bool RemoveAt(int index)
        {
            var group = this[index];

            if(group is TDMSChannelGroup groupIn)
            {
                var success = DDC.RemoveChannelGroup(groupIn.GetPtr());
                TDMSErrorException.ThrowIfError(success, "Failed to remove channel group");
            }

            return false;
        }
        #endregion
    }
}