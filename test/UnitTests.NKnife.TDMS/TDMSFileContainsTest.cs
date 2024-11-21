using FluentAssertions;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileContainsTest
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

        [Fact(DisplayName = "2. 添加多个ChannelGroup，测试判断Group的name的property，以确认该Group是否存在。")]
        public void Contains_Test02()
        {
            // Arrange
            var groupName1 = "group1";
            var groupName2 = "group2";
            var groupName3 = "group3";

            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup(groupName1);
            using var group2 = tdmsFile.AddGroup(groupName2);
            using var group3 = tdmsFile.AddGroup(groupName3);

            // Assert
            group1.Should().NotBeNull();
            group2.Should().NotBeNull();
            group3.Should().NotBeNull();
            tdmsFile.ChildCount.Should().Be(3);

            // 是否包含
            var hasGroup1 = tdmsFile.Contains(groupName1);
            var hasGroup2 = tdmsFile.Contains(groupName2);
            var hasGroup3 = tdmsFile.Contains(groupName3);
            hasGroup1.Should().BeTrue();
            hasGroup2.Should().BeTrue();
            hasGroup3.Should().BeTrue();
        }

        [Fact(DisplayName = "3. 添加多个ChannelGroup，但测试判断不存在的group。")]
        public void Contains_Test03()
        {
            // Arrange
            var groupName1 = "group1";
            var groupName2 = "group2";
            var groupName3 = "group3";

            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup(groupName1);
            using var group2 = tdmsFile.AddGroup(groupName2);
            using var group3 = tdmsFile.AddGroup(groupName3);

            // Assert
            group1.Should().NotBeNull();
            group2.Should().NotBeNull();
            group3.Should().NotBeNull();
            tdmsFile.ChildCount.Should().Be(3);

            // 是否包含
            var hasGroup1 = tdmsFile.Contains(groupName1);
            var hasGroup2 = tdmsFile.Contains(groupName2);
            var hasGroup3 = tdmsFile.Contains(groupName3);
            hasGroup1.Should().BeTrue();
            hasGroup2.Should().BeTrue();
            hasGroup3.Should().BeTrue();

            var hasGroup4 = tdmsFile.Contains("group4");
            hasGroup4.Should().BeFalse();

            var hasGroup5 = tdmsFile.Contains(string.Empty);
            hasGroup4.Should().BeFalse();
        }
    }
}