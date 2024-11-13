using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : BaseDDC
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_GetLibraryErrorDescription")]
        public static extern IntPtr GetLibraryErrorDescription(int errorCode);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_FreeMemory")]
        public static extern void FreeMemory(IntPtr memoryPointer);

        public static string GetErrorDescription(int errorCode)
        {
            var ptr = GetLibraryErrorDescription(errorCode);

            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}