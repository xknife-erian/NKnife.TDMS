using System;
using System.Collections;
using NKnife.TDMS.Common;

namespace NKnife.TDMS
{
    public interface ITDMSChannelGroup : ITDMSNode, IDisposable
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

    public interface ITDMSChannel : ITDMSNode, IEnumerable, IDisposable
    {
        bool AddData<T>(T[] values) where T : struct;

        /// <summary>
        /// 获取指定通道中的数据值
        /// </summary>
        /// <typeparam name="T">待获取的数据的数据类型</typeparam>
        T[] GetDataValues<T>(uint startIndex, uint length);
    }
}