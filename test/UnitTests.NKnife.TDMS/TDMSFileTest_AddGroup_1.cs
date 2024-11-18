using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Default;

// ReSharper disable InconsistentNaming

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileTest_AddGroup_1
    {
        [Fact(DisplayName = "1. 添加1个ChannelGroup, 基本测试。")]
        public void Add_Test01()
        {
            // Arrange
            using var context  = new TestFileContext();
            var fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.Add("group1");

            // Assert
            group1.Should().NotBeNull();
            tdmsFile.ChildCount.Should().Be(1);
        }

        [Fact(DisplayName = "2. 添加500个ChannelGroup, 基本测试。")]
        public void Add_Test02()
        {
            // Arrange
            using var context  = new TestFileContext();
            var fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            var count = 500;
            for (int i = 0; i < count; i++)
            {
                // Act
                using var group = tdmsFile.Add($"group{i}");
                group.Should().NotBeNull();
            }

            // Assert
            tdmsFile.ChildCount.Should().Be((ulong)count);
        }
    }
}
