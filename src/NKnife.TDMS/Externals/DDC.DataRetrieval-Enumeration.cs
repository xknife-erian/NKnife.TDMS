using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : BaseDDC
    {
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetNumChannelGroups")]
        public static extern int GetNumChannelGroups(IntPtr file, out uint numChannelGroups);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetChannelGroups")]
        public static extern int GetChannelGroups(IntPtr file, IntPtr[] channelGroupsBuf, UIntPtr numChannelGroups);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetNumChannels")]
        public static extern int GetNumChannels(IntPtr channelGroup, out uint numChannels);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetChannels")]
        public static extern int GetChannels(IntPtr channelGroup, IntPtr[] channelsBuf, UIntPtr numChannels);
    }
}