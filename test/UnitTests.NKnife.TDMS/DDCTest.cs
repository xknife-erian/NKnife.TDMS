using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Common;
using NKnife.TDMS.Externals;

namespace UnitTests.NKnife.TDMS
{
    public class DDCTest
    {
        [Fact(DisplayName = "正确创建TDM文件")]
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

        [Fact(DisplayName = "正确创建TDMS文件")]
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

        [Fact(DisplayName = "错误创建TDM文件，给定的文件类型与文件后缀名不匹配")]
        public void CreateFile_Test3()
        {
            // Act
            var filePath = "~ddc3.tdm";
            var success  = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM_STREAMING, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().NotBe(0);
            file.Should().Be(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }

        [Fact(DisplayName = "创建TDMS文件，给定的文件类型与文件后缀名不匹配时也可以创建(官方库的实现)")]
        public void CreateFile_Test4()
        {
            // Act
            var filePath = "~ddc4.tdms";
            if(File.Exists(filePath))
                File.Delete(filePath);
            var success = DDC.CreateFile(filePath, Constants.DDC_FILE_TYPE_TDM, "name", "des", "title", "auth", out var file);

            // Assert
            success.Should().Be(0);
            file.Should().NotBe(IntPtr.Zero);

            DDC.CloseFile(file);
            File.Delete(filePath);
        }
    }
}
