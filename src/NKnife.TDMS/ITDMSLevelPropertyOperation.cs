﻿using NKnife.TDMS.Common;

namespace NKnife.TDMS
{
    /// <summary>
    /// 自定义标量属性的操作接口
    /// </summary>
    public interface ITDMSLevelPropertyOperation
    {
        /// <summary>
        ///     增加属性（如果属性已经存在，则更新属性值）
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        void CreateOrUpdateProperty<T>(string propertyName, T propertyValue);

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
        /// <param name="propertyValue">输出：属性值</param>
        /// <returns>属性值获取是否成功，如成功，元数据包含属性值</returns>
        bool TryGetProperty<T>(string propertyName, out T propertyValue);
    }
}