using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using Xunit;

namespace UnitTests.NKnife.TDMS
{
    public class BasicTest : IClassFixture<TestFileContext>
    {
        public BasicTest(TestFileContext context)
        {
            context.CleanFiles();
        }

        [Fact(DisplayName = "创建文件测试")]
        public void Test_CreateFile()
        {
            var fileInfo = TestFileContext.CreateTestFile();
            using var file = new TDMSFile();
            file.Open(fileInfo);

            fileInfo.Exists.Should().BeTrue();
        }

        [Fact(DisplayName = "添加通道组测试")]
        public void Test_AddGroup()
        {
            var       fileInfo = TestFileContext.CreateTestFile();
            using var file     = new TDMSFile();
            file.Open(fileInfo);

            using var group = file.Add("Group1", "Group1Description");
            group.Should().NotBeNull();
        }

        [Fact(DisplayName = "添加通道测试")]
        public void Test_AddChannel()
        {
            var       fileInfo = TestFileContext.CreateTestFile();
            using var file     = new TDMSFile();
            file.Open(fileInfo);

            using var group   = file.Add("Group1", "Group1Description");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "V", "Channel1Description");
            channel.Should().NotBeNull();
        }

        [Fact(DisplayName = "添加数据测试")]
        public void Test_AddData()
        {
            var fileInfo = TestFileContext.CreateTestFile();
            using (var file = new TDMSFile())
            {
                file.Open(fileInfo);

                using var group   = file.Add("Group1", "Group1Description");
                using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "V", "Channel1Description");
                channel.AddData([1.0, 2.0, 3.0]);
                file.Save();
                file.Close();
            }
        }
    }
}