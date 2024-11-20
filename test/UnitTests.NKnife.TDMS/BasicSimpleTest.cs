using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;
using Xunit;

namespace UnitTests.NKnife.TDMS
{
    public class BasicSimpleTest
    {
        private readonly TestFileContext _context;

        public BasicSimpleTest()
        {
            _context = new TestFileContext();
            _context.CleanFiles();
        }

        [Fact(DisplayName = "�����ļ�����")]
        public void Test_CreateFile()
        {
            var       fileInfo = _context.CreateTestFile();
            using var file     = new TDMSFile();
            file.Open(fileInfo);

            fileInfo.Exists.Should().BeTrue();
        }

        [Fact(DisplayName = "���ͨ�������")]
        public void Test_AddGroup()
        {
            var       fileInfo = _context.CreateTestFile();
            using var file     = new TDMSFile();
            file.Open(fileInfo);

            using var group = file.AddGroup("Group1", "Group1Description");
            group.Should().NotBeNull();
        }

        [Fact(DisplayName = "���ͨ������")]
        public void Test_AddChannel()
        {
            var       fileInfo = _context.CreateTestFile();
            using var file     = new TDMSFile();
            file.Open(fileInfo);

            using var group   = file.AddGroup("Group1", "Group1Description");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "V", "Channel1Description");
            channel.Should().NotBeNull();
        }

        [Fact(DisplayName = "������ݲ���")]
        public void Test_AddData()
        {
            var       fileInfo = _context.CreateTestFile();

            using var file     = new TDMSFile();

            file.Open(fileInfo);

            using var group   = file.AddGroup("Group1", "Group1Description");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "V", "Channel1Description");

            channel.AddData([1.0, 2.0, 3.0]);
            file.Save();
            channel.ChildCount.Should().Be(3);
        }
    }
}