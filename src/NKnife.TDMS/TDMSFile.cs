using System;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    /// <summary>
    ///     为解决测试和测量中数据读取和分析时的种种问题，如没有描述、格式不一致、存储混乱等，
    ///     NI定义了一个技术数据管理(Technical Data Management，TDM)解决方案。<br />
    ///     TDMS文件格式由三个层次组成：文件、组、通道。文件层可包含任意数量的组，而每个组又可包含任意数量的通道。
    ///     通过通道分组，用户可以选择如何组织数据以便使其更易于理解。
    /// </summary>
    public class TDMSFile : IDisposable
    {
        private readonly IntPtr _file;

        public TDMSFile(TDMSFileInfo fileInfo)
        {
            FileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
            if (!FileInfo.Exists)
            {
                var result = DDC.CreateFile(FileInfo.FilePath, FileInfo.FileType, FileInfo.Name, FileInfo.Description,
                                            FileInfo.Title, FileInfo.Author, out var ptr);
                if(result == 0)
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

        public void Dispose() { }
    }
}