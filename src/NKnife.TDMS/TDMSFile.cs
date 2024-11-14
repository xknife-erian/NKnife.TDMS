using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public class TDMSFile : ITDMSFile, IEnumerable<ITDMSChannelGroup>
    {
        private IntPtr _filePtr;

        public TDMSFile(TDMSFileInfo fileInfo)
        {
            FileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
            if (!FileInfo.Exists)
            {
                var result = DDC.CreateFile(FileInfo.FilePath, FileInfo.FileType, FileInfo.Name, FileInfo.Description,
                                            FileInfo.Title, FileInfo.Author, out var ptr);
                if (result == 0)
                {
                    _filePtr = ptr;
                }
                else
                {
                    throw new TDMSErrorException("Failed to create TDMS file.");
                }
                DDC.SaveFile(_filePtr);
            }
        }

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

        public void Load(string filePath)
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

        /// <inheritdoc />
        public int Count { get; set; }

        /// <inheritdoc />
        public ITDMSChannelGroup Add(string groupName, string description = "")
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SetFileProperty(string propertyName, string propertyValue)
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
            Close();
        }

        public ITDMSChannelGroup GetGroup(int i)
        {
            throw new NotImplementedException();
        }

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

        internal IntPtr GetPtr()
        {
            return _filePtr;
        }
    }
}