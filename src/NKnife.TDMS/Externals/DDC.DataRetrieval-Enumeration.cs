using System;
using System.Runtime.InteropServices;

namespace NKnife.TDMS.Externals
{
    internal partial class DDC : DIAdemConnectivity
    {
        /// <summary>
        /// 获取指定文件中的通道组数量。
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="numChannelGroups">指定文件中通道组的数量</param>
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetNumChannelGroups")]
        public static extern int CountChannelGroups(IntPtr file, out uint numChannelGroups);

        /// <summary>
        /// 获取指定文件中的通道组。
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="channelGroupsBuffer">接收指定文件的通道组的数组缓冲区。</param>
        /// <param name="numberOfChannelGroups">要复制到缓冲区的通道组数量。</param>
        /// <returns>是否成功。除非另有说明，否则零表示执行成功，负数表示错误代码。</returns>
        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetChannelGroups")]
        public static extern int GetChannelGroups(IntPtr file, IntPtr[] channelGroupsBuffer, UIntPtr numberOfChannelGroups);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetNumChannels")]
        public static extern int CountChannels(IntPtr channelGroup, out uint numChannels);

        [DllImport(DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetChannels")]
        public static extern int GetChannels(IntPtr channelGroup, IntPtr[] channelsBuf, UIntPtr numChannels);
    }
}