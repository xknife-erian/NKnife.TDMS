// ReSharper disable InconsistentNaming

namespace NKnife.TDMS.Common
{
    internal static class Constants
    {
        // File type constants
        public const string DDC_FILE_TYPE_TDM = "TDM";
        public const string DDC_FILE_TYPE_TDM_STREAMING = "TDMS";

        // File property constants
        public const string DDC_FILE_NAME = "name";               // Name
        public const string DDC_FILE_DESCRIPTION = "description"; // Description
        public const string DDC_FILE_TITLE = "title";             // Title
        public const string DDC_FILE_AUTHOR = "author";           // Author
        public const string DDC_FILE_DATETIME = "datetime";       // Date/Time

        // ChannelGroup property constants
        public const string DDC_CHANNEL_GROUP_NAME = "name";               // Name
        public const string DDC_CHANNEL_GROUP_DESCRIPTION = "description"; // Description

        // Channel property constants
        public const string DDC_CHANNEL_NAME = "name";               // Name
        public const string DDC_CHANNEL_DESCRIPTION = "description"; // Description
        public const string DDC_CHANNEL_UNIT_STRING = "unit_string"; // Unit String
        public const string DDC_CHANNEL_MINIMUM = "minimum";         // Minimum
        public const string DDC_CHANNEL_MAXIMUM = "maximum";         // Maximum
    }
}