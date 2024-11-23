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
        [Fact(DisplayName = "AddChannel_Test_1: 添加新通道，验证返回的通道对象不为空")]
        public void AddChannel_Test_01()
        {
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            
            using var file     = new TDMSFile();
            file.Open(fileInfo);
            
            using var group = file.AddGroup("Group1");

            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");

            group.ChildCount.Should().Be(1);
        }

        [Fact(DisplayName = "AddChannel_Test_2: 添加新通道后，验证name/description/unit的属性值")]
        public void AddChannel_Test_02()
        {
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var file = new TDMSFile();
            file.Open(fileInfo);
            
            using var group = file.AddGroup("Group1");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1", "Description1");
            channel.Name.Should().Be("Channel1");
            channel.Unit.Should().Be("Unit1");
            channel.Description.Should().Be("Description1");

            group.ChildCount.Should().Be(1);
        }

        [Fact(DisplayName = "Indexer_Test_01: 1个channel, 通过索引获取通道，验证返回的通道对象不为空")]
        public void Indexer_Test_01()
        {
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var file = new TDMSFile();
            file.Open(fileInfo);

            using var group = file.AddGroup("Group1");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");

            var retrievedChannel = group[0];
            retrievedChannel.Should().NotBeNull();
            retrievedChannel.Name.Should().Be("Channel1");
            retrievedChannel.Unit.Should().Be("Unit1");
        }

        [Fact(DisplayName = "Indexer_Test_02: 3个channel, 通过索引1~3获取通道，验证返回的通道对象不为空")]
        public void Indexer_Test_02()
        {
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var file = new TDMSFile();
            file.Open(fileInfo);

            using var group = file.AddGroup("Group1");
            using var channel1 = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");
            using var channel2 = group.AddChannel(TDMSDataType.Double, "Channel2", "Unit2");
            using var channel3 = group.AddChannel(TDMSDataType.Double, "Channel3", "Unit3");

            using var retrievedChannel1 = group[0];
            using var retrievedChannel2 = group[1];
            using var retrievedChannel3 = group[2];

            retrievedChannel1.Should().NotBeNull();
            retrievedChannel1.Name.Should().Be("Channel1");
            retrievedChannel1.Unit.Should().Be("Unit1");

            retrievedChannel2.Should().NotBeNull();
            retrievedChannel2.Name.Should().Be("Channel2");
            retrievedChannel2.Unit.Should().Be("Unit2");

            retrievedChannel3.Should().NotBeNull();
            retrievedChannel3.Name.Should().Be("Channel3");
            retrievedChannel3.Unit.Should().Be("Unit3");
        }

        [Fact(DisplayName = "Indexer_Test_03: 没有channel时，返回空")]
        public void Indexer_Test_03()
        {
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var file = new TDMSFile();
            file.Open(fileInfo);

            using var group = file.AddGroup("Group1");

            group.ChildCount.Should().Be(0);
            group.Invoking(g => g[0]).Should().Throw<IndexOutOfRangeException>();
        }

        [Fact(DisplayName = "Indexer_Test_04: 当索引号大于数量时，抛出异常")]
        public void Indexer_Test_4()
        {
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();

            using var file = new TDMSFile();
            file.Open(fileInfo);

            using var group = file.AddGroup("Group1");
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");

            group.ChildCount.Should().Be(1);
            group.Invoking(g => g[1]).Should().Throw<IndexOutOfRangeException>();
        }

        [Fact(DisplayName = "Indexer_Test_10: 添加多个通道，然后通过名称获取通道，验证返回的通道对象不为空，通道属性值均正确")]
        public void Indexer_Test_10()
        {
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var file = new TDMSFile();
            file.Open(fileInfo);
            using var group = file.AddGroup("Group1");
            using var channel1 = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");
            using var channel2 = group.AddChannel(TDMSDataType.Double, "Channel2", "Unit2");
            using var channel3 = group.AddChannel(TDMSDataType.Double, "Channel3", "Unit3");

            using var retrievedChannel1 = group["Channel1"];
            using var retrievedChannel2 = group["Channel2"];
            using var retrievedChannel3 = group["Channel3"];
            retrievedChannel1.Should().NotBeNull();
            retrievedChannel1.Name.Should().Be("Channel1");
            retrievedChannel1.Unit.Should().Be("Unit1");
            retrievedChannel2.Should().NotBeNull();
            retrievedChannel2.Name.Should().Be("Channel2");
            retrievedChannel2.Unit.Should().Be("Unit2");
            retrievedChannel3.Should().NotBeNull();
            retrievedChannel3.Name.Should().Be("Channel3");
            retrievedChannel3.Unit.Should().Be("Unit3");
        }


        [Fact(DisplayName = "Indexer_Test_11: 通过不存在的名称获取通道，返回null")]
        public void Indexer_Test_11()
        {
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var file = new TDMSFile();
            file.Open(fileInfo);
            using var group = file.AddGroup("Group1");
            using var channel1 = group.AddChannel(TDMSDataType.Double, "Channel1", "Unit1");

            var retrievedChannel = group["NonExistentChannel"];
            retrievedChannel.Should().BeNull();
        }
    }
}
