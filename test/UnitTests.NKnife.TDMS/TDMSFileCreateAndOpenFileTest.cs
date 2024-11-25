using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileCreateAndOpenFileTest
    {
        [Fact(DisplayName = "检查属性是否存在")]
        public void PropertyExists_Test()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            string propertyName = "TestProperty";

            // Act
            bool exists = tdmsFile.PropertyExists(propertyName);

            // Assert
            exists.Should().BeFalse("因为属性尚未被添加");
        }

        [Fact(DisplayName = "文件名tdm。自动增加tdms后缀名，创建数据文件成功。")]
        public void CreateAndOpenFile_Test01()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using (var tdmsFile = new TDMSFile())
            {
                var success = tdmsFile.Create(fileInfo);
                success.Should().BeTrue();
            }

            using (var tdmsFile = TDMSDataBuilder.OpenExistingFile(fileInfo))
            {
                tdmsFile.Should().NotBeNull();
                var ps = tdmsFile.GetDefaultProperties();
                ps.Count.Should().Be(5);
            }
        }

        [Fact(DisplayName = "文件名tdms。创建数据文件成功。")]
        public void CreateAndOpenFile_Test02()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using (var tdmsFile = new TDMSFile())
            {
                var success = tdmsFile.Create(fileInfo);
                success.Should().BeTrue();
            }

            using (var tdmsFile = TDMSDataBuilder.OpenExistingFile(fileInfo))
            {
                tdmsFile.Should().NotBeNull();
                var ps = tdmsFile.GetDefaultProperties();
                ps.Count.Should().Be(5);
            }
        }

        [Fact(DisplayName = "文件名中后缀名其他。自动增加tdms后缀名，创建数据文件成功。")]
        public void CreateAndOpenFile_Test03()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileName = $"~tdms~{nameof(CreateAndOpenFile_Test03)}~tdms03.zip";
            var       fileInfo = new TDMSFileInfo(fileName);

            using (var tdmsFile = new TDMSFile())
            {
                var success = tdmsFile.Create(fileInfo);
                success.Should().BeTrue();
            }

            using (var tdmsFile = TDMSDataBuilder.OpenExistingFile(fileInfo))
            {
                tdmsFile.Should().NotBeNull();
                var ps = tdmsFile.GetDefaultProperties();
                ps.Count.Should().Be(5);
            }

            context.CleanFiles();
        }

        [Fact(DisplayName = "文件名中无后缀名。自动增加tdms后缀名，创建数据文件成功。")]
        public void CreateAndOpenFile_Test04()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileName = $"~tdms~{nameof(CreateAndOpenFile_Test04)}~tdms04";
            var       fileInfo = new TDMSFileInfo(fileName);

            using (var tdmsFile = new TDMSFile())
            {
                var success = tdmsFile.Create(fileInfo);
                success.Should().BeTrue();
            }

            using (var tdmsFile = TDMSDataBuilder.OpenExistingFile(fileInfo))
            {
                tdmsFile.Should().NotBeNull();
                var ps = tdmsFile.GetDefaultProperties();
                ps.Count.Should().Be(5);
            }

            context.CleanFiles();
        }
    }
}