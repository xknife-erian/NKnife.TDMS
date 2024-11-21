using FluentAssertions;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileIndexTest
    {
        [Fact(DisplayName = "1. 添加1个ChannelGroup，测试通过序号索引器方式获取Group。")]
        public void Indexer_Test01()
        {
            // Arrange
            var groupName = "group1";

            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup(groupName);

            // Assert
            using var group = tdmsFile[0];
            group.Should().NotBeNull();
            group.Name.Should().Be(groupName);
        }

        [Fact(DisplayName = "2. 添加多个ChannelGroup，测试可以通过任意序号索引器方式获取Group。")]
        public void Indexer_Test02()
        {
            // Arrange
            var groupName1 = "group1";
            var groupName2 = "group2";
            var groupName3 = "group3";
            var groupName4 = "group4";

            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup(groupName1);
            using var group2 = tdmsFile.AddGroup(groupName2);
            using var group3 = tdmsFile.AddGroup(groupName3);
            using var group4 = tdmsFile.AddGroup(groupName4);

            // Assert
            using var gr1 = tdmsFile[0];
            gr1.Should().NotBeNull();
            gr1.Name.Should().Be(groupName1);

            using var gr2 = tdmsFile[1];
            gr2.Should().NotBeNull();
            gr2.Name.Should().Be(groupName2);    
            
            using var gr3 = tdmsFile[2];
            gr3.Should().NotBeNull();
            gr3.Name.Should().Be(groupName3);        
            
            using var gr4 = tdmsFile[3];
            gr4.Should().NotBeNull();
            gr4.Name.Should().Be(groupName4);
        }

        [Fact(DisplayName = "3. 添加1个ChannelGroup，测试通过名称索引器方式获取Group。")]
        public void Indexer_Test03()
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
            using var group = tdmsFile[groupName];
            group.Should().NotBeNull();
            group.Name.Should().Be(groupName);
        }

        [Fact(DisplayName = "4. 添加多个ChannelGroup，测试可以通过任意名称索引器方式获取Group。")]
        public void Indexer_Test04()
        {
            // Arrange
            var groupName1 = "group1";
            var groupName2 = "group2";
            var groupName3 = "group3";
            var groupName4 = "group4";

            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            // Act
            using var group1 = tdmsFile.AddGroup(groupName1);
            using var group2 = tdmsFile.AddGroup(groupName2);
            using var group3 = tdmsFile.AddGroup(groupName3);
            using var group4 = tdmsFile.AddGroup(groupName4);

            // Assert
            using var gr1 = tdmsFile[groupName1];
            gr1.Should().NotBeNull();
            gr1.Name.Should().Be(groupName1);

            using var gr2 = tdmsFile[groupName2];
            gr2.Should().NotBeNull();
            gr2.Name.Should().Be(groupName2);

            using var gr3 = tdmsFile[groupName3];
            gr3.Should().NotBeNull();
            gr3.Name.Should().Be(groupName3);

            using var gr4 = tdmsFile[groupName4];
            gr4.Should().NotBeNull();
            gr4.Name.Should().Be(groupName4);
        }
    }
}