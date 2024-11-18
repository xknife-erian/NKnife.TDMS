using System;
using System.IO;
using NKnife.TDMS.Common;

namespace NKnife.TDMS
{
    /// <summary>
    ///     TDMS文件的相关信息
    /// </summary>
    public class TDMSFileInfo
    {
        /// <summary>
        ///     TDMS文件的相关信息。本库统一使用TDMS文件格式。
        /// </summary>
        public TDMSFileInfo(string filePath)
        {
            if(string.IsNullOrEmpty(filePath))
                throw new TDMSErrorException("file path cannot null or empty.", new ArgumentNullException(nameof(filePath)));

            // 检查filePath是否是正确的文件与路径格式
            if(!Util.IsValidPath(filePath))
                throw new TDMSErrorException("file path is not in a correct format.", new ArgumentException(nameof(filePath)));

            FilePath = filePath;

            var ext = Path.GetExtension(filePath);

            if(string.IsNullOrEmpty(ext)
               || ext.TrimStart('.').ToUpper() != Constants.DDC_FILE_TYPE_TDM_STREAMING)
            { //如果没有后缀名或者后缀名不是TDMS格式，则自动添加“tdms”后缀名
                FilePath = $"{filePath}.{Constants.DDC_FILE_TYPE_TDM_STREAMING.ToLower()}";
            }
        }

        /// <summary>
        ///     TDMS文件的路径
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        ///     文件对象的类型。仅为DDC_FILE_TYPE_TDM_STREAMING格式。
        /// </summary>
        public string FileType => Constants.DDC_FILE_TYPE_TDM_STREAMING.ToLower(); //本库统一使用TDMS文件格式

        /// <summary>
        ///     文件对象的name属性值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     文件对象的description属性的值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     文件对象的title属性值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     文件对象的author属性值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        ///     文件对象的datetime属性值(通常是文件的创建时间)。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     信息中表达的文件是否存在
        /// </summary>
        public bool Exists => File.Exists(FilePath);
    }
}