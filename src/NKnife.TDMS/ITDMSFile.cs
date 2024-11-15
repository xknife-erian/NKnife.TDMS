using System;
using System.Collections.Generic;

namespace NKnife.TDMS
{
    /// <summary>
    ///     NI定义了一个技术数据管理(Technical Data Management，TDM)解决方案。<br />
    ///     - TDMS文件就是该技术解决方案的承载数据文件。本接口是这个文件格式的具体描述。<br/>
    ///     - 文件格式由三个层次组成：文件、组、通道。<br/>
    ///     - 文件层可包含任意数量的组，而每个组又可包含任意数量的通道。<br/>
    ///     - 在层次结构的每个级别，可以存储无限数量的自定义标量属性。<br/>
    ///     - 每个级别都接受无限数量的自定义属性，以实现文档齐全且可供搜索的数据文件。<br/>
    ///     - 位于文件中的描述性信息是此模型的一个主要优点，它提供了一种简单的方法来记录数据，而无需设计您自己的标头结构。<br/><br/>
    ///
    ///     https://knowledge.ni.com/KnowledgeArticleDetails?id=kA03q000000x4PcCAI&l=zh-CN <br/>
    ///     https://www.ni.com/en/support/documentation/supplemental/06/the-ni-tdms-file-format.html <br/>
    ///     https://www.ni.com/docs/en-US/bundle/labwindows-cvi/page/cvi/libref/cvitdmslibrary.htm
    /// </summary>
    public interface ITDMSFile : ITDMSNode, IEnumerable<ITDMSChannelGroup>, IDisposable
    {
        TDMSFileInfo FileInfo { get; }

        void Save();

        void Open(string filePath);
        void Open(TDMSFileInfo fileInfo);
        void Create(string filePath, string fileType, string name, string description, string title, string author);
        void Create(TDMSFileInfo fileInfo);
        void Close();


        ITDMSChannelGroup Add(string groupName, string description = "");



        void Clear();

        bool Contains(string groupName);

        bool Remove(string groupName);

        void RemoveAt(int index);

        ITDMSChannelGroup this[int index] { get; set; }
        ITDMSChannelGroup this[string groupName] { get; set; }
    }
}