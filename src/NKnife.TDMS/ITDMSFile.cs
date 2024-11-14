using System;
using System.Collections.Generic;
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
    ///     https://www.ni.com/en/support/documentation/supplemental/06/the-ni-tdms-file-format.html <br/>
    ///     https://www.ni.com/docs/en-US/bundle/labwindows-cvi/page/cvi/libref/cvitdmslibrary.htm
    /// </summary>
    public interface ITDMSFile : IDisposable
    {
        TDMSFileInfo FileInfo { get; set; }
        
        void Save();
        
        void Load(string filePath);
        
        void Close();

        public int Count { get; set; }

        ITDMSChannelGroup Add(string groupName, string description = "");

        void Clear();

        bool Contains(string groupName);

        bool Remove(string groupName);
        
        void RemoveAt(int index);

        ITDMSChannelGroup this[int index] { get; set; }
        ITDMSChannelGroup this[string groupName] { get; set; }
    }

    public interface ITDMSChannelGroup : IDisposable
    {
        /// <summary>
        /// 向通道组添加新通道。一个有效的通道组应该包含一个或多个通道。
        /// </summary>
        /// <param name="dataType">新建通道的数据类型。此通道的数据必须与指定的数据类型匹配。</param>
        /// <param name="channelName">通道对象的name属性的值。该属性存储在通道中。</param>
        /// <param name="unit">通道对象的单位，用字符串表达。该属性存储在通道中</param>
        /// <param name="description">通道对象的description属性值。该属性存储在通道中。</param>
        /// <returns>新通道</returns>
        public ITDMSChannel AddChannel(TDMSDataType dataType, string channelName, string unit, string description);

        ITDMSChannel GetChannel(int i);
    }

    public interface ITDMSChannel : IDisposable
    {
        bool AddData<T>(T[] values) where T : struct;

        /// <summary>
        /// 获取指定通道中的数据值
        /// </summary>
        /// <typeparam name="T">待获取的数据的数据类型</typeparam>
        T[] GetDataValues<T>(uint startIndex, uint length);

        ulong Count { get; }
    }
}