using System;
using System.IO;
using System.Text.RegularExpressions;
using NKnife.TDMS.Common;

namespace NKnife.TDMS
{
    /// <summary>
    ///     TDMS文件的相关信息
    /// </summary>
    public class TDMSFileInfo
    {
        /// <summary>
        ///     TDMS文件的相关信息
        /// </summary>
        public TDMSFileInfo(string filePath, string fileType = Constants.DDC_FILE_TYPE_TDM_STREAMING)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new TDMSErrorException("file path cannot null or empty.", new ArgumentNullException(nameof(filePath)));

            // 检查filePath是否是正确的文件与路径格式
            if (!Util.IsValidPath(filePath))
                throw new TDMSErrorException("file path is not in a correct format.", new ArgumentException(nameof(filePath)));

            if (string.IsNullOrEmpty(fileType))
                throw new TDMSErrorException("file path cannot null or empty.", new ArgumentNullException(nameof(filePath)));

            this.FilePath = filePath;
            this.FileType = fileType;
            var ext = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(ext))
            {
                this.FilePath = $"{filePath}.{fileType}";
            }
        }

        /// <summary>
        /// TDMS文件的路径
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        ///    文件对象的类型。默认值为DDC_FILE_TYPE_TDM_STREAMING，以方便创建最新版本的TDM流文件格式(DDC_FILE_TYPE_TDM_STREAMING)。
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        ///     文件对象的name属性值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     文件对象的description属性的值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     文件对象的title属性值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     文件对象的author属性值。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///    文件对象的datetime属性值(通常是文件的创建时间)。该属性存储在文件中，可以通过DDC_SetFileProperty和DDC_GetFileProperty函数访问。
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 信息中表达的文件是否存在
        /// </summary>
        public bool Exists => File.Exists(FilePath);
    }
}