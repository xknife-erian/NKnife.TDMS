using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    class DataRetrievalEnumeration : DDC
    {
        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetNumChannelGroups(
            IntPtr file,
            out uint numChannelGroups);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannelGroups(
            IntPtr file,
            IntPtr[] channelGroupsBuf,
            UIntPtr numChannelGroups);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetNumChannels(
            IntPtr channelGroup,
            out uint numChannels);

        [DllImport(Dll, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDC_GetChannels(
            IntPtr channelGroup,
            IntPtr[] channelsBuf,
            UIntPtr numChannels);
    }
}