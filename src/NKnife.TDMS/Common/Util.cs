using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NKnife.TDMS.Common
{
    internal class Util
    {
        public static TDMSDataType GetTDMSDataType<T>()
        {
            var dataType  = Common.TDMSDataType.String;
            var inputType = typeof(T);
            if (inputType == typeof(byte))
            {
                dataType = Common.TDMSDataType.UInt8;
            }
            else if (inputType == typeof(short))
            {
                dataType = Common.TDMSDataType.Int16;
            }
            else if (inputType == typeof(int))
            {
                dataType = Common.TDMSDataType.Int32;
            }
            else if (inputType == typeof(float))
            {
                dataType = Common.TDMSDataType.Float;
            }
            else if (inputType == typeof(double))
            {
                dataType = Common.TDMSDataType.Double;
            }
            else if (inputType == typeof(DateTime))
            {
                dataType = Common.TDMSDataType.Timestamp;
            }
            else
            {
                throw new TDMSErrorException("Unsupported data type.");
            }

            return dataType;
        }

        public static bool IsValidPath(string path)
        {
            // 检查是否为空或只包含空格
            if (string.IsNullOrWhiteSpace(path))
                return false;

            try
            {
                // 获取路径中的非法字符数组
                char[] invalidChars = Path.GetInvalidPathChars();
                // 检查路径中是否包含非法字符
                foreach (char c in invalidChars)
                {
                    if (path.IndexOf(c) >= 0)
                        return false;
                }

                // 尝试获取路径的标准化形式
                string normalizedPath = Path.GetFullPath(path);

                // 检查路径长度是否超过最大限制
                if (normalizedPath.Length > 248 || Path.GetFileName(normalizedPath).Length > 260)
                    return false;

                // 如果以上条件都满足，则认为路径有效
                return true;
            }
            catch
            {
                // 如果在处理过程中抛出异常，如路径太长等，也视为无效路径
                return false;
            }
        }
    }
}
