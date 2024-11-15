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
        [Fact(DisplayName = "Weave_Test_1: 当调用Weave方法时，应返回一个ITDMSFile实例")]
        public void Weave_Test_1()
        {
            // Act
            Clean();
            using ITDMSFile result = TDMSWeaver.Weave();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "CreateNewFile_Test_1: 当调用CreateNewFile方法时，应创建一个新的ITDMSFile实例并返回")]
        public void CreateNewFile_Test_1()
        {
            // Arrange
            Clean();
            string filePath = "test.tdm";
            string fileType = Constants.DDC_FILE_TYPE_TDM;
            string name = "Test File";
            string description = "This is a test file";
            string title = "Test";
            string author = "John Doe";

            // Act
            using ITDMSFile result = TDMSWeaver.CreateNewFile(filePath, fileType, name, description, title, author);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "OpenExistingFile_Test_1: 当调用OpenExistingFile方法时，应打开指定路径的ITDMSFile实例并返回")]
        public void OpenExistingFile_Test_1()
        {
            // Arrange
            Clean();
            string filePath = "test.tdm";
            using (var file = new TDMSFile())
            {
                file.Create(filePath, Constants.DDC_FILE_TYPE_TDM, "Test File", "TestFile", "Test", "Erian");
            }

            // Act
            using ITDMSFile result = TDMSWeaver.OpenExistingFile(filePath);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        [Fact(DisplayName = "OpenExistingFile_Test_2: 当调用OpenExistingFile方法时，应打开指定的TDMSFileInfo的ITDMSFile实例并返回")]
        public void OpenExistingFile_Test_2()
        {
            // Arrange
            Clean();
            string filePath = "test.tdm";
            using (var file = new TDMSFile())
            {
                file.Create(filePath, Constants.DDC_FILE_TYPE_TDM, "Test File", "TestFile", "Test", "Erian");
            }

            TDMSFileInfo fileInfo = new TDMSFileInfo
            {
                FilePath = filePath
            };

            // Act
            using ITDMSFile result = TDMSWeaver.OpenExistingFile(fileInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TDMSFile>();
        }

        private static void Clean()
        {
            var path  = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(path, $"*.tdm");
            foreach (var fileToDelete in files)
                File.Delete(fileToDelete);
        }
    }
}
