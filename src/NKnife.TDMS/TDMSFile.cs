using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                    throw new TDMSErrorException("Failed to create TDMS file.");
                }
                DDC.SaveFile(_file);
            }
        }

        public TDMSFileInfo FileInfo { get; set; }

        public ITDMSChannelGroup AddGroup(string groupName, string description, Dictionary<string, string> properties)
        {
            var result = DDC.AddChannelGroup(_file, groupName, description, out var groupPtr);
            if (result != 0)
            {
                throw new TDMSErrorException("Failed to add group with properties.");
            }

            return new TDMSChannelGroup(groupPtr);
        }

        public void Save()
        {
            var result = DDC.SaveFile(_file);
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
            _file = filePtr;
        }

        public void Close()
        {
            var result = DDC.CloseFile(_file);
            if (result != 0)
            {
                throw new TDMSErrorException("Failed to close file.");
            }
        }

        public void Dispose()
        {
            Close();
        }

        public ITDMSChannelGroup GetGroup(int i)
        {
            throw new NotImplementedException();
        }
    }
}