// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppData.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueApp.Models
{
    #region Using Directives

    using System.Collections.Generic;
    using DataValueLib;

    #endregion Using Directives

    public class AppData : DataValue
    {
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
            public string Address { get; set; } = string.Empty;
            public byte[] Bytes { get; set; } = System.Array.Empty<byte>();
            public List<string> Operations { get; set; } = new List<string> { };
        }

        public class TestData
        {
            public string Name { get; set; } = string.Empty;
            public int Value { get; set; }
            public IPAddress Url { get; set; } = new IPAddress();
            public ReturnCodes Code { get; set; } = ReturnCodes.Ok;
        }

        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public static string Info { get; set; }
        public TestData Data { get; set; } = new TestData();
        public TestData[] Array { get; set; } = System.Array.Empty<TestData>();
        public List<TestData> List { get; set; } = new List<TestData> { };
    }
}