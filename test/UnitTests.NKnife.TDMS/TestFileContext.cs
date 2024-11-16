using NKnife.TDMS;
using NKnife.TDMS.Common;

public class TestFileContext : IDisposable
{
    private readonly string _testFileName = $"~tdms{Guid.NewGuid().ToString("N").Substring(0,8)}";

    public void Dispose()
    {
        CleanFiles();
    }

    public void CleanFiles()
    {
        var fileToDelete = GetTestFileName(_testFileName);
        File.Delete(fileToDelete);
    }

    private static string GetTestFileName(string basicName)
    {
        return $"{basicName}.tmd";
    }

    public TDMSFileInfo CreateTestFile()
    {
        var path = Directory.GetCurrentDirectory();
        var fileInfo = new TDMSFileInfo(Path.Combine(path, GetTestFileName(_testFileName)))
        {
            Name        = "+TestName+",
            FileType    = Constants.DDC_FILE_TYPE_TDM,
            Description = "+TestÖÐÎÄDescription+",
            Title       = "+TestTitle+",
            Author      = "+TestAuthor+",
            DateTime    = new DateTime(2222, 2, 2, 2, 2, 2)
        };

        return fileInfo;
    }
}