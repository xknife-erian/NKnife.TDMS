using System;

namespace NKnife.TDMS
{
    public interface ITDMSLevel : ITDMSNodePropertyOperation, IDisposable
    {
        string Name { get; }
        string Description { get; }

        /// <summary>
        /// 关闭当前<see cref="ITDMSLevel"/>自身的访问(读写)能力。
        /// </summary>
        bool Close();

        /// <summary>
        ///     子项目的数量（File->Group->Channel->Data）
        /// </summary>
        public ulong ChildCount { get; }

        /// <summary>
        ///     清空当前节点的所有子项目数据。
        /// </summary>
        bool Clear();

        /// <summary>
        /// 判断是否包含指定名称的子项目。
        /// </summary>
        /// <param name="levelName">组名称。</param>
        /// <returns>如果包含指定名称的子项目，则为 true；否则为 false。</returns>
        bool Contains(string levelName);

        /// <summary>
        /// 尝试获取指定名称的子项目。
        /// </summary>
        /// <param name="levelName">子项目的名称</param>
        /// <param name="level">当存在时，out指定名称的子项目</param>
        /// <returns>是否存在指定名称的子项目</returns>
        bool TryGetItem(string levelName, out ITDMSLevel level);

        /// <summary>
        /// 移除指定名称的子项目。
        /// </summary>
        /// <param name="levelName">组名称。</param>
        /// <returns>如果成功移除指定名称的子项目，则为 true；否则为 false。</returns>
        bool Remove(string levelName);

        /// <summary>
        /// 移除 TDMS 文件中指定索引位置的子项目。
        /// </summary>
        /// <param name="index">索引位置。</param>
        bool RemoveAt(int index);
    }
}