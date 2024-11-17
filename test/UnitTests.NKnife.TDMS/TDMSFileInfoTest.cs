using NKnife.TDMS.Default;
using NKnife.TDMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileInfoTest
    {
        [Fact(DisplayName = "Test_1:简单判断，无文件")]
        public void Exists_Test_1()
        {
            // Act
            var fi = new TDMSFileInfo("test.tdms");

            // Assert
            fi.Exists.Should().BeFalse();
        }

        [Fact(DisplayName = "Test_2:简单判断，有文件")]
        public void Exists_Test_2()
        {
            // Act
            var filePath = "test.tdms";

            var stream = File.Open(filePath, FileMode.OpenOrCreate);
            stream.Write(Guid.NewGuid().ToByteArray());
            stream.Close();
            stream.Dispose();

            var fi = new TDMSFileInfo(filePath);

            // Assert
            fi.Exists.Should().BeTrue();
            File.Delete(filePath);
        }

        [Fact(DisplayName = "Test_3:简单判断，非法路径")]
        public void Exists_Test_3()
        {
            // Act
            var filePath = "<<>>\\:::<<>>\\\\>><<.tdms";
            var fi = new TDMSFileInfo(filePath);

            // Assert
            fi.Exists.Should().BeFalse();
        }

        [Fact(DisplayName = "Test_4:简单判断，空")]
        public void Exists_Test_4()
        {
            // Act
            var filePath = string.Empty;

            // Assert
            Action act = () =>
            {
                var fi = new TDMSFileInfo(filePath);
                fi.Exists.Should().BeFalse();
            };
            act.Should().Throw<TDMSErrorException>();
        }
    }
}
