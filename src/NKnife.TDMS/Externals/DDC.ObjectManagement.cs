﻿using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        /// <summary>
        /// 创建一个新的TDMS文件。<br/>
        /// 推荐使用最新版本的TDM流文件格式(DDC_FILE_TYPE_TDM_STREAMING)创建.tdms文件。<br/>
        /// 注意：如果您在filePath参数中指定的文件扩展名是.tdm或.tdms，那么您可以传递NULL或空字符串来自动使用文件扩展名来指定文件类型。<br/>
        /// 但是如果指定了文件类型，那么必须和filePath参数指定的文件扩展名是匹配的。<br/>
        /// 通过传递DDC_FILE_TYPE_TDM来创建一个.tdm文件。通过传递DDC_FILE_TYPE_TDMS来创建.tdms文件。
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileType">
        /// 要创建的文件类型。<br/>
        /// 传递DDC_FILE_TYPE_TDM_STREAMING将使用安装支持的最新版本的TDM流文件格式创建一个.tdms文件。<br/>
        /// 通过传递DDC_FILE_TYPE_TDM来创建一个.tdm文件。通过传递DDC_FILE_TYPE_TDMS来创建.tdms文件。<br/>
        /// 如果您在filePath参数中指定的文件扩展名是.tdm或.tdms，那么您可以传递NULL或空字符串来自动使用文件扩展名来指定文件类型。
        /// </param>
        /// <param name="name">TDMS文件中的name属性</param>
        /// <param name="description">TDMS文件中的description属性</param>
        /// <param name="title">TDMS文件中的title属性</param>
        /// <param name="author">TDMS文件中的author属性</param>
        /// <param name="file">TDMS文件的句柄</param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "DDC_CreateFile", CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFile(string filePath,
                                            string fileType,
                                            string name,
                                            string description,
                                            string title,
                                            string author,
                                            out IntPtr file);

        [DllImport(DLL, EntryPoint = "DDC_AddChannelGroup", CallingConvention = CallingConvention.StdCall)]
        public static extern int AddChannelGroup(IntPtr file,
                                                 string name,
                                                 string description,
                                                 out IntPtr channelGroup);

        [DllImport(DLL, EntryPoint = "DDC_AddChannel", CallingConvention = CallingConvention.StdCall)]
        public static extern int AddChannel(IntPtr channelGroup,
                                            TDMSDataType dataType,
                                            string name,
                                            string description,
                                            string unitString,
                                            out IntPtr channel);

        [DllImport(DLL, EntryPoint = "DDC_SaveFile", CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveFile(IntPtr file);

        [DllImport(DLL, EntryPoint = "DDC_CloseFile", CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseFile(IntPtr file);

        [DllImport(DLL, EntryPoint = "DDC_OpenFileEx", CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenFileEx(string filePath,
                                            string fileType,
                                            int readOnly,
                                            out IntPtr file);
    }
}