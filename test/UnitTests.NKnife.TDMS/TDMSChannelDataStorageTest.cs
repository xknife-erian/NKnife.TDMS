using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSChannelDataStorageTest
    {
        [Fact(DisplayName = "01. 向1个通道里添加一些String数据")]
        public void TDMSChannel_DataStorageTest_01()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            using var group   = tdmsFile.AddGroup("Group1");

            // Assert
            using var channel = group.AddChannel(TDMSDataType.String, "Channel1", "mm/s", string.Empty);
            channel.SetData("string1");
            channel.AppendData("string2");
            channel.AppendData("string3");


            // Act
            var data = channel.GetDataValues<string>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be("string1");
            data[1].Should().Be("string2");
            data[2].Should().Be("string3");
        }

        [Fact(DisplayName = "02. 向1个通道里添加一些UInt8数据")]
        public void TDMSChannel_DataStorageTest_02()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.UInt8, "Channel1", "mm/s", string.Empty);
            channel.SetData(1);
            channel.AppendData(2);
            channel.AppendData(3);
            // Act
            var data = channel.GetDataValues<byte>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(1);
            data[1].Should().Be(2);
            data[2].Should().Be(3);
        }

        [Fact(DisplayName = "03. 向1个通道里添加一些Int16数据")]
        public void TDMSChannel_DataStorageTest_03()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.Int16, "Channel1", "mm/s", string.Empty);
            channel.SetData(1);
            channel.AppendData(2);
            channel.AppendData(3);
            // Act
            var data = channel.GetDataValues<short>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(1);
            data[1].Should().Be(2);
            data[2].Should().Be(3);
        }

        [Fact(DisplayName = "04. 向1个通道里添加一些Int32数据")]
        public void TDMSChannel_DataStorageTest_04()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.Int32, "Channel1", "mm/s", string.Empty);
            channel.SetData(1);
            channel.AppendData(2);
            channel.AppendData(3);
            // Act
            var data = channel.GetDataValues<int>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(1);
            data[1].Should().Be(2);
            data[2].Should().Be(3);
        }

        [Fact(DisplayName = "05. 向1个通道里添加一些Float数据1")]
        public void TDMSChannel_DataStorageTest_05()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.Float, "Channel1", "mm/s", string.Empty);
            channel.SetData(0.11f);
            channel.AppendData(0.22f);
            channel.AppendData(0.33f);
            // Act
            var data = channel.GetDataValues<float>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(0.11f);
            data[1].Should().Be(0.22f);
            data[2].Should().Be(0.33f);
        }

        [Fact(DisplayName = "06. 向1个通道里添加一些Float数据2")]
        public void TDMSChannel_DataStorageTest_06()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.Float, "Channel1", "mm/s", string.Empty);
            channel.SetData(1.1f);
            channel.AppendData(2.2f);
            channel.AppendData(3.3f);
            // Act
            var data = channel.GetDataValues<float>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(1.1f);
            data[1].Should().Be(2.2f);
            data[2].Should().Be(3.3f);
        }

        [Fact(DisplayName = "07. 向1个通道里添加一些Double数据")]
        public void TDMSChannel_DataStorageTest_07()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.Double, "Channel1", "mm/s", string.Empty);
            channel.SetData(1.11111111);
            channel.AppendData(2.22222222);
            channel.AppendData(3.33333333);
            // Act
            var data = channel.GetDataValues<double>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(1.11111111);
            data[1].Should().Be(2.22222222);
            data[2].Should().Be(3.33333333);
        }

        [Fact(DisplayName = "08. 向1个通道里添加一些DateTime数据")]
        public void TDMSChannel_DataStorageTest_08()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);
            using var group = tdmsFile.AddGroup("Group1");
            // Assert
            using var channel = group.AddChannel(TDMSDataType.Timestamp, "Channel1", "mm/s", string.Empty);
            channel.SetData(new DateTime(2022, 2, 2, 2, 2, 2));
            channel.AppendData(new DateTime(2022, 2, 2, 2, 2, 3));
            channel.AppendData(new DateTime(2022, 2, 2, 2, 2, 4));
            // Act
            var data = channel.GetDataValues<DateTime>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(new DateTime(2022, 2, 2, 2, 2, 2));
            data[1].Should().Be(new DateTime(2022, 2, 2, 2, 2, 3));
            data[2].Should().Be(new DateTime(2022, 2, 2, 2, 2, 4));
        }
    }
}
