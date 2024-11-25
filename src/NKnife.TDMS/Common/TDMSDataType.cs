﻿namespace NKnife.TDMS.Common
{
    public enum TDMSDataType
    {
        UInt8 = 5,
        Int16 = 2,
        Int32 = 3,
        Float = 9,
        Double = 10,
        String = 23,

        /// <summary>
        ///     timestamp (Year/Month/Day/Hour/Minute/Second/Millisecond components)
        /// </summary>
        Timestamp = 30,

        UnDefine = 255,
    }
}