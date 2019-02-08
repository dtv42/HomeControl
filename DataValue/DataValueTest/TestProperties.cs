// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestProperties.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueTest
{
    #region Using Directives

    using System.Collections.Generic;
    using Xunit;
    using DataValueLib;

    #endregion Using Directives

    /// <summary>
    /// XUnit testing class.
    /// </summary>
    public class TestProperties : PropertyHelper<TestProperties>
    {
        #region Public Properties

        public TestClass Test { get; set; } = new TestClass();
        public TestPropertyClass TestProperty { get; set; } = new TestPropertyClass();

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("Test")]
        [InlineData("Test.Value")]
        [InlineData("Test.Name")]
        [InlineData("Test.Info")]
        [InlineData("Test.Data")]
        [InlineData("Test.Data.Name")]
        [InlineData("Test.Data.Value")]
        [InlineData("Test.Data.Url")]
        [InlineData("Test.Data.Code")]
        [InlineData("Test.Data.Url.Address")]
        [InlineData("Test.Data.Url.Address.Length")]
        [InlineData("Test.Data.Url.Bytes")]
        [InlineData("Test.Data.Url.Bytes.Length")]
        [InlineData("Test.Data.Url.Operations")]
        [InlineData("Test.Data.Url.Operations.Count")]
        [InlineData("Test.Data.Url.Operations[].Length")]
        [InlineData("Test.Array")]
        [InlineData("Test.Array.Length")]
        [InlineData("Test.Array[].Name")]
        [InlineData("Test.Array[].Value")]
        [InlineData("Test.Array[].Url")]
        [InlineData("Test.Array[].Code")]
        [InlineData("Test.Array[].Url.Address")]
        [InlineData("Test.Array[].Url.Address.Length")]
        [InlineData("Test.Array[].Url.Bytes")]
        [InlineData("Test.Array[].Url.Bytes.Length")]
        [InlineData("Test.Array[].Url.Operations")]
        [InlineData("Test.Array[].Url.Operations.Count")]
        [InlineData("Test.Array[].Url.Operations[].Length")]
        [InlineData("Test.Array[0].Name")]
        [InlineData("Test.Array[0].Value")]
        [InlineData("Test.Array[0].Url")]
        [InlineData("Test.Array[0].Code")]
        [InlineData("Test.Array[0].Url.Address")]
        [InlineData("Test.Array[0].Url.Address.Length")]
        [InlineData("Test.Array[0].Url.Bytes")]
        [InlineData("Test.Array[0].Url.Bytes.Length")]
        [InlineData("Test.Array[0].Url.Operations")]
        [InlineData("Test.Array[0].Url.Operations.Count")]
        [InlineData("Test.Array[0].Url.Operations[0].Length")]
        [InlineData("Test.List")]
        [InlineData("Test.List[].Name")]
        [InlineData("Test.List[].Value")]
        [InlineData("Test.List[].Url")]
        [InlineData("Test.List[].Code")]
        [InlineData("Test.List[].Url.Address")]
        [InlineData("Test.List[].Url.Address.Length")]
        [InlineData("Test.List[].Url.Bytes")]
        [InlineData("Test.List[].Url.Bytes.Length")]
        [InlineData("Test.List[].Url.Operations")]
        [InlineData("Test.List[].Url.Operations.Count")]
        [InlineData("Test.List[].Url.Operations[].Length")]
        [InlineData("Test.List[0].Name")]
        [InlineData("Test.List[0].Value")]
        [InlineData("Test.List[0].Url")]
        [InlineData("Test.List[0].Code")]
        [InlineData("Test.List[0].Url.Address")]
        [InlineData("Test.List[0].Url.Address.Length")]
        [InlineData("Test.List[0].Url.Bytes")]
        [InlineData("Test.List[0].Url.Bytes.Length")]
        [InlineData("Test.List[0].Url.Operations")]
        [InlineData("Test.List[0].Url.Operations.Count")]
        [InlineData("Test.List[0].Url.Operations[0].Length")]
        [InlineData("Test.Status")]
        [InlineData("Test.Status.Code")]
        [InlineData("Test.Status.Name")]
        [InlineData("Test.Status.Explanation")]
        [InlineData("Test.Timestamp")]
        [InlineData("Test.IsGood")]
        [InlineData("Test.IsBad")]
        [InlineData("Test.IsUncertain")]
        public void TestIsProperty(string property)
        {
            Assert.True(IsProperty(property));
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("Test.Value")]
        [InlineData("Test.Name")]
        [InlineData("Test.Info")]
        [InlineData("Test.Data")]
        [InlineData("Test.Data.Name")]
        [InlineData("Test.Data.Value")]
        [InlineData("Test.Data.Url")]
        [InlineData("Test.Data.Code")]
        [InlineData("Test.Data.Url.Address")]
        [InlineData("Test.Data.Url.Address.Length")]
        [InlineData("Test.Data.Url.Bytes")]
        [InlineData("Test.Data.Url.Bytes.Length")]
        [InlineData("Test.Data.Url.Operations")]
        [InlineData("Test.Data.Url.Operations.Count")]
        [InlineData("Test.Data.Url.Operations[].Length")]
        [InlineData("Test.Array")]
        [InlineData("Test.Array.Length")]
        [InlineData("Test.Array[].Name")]
        [InlineData("Test.Array[].Value")]
        [InlineData("Test.Array[].Url")]
        [InlineData("Test.Array[].Code")]
        [InlineData("Test.Array[].Url.Address")]
        [InlineData("Test.Array[].Url.Address.Length")]
        [InlineData("Test.Array[].Url.Bytes")]
        [InlineData("Test.Array[].Url.Bytes.Length")]
        [InlineData("Test.Array[].Url.Operations")]
        [InlineData("Test.Array[].Url.Operations.Count")]
        [InlineData("Test.Array[].Url.Operations[].Length")]
        [InlineData("Test.Array[0].Name")]
        [InlineData("Test.Array[0].Value")]
        [InlineData("Test.Array[0].Url")]
        [InlineData("Test.Array[0].Code")]
        [InlineData("Test.Array[0].Url.Address")]
        [InlineData("Test.Array[0].Url.Address.Length")]
        [InlineData("Test.Array[0].Url.Bytes")]
        [InlineData("Test.Array[0].Url.Bytes.Length")]
        [InlineData("Test.Array[0].Url.Operations")]
        [InlineData("Test.Array[0].Url.Operations.Count")]
        [InlineData("Test.Array[0].Url.Operations[0].Length")]
        [InlineData("Test.List")]
        [InlineData("Test.List.Count")]
        [InlineData("Test.List[].Name")]
        [InlineData("Test.List[].Value")]
        [InlineData("Test.List[].Url")]
        [InlineData("Test.List[].Code")]
        [InlineData("Test.List[].Url.Address")]
        [InlineData("Test.List[].Url.Address.Length")]
        [InlineData("Test.List[].Url.Bytes")]
        [InlineData("Test.List[].Url.Bytes.Length")]
        [InlineData("Test.List[].Url.Operations")]
        [InlineData("Test.List[].Url.Operations.Count")]
        [InlineData("Test.List[].Url.Operations[].Length")]
        [InlineData("Test.List[0].Name")]
        [InlineData("Test.List[0].Value")]
        [InlineData("Test.List[0].Url")]
        [InlineData("Test.List[0].Code")]
        [InlineData("Test.List[0].Url.Address")]
        [InlineData("Test.List[0].Url.Address.Length")]
        [InlineData("Test.List[0].Url.Bytes")]
        [InlineData("Test.List[0].Url.Bytes.Length")]
        [InlineData("Test.List[0].Url.Operations")]
        [InlineData("Test.List[0].Url.Operations.Count")]
        [InlineData("Test.List[0].Url.Operations[0].Length")]
        [InlineData("Test.Status")]
        [InlineData("Test.Status.Code")]
        [InlineData("Test.Status.Name")]
        [InlineData("Test.Status.Explanation")]
        [InlineData("Test.Timestamp")]
        [InlineData("Test.IsGood")]
        [InlineData("Test.IsBad")]
        [InlineData("Test.IsUncertain")]
        public void TestGetPropertyInfo(string property)
        {
            var info = GetPropertyInfo(property);

            Assert.NotNull(info);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("Test.Value")]
        [InlineData("Test.Name")]
        [InlineData("Test.Info")]
        [InlineData("Test.Data")]
        [InlineData("Test.Data.Name")]
        [InlineData("Test.Data.Value")]
        [InlineData("Test.Data.Url")]
        [InlineData("Test.Data.Code")]
        [InlineData("Test.Data.Url.Address")]
        [InlineData("Test.Data.Url.Address.Length")]
        [InlineData("Test.Data.Url.Bytes")]
        [InlineData("Test.Data.Url.Bytes.Length")]
        [InlineData("Test.Data.Url.Bytes[0]")]
        [InlineData("Test.Data.Url.Bytes[1]")]
        [InlineData("Test.Data.Url.Bytes[2]")]
        [InlineData("Test.Data.Url.Bytes[3]")]
        [InlineData("Test.Data.Url.Operations")]
        [InlineData("Test.Data.Url.Operations.Count")]
        [InlineData("Test.Data.Url.Operations[0]")]
        [InlineData("Test.Data.Url.Operations[1]")]
        [InlineData("Test.Data.Url.Operations[2]")]
        [InlineData("Test.Data.Url.Operations[0].Length")]
        [InlineData("Test.Data.Url.Operations[1].Length")]
        [InlineData("Test.Data.Url.Operations[2].Length")]
        [InlineData("Test.Array")]
        [InlineData("Test.Array.Length")]
        [InlineData("Test.Array[0]")]
        [InlineData("Test.Array[0].Name")]
        [InlineData("Test.Array[0].Value")]
        [InlineData("Test.Array[0].Url")]
        [InlineData("Test.Array[0].Code")]
        [InlineData("Test.Array[0].Url.Address")]
        [InlineData("Test.Array[0].Url.Address.Length")]
        [InlineData("Test.Array[0].Url.Bytes")]
        [InlineData("Test.Array[0].Url.Bytes.Length")]
        [InlineData("Test.Array[0].Url.Bytes[0]")]
        [InlineData("Test.Array[0].Url.Bytes[1]")]
        [InlineData("Test.Array[0].Url.Bytes[2]")]
        [InlineData("Test.Array[0].Url.Bytes[3]")]
        [InlineData("Test.Array[0].Url.Operations")]
        [InlineData("Test.Array[0].Url.Operations.Count")]
        [InlineData("Test.Array[0].Url.Operations[0]")]
        [InlineData("Test.Array[0].Url.Operations[1]")]
        [InlineData("Test.Array[0].Url.Operations[2]")]
        [InlineData("Test.Array[0].Url.Operations[0].Length")]
        [InlineData("Test.Array[0].Url.Operations[1].Length")]
        [InlineData("Test.Array[0].Url.Operations[2].Length")]
        [InlineData("Test.Array[1]")]
        [InlineData("Test.Array[1].Name")]
        [InlineData("Test.Array[1].Value")]
        [InlineData("Test.Array[1].Url")]
        [InlineData("Test.Array[1].Code")]
        [InlineData("Test.Array[1].Url.Address")]
        [InlineData("Test.Array[1].Url.Address.Length")]
        [InlineData("Test.Array[1].Url.Bytes")]
        [InlineData("Test.Array[1].Url.Bytes.Length")]
        [InlineData("Test.Array[1].Url.Bytes[0]")]
        [InlineData("Test.Array[1].Url.Bytes[1]")]
        [InlineData("Test.Array[1].Url.Bytes[2]")]
        [InlineData("Test.Array[1].Url.Bytes[3]")]
        [InlineData("Test.Array[1].Url.Operations")]
        [InlineData("Test.Array[1].Url.Operations.Count")]
        [InlineData("Test.Array[1].Url.Operations[0]")]
        [InlineData("Test.Array[1].Url.Operations[1]")]
        [InlineData("Test.Array[1].Url.Operations[2]")]
        [InlineData("Test.Array[1].Url.Operations[0].Length")]
        [InlineData("Test.Array[1].Url.Operations[1].Length")]
        [InlineData("Test.Array[1].Url.Operations[2].Length")]
        [InlineData("Test.List")]
        [InlineData("Test.List.Count")]
        [InlineData("Test.List[0]")]
        [InlineData("Test.List[0].Name")]
        [InlineData("Test.List[0].Value")]
        [InlineData("Test.List[0].Url")]
        [InlineData("Test.List[0].Code")]
        [InlineData("Test.List[0].Url.Address")]
        [InlineData("Test.List[0].Url.Address.Length")]
        [InlineData("Test.List[0].Url.Bytes")]
        [InlineData("Test.List[0].Url.Bytes.Length")]
        [InlineData("Test.List[0].Url.Bytes[0]")]
        [InlineData("Test.List[0].Url.Bytes[1]")]
        [InlineData("Test.List[0].Url.Bytes[2]")]
        [InlineData("Test.List[0].Url.Bytes[3]")]
        [InlineData("Test.List[0].Url.Operations")]
        [InlineData("Test.List[0].Url.Operations.Count")]
        [InlineData("Test.List[0].Url.Operations[0]")]
        [InlineData("Test.List[0].Url.Operations[1]")]
        [InlineData("Test.List[0].Url.Operations[2]")]
        [InlineData("Test.List[0].Url.Operations[0].Length")]
        [InlineData("Test.List[0].Url.Operations[1].Length")]
        [InlineData("Test.List[0].Url.Operations[2].Length")]
        [InlineData("Test.List[1]")]
        [InlineData("Test.List[1].Name")]
        [InlineData("Test.List[1].Value")]
        [InlineData("Test.List[1].Url")]
        [InlineData("Test.List[1].Code")]
        [InlineData("Test.List[1].Url.Address")]
        [InlineData("Test.List[1].Url.Address.Length")]
        [InlineData("Test.List[1].Url.Bytes")]
        [InlineData("Test.List[1].Url.Bytes.Length")]
        [InlineData("Test.List[1].Url.Bytes[0]")]
        [InlineData("Test.List[1].Url.Bytes[1]")]
        [InlineData("Test.List[1].Url.Bytes[2]")]
        [InlineData("Test.List[1].Url.Bytes[3]")]
        [InlineData("Test.List[1].Url.Operations")]
        [InlineData("Test.List[1].Url.Operations.Count")]
        [InlineData("Test.List[1].Url.Operations[0]")]
        [InlineData("Test.List[1].Url.Operations[1]")]
        [InlineData("Test.List[1].Url.Operations[2]")]
        [InlineData("Test.List[1].Url.Operations[0].Length")]
        [InlineData("Test.List[1].Url.Operations[1].Length")]
        [InlineData("Test.List[1].Url.Operations[2].Length")]
        [InlineData("Test.Status")]
        [InlineData("Test.Status.Code")]
        [InlineData("Test.Status.Name")]
        [InlineData("Test.Status.Explanation")]
        [InlineData("Test.Timestamp")]
        [InlineData("Test.IsGood")]
        [InlineData("Test.IsBad")]
        [InlineData("Test.IsUncertain")]
        public void TestGetPropertyValue(string property)
        {
            var value = GetPropertyValue(property);

            Assert.NotNull(value);
        }

        [Theory]
        [InlineData("Test.Name", "TestData")]
        [InlineData("Test.Data.Name", "TestData")]
        [InlineData("Test.Data.Value", 12)]
        [InlineData("Test.Data.Code", ReturnCodes.ServerError)]
        [InlineData("Test.Data.Url.Address", "8.8.8.8")]
        [InlineData("Test.Array[0].Name", "TestData")]
        [InlineData("Test.Array[0].Value", 12)]
        [InlineData("Test.Array[0].Code", ReturnCodes.ServerError)]
        [InlineData("Test.Array[0].Url.Address", "8.8.8.8")]
        [InlineData("Test.Array[0].Url.Bytes[0]", (byte)8)]
        [InlineData("Test.Array[0].Url.Bytes[1]", (byte)8)]
        [InlineData("Test.Array[0].Url.Bytes[2]", (byte)8)]
        [InlineData("Test.Array[0].Url.Bytes[3]", (byte)8)]
        [InlineData("Test.Array[0].Url.Operations[0]", "Get")]
        [InlineData("Test.Array[0].Url.Operations[1]", "Put")]
        [InlineData("Test.Array[0].Url.Operations[2]", "Post")]
        [InlineData("Test.Array[1].Name", "TestData")]
        [InlineData("Test.Array[1].Value", 12)]
        [InlineData("Test.Array[1].Code", ReturnCodes.ServerError)]
        [InlineData("Test.Array[1].Url.Address", "8.8.8.8")]
        [InlineData("Test.Array[1].Url.Bytes[0]", (byte)8)]
        [InlineData("Test.Array[1].Url.Bytes[1]", (byte)8)]
        [InlineData("Test.Array[1].Url.Bytes[2]", (byte)8)]
        [InlineData("Test.Array[1].Url.Bytes[3]", (byte)8)]
        [InlineData("Test.Array[1].Url.Operations[0]", "Get")]
        [InlineData("Test.Array[1].Url.Operations[1]", "Put")]
        [InlineData("Test.Array[1].Url.Operations[2]", "Post")]
        public void TestSetPropertyValue(string property, dynamic value)
        {
            SetPropertyValue(property, value);
            var data = GetPropertyValue(property);

            Assert.NotNull(data);
            Assert.Equal(data, value);
        }

        [Fact]
        public void TestDataUrl()
        {
            Assert.Equal("127.0.0.1", Test.Data.Url.Address);
            Assert.Equal(4, Test.Data.Url.Bytes.Length);
            Assert.Equal(127, Test.Data.Url.Bytes[0]);
            Assert.Equal(0, Test.Data.Url.Bytes[1]);
            Assert.Equal(0, Test.Data.Url.Bytes[2]);
            Assert.Equal(1, Test.Data.Url.Bytes[3]);
            Assert.Equal(3, Test.Data.Url.Operations.Count);
            Assert.Equal("GET", Test.Data.Url.Operations[0]);
            Assert.Equal("PUT", Test.Data.Url.Operations[1]);
            Assert.Equal("POST", Test.Data.Url.Operations[2]);

            SetPropertyValue("Test.Data.Url", new IPAddress() { Address = "8.8.8.8", Bytes = new byte[] { 8, 8, 8, 8 }, Operations = new List<string> { "PUT" } });
            Assert.Equal("8.8.8.8", Test.Data.Url.Address);
            Assert.Equal(4, Test.Data.Url.Bytes.Length);
            Assert.Equal(8, Test.Data.Url.Bytes[0]);
            Assert.Equal(8, Test.Data.Url.Bytes[1]);
            Assert.Equal(8, Test.Data.Url.Bytes[2]);
            Assert.Equal(8, Test.Data.Url.Bytes[3]);
            Assert.Single(Test.Data.Url.Operations);
            Assert.Equal("PUT", Test.Data.Url.Operations[0]);

            IPAddress url = (IPAddress)GetPropertyValue("Test.Data.Url");
            Assert.Equal("8.8.8.8", url.Address);
            Assert.Equal(4, url.Bytes.Length);
            Assert.Equal(8, url.Bytes[0]);
            Assert.Equal(8, url.Bytes[1]);
            Assert.Equal(8, url.Bytes[2]);
            Assert.Equal(8, url.Bytes[3]);

            byte[] bytes = (byte[])GetPropertyValue("Test.Data.Url.Bytes");
            Assert.Equal(4, bytes.Length);
            Assert.Equal(8, bytes[0]);
            Assert.Equal(8, bytes[1]);
            Assert.Equal(8, bytes[2]);
            Assert.Equal(8, bytes[3]);

            byte bytedata = (byte)GetPropertyValue("Test.Data.Url.Bytes[0]");
            Assert.Equal(8, bytedata);

            List<string> operations = (List<string>)GetPropertyValue("Test.Data.Url.Operations");
            Assert.Single(operations);
            Assert.Equal("PUT", operations[0]);

            string stringdata = (string)GetPropertyValue("Test.Data.Url.Operations[0]");
            Assert.Equal("PUT", stringdata);
        }

        [Fact]
        public void TestArrayUrl()
        {
            Assert.Equal("127.0.0.1", Test.Array[0].Url.Address);
            Assert.Equal(4, Test.Array[0].Url.Bytes.Length);
            Assert.Equal(127, Test.Array[0].Url.Bytes[0]);
            Assert.Equal(0, Test.Array[0].Url.Bytes[1]);
            Assert.Equal(0, Test.Array[0].Url.Bytes[2]);
            Assert.Equal(1, Test.Array[0].Url.Bytes[3]);
            Assert.Equal(3, Test.Array[0].Url.Operations.Count);
            Assert.Equal("GET", Test.Array[0].Url.Operations[0]);
            Assert.Equal("PUT", Test.Array[0].Url.Operations[1]);
            Assert.Equal("POST", Test.Array[0].Url.Operations[2]);

            SetPropertyValue("Test.Array[0].Url", new IPAddress() { Address = "8.8.8.8", Bytes = new byte[] { 8, 8, 8, 8 }, Operations = new List<string> { "PUT" } });
            Assert.Equal("8.8.8.8", Test.Array[0].Url.Address);
            Assert.Equal(4, Test.Array[0].Url.Bytes.Length);
            Assert.Equal(8, Test.Array[0].Url.Bytes[0]);
            Assert.Equal(8, Test.Array[0].Url.Bytes[1]);
            Assert.Equal(8, Test.Array[0].Url.Bytes[2]);
            Assert.Equal(8, Test.Array[0].Url.Bytes[3]);
            Assert.Single(Test.Array[0].Url.Operations);
            Assert.Equal("PUT", Test.Array[0].Url.Operations[0]);

            IPAddress url = (IPAddress)GetPropertyValue("Test.Array[0].Url");
            Assert.Equal("8.8.8.8", url.Address);
            Assert.Equal(4, url.Bytes.Length);
            Assert.Equal(8, url.Bytes[0]);
            Assert.Equal(8, url.Bytes[1]);
            Assert.Equal(8, url.Bytes[2]);
            Assert.Equal(8, url.Bytes[3]);

            byte[] bytes = (byte[])GetPropertyValue("Test.Array[0].Url.Bytes");
            Assert.Equal(4, bytes.Length);
            Assert.Equal(8, bytes[0]);
            Assert.Equal(8, bytes[1]);
            Assert.Equal(8, bytes[2]);
            Assert.Equal(8, bytes[3]);

            byte bytedata = (byte)GetPropertyValue("Test.Array[0].Url.Bytes[0]");
            Assert.Equal(8, bytedata);

            List<string> operations = (List<string>)GetPropertyValue("Test.Array[0].Url.Operations");
            Assert.Single(operations);
            Assert.Equal("PUT", operations[0]);

            string stringdata = (string)GetPropertyValue("Test.Array[0].Url.Operations[0]");
            Assert.Equal("PUT", stringdata);
        }

        [Fact]
        public void TestListUrl()
        {
            Assert.Equal("127.0.0.1", Test.List[0].Url.Address);
            Assert.Equal(4, Test.List[0].Url.Bytes.Length);
            Assert.Equal(127, Test.List[0].Url.Bytes[0]);
            Assert.Equal(0, Test.List[0].Url.Bytes[1]);
            Assert.Equal(0, Test.List[0].Url.Bytes[2]);
            Assert.Equal(1, Test.List[0].Url.Bytes[3]);
            Assert.Equal(3, Test.List[0].Url.Operations.Count);
            Assert.Equal("GET", Test.List[0].Url.Operations[0]);
            Assert.Equal("PUT", Test.List[0].Url.Operations[1]);
            Assert.Equal("POST", Test.List[0].Url.Operations[2]);

            SetPropertyValue("Test.List[0].Url", new IPAddress() { Address = "8.8.8.8", Bytes = new byte[] { 8, 8, 8, 8 }, Operations = new List<string> { "PUT" } });
            Assert.Equal("8.8.8.8", Test.List[0].Url.Address);
            Assert.Equal(4, Test.List[0].Url.Bytes.Length);
            Assert.Equal(8, Test.List[0].Url.Bytes[0]);
            Assert.Equal(8, Test.List[0].Url.Bytes[1]);
            Assert.Equal(8, Test.List[0].Url.Bytes[2]);
            Assert.Equal(8, Test.List[0].Url.Bytes[3]);
            Assert.Single(Test.List[0].Url.Operations);
            Assert.Equal("PUT", Test.List[0].Url.Operations[0]);

            IPAddress url = (IPAddress)GetPropertyValue("Test.List[0].Url");
            Assert.Equal("8.8.8.8", url.Address);
            Assert.Equal(4, url.Bytes.Length);
            Assert.Equal(8, url.Bytes[0]);
            Assert.Equal(8, url.Bytes[1]);
            Assert.Equal(8, url.Bytes[2]);
            Assert.Equal(8, url.Bytes[3]);

            byte[] bytes = (byte[])GetPropertyValue("Test.List[0].Url.Bytes");
            Assert.Equal(4, bytes.Length);
            Assert.Equal(8, bytes[0]);
            Assert.Equal(8, bytes[1]);
            Assert.Equal(8, bytes[2]);
            Assert.Equal(8, bytes[3]);

            byte bytedata = (byte)GetPropertyValue("Test.List[0].Url.Bytes[0]");
            Assert.Equal(8, bytedata);

            List<string> operations = (List<string>)GetPropertyValue("Test.List[0].Url.Operations");
            Assert.Single(operations);
            Assert.Equal("PUT", operations[0]);

            string stringdata = (string)GetPropertyValue("Test.List[0].Url.Operations[0]");
            Assert.Equal("PUT", stringdata);
        }

        [Fact]
        public void TestPropertyChanged()
        {
            bool nameChanged = false;
            bool dataChanged = false;
            bool listChanged = false;
            bool arrayChanged = false;

            TestProperty.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Name")
                {
                    nameChanged = true;
                }

                if (e.PropertyName == "Data")
                {
                    dataChanged = true;
                }

                if (e.PropertyName == "List")
                {
                    listChanged = true;
                }

                if (e.PropertyName == "Array")
                {
                    arrayChanged = true;
                }
            };

            TestProperty.Name = "Some new value";
            TestProperty.Data = new TestData();
            TestProperty.List = new List<TestData> { };
            TestProperty.Array = System.Array.Empty<TestData>();

            Assert.True(nameChanged);
            Assert.True(dataChanged);
            Assert.True(listChanged);
            Assert.True(arrayChanged);
        }

        #endregion
    }
}