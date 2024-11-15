using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public class TDMSFile : ITDMSFile, IEnumerable<ITDMSChannelGroup>
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
        public int Count
        {
            get
            {
                var success = DDC.CountChannelGroups(_filePtr, out var count);
                TDMSErrorException.ThrowIfError(success, "查询通道组数量异常");

                return (int)count;
            }
        }

        /// <inheritdoc />
        public ITDMSChannelGroup Add(string groupName, string description = "")
        {
            var result = DDC.AddChannelGroup(_filePtr, groupName, description, out var groupPtr);
            if (result != 0)
            {
                throw new TDMSErrorException("Failed to add group with properties.");
            }
            return new TDMSChannelGroup(groupPtr);
        }

        /// <inheritdoc />
        public void SetFileProperty(string propertyName, string propertyValue)
        {
            int status = DDC.SetFileProperty(_filePtr, propertyName, __arglist(propertyValue));
            if (status != 0)
            {
                Console.WriteLine("设置文件名称属性失败，错误代码: " + status);
            }
        }

        /// <inheritdoc />
        public bool PropertyExists(string propertyName)
        {
            var success = DDC.FilePropertyExists(_filePtr, propertyName, out var isExists);
            TDMSErrorException.ThrowIfError(success, "Failed to check property exists.");

            return isExists == 1;
        }

        /// <inheritdoc />
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Contains(string groupName)
        {
            throw new NotImplementedException();
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
