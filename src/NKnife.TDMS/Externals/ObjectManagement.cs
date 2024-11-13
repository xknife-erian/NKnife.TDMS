using System;
using System.Runtime.InteropServices;
using NKnife.TDMS.Common;

namespace NKnife.TDMS.Externals
{
    internal class ObjectManagement : DDC
    {
        [DllImport(Dll, EntryPoint = "DDC_CreateFile", CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFile(
            string filePath,
            string fileType,
            string name,
            string description,
            string title,
            string author,
            out IntPtr file);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AddChannelGroup(
            IntPtr file,
            string name,
            string description,
            out IntPtr channelGroup);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_AddChannel(
            IntPtr channelGroup,
            DataType dataType,
            string name,
            string description,
            string unitString,
            out IntPtr channel);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_SaveFile(IntPtr file);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CloseFile(IntPtr file);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_OpenFileEx(
            string filePath,
            string fileType,
            int readOnly,
            out IntPtr file);
    }
}
