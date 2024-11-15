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
        public static ITDMSFile Weave()
        {
            return new TDMSFile();
        }

        public static ITDMSFile CreateNewFile(string filePath, string fileType, string name, string description, string title, string author)
        {
            ITDMSFile tdmsFile = new TDMSFile(); 
            tdmsFile.Create(filePath, fileType, name, description, title, author);
            return tdmsFile;
        }

        public static ITDMSFile OpenExistingFile(string filePath)
        {
            ITDMSFile tdmsFile = new TDMSFile(); 
            tdmsFile.Open(filePath);
            return tdmsFile;
        }

        public static ITDMSFile OpenExistingFile(TDMSFileInfo fileInfo)
        {
            ITDMSFile tdmsFile = new TDMSFile(); 
            tdmsFile.Open(fileInfo);
            return tdmsFile;
        }
    }
}
