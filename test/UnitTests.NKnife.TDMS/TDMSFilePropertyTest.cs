using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.TDMS;
using NKnife.TDMS.Default;

namespace UnitTests.NKnife.TDMS
{
    public class TDMSFilePropertyTest : IClassFixture<TestFileContext>
    {

        [Fact(DisplayName = "检查属性是否存在")]
        public void PropertyExists_Test()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
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
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            string propertyName = "TestProperty";
            tdmsFile.CreateOrUpdateProperty(propertyName, "SomeValue");

            // Act
            bool exists = tdmsFile.PropertyExists(propertyName);

            // Assert
            exists.Should().BeTrue("因为属性已经被添加");
        }

        [Fact(DisplayName = "新增2个属性，获取所有属性名称，共7个属性")]
        public void GetPropertyNames_Test()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            var expectedPropertyNames = new List<string> { "Property1", "Property2" };
            tdmsFile.CreateOrUpdateProperty("Property1", "Value1");
            tdmsFile.CreateOrUpdateProperty("Property2", "Value2");

            // Act
            var propertyNames = tdmsFile.GetPropertyNames();

            // Assert
            propertyNames.Should().Contain(expectedPropertyNames);
        }

        [Fact(DisplayName = "当没有新增属性时，获取所有属性，仅有5个基础属性")]
        public void GetPropertyNames_WhenNoProperties_Test()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            // Act
            var propertyNames = tdmsFile.GetPropertyNames();

            // Assert
            propertyNames.Length.Should().Be(5);
        }

        [Fact(DisplayName = "获取默认属性名称")]
        public void GetDefaultPropertyNames_Test1()
        {
            // Arrange
            using var context  = new TestFileContext();
            var       fileInfo = context.CreateTestFile();
            using var tdmsFile = new TDMSFile();
            tdmsFile.Open(fileInfo);

            // Act
            var defaultPropertyNames = tdmsFile.GetDefaultProperties();

            // Assert
            defaultPropertyNames.Count.Should().Be(5);
            var key = defaultPropertyNames.Keys;
            key.Should().Contain("name");
            key.Should().Contain("description");
            key.Should().Contain("title");
            key.Should().Contain("author");
            key.Should().Contain("datetime");
        }

        [Fact(DisplayName = "获取默认属性名称，创建时未给出实际值，依旧返回基本的5个值（值为空）")]
        public void GetDefaultPropertyNames_Test2()
        {
            // Arrange
            var fileInfo = new TDMSFileInfo($"~tdms{nameof(GetDefaultPropertyNames_Test2)}.tdms");

            using (var tdmsFile = new TDMSFile())
            {
                tdmsFile.Open(fileInfo);

                // Act
                var defaultPropertyNames = tdmsFile.GetDefaultProperties();

                // Assert
                defaultPropertyNames.Count.Should().Be(5);
                var key = defaultPropertyNames.Keys;
                key.Should().Contain("name");
                key.Should().Contain("description");
                key.Should().Contain("title");
                key.Should().Contain("author");
                key.Should().Contain("datetime");

                defaultPropertyNames["name"].Should().BeEmpty();
                defaultPropertyNames["description"].Should().BeEmpty();
                defaultPropertyNames["title"].Should().BeEmpty();
                defaultPropertyNames["author"].Should().BeEmpty();

                var datetime = defaultPropertyNames["datetime"];
                datetime.Should().NotBeEmpty();
                DateTime.TryParse(datetime, out var dt).Should().BeTrue();

                var src = fileInfo.DateTime;
                dt.Year.Should().Be(src.Year);
                dt.Month.Should().Be(src.Month);
                dt.Day.Should().Be(src.Day);
                dt.Hour.Should().Be(src.Hour);
                dt.Minute.Should().Be(src.Minute);
                dt.Second.Should().Be(src.Second);
                dt.Millisecond.Should().Be(src.Millisecond);
            }
            File.Delete(fileInfo.FilePath);
        }
    }
}