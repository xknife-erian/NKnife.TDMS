using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NKnife.TDMS.Common
{
    internal static class Util
    {
        public static TDMSDataType GetTDMSDataType<T>()
        {
            var dataType = Common.TDMSDataType.String;
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

        public static TDMSDataType ToDataType(this Type type)
        {
            if(type == typeof(string))
            {
                return Common.TDMSDataType.String;
            }
            else if(type == typeof(byte))
            {
                return Common.TDMSDataType.UInt8;
            }
            else if(type == typeof(short))
            {
                return Common.TDMSDataType.Int16;
            }
            else if(type == typeof(int))
            {
                return Common.TDMSDataType.Int32;
            }
            else if(type == typeof(float))
            {
                return Common.TDMSDataType.Float;
            }
            else if(type == typeof(double))
            {
                return Common.TDMSDataType.Double;
            }
            else if(type == typeof(DateTime))
            {
                return Common.TDMSDataType.Timestamp;
            }
            else
            {
                throw new TDMSErrorException("Unsupported data type.");
            }
        }

        public static DatetimeFactor Factoring(this DateTime[] values)
        {
            var df = new DatetimeFactor(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                var t = (DateTime)(object)values[i];
                df.Years[i]        = (uint)t.Year;
                df.Months[i]       = (uint)t.Month;
                df.Days[i]         = (uint)t.Day;
                df.Hours[i]        = (uint)t.Hour;
                df.Minutes[i]      = (uint)t.Minute;
                df.Seconds[i]      = (uint)t.Second;
                df.Milliseconds[i] = (double)t.Millisecond;
            }

            return df;
        }
    }

    /// <summary>
    /// 用于存储DateTime数组的各个时间分量
    /// </summary>
    class DatetimeFactor(int count)
    {
        public uint[] Years { get; } = new uint[count];
        public uint[] Months { get; } = new uint[count];
        public uint[] Days { get; } = new uint[count];
        public uint[] Hours { get; } = new uint[count];
        public uint[] Minutes { get; } = new uint[count];
        public uint[] Seconds { get; } = new uint[count];
        public double[] Milliseconds { get; } = new double[count];
        public uint[] WeekDays { get; } = new uint[count];

        public DateTime[] ToDateTimeArray()
        {
            var result = new DateTime[Years.Length];
            for (int i = 0; i < Years.Length; i++)
            {
                result[i] = new DateTime((int)Years[i], (int)Months[i], (int)Days[i], (int)Hours[i], (int)Minutes[i], (int)Seconds[i], (int)Milliseconds[i]);
            }
            return result;
        }
    }
}
