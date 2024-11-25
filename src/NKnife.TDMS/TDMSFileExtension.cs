﻿using System;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;
using NKnife.TDMS.Externals;

namespace NKnife.TDMS
{
    public static class TDMSFileExtension
    {
        public static void SetLevelName(this ITDMSLevel level, string name)
        {
            level.CreateOrUpdateProperty(Constants.DDC_FILE_NAME, name);
        }

        public static void SetLevelDescription(this ITDMSLevel level, string name)
        {
            level.CreateOrUpdateProperty(Constants.DDC_FILE_DESCRIPTION, name);
        }

        public static void SetFileTitle(this ITDMSFile file, string name)
        {
            file.CreateOrUpdateProperty(Constants.DDC_FILE_TITLE, name);
        }

        public static void SetFileAuthor(this ITDMSFile file, string name)
        {
            file.CreateOrUpdateProperty(Constants.DDC_FILE_AUTHOR, name);
        }

        public static void SetFileDateTime(this ITDMSFile file, DateTime dateTime)
        {
            var tdms = (TDMSFile)file;
            var ptr  = tdms.GetPtr();
            var dt   = new TDMSDateTime(dateTime);
            DDC.SetFilePropertyTimestampComponents(ptr, Constants.DDC_FILE_DATETIME, dt.Year, dt.Month, dt.Day, dt.Hour,
                                                   dt.Second, dt.Minute, dt.MilliSecond);
        }
    }
}