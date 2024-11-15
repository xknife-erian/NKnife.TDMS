using System;
using System.Collections.Generic;
using System.Text;
using NKnife.TDMS.Default;

namespace NKnife.TDMS
{
    /// <summary>
    /// 工厂模式的创建类，也是TDMS的入口类。
    /// </summary>
    public class TDMSWeaver
    {
        public static ITDMSFile Weave(TDMSFileInfo fileInfo)
        {
            return new TDMSFile();
        }

        public ITDMSFile CreateNewFile(string filePath, string fileType, string name, string description, string title, string author)
        {
            ITDMSFile tdmsFile = new TDMSFile(); // 假设 TDMSFile 是 ITDMSFile 的实现类
            tdmsFile.Create(filePath, fileType, name, description, title, author);
            return tdmsFile;
        }

        public ITDMSFile OpenExistingFile(string filePath)
        {
            ITDMSFile tdmsFile = new TDMSFile(); // 假设 TDMSFile 是 ITDMSFile 的实现类
            tdmsFile.Open(filePath);
            return tdmsFile;
        }

        public ITDMSFile OpenExistingFile(TDMSFileInfo fileInfo)
        {
            ITDMSFile tdmsFile = new TDMSFile(); // 假设 TDMSFile 是 ITDMSFile 的实现类
            tdmsFile.Open(fileInfo);
            return tdmsFile;
        }
    }
}
