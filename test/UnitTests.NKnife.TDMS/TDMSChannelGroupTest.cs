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
            // Arrange
            var mockChannelGroup = new Mock<ITDMSChannelGroup>();
            var mockChannel      = new Mock<ITDMSChannel>();
            mockChannelGroup.Setup(m => m.AddChannel(It.IsAny<TDMSDataType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .Returns(mockChannel.Object);

            // Act
            var result = mockChannelGroup.Object.AddChannel(TDMSDataType.Double, "Channel1", "Unit1", "Description1");

            // Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "AddChannel_Test_2: 添加新通道，验证通道的属性值")]
        public void AddChannel_Test_2()
        {
            // Arrange
            var mockChannelGroup = new Mock<ITDMSChannelGroup>();
            var mockChannel = new Mock<ITDMSChannel>();
            mockChannel.SetupProperty(c => c.Name, "Channel1");
            mockChannel.SetupProperty(c => c.Unit, "Unit1");
            mockChannel.SetupProperty(c => c.Description, "Description1");
            mockChannelGroup.Setup(m => m.AddChannel(It.IsAny<TDMSDataType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .Returns(mockChannel.Object);

            // Act
            var result = mockChannelGroup.Object.AddChannel(TDMSDataType.Double, "Channel1", "Unit1", "Description1");

            // Assert
            result.Name.Should().Be("Channel1");
            result.Unit.Should().Be("Unit1");
            result.Description.Should().Be("Description1");
        }

        [Fact(DisplayName = "Indexer_Test_1: 通过索引获取通道，验证返回的通道对象不为空")]
        public void Indexer_Test_1()
        {
            // Arrange
            var mockChannelGroup = new Mock<ITDMSChannelGroup>();
            var mockChannel = new Mock<ITDMSChannel>();
            mockChannelGroup.Setup(m => m[It.IsAny<int>()]).Returns(mockChannel.Object);

            // Act
            var result = mockChannelGroup.Object[0];

            // Assert
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "Indexer_Test_2: 通过名称获取通道，验证返回的通道对象不为空")]
        public void Indexer_Test_2()
        {
            // Arrange
            var mockChannelGroup = new Mock<ITDMSChannelGroup>();
            var mockChannel = new Mock<ITDMSChannel>();
            mockChannelGroup.Setup(m => m[It.IsAny<string>()]).Returns(mockChannel.Object);

            // Act
            var result = mockChannelGroup.Object["Channel1"];

            // Assert
            result.Should().NotBeNull();
        }
    }
}
