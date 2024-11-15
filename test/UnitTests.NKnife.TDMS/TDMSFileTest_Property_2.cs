using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS.Default;

// ReSharper disable InconsistentNaming

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFileTest_Property_2 : IClassFixture<TestFileContext>
    {
        public TDMSFileTest_Property_2(TestFileContext context)
        {
            context.CleanFiles();
        }

        [Fact(DisplayName = "检查属性是否存在")]
        public void PropertyExists_Test()
        {
            // Arrange
            var    fileInfo     = TestFileContext.CreateTestFile();
            using var    tdmsFile     = new TDMSFile();
            tdmsFile.Open(fileInfo);
            
            string propertyName = "TestProperty";

            // Act
            bool exists = tdmsFile.PropertyExists(propertyName);

            // Assert
            exists.Should().BeFalse("因为属性尚未被添加");
        }

        [Fact(DisplayName = "检查属性存在")]
        public void PropertyExists_AfterAdding_Test()
        {
            // Arrange
            var       fileInfo = TestFileContext.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            string propertyName = "TestProperty";
            tdmsFile.AddOrUpdateProperty(propertyName, "SomeValue");

            // Act
            bool exists = tdmsFile.PropertyExists(propertyName);

            // Assert
            exists.Should().BeTrue("因为属性已经被添加");
        }

        [Fact(DisplayName = "获取所有属性名称")]
        public void GetPropertyNames_Test()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            var expectedPropertyNames = new List<string> { "Property1", "Property2" };
            tdmsFile.AddOrUpdateProperty("Property1", "Value1");
            tdmsFile.AddOrUpdateProperty("Property2", "Value2");

            // Act
            var propertyNames = tdmsFile.GetPropertyNames();

            // Assert
            propertyNames.Should().Contain(expectedPropertyNames);
        }

        [Fact(DisplayName = "当没有属性时，获取所有属性")]
        public void GetPropertyNames_WhenNoProperties_Test()
        {
            // Arrange
            var fileInfo = TestFileContext.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            // Act
            var propertyNames = tdmsFile.GetPropertyNames();

            // Assert
            propertyNames.Length.Should().Be(5);
        }
    }
}