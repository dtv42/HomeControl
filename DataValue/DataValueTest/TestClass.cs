// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestClass.cs" company="DTV-Online">
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
    using DataValueLib;

    #endregion Using Directives

    public enum ReturnCodes
    {
        Ok = 200,
        BadRequest = 400,
        NotFound = 404,
        ServerError = 500,
        NotImplemented = 501
    }

    public class IPAddress
    {
        public string Address { get; set; } = "127.0.0.1";
        public byte[] Bytes { get; set; } = new byte[] { 127, 0, 0, 1 };
        public List<string> Operations { get; set; } = new List<string> { "GET", "PUT", "POST" };
    }

    public class TestData
    {
        public string Name { get; set; } = "Data";
        public int Value { get; set; } = 42;
        public IPAddress Url { get; set; } = new IPAddress();
        public ReturnCodes Code { get; set; } = ReturnCodes.Ok;
    }

    public class TestClass : DataValue
    {
        public int Value { get; } = 42;
        public string Name { get; set; } = "Test";
        public static string Info { get; set; } = "Info";
        public TestData Data { get; set; } = new TestData();
        public TestData[] Array { get; set; } = new TestData[] { new TestData() { Name = "Test1" }, new TestData() { Name = "Test2" } };
        public List<TestData> List { get; set; } = new List<TestData> { new TestData() { Name = "Test1" }, new TestData() { Name = "Test2" } };
    }

    public class TestPropertyClass : DataValue
    {
        private string _name = "Test";
        private static string s_info = "Info";
        private TestData _data = new TestData();
        private TestData[] _array = new TestData[] { new TestData() { Name = "Test1" }, new TestData() { Name = "Test2" } };
        private List<TestData> _list = new List<TestData> { new TestData() { Name = "Test1" }, new TestData() { Name = "Test2" } };

        public int Value { get; } = 42;
        public string Name { get => _name; set => SetField(ref _name, value); }
        public static string Info { get => s_info; set => s_info = value; }
        public TestData Data { get => _data; set => SetField(ref _data, value); }
        public TestData[] Array { get => _array; set => SetField(ref _array, value); }
        public List<TestData> List { get => _list; set => SetField(ref _list, value); }
    }
}