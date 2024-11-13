using System;
using System.Collections.Generic;

namespace NKnife.TDMS
{
    /// <summary>
    ///     为解决测试和测量中数据读取和分析时的种种问题，如没有描述、格式不一致、存储混乱等，
    ///     NI定义了一个技术数据管理(Technical Data Management，TDM)解决方案。<br />
    ///     TDMS文件格式由三个层次组成：文件、组、通道。文件层可包含任意数量的组，而每个组又可包含任意数量的通道。
    ///     通过通道分组，用户可以选择如何组织数据以便使其更易于理解。
    /// </summary>
    public interface ITDMSFile : IDisposable
    {
        public TDMSFileInfo FileInfo { get; set; }

        public ITDMSGroup AddGroup(string groupName, string description = "", Dictionary<string, string> properties = null);


        // 其他可能的方法签名
        public void Save();
        public void Load(string filePath);
        public void Close();
    }

    public interface ITDMSGroup : IDisposable
    {
        public ITDMSChannel AddChannel(string channelName,
                                       string description = "",
                                       Dictionary<string, string> properties = null);
    }

    public interface ITDMSChannel : IDisposable
    {
        // 添加数据的方法签名
        public void AddData(string groupName, string channelName, double[] data);
        public void AddData(string groupName, string channelName, int[] data);
        public void AddData(string groupName, string channelName, string[] data);
        public void AddData(string groupName, string channelName, DateTime[] data);

    }
}