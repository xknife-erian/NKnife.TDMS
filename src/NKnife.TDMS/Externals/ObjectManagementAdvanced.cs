using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal class ObjectManagementAdvanced : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_RemoveChannelGroup(IntPtr channelGroup);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_RemoveChannel(IntPtr channel);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CloseChannelGroup(IntPtr channelGroup);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_CloseChannel(IntPtr channel);
    }
}
