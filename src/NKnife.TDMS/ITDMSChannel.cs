using System;

namespace NKnife.TDMS
{
    public interface ITDMSChannel : ITDMSLevel, IDisposable
    {
        /// <summary>
        ///     获取通道数据的单位，例如：V、A、m/s等。
        /// </summary>
        string Unit { get; }

        /// <summary>
        ///     向通道内设置数据。该方法将会从索引为0的位置开始覆盖原有数据。
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="values">数据集合</param>
        /// <returns>是否执行成功</returns>
        bool SetData<T>(T[] values) where T : struct;

        /// <summary>
        ///     向通道内追加数据。
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="values">数据集合</param>
        /// <returns>是否执行成功</returns>
        bool AppendData<T>(T[] values) where T : struct;

        /// <summary>
        ///     向通道内更新指定位置的数据。
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="index">
        ///     要更新的通道中第一个数据值的从零开始的索引。从该索引开始的数据值将被values参数指定的值所更新。
        /// </param>
        /// <param name="values">数据集合</param>
        /// <returns>是否执行成功</returns>
        bool UpdateData<T>(int index, T[] values) where T : struct;

        /// <summary>
        ///     获取指定通道中从指定的某一个位置开始共计指定的数量的数据值
        /// </summary>
        /// <typeparam name="T">待获取的数据的数据类型</typeparam>
        /// <param name="startIndex">指定的第一个数据值的从零开始的索引</param>
        /// <param name="length">指定的待获取数据的数量</param>
        T[] GetDataValues<T>(uint startIndex, uint length);
    }
}