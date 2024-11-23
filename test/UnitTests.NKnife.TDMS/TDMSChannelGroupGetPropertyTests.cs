using FluentAssertions;
using NKnife.TDMS.Common;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSChannelGroupGetPropertyTests
    {
        [Fact(DisplayName = "String数据属性的读取,新建属性")]
        public void SetProperty_Test_String()
        {
            // Arrange
            using var context = new TestFileContext();
            var fileInfo = context.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue = "Test String Value";

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out string value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "UInt8数据属性的读取,新建属性")]
        public void SetProperty_Test_UInt8()
        {
            // Arrange
            using var context      = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyUInt8";
            var       propertyValue = (byte)255;

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out byte value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Int16数据属性的读取,新建属性")]
        public void SetProperty_Test_Int16()
        {
            // Arrange
            using var context      = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyInt16";
            var       propertyValue = (short)32767;

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out short value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Int32数据属性的读取,新建属性")]
        public void SetProperty_Test_Int32()
        {
            // Arrange
            using var context      = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyInt32";
            var       propertyValue = 2147483647;

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out int value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Float数据属性的读取,新建属性")]
        public void SetProperty_Test_Float()
        {
            // Arrange
            using var context      = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyFloat";
            var       propertyValue = 3.14f;

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out float value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Double数据属性的读取,新建属性")]
        public void SetProperty_Test_Double()
        {
            // Arrange
            using var context      = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyDouble";
            var       propertyValue = 3.14159265359;

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out double value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "Timestamp数据属性的读取,新建属性")]
        public void SetProperty_Test_Timestamp()
        {
            // Arrange
            using var context      = new TestFileContext();
            var       fileInfo      = context.CreateTestFile();
            var       propertyName  = "PropertyTimestamp";
            var       propertyValue = new DateTime(2023, 10, 1, 12, 55, 55, DateTimeKind.Utc);

            using var file = new TDMSFile();
            file.Open(fileInfo);
            var group = file.AddGroup("Group1");

            // Act
            group.CreateOrUpdateProperty(propertyName, propertyValue);

            // Assert
            var success = group.TryGetProperty(propertyName, out DateTime value);

            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }
    }
}