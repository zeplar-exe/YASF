using System;
using System.Diagnostics;
using NUnit.Framework;
using SettingsConfig.Parser;
using SettingsConfig.Serialization;

namespace SettingsConfig_Tests
{
    [TestFixture]
    public class DeserializeTests
    {
        [Test]
        public void TestDeserializer()
        {
            var document = new SettingsParser("name=\"Bru\" text=\"Matou\" ignorable=500").ParseDocument();
            var type = SettingsDeserializer.Deserialize<TestType>(document);
            
            Assert.True(type.Name == "Bru" && type.Text == "Matou" && type.Ignorable == 0);
        }

        [Test]
        public void TestDeserializerNesting()
        {
            var document = new SettingsParser("type=[name=\"h\"]").ParseDocument();
            var type = SettingsDeserializer.Deserialize<TestNestedType>(document);
            
            Assert.True(type.Type.Name == "h");
        }

        [Test]
        public void TestDeserializerName()
        {
            var document = new SettingsParser("some_property=\"AYO\"").ParseDocument();
            var type = SettingsDeserializer.Deserialize<TestCustomNamesType>(document);
            
            Assert.True(type.ThisIsAProperty == "AYO");
        }
    }

    public class TestType
    {
        public string Name { get; set; }
        internal string Text { get; set; }
        [SettingsDeserializer.Ignore] public int Ignorable { get; set; }
    }

    public class TestCustomNamesType
    {
        [SettingsDeserializer.SerializationName("some_property")]
        public string ThisIsAProperty { get; set; }
    }

    public class TestNestedType
    {
        public TestType Type { get; set; }
    }
}