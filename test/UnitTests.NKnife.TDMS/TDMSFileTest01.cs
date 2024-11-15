using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileTest01 : IClassFixture<TestFileContext>
    {
        public TDMSFileTest01(TestFileContext context)
        {
            context.CleanFiles();
        }

        [Fact(DisplayName = "SetProperty_Test_01")]
        public void SetProperty_Test_01()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue = "Value1";
            var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.SetProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out _);
            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "SetProperty_Test_02")]
        public void SetProperty_Test_02()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue = "Value1";
            var file = new TDMSFile();
            file.Open(fileInfo);

            // Act
            file.SetProperty(propertyName, propertyValue);

            // Assert
            var (success, value) = file.GetProperty(propertyName, out _);
            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "GetProperty_Test_01")]
        public void GetProperty_Test_01()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue = "Value1";
            var file = new TDMSFile();
            file.Open(fileInfo);
            file.SetProperty(propertyName, propertyValue);

            // Act
            var (success, value) = file.GetProperty(propertyName, out _);

            // Assert
            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }

        [Fact(DisplayName = "GetProperty_Test_02")]
        public void GetProperty_Test_02()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            var propertyName = "Property1";
            var propertyValue = "Value1";
            var file = new TDMSFile();
            file.Open(fileInfo);
            file.SetProperty(propertyName, propertyValue);

            // Act
            var (success, value) = file.GetProperty(propertyName, out _);

            // Assert
            success.Should().BeTrue();
            value.Should().Be(propertyValue);
        }
    }
}
