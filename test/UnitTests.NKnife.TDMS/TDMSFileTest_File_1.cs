using FluentAssertions;
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

        [Fact(DisplayName = "检查设置不同的FileType时的文件创建、并打开该文件")]
        public void CreateAndOpenFile_WithDifferentFileTypes_Test()
        {
            // Arrange
            var fileTypes = new[] { Constants.DDC_FILE_TYPE_TDM, Constants.DDC_FILE_TYPE_TDM_STREAMING, "TypeA" };

            foreach (var fileType in fileTypes)
            {
                var fileInfo = _context.CreateTestFile();
                fileInfo.FileType = fileType;

                using (var tdmsFile = new TDMSFile())
                {
                    var success = tdmsFile.Create(fileInfo);
                    success.Should().BeTrue();
                }

                using (var tdmsFile = new TDMSFile())
                {
                    var success = tdmsFile.Open(fileInfo);
                    success.Should().BeTrue();
                    var ps = tdmsFile.GetDefaultProperties();
                    ps.Count.Should().Be(5);
                }
                _context.CleanFiles();
            }
        }
    }
}