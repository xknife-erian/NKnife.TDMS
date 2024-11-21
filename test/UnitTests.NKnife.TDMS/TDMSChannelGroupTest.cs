using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSChannelGroupTest
    {
        private readonly TestFileContext _context;

        public TDMSChannelGroupTest()
        {
            _context = new TestFileContext();
            _context.CleanFiles();
        }

        [Fact(DisplayName = "AddChannel_Test_1: 添加新通道，验证返回的通道对象不为空")]
        public void AddChannel_Test_1()
        {
            var       fileInfo = _context.CreateTestFile();
            
            using var file     = new TDMSFile();
            file.Open(fileInfo);
            
            using var group = file.AddGroup("Group1");

            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");

            group.ChildCount.Should().Be(1);
        }

        [Fact(DisplayName = "AddChannel_Test_2: 添加新通道后，验证name/description/unit的属性值")]
        public void AddChannel_Test_2()
        {
            var fileInfo = _context.CreateTestFile();
            
            using var file = new TDMSFile();
            file.Open(fileInfo);
            
            using var group = file.AddGroup("Group1");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1", "Description1");
            channel.Name.Should().Be("Channel1");
            channel.Unit.Should().Be("Unit1");
            channel.Description.Should().Be("Description1");

            group.ChildCount.Should().Be(1);
        }

        [Fact(DisplayName = "Indexer_Test_1: 通过索引获取通道，验证返回的通道对象不为空")]
        public void Indexer_Test_1()
        {
        }

        [Fact(DisplayName = "Indexer_Test_2: 通过名称获取通道，验证返回的通道对象不为空")]
        public void Indexer_Test_2()
        {
        }
    }
}
