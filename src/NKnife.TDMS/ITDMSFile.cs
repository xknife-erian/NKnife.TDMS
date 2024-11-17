using System;
using System.Collections.Generic;
using System.ComponentModel;
using NKnife.TDMS.Common;

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
    public interface ITDMSFile : ITDMSNode, IDisposable
    {
        /// <summary>
        /// 获取 TDMS 文件的信息。
        /// </summary>
        TDMSFileInfo FileInfo { get; }

        /// <summary>
        /// 保存 TDMS 文件。
        /// </summary>
        bool Save();

        /// <summary>
        /// 打开指定路径的 TDMS 文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        bool Open(string filePath);

        /// <summary>
        /// 通过指定的 TDMS 文件信息打开文件。
        /// </summary>
        /// <param name="fileInfo">文件信息。</param>
        bool Open(TDMSFileInfo fileInfo);

        /// <summary>
        /// 创建 TDMS 文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="fileType">文件类型。</param>
        /// <param name="name">文件名称。</param>
        /// <param name="description">文件描述。</param>
        /// <param name="title">文件标题。</param>
        /// <param name="author">文件作者。</param>
        bool Create(string filePath, string fileType, string name, string description, string title, string author);

        /// <summary>
        /// 创建 TDMS 文件。
        /// </summary>
        /// <param name="fileInfo">文件信息。</param>
        bool Create(TDMSFileInfo fileInfo);

        /// <summary>
        /// 关闭 TDMS 文件。
        /// </summary>
        bool Close();

        /// <summary>
        /// 添加指定名称和描述的通道组。
        /// </summary>
        /// <param name="groupName">组名称。</param>
        /// <param name="description">组描述。</param>
        /// <returns>添加的通道组。</returns>
        ITDMSChannelGroup Add(string groupName, string description = "");

        /// <summary>
        /// 获取或设置指定索引位置的通道组。
        /// </summary>
        /// <param name="index">索引位置。</param>
        /// <returns>指定索引位置的通道组。</returns>
        ITDMSChannelGroup this[int index] { get; }

        /// <summary>
        /// 获取或设置指定名称的通道组。
        /// </summary>
        /// <param name="groupName">组名称。</param>
        /// <returns>指定名称的通道组。</returns>
        ITDMSChannelGroup this[string groupName] { get; }

        /// <summary>
        /// 获取 TDMS 文件中的默认属性值。<br/>
        /// - name<br/>
        /// - description<br/>
        /// - title<br/>
        /// - author<br/>
        /// - datetime
        /// </summary>
        /// <returns>属性值字典</returns>
        IDictionary<string, string> GetDefaultProperties();

        /// <summary>
        /// 判断 TDMS 文件是否包含指定名称的通道组。
        /// </summary>
        /// <param name="groupName">组名称。</param>
        /// <returns>如果包含指定名称的通道组，则为 true；否则为 false。</returns>
        bool Contains(string groupName);

        /// <summary>
        /// 移除 TDMS 文件中指定名称的通道组。
        /// </summary>
        /// <param name="groupName">组名称。</param>
        /// <returns>如果成功移除通道组，则为 true；否则为 false。</returns>
        bool Remove(string groupName);

        /// <summary>
        /// 移除 TDMS 文件中指定索引位置的通道组。
        /// </summary>
        /// <param name="index">索引位置。</param>
        bool RemoveAt(int index);
    }
}