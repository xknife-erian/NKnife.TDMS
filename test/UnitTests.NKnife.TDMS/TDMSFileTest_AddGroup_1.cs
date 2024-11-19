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
            using var group1 = tdmsFile.AddGroup("group1");

            // Assert
            group1.Should().NotBeNull();
            tdmsFile.ChildCount.Should().Be(1);
        }

        [Fact(DisplayName = "2. 添加1000个ChannelGroup, 基本测试。")]
        public void Add_Test02()
        {
            // Arrange
            using var context  = new TestFileContext();
            var fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            var count = 1000;
            for (int i = 0; i < count; i++)
            {
                // Act
                using var group = tdmsFile.AddGroup($"group{i}");
                group.Should().NotBeNull();
            }

            // Assert
            tdmsFile.ChildCount.Should().Be((ulong)count);
        }

        [Fact(DisplayName = "3. 添加重复的ChannelGroup，将失败。")]
        public void Add_Test03()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup("groupName1");

            // Assert
            group1.Should().NotBeNull();
            tdmsFile.ChildCount.Should().Be(1);

            // Act
            using var group2 = tdmsFile.AddGroup("groupName1");

            // Assert
            group2.Should().BeNull();
            tdmsFile.ChildCount.Should().Be(1);
        }
    }

    public class TDMSFileTest_Contains
    {
        [Fact(DisplayName = "1. 添加1个ChannelGroup，测试判断Group的name的property，以确认该Group是否存在。")]
        public void Contains_Test01()
        {
            // Arrange
            var groupName = "group1";

            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup(groupName);

            // Assert
            group1.Should().NotBeNull();
            tdmsFile.ChildCount.Should().Be(1);

            // 是否包含
            var has = tdmsFile.Contains(groupName);
            has.Should().BeTrue();
        }
    }
}
