using NKnife.TDMS.Common;

namespace NKnife.TDMS
{
    public interface ITDMSNode
    {
        string Name { get; }
        string Description { get; }

        /// <summary>
        ///     子项目的数量（File->Group->Channel->Data）
        /// </summary>
        public ulong ChildCount { get; }

        /// <summary>
        ///     清空当前节点的所有子项目数据。
        /// </summary>
        bool Clear();

        /// <summary>
        ///     增加属性（如果属性已经存在，则更新属性值）
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        void AddOrUpdateProperty<T>(string propertyName, T propertyValue);

        /// <summary>
        ///     属性是否存在
        /// </summary>
        /// <param name="propertyName">属性名</param>
        bool PropertyExists(string propertyName);

        /// <summary>
        ///     获取所有的属性名
        /// </summary>
        /// <returns>当前节点保存的所有属性的名称集合</returns>
        string[] GetPropertyNames();

        /// <summary>
        ///     获取属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="dataType">属性的类型</param>
        /// <returns>属性值获取是否成功，如成功，元数据包含属性值</returns>
        (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType);
    }
}