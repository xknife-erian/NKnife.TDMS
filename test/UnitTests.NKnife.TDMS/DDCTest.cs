using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;
using NKnife.TDMS.Externals;

namespace UnitTests.NKnife.TDMS
{
    public class DDCTest
    {
        [Fact(DisplayName = "1.库验证：创建成功。正确创建TDM文件")]
        public void CreateFile_Test1()
        {
            // Act
            var filePath = "~ddc1.tdm";
            var success  = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().Be(0);
            file.Should().NotBe(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact(DisplayName = "2.库验证：创建成功。正确创建TDMS文件")]
        public void CreateFile_Test2()
        {
            // Act
            var filePath = "~ddc2.tdms";
            var success  = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM_STREAMING, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().Be(0);
            file.Should().NotBe(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact(DisplayName = "3.库验证：创建失败。错误创建TDM文件，Type=TDMS，给定的文件类型与文件后缀名不匹配")]
        public void CreateFile_Test3()
        {
            // Act
            var filePath = "~ddc3.tdm";
            var success  = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM_STREAMING, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().NotBe(0);//失败
            file.Should().Be(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact(DisplayName = "4.库验证：创建成功。创建TDMS文件，Type=TDM，给定的文件类型与文件后缀名不匹配时也可以创建(官方库的实现)")]
        public void CreateFile_Test4()
        {
            // Act
            var filePath = "~ddc4.tdms";
            var success = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().Be(0);
            file.Should().NotBe(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact(DisplayName = "5.库验证：创建失败。错误创建TDM文件，Type=其他字符串，给定的文件类型与文件后缀名不匹配")]
        public void CreateFile_Test5()
        {
            // Act
            var filePath = "~ddc5.tdm";
            var success = DDC.CreateFile(filePath, "txt", "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().NotBe(0);//失败
            file.Should().Be(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }       
        
        [Fact(DisplayName = "6.库验证：创建失败。错误创建TDMS文件，Type=其他字符串，给定的文件类型与文件后缀名不匹配")]
        public void CreateFile_Test6()
        {
            // Act
            var filePath = "~ddc6.tdms";
            var success = DDC.CreateFile(filePath, "txt", "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().NotBe(0);//失败
            file.Should().Be(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }        

        [Fact(DisplayName = "7.库验证：创建成功。成功创建TDM文件，Type=TDM，文件后缀名!=TDM，给定的文件类型与文件后缀名不匹配")]
        public void CreateFile_Test7()
        {
            // Act
            var filePath = "~ddc6.txt";
            var success = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().Be(0);//失败
            file.Should().NotBe(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact(DisplayName = "8.库验证：创建失败。错误创建TDMS文件，Type=TDMS，文件后缀名!=TDM，给定的文件类型与文件后缀名不匹配")]
        public void CreateFile_Test8()
        {
            // Act
            var filePath = "~ddc6.txt";
            var success  = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM_STREAMING, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().NotBe(0);//失败
            file.Should().Be(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact]
        public void CreateChannelGroupProperty_Test1()
        {
            // Arrange
            using var context       = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyDouble";
            var       propertyValue = 3.14159265359;

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = (TDMSChannelGroup)file.AddGroup("Group1");

            // Act
            DDC.CreateChannelGroupProperty(group.GetPtr(), "test", TDMSDataType.String, new RuntimeArgumentHandle());

            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = group.TryGetProperty(propertyName, out TODO, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
            type.Should().Be(TDMSDataType.Double);
        }
    }
}
