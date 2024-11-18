using System;
using System.Collections.Generic;
using System.Text;
using NKnife.TDMS.Default;

namespace NKnife.TDMS
{
    /// <summary>
    /// TDMS数据文件的创建类。技术上是工厂模式的创建模式。也是<see cref="NKnife.TDMS"/>的入口类。
    /// </summary>
    public static class TDMSDataBuilder
    {
        public static ITDMSFile Build()
        {
            return new TDMSFile();
        }

        public static ITDMSFile BuildNew(string filePath, string fileType, string name, string description, string title, string author)
        {
            ITDMSFile tdmsFile = new TDMSFile(); 
            tdmsFile.Create(filePath, fileType, name, description, title, author);
            return tdmsFile;
        }

        /// <summary>
        /// 打开一个已经存在的TDMS文件<br/>
        /// 注意：如果是刚刚代码创建的TDMS文件，请在打开之前等待约50-100ms，以确保文件已经完全创建。这是NI库的特性。
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回一个文件的实例</returns>
        public static ITDMSFile OpenExisting(string filePath)
        {
            ITDMSFile tdmsFile = new TDMSFile(); 
            tdmsFile.Open(filePath);
            return tdmsFile;
        }

        /// <summary>
        /// 打开一个已经存在的TDMS文件<br/>
        /// 注意：如果是刚刚代码创建的TDMS文件，请在打开之前等待约50-100ms，以确保文件已经完全创建。这是NI库的特性。
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <returns>返回一个文件的实例</returns>
        public static ITDMSFile OpenExistingFile(TDMSFileInfo fileInfo)
        {
            ITDMSFile tdmsFile = new TDMSFile(); 
            tdmsFile.Open(fileInfo);
            return tdmsFile;
        }
    }
}
