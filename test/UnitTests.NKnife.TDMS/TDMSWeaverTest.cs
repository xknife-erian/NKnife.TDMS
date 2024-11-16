using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSWeaverTest
    {
        private readonly TestFileContext _context;

        public TDMSWeaverTest()
        {
            _context = new TestFileContext();
            _context.CleanFiles();
        }
    
        [Fact(DisplayName = "Weave_Test_1: 当调用Weave方法时，应返回一个ITDMSFile实例")]
        public void Weave_Test_1()
        {
            // Act
            using var result = TDMSWeaver.Weave();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "CreateNewFile_Test_1: 当调用CreateNewFile方法时，应创建一个新的ITDMSFile实例并返回")]
        public void CreateNewFile_Test_1()
        {
            // Arrange
            var    fileInfo    = _context.CreateTestFile();
            string  filePath    = fileInfo.FilePath;
            string fileType    = Constants.DDC_FILE_TYPE_TDM;
            string name        = "Test File";
            string description = "This is a test file";
            string title       = "Test";
            string author      = "John Doe";

            // Act
            using var result = TDMSWeaver.CreateNewFile(filePath, fileType, name, description, title, author);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "OpenExistingFile_Test_1: 当调用OpenExistingFile方法时，应打开指定路径的ITDMSFile实例并返回")]
        public void OpenExistingFile_Test_1()
        {
            // Arrange
            var fileInfo = _context.CreateTestFile();
            using (var file = new TDMSFile())
            {
                file.Create(fileInfo);
            }

            // Act
            using var result = TDMSWeaver.OpenExistingFile(fileInfo.FilePath);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "OpenExistingFile_Test_2: 当调用OpenExistingFile方法时，应打开指定的TDMSFileInfo的ITDMSFile实例并返回")]
        public void OpenExistingFile_Test_2()
        {
            // Arrange

            var fileInfo = _context.CreateTestFile();
            using (var file = new TDMSFile())
            {
                file.Create(fileInfo.FilePath, Constants.DDC_FILE_TYPE_TDM, "Test File", "TestFile", "Test", "Erian");
            }

            // Act
            using var result = TDMSWeaver.OpenExistingFile(fileInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }
    }
}
