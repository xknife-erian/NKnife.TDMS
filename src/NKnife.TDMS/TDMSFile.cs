using System;
using System.Collections.Generic;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public class TDMSFile : ITDMSFile
    {
        private IntPtr _file;

        public TDMSFile(TDMSFileInfo fileInfo)
        {
            FileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
            if (!FileInfo.Exists)
            {
                var result = DDC.CreateFile(FileInfo.FilePath, FileInfo.FileType, FileInfo.Name, FileInfo.Description,
                                            FileInfo.Title, FileInfo.Author, out var ptr);
                if (result == 0)
                {
                    _file = ptr;
                }
                else
                {
                    throw new TDMSFileErrorException("Failed to create TDMS file.");
                }
                DDC.SaveFile(_file);
            }
        }

        public TDMSFileInfo FileInfo { get; set; }

        public ITDMSGroup AddGroup(string groupName, string description, Dictionary<string, string> properties)
        {
            var result = DDC.AddChannelGroup(_file, groupName, description, out var groupPtr);
            if (result != 0)
            {
                throw new TDMSFileErrorException("Failed to add group with properties.");
            }

            return new TDMSGroup(groupPtr);
        }

        public void Save()
        {
            var result = DDC.SaveFile(_file);
            if (result != 0)
            {
                throw new TDMSFileErrorException("Failed to save file.");
            }
        }

        public void Load(string filePath)
        {
            var result = DDC.OpenFile(filePath, Constants.DDC_FILE_TYPE_TDM, out var filePtr);
            if (result != 0)
            {
                throw new TDMSFileErrorException("Failed to load file.");
            }
            _file = filePtr;
        }

        public void Close()
        {
            var result = DDC.CloseFile(_file);
            if (result != 0)
            {
                throw new TDMSFileErrorException("Failed to close file.");
            }
        }

        public void Dispose()
        {
            Close();
        }
    }

    public class TDMSGroup : ITDMSGroup
    {
        public TDMSGroup(IntPtr groupPtr)
        {
            GroupPtr = groupPtr;
        }

        public IntPtr GroupPtr { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ITDMSChannel AddChannel(string channelName, string description = "", Dictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
    }


}