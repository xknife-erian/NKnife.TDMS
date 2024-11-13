using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : BaseDDC
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_OpenFile")]
        public static extern int OpenFile(string filePath, string fileType, out IntPtr file);
    }
}