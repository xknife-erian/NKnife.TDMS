using System;
using System.Collections;
using System.Collections.Generic;

namespace NKnife.TDMS
{
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