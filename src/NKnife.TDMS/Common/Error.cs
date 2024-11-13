namespace NKnife.TDMS.Common
{
    enum Error : int
    {
        /// <summary>
        /// No error
        /// </summary>
        NoError = 0,

        /// <summary>
        /// Error begin
        /// </summary>
        ErrorBegin = -6201,

        /// <summary>
        /// The library could not allocate memory.
        /// </summary>
        OutOfMemory = -6201,

        /// <summary>
        /// An invalid argument was passed to the library.
        /// </summary>
        InvalidArgument = -6202,

        /// <summary>
        /// An invalid data type was passed to the library.
        /// </summary>
        InvalidDataType = -6203,

        /// <summary>
        /// An unexpected error occurred in the library.
        /// </summary>
        UnexpectedError = -6204,

        /// <summary>
        /// The USI engine could not be loaded.
        /// </summary>
        UsiCouldNotBeLoaded = -6205,

        /// <summary>
        /// An invalid file handle was passed to the library.
        /// </summary>
        InvalidFileHandle = -6206,

        /// <summary>
        /// An invalid channel group handle was passed to the library.
        /// </summary>
        InvalidChannelGroupHandle = -6207,

        /// <summary>
        /// An invalid channel handle was passed to the library.
        /// </summary>
        InvalidChannelHandle = -6208,

        /// <summary>
        /// The file passed to the library does not exist.
        /// </summary>
        FileDoesNotExist = -6209,

        /// <summary>
        /// The file passed to the library is read only and cannot be modified.
        /// </summary>
        CannotWriteToReadOnlyFile = -6210,

        /// <summary>
        /// The storage could not be opened.
        /// </summary>
        StorageCouldNotBeOpened = -6211,

        /// <summary>
        /// The file passed to the library already exists and cannot be created.
        /// </summary>
        FileAlreadyExists = -6212,

        /// <summary>
        /// The property passed to the library does not exist.
        /// </summary>
        PropertyDoesNotExist = -6213,

        /// <summary>
        /// The property passed to the library does not have a value.
        /// </summary>
        PropertyDoesNotContainData = -6214,

        /// <summary>
        /// The value of the property passed to the library is an array and not a scalar.
        /// </summary>
        PropertyIsNotAScalar = -6215,

        /// <summary>
        /// The object type passed to the library does not exist.
        /// </summary>
        DataObjectTypeNotFound = -6216,

        /// <summary>
        /// The current implementation does not support this operation.
        /// </summary>
        NotImplemented = -6217,

        /// <summary>
        /// The file could not be saved.
        /// </summary>
        CouldNotSaveFile = -6218,

        /// <summary>
        /// The request would exceed the maximum number of data values for a channel.
        /// </summary>
        MaximumNumberOfDataValuesExceeded = -6219,

        /// <summary>
        /// An invalid channel name was passed to the library.
        /// </summary>
        InvalidChannelName = -6220,

        /// <summary>
        /// The channel group already contains a channel with this name.
        /// </summary>
        DuplicateChannelName = -6221,

        /// <summary>
        /// The current implementation does not support this data type.
        /// </summary>
        DataTypeNotSupported = -6222,

        /// <summary>
        /// File access denied.
        /// </summary>
        FileAccessDenied = -6224,

        /// <summary>
        /// The specified time value is invalid.
        /// </summary>
        InvalidTimeValue = -6225,

        /// <summary>
        /// The replace operation is not supported on data that has already been saved to a TDM Streaming file.
        /// </summary>
        ReplaceNotSupportedForSavedTDMSData = -6226,

        /// <summary>
        /// The data type of the property does not match the expected data type.
        /// </summary>
        PropertyDataTypeMismatch = -6227,

        /// <summary>
        /// The data type of the channel does not match the expected data type.
        /// </summary>
        ChannelDataTypeMismatch = -6228,

        /// <summary>
        /// Error end
        /// </summary>
        ErrorEnd = -6228,

        /// <summary>
        /// Force size to 32 bits
        /// </summary>
        ErrorForceSizeTo32Bits = unchecked((int)0xffffffff)
    }
}