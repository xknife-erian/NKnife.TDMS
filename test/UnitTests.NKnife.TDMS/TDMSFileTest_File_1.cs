using FluentAssertions;
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
    }
}