using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;
// ReSharper disable InconsistentNaming

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileTest_File_1 : IClassFixture<TestFileContext>
    {
        private readonly TestFileContext _context;

        public TDMSFileTest_File_1(TestFileContext context)
        {
            _context = context;
            context.CleanFiles();
        }

        [Fact(DisplayName = "检查属性是否存在")]
        public void PropertyExists_Test()
        {
            // Arrange
            var       fileInfo = _context.CreateTestFile();
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
            var fileName = "~tdms01.tdm";
            var fileInfo = new TDMSFileInfo(fileName);

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

            _context.CleanFiles();
        }

        [Fact(DisplayName = "文件名tdms。创建数据文件成功。")]
        public void CreateAndOpenFile_Test02()
        {
            // Arrange
            var fileName = "~tdms02.tdms";
            var fileInfo = new TDMSFileInfo(fileName);

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

            _context.CleanFiles();
        }

        [Fact(DisplayName = "文件名中后缀名其他。自动增加tdms后缀名，创建数据文件成功。")]
        public void CreateAndOpenFile_Test03()
        {
            // Arrange
            var fileName = "~tdms03.zip";
            var fileInfo = new TDMSFileInfo(fileName);

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

            _context.CleanFiles();
        }

        [Fact(DisplayName = "文件名中无后缀名。自动增加tdms后缀名，创建数据文件成功。")]
        public void CreateAndOpenFile_Test04()
        {
            // Arrange
            var fileName = "~tdms04";
            var fileInfo = new TDMSFileInfo(fileName);

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

            _context.CleanFiles();
        }

    }
}