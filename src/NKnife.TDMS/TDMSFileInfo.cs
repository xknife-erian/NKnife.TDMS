using System;
using System.IO;

namespace NKnife.TDMS
{
    /// <summary>
    ///     TDMS文件的相关信息
    /// </summary>
    public record TDMSFileInfo(string FilePath)
    {
        public string FilePath { get; private set; } = FilePath;
        public string FileType { get; set; } = string.Empty;

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

        public bool Exists => File.Exists(FilePath);
    }
}