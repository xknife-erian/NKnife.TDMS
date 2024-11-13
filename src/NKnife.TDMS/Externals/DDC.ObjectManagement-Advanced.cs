using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_RemoveChannelGroup")]
        public static extern int RemoveChannelGroup(IntPtr channelGroup);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_RemoveChannel")]
        public static extern int RemoveChannel(IntPtr channel);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CloseChannelGroup")]
        public static extern int CloseChannelGroup(IntPtr channelGroup);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "DDC_CloseChannel")]
        public static extern int CloseChannel(IntPtr channel);
    }
}