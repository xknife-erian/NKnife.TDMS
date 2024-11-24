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
    public class TDMSDataBuilderTest
    {
        [Fact(DisplayName = "CreateNewFile_Test_1: 当调用BuildNew方法时，应创建一个新的ITDMSFile实例并返回")]
        public void CreateNewFile_Test_1()
        {
            // Arrange
            using var context = new TestFileContext();
            var    fileInfo    = context.CreateTestFile();
            string filePath    = fileInfo.FilePath;
            string fileType    = Constants.DDC_FILE_TYPE_TDM;
            string name        = "Test File";
            string description = "This is a test file";
            string title       = "Test";
            string author      = "John Doe";

            // Act
            using var result = TDMSDataBuilder.BuildNew(filePath, name, description, title, author, fileType);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "CreateNewFile_Test_2: 当调用BuildNew方法时，应创建一个新的ITDMSFile实例并返回")]
        public void CreateNewFile_Test_2()
        {
            // Arrange
            using var context     = new TestFileContext();
            var       fileInfo    = context.CreateTestFile();
            string    filePath    = fileInfo.FilePath;
            string    name        = "Test File";

            // Act
            using var result = TDMSDataBuilder.BuildNew(filePath, name);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "OpenExistingFile_Test_1: 当调用OpenExistingFile方法时，应打开指定路径的ITDMSFile实例并返回")]
        public void OpenExistingFile_Test_1()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using (var file = new TDMSFile())
            {
                file.Create(fileInfo);
            }

            // Act
            using var result = TDMSDataBuilder.OpenExisting(fileInfo.FilePath);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "OpenExistingFile_Test_2: 当调用OpenExistingFile方法时，应打开指定的TDMSFileInfo的ITDMSFile实例并返回")]
        public void OpenExistingFile_Test_2()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using (var file = new TDMSFile())
            {
                file.Create(fileInfo.FilePath, fileInfo.FileType, "Test File", "TestFile", "Test", "Erian");
            }

            // Act
            using var result = TDMSDataBuilder.OpenExistingFile(fileInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }
    }
}
