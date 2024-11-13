using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    class Miscellaneous : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr DDC_GetLibraryErrorDescription(int errorCode);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern void DDC_FreeMemory(IntPtr memoryPointer);

        public static string GetLibraryErrorDescription(int errorCode)
        {
            IntPtr ptr = DDC_GetLibraryErrorDescription(errorCode);
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}