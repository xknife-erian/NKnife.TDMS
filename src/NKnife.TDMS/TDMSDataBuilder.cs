using System;
using System.IO;
using System.Threading;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;

namespace NKnife.TDMS
{
    /// <summary>
    ///     TDMS数据文件的创建类。技术上是工厂模式的创建模式。也是<see cref="NKnife.TDMS" />的入口类。
    /// </summary>
    public static class TDMSDataBuilder
    {
        private static readonly int s_waitTimeBeforeOpeningFile = 20; //防止文件正在被其他进程写入(保存)而导致Open失败

        static TDMSDataBuilder()
        {
#if RELEASE
            s_waitTimeBeforeOpeningFile = 50; //生产环境下，等待时间略长一些，以确保成功率。
#endif
            var libsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libs");
            VerifyNiLibFiles(libsDirectory);
        }

        /// <summary>
        ///     检查dll库文件是否存在
        /// </summary>
        internal static void VerifyNiLibFiles(string libsDirectory)
        {
            string[] dllFiles = ["dacasr.dll", "nilibddc.dll", "tdms_ebd.dll"];

            foreach (var dllFile in dllFiles)
            {
                var filePath = Path.Combine(libsDirectory, dllFile);

                if(!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"NI TDMS library file does not exist：{filePath}");
                }
            }
        }

        public static ITDMSFile BuildNew(string filePath,
                                         string name,
                                         string description = "",
                                         string title = "",
                                         string author = "",
                                         string fileType = Constants.DDC_FILE_TYPE_TDM_STREAMING)
        {
            ITDMSFile tdmsFile = new TDMSFile();
            tdmsFile.Create(filePath, fileType, name, description, title, author);

            return tdmsFile;
        }

        /// <summary>
        ///     打开一个已经存在的TDMS文件<br />
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回一个文件的实例</returns>
        public static ITDMSFile OpenExisting(string filePath)
        {
            ITDMSFile tdmsFile = new TDMSFile();
            Thread.Sleep(s_waitTimeBeforeOpeningFile);
            tdmsFile.Open(filePath);

            return tdmsFile;
        }

        /// <summary>
        ///     打开一个已经存在的TDMS文件<br />
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <returns>返回一个文件的实例</returns>
        public static ITDMSFile OpenExistingFile(TDMSFileInfo fileInfo)
        {
            ITDMSFile tdmsFile = new TDMSFile();
            Thread.Sleep(s_waitTimeBeforeOpeningFile);
            tdmsFile.Open(fileInfo);

            return tdmsFile;
        }
    }
}