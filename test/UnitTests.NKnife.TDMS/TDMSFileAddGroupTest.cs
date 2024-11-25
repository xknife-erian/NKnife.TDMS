﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileAddGroupTest
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

        [Fact(DisplayName = "2. 添加多个ChannelGroup, 基本测试，并检查name,description")]
        public void Add_Test02()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            var name        = "group";
            var description = "description";

            using (var group1 = tdmsFile.AddGroup(name, description))
            {

                // Assert
                group1.Should().NotBeNull();
                group1.Name.Should().Be(name);
                group1.Description.Should().Be(description);
                tdmsFile.ChildCount.Should().Be(1);
            }

            name        = "中文name中文";
            description = "中文descriptionのの";

            using (var group2 = tdmsFile.AddGroup(name, description))
            {
                // Assert
                group2.Should().NotBeNull();
                group2.Name.Should().Be(name);
                group2.Description.Should().Be(description);
                tdmsFile.ChildCount.Should().Be(2);
            }
        }

        [Fact(DisplayName = "3. 添加10个ChannelGroup, 基本测试。")]
        public void Add_Test03()
        {
            // Arrange
            using var context  = new TestFileContext();
            var fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            var count = 10;
            for (int i = 0; i < count; i++)
            {
                // Act
                using var group = tdmsFile.AddGroup($"group{i}");
                group.Should().NotBeNull();
            }

            // Assert
            tdmsFile.ChildCount.Should().Be((ulong)count);
        }

        [Fact(DisplayName = "4. 添加重复的ChannelGroup，将失败。")]
        public void Add_Test04()
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
}
