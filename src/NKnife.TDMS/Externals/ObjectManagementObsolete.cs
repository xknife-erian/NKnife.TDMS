using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal class ObjectManagementObsolete : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_OpenFile(string filePath, string fileType, out IntPtr file);
    }
}