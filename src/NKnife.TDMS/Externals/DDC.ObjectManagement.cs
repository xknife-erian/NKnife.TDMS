using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
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