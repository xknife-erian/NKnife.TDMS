using FluentAssertions;
using NKnife.TDMS;

namespace UnitTests.NKnife.TDMS
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var path = Directory.GetCurrentDirectory();
            var fileInfo = new TDMSFileInfo()
            {
                FilePath    = Path.Combine(path, "~test.tdms"),
                Name        = "TestName",
                Description = "TestDescription",
                Title       = "TestTitle",
                Author      = "TestAuthor"
            };

            if(fileInfo.Exists)
            {
                var files = Directory.GetFiles(path, "~test.*");
                foreach (var fileToDelete in files)
                    File.Delete(fileToDelete);
            }

            using var file = new TDMSFile(fileInfo);

            fileInfo.Exists.Should().BeTrue();
        }
    }
}