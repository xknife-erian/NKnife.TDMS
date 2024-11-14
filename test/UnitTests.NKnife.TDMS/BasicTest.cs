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

        [Fact(DisplayName = "�����ļ�����")]
        public void Test_CreateFile()
        {
            var fileInfo = TestFileContext.CreateTestFile();
            using var file = new TDMSFile(fileInfo);

            fileInfo.Exists.Should().BeTrue();
        }

        [Fact(DisplayName = "����ͨ�������")]
        public void Test_AddGroup()
        {
            var fileInfo = TestFileContext.CreateTestFile();
            using var file = new TDMSFile(fileInfo);

            using var group = file.Add("Group1", "Group1Description", null);
            group.Should().NotBeNull();
        }

        [Fact(DisplayName = "����ͨ������")]
        public void Test_AddChannel()
        {
            var fileInfo = TestFileContext.CreateTestFile();
            using var file = new TDMSFile(fileInfo);

            using var group   = file.Add("Group1", "Group1Description", null);
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "V", "Channel1Description");
            channel.Should().NotBeNull();
        }

        [Fact(DisplayName = "�������ݲ���")]
        public void Test_AddData()
        {
            var fileInfo = TestFileContext.CreateTestFile();
            using (var file = new TDMSFile(fileInfo))
            {
                using var group   = file.Add("Group1", "Group1Description", null);
                using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "V", "Channel1Description");
                channel.AddData([1.0, 2.0, 3.0]);
                file.Save();
                file.Close();
            }

            using (var file = new TDMSFile(fileInfo))
            {
                var group = file.GetGroup(0);
                var channel = group.GetChannel(0);
                var data = channel.GetDataValues<double>(0, 3);
                data.Should().BeEquivalentTo([1.0, 2.0, 3.0]);
            }
        }
    }
}