// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDataValue.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace DataValueTest
{
    #region Using Directives

    using System;
    using Xunit;
    using DataValueLib;

    #endregion Using Directives

    public class TestDataValue
    {
        #region Test Methods

        [Fact]
        public void TestUncertain()
        {
            DateTime frozen = DateTime.Now;
            TestClass Test = new TestClass();
            Assert.NotNull(Test.Status);
            Assert.Equal(DataValue.Uncertain, Test.Status);
            Assert.True(Test.Timestamp > frozen);
            Assert.False(Test.IsBad);
            Assert.False(Test.IsGood);
            Assert.True(Test.IsUncertain);
        }

        [Theory]
        [InlineData(DataStatus.Bad)]
        [InlineData(DataStatus.BadUnexpectedError)]
        [InlineData(DataStatus.BadInternalError)]
        [InlineData(DataStatus.BadOutOfMemory)]
        [InlineData(DataStatus.BadResourceUnavailable)]
        [InlineData(DataStatus.BadCommunicationError)]
        [InlineData(DataStatus.BadEncodingError)]
        [InlineData(DataStatus.BadDecodingError)]
        [InlineData(DataStatus.BadEncodingLimitsExceeded)]
        [InlineData(DataStatus.BadRequestTooLarge)]
        [InlineData(DataStatus.BadResponseTooLarge)]
        [InlineData(DataStatus.BadUnknownResponse)]
        [InlineData(DataStatus.BadTimeout)]
        [InlineData(DataStatus.BadNoCommunication)]
        [InlineData(DataStatus.BadWaitingForInitialData)]
        [InlineData(DataStatus.BadNotReadable)]
        [InlineData(DataStatus.BadNotWritable)]
        [InlineData(DataStatus.BadOutOfRange)]
        [InlineData(DataStatus.BadNotSupported)]
        [InlineData(DataStatus.BadNotFound)]
        [InlineData(DataStatus.BadTcpServerTooBusy)]
        [InlineData(DataStatus.BadTcpMessageTooLarge)]
        [InlineData(DataStatus.BadTcpNotEnoughResources)]
        [InlineData(DataStatus.BadTcpInternalError)]
        [InlineData(DataStatus.BadTcpEndpointUrlInvalid)]
        [InlineData(DataStatus.BadRequestInterrupted)]
        [InlineData(DataStatus.BadRequestTimeout)]
        [InlineData(DataStatus.BadNotConnected)]
        [InlineData(DataStatus.BadDeviceFailure)]
        public void TestBad(uint code)
        {
            DateTime frozen = DateTime.Now;
            TestClass Test = new TestClass();
            Test.Status = DataValue.GetDataStatus(code);
            Assert.True(Test.Timestamp > frozen);
            Assert.True(Test.IsBad);
            Assert.False(Test.IsGood);
            Assert.False(Test.IsUncertain);
        }

        [Fact]
        public void TestGood()
        {
            DateTime frozen = DateTime.Now;
            TestClass Test = new TestClass();
            Test.Status = DataValue.Good;
            Assert.True(Test.Timestamp > frozen);
            Assert.False(Test.IsBad);
            Assert.True(Test.IsGood);
            Assert.False(Test.IsUncertain);
        }

        #endregion
    }
}