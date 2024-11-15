using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;
// ReSharper disable InconsistentNaming

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileTest_Property_01 : IClassFixture<TestFileContext>
    {
        public TDMSFileTest_Property_01(TestFileContext context)
        {
            context.CleanFiles();
        }

        [Fact(DisplayName = "String数据属性的读取,新建属性")]
        public void SetProperty_Test_01()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue = "NI has defined a technical data management (TDM)+";

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "String数据属性的读取,更新属性")]
        public void SetProperty_Test_02()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue1 = "NI has defined a technical data management (TDM)+";
            var propertyValue2 = "TDMS File Structure?";

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
        }

        [Fact(DisplayName = "String数据属性的读取,中文,更新属性")]
        public void SetProperty_Test_03()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue1 = "TDMS是NI定义的一种技术数据管理解决方案";
            var propertyValue2 = "写入可数据管理的TDMS文件?";

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
        }

        [Fact(DisplayName = "UInt8数据属性的读取,新建属性")]
        public void SetProperty_Test_UInt8()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyUInt8";
            var propertyValue = (byte)255;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Int16数据属性的读取,新建属性")]
        public void SetProperty_Test_Int16()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyInt16";
            var propertyValue = (short)32767;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Int32数据属性的读取,新建属性")]
        public void SetProperty_Test_Int32()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyInt32";
            var propertyValue = 2147483647;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Float数据属性的读取,新建属性")]
        public void SetProperty_Test_Float()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyFloat";
            var propertyValue = 3.14f;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Double数据属性的读取,新建属性")]
        public void SetProperty_Test_Double()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyDouble";
            var propertyValue = 3.14159265359;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
            type.Should().Be(TDMSDataType.Double);
        }

        [Fact(DisplayName = "Timestamp数据属性的读取,新建属性")]
        public void SetProperty_Test_Timestamp()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyTimestamp";
            var propertyValue = new DateTime(2023, 10, 1, 12, 0, 0, DateTimeKind.Utc);

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
            type.Should().Be(TDMSDataType.Timestamp);
        }

        [Fact(DisplayName = "String数据属性的读取,新建属性,多个属性")]
        public void SetProperty_Test_04()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName1 = "Property1";
            var propertyValue1 = "NI has defined a technical data management (TDM)+";
            var propertyName2 = "Property2";
            var propertyValue2 = "TDMS File Structure?";
            var propertyName3 = "Property3";
            var propertyValue3 = "TDMS是NI定义的一种技术数据管理解决方案";
            var propertyName4 = "Property4";
            var propertyValue4 = "写入可数据管理的TDMS文件?";

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName1, propertyValue1);
            file.AddOrUpdateProperty(propertyName2, propertyValue2);
            file.AddOrUpdateProperty(propertyName3, propertyValue3);
            file.AddOrUpdateProperty(propertyName4, propertyValue4);

            // Assert
            var (success1, value1) = file.GetProperty(propertyName1, out var type1);
            var (success2, value2) = file.GetProperty(propertyName2, out var type2);
            var (success3, value3) = file.GetProperty(propertyName3, out var type3);
            var (success4, value4) = file.GetProperty(propertyName4, out var type4);

            success1.Should().BeTrue();
            value1.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.String);
            success2.Should().BeTrue();
            value2.Should().Be(propertyValue2);
            success3.Should().BeTrue();
            value3.Should().Be(propertyValue3);
            success4.Should().BeTrue();
            value4.Should().Be(propertyValue4);
        }
        [Fact(DisplayName = "UInt8数据属性的读取,更新属性")]
        public void SetProperty_Test_UInt8_Update()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyUInt8";
            var propertyValue1 = (byte)255;
            var propertyValue2 = (byte)128;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.UInt8);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
            type2.Should().Be(TDMSDataType.UInt8);
        }

        [Fact(DisplayName = "Int16数据属性的读取,更新属性")]
        public void SetProperty_Test_Int16_Update()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyInt16";
            var propertyValue1 = (short)32767;
            var propertyValue2 = (short)12345;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.Int16);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
            type2.Should().Be(TDMSDataType.Int16);
        }

        [Fact(DisplayName = "Int32数据属性的读取,更新属性")]
        public void SetProperty_Test_Int32_Update()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyInt32";
            var propertyValue1 = 2147483647;
            var propertyValue2 = 1234567890;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.Int32);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
            type2.Should().Be(TDMSDataType.Int32);
        }

        [Fact(DisplayName = "Float数据属性的读取,更新属性")]
        public void SetProperty_Test_Float_Update()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyFloat";
            var propertyValue1 = 3.14f;
            var propertyValue2 = 1.23f;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.Float);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
            type2.Should().Be(TDMSDataType.Float);
        }

        [Fact(DisplayName = "Double数据属性的读取,更新属性")]
        public void SetProperty_Test_Double_Update()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyDouble";
            var propertyValue1 = 3.14159265359;
            var propertyValue2 = 2.71828182846;

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.Double);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
            type2.Should().Be(TDMSDataType.Double);
        }

        [Fact(DisplayName = "Timestamp数据属性的读取,更新属性")]
        public void SetProperty_Test_Timestamp_Update()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "PropertyTimestamp";
            var propertyValue1 = new DateTime(2023, 10, 1, 12, 0, 0, DateTimeKind.Utc);
            var propertyValue2 = new DateTime(2023, 11, 1, 12, 0, 0, DateTimeKind.Utc);

            using var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue1);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out var type1);

            success.Should().BeTrue();
            value.Should().Be(propertyValue1);
            type1.Should().Be(TDMSDataType.Timestamp);

            // Act
            file.AddOrUpdateProperty(propertyName, propertyValue2);

            // Assert
            (success, value) = file.GetProperty(propertyName, out var type2);
            success.Should().BeTrue();
            value.Should().Be(propertyValue2);
            type2.Should().Be(TDMSDataType.Timestamp);
        }


    }
}