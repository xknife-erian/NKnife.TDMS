using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS.Default
{
    class TDMSFile : ITDMSFile, IEnumerable<ITDMSChannelGroup>
    {
        private IntPtr _filePtr;
        internal IntPtr GetPtr()
        {
            return _filePtr;
        }

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
                FilePath = filePath,
                FileType = fileType,
                Name = name,
                Description = description,
                Title = title,
                Author = author
            };
            Create(tdmsFileInfo);
        }

        /// <inheritdoc />
        public void Create(TDMSFileInfo fileInfo)
        {
            FileInfo = fileInfo;
            var success = DDC.CreateFile(FileInfo.FilePath, FileInfo.FileType, FileInfo.Name, FileInfo.Description,
                                         FileInfo.Title, FileInfo.Author, out var filePtr);
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
                return (ulong)count;
            }
        }

        /// <inheritdoc />
        public ITDMSChannelGroup Add(string groupName, string description = "")
        {
            var result = DDC.AddChannelGroup(_filePtr, groupName, description, out var groupPtr);
            if (result != 0)
            {
                throw new TDMSErrorException("Failed to add group with properties");
            }
            return new TDMSChannelGroup(groupPtr);
        }

        /// <inheritdoc />
        public void SetProperty(string propertyName, string propertyValue)
        {
            int status = DDC.SetFileProperty(_filePtr, propertyName, __arglist(propertyValue));
            if (status != 0)
            {
                Console.WriteLine("设置文件名称属性失败，错误代码: " + status);
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

                    var str = new char[length];
                    success = DDC.GetFilePropertyString(_filePtr, propertyName, str, (UIntPtr)length);
                    TDMSErrorException.ThrowIfError(success, "Failed to get property value");

                    return (true, str);
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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Clear()
        {
            var groupsBuffer = new IntPtr[ChildCount];
            var success = DDC.GetChannelGroups(_filePtr, groupsBuffer, (UIntPtr)ChildCount);
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
            var success = DDC.GetChannelGroups(_filePtr, channelGroupsBuffer, (UIntPtr)ChildCount);
            TDMSErrorException.ThrowIfError(success, "Failed to get channel group names");

            foreach (var intPtr in channelGroupsBuffer)
            {
                var group = new TDMSChannelGroup(intPtr);
                var name = group.GetProperty(Constants.DDC_FILE_NAME);
                if (name == groupName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool Remove(string groupName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITDMSChannelGroup this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITDMSChannelGroup this[string groupName]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        #endregion

        #region Implementation of IEnumerable
        /// <inheritdoc />
        public IEnumerator<ITDMSChannelGroup> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Implementation of IDisposable
        /// <inheritdoc />
        public void Dispose()
        {
            Close();
        }
        #endregion
    }
}


/*
public TDMSFileInfo FileInfo { get; set; }

public ITDMSChannelGroup Add(string groupName, string description, Dictionary<string, string> properties)
{
    var result = DDC.AddChannelGroup(_filePtr, groupName, description, out var groupPtr);
    if (result != 0)
    {
        throw new TDMSErrorException("Failed to add group with properties.");
    }

    return new TDMSChannelGroup(groupPtr);
}

public void Save()
{
    var result = DDC.SaveFile(_filePtr);
    if (result != 0)
    {
        throw new TDMSErrorException("Failed to save file.");
    }
}

public void Open(string filePath)
{
    var result = DDC.OpenFile(filePath, Constants.DDC_FILE_TYPE_TDM, out var filePtr);
    if (result != 0)
    {
        throw new TDMSErrorException("Failed to load file.");
    }
    _filePtr = filePtr;
}

public void Close()
{
    DDC.CloseFile(_filePtr);
}
*/
