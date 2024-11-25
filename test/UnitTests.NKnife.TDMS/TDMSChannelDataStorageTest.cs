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

        [Fact(DisplayName = "01.1. 向1个通道里添加一些简体中文、繁体中文、日文、String数据")]
        public void TDMSChannel_DataStorageTest_01_1()
        {
            /*
             简体中文
               这是一个测试。
               欢迎使用我们的服务。
               系统正在更新中，请稍后再试。
             繁体中文
               這是一個測試。
               歡迎使用我們的服務。
               系統正在更新中，請稍後再試。
             日文
               これはテストです。
               サービスをご利用いただきありがとうございます。
               システムは現在更新中です。後でもう一度お試しください。
             */
            // Arrange

            var cn1 = "简体中文";
            var cn2 = "这是一个测试。";
            var cn3 = "欢迎使用我们的服务。";
            var cn4 = "系统正在更新中，请稍后再试。";

            var tw1 = "繁体中文";
            var tw2 = "這是一個測試。";
            var tw3 = "歡迎使用我們的服務。";
            var tw4 = "系統正在更新中，請稍後再試。";

            var jp1 = "日文";
            var jp2 = "これはテストです。";
            var jp3 = "サービスをご利用いただきありがとうございます。";
            var jp4 = "システムは現在更新中です。後でもう一度お試しください。";

            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            using var group = tdmsFile.AddGroup("Group1");

            // Assert
            using var channel = group.AddChannel(TDMSDataType.String, "Channel1", "mm/s", string.Empty);

            channel.AppendData(cn1);
            channel.AppendData(cn2);
            channel.AppendData(cn3);
            channel.AppendData(cn4);
            channel.AppendData(tw1);
            channel.AppendData(tw2);
            channel.AppendData(tw3);
            channel.AppendData(tw4);
            channel.AppendData(jp1);
            channel.AppendData(jp2);
            channel.AppendData(jp3);
            channel.AppendData(jp4);

            // Act
            var data = channel.GetDataValues<string>(0, 12);
            data.Length.Should().Be(12);
            data[0].Should().Be(cn1);
            data[1].Should().Be(cn2);
            data[2].Should().Be(cn3);
            data[3].Should().Be(cn4);
            data[4].Should().Be(tw1);
            data[5].Should().Be(tw2);
            data[6].Should().Be(tw3);
            data[7].Should().Be(tw4);
            data[8].Should().Be(jp1);
            data[9].Should().Be(jp2);
            data[10].Should().Be(jp3);
            data[11].Should().Be(jp4);
        }

        [Fact(DisplayName = "01.2. 向1个通道里添加一些阿拉伯文数据")]
        public void TDMSChannel_DataStorageTest_01_2()
        {
            //TODO: 阿拉伯文的测试，暂时不知道怎么搞，以后再说。2024-11-25
            return;
            /*
             阿拉伯文
               هذا اختبار.
               مرحبًا بكم في خدمتنا.
               النظام قيد التحديث ، يرجى المحاولة مرة أخرى لاحقًا.
             */
            // Arrange

            var ar1 = "هذا اختبار.";
            var ar2 = "مرحبًا بكم في خدمتنا.";
            var ar3 = "النظام قيد التحديث ، يرجى المحاولة مرة أخرى لاحقًا.";

            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();

            tdmsFile.Open(fileInfo);

            using var group = tdmsFile.AddGroup("Group1");

            // Assert
            using var channel = group.AddChannel(TDMSDataType.String, "Channel1", "mm/s", string.Empty);

            channel.AppendData(ar1);
            channel.AppendData(ar2);
            channel.AppendData(ar3);

            // Act
            var data = channel.GetDataValues<string>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be(ar1);
            data[1].Should().Be(ar2);
            data[2].Should().Be(ar3);
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
            channel.SetData((byte)0x01);
            channel.AppendData((byte)0x02);
            channel.AppendData((byte)0xFF);
            // Act
            var data = channel.GetDataValues<byte>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be((byte)0x01);
            data[1].Should().Be((byte)0x02);
            data[2].Should().Be((byte)0xFF);
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
            channel.SetData((short)1);
            channel.AppendData((short)2);
            channel.AppendData((short)3);
            // Act
            var data = channel.GetDataValues<short>(0, 3);
            data.Length.Should().Be(3);
            data[0].Should().Be((short)1);
            data[1].Should().Be((short)2);
            data[2].Should().Be((short)3);
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
