using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

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

            FileInfo = fileInfo;
            var success = DDC.OpenFile(fileInfo.FilePath, fileInfo.FileType, out var filePtr);
            TDMSErrorException.ThrowIfError(success, $"Failed to open file:{fileInfo.FilePath},Type:{fileInfo.FileType}");

            _SelfPtr         = filePtr;
            PropertyOperator = new FilePropertyOperator(_SelfPtr);

            if (TryGetProperty<DateTime>(Constants.DDC_FILE_DATETIME, out var dateTime))
                fileInfo.DateTime = dateTime;

            if(TryGetProperty(Constants.DDC_FILE_NAME, out string name))
                fileInfo.Name = name;

            if(TryGetProperty(Constants.DDC_FILE_DESCRIPTION, out string desc))
                fileInfo.Description = desc;

            if(TryGetProperty(Constants.DDC_FILE_TITLE, out string title))
                fileInfo.Title = title;

            if(TryGetProperty(Constants.DDC_FILE_AUTHOR, out string author))
                fileInfo.Author = author;

            SetNameAndDescription();

            return success == 0;
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

            _SelfPtr         = filePtr;
            PropertyOperator = new FilePropertyOperator(_SelfPtr);

            SetNameAndDescription();
            CreateOrUpdateProperty(Constants.DDC_FILE_DATETIME, FileInfo.DateTime); //即将存储，添加文件创建时间

            var isSave = Save();
            Thread.Sleep(6); //持久化到硬盘需要一些时间，略做等待

            return success == 0 && isSave;
        }

        /// <inheritdoc />
        public ITDMSChannelGroup this[int index]
        {
            get
            {
                if(index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");
                var channelGroupsBuffer = new IntPtr[ChildCount];
                var success             = DDC.GetChannelGroups(_SelfPtr, channelGroupsBuffer, (UIntPtr)ChildCount);
                TDMSErrorException.ThrowIfError(success, "Failed to get channels");

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
                var success             = DDC.GetChannelGroups(_SelfPtr, channelGroupsBuffer, (UIntPtr)ChildCount);

                TDMSErrorException.ThrowIfError(success, "Failed to get channel groups");

                foreach (var intPtr in channelGroupsBuffer)
                {
                    var group = new TDMSChannelGroup(intPtr);

                    if(group.Name == groupName) 
                        return group;
                    group.Dispose();
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
            if(key == Constants.DDC_FILE_DATETIME)
            {
                if(TryGetProperty(key, out DateTime dateTime))
                {
                    dict.Add(key, dateTime.ToString("O"));
                }
            }
            else
            {
                if(TryGetProperty(key, out string propertyValue))
                {
                    dict.Add(key, propertyValue);
                }
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
                TDMSErrorException.ThrowIfError(success, "Failed to query the number of channel groups");

                return count;
            }
        }

        /// <inheritdoc />
        public ITDMSChannelGroup AddGroup(string groupName, string description = "")
        {
            if(string.IsNullOrEmpty(groupName))
                throw new TDMSErrorException("Channel group name cannot be null or empty",
                                             new ArgumentNullException(nameof(groupName)));

            if(Contains(groupName))
                return null;

            var success = DDC.AddChannelGroup(_SelfPtr, groupName, description, out var groupPtr);
            TDMSErrorException.ThrowIfError(success, "Failed to add channel group");

            return new TDMSChannelGroup(groupPtr);
        }

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

            if (count == 0)
                return false;

            var names = PropertyOperator.GetPropertyNames();
            return names.Any(name => name.Equals(groupName));
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
            if (TryGetItem(groupName, out var group) && group is TDMSChannelGroup @in)
            {
                var success = DDC.RemoveChannelGroup(@in.GetPtr());
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
        #endregion
    }
}