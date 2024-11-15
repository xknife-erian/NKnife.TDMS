using NKnife.TDMS.Common;

namespace NKnife.TDMS
{
    public interface ITDMSNode
    {
        /// <summary>
        ///     子项目的数量（File->Group->Channel->Data）
        /// </summary>
        public ulong ChildCount { get; }

        /// <summary>
        ///     设置属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        void SetProperty(string propertyName, string propertyValue);

        /// <summary>
        ///     获取属性
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="dataType">属性的类型</param>
        /// <returns>属性值获取是否成功，如成功，元数据包含属性值</returns>
        (bool Success, object PropertyValue) GetProperty(string propertyName, out TDMSDataType dataType);

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
    }
}