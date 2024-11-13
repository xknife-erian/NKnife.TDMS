using System.IO;
using NKnife.TDMS;
using Xunit;

public class TestFileContext : IDisposable
{
    public const string TEST_FILE_NAME = "~xunitTest";
    public void CleanFiles()
    {
        var path = Directory.GetCurrentDirectory();
        var files = Directory.GetFiles(path, $"{TEST_FILE_NAME}.*");
        foreach (var fileToDelete in files)
            File.Delete(fileToDelete);
    }

    public void Dispose()
    {
        CleanFiles();
    }

    public static TDMSFileInfo CreateTestFile()
    {
        var path = Directory.GetCurrentDirectory();
        var fileInfo = new TDMSFileInfo()
        {
            FilePath    = Path.Combine(path, $"{TEST_FILE_NAME}.tdms"),
            Name        = "TestName",
            Description = "TestDescription",
            Title       = "TestTitle",
            Author      = "TestAuthor"
        };
        return fileInfo;
    }
}


