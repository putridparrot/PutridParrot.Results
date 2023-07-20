//using System;
//using System.Diagnostics.CodeAnalysis;
//using NUnit.Framework;
//using PutridParrot.Results;

//namespace Tests.PutridParrot.Results;

//[ExcludeFromCodeCoverage]
//[TestFixture]
//public class FluentResultTests
//{
//    [Test]
//    public void Try_WithNoException_ExpectSuccess()
//    {
//        int WillException(int value) => value;

//        Assert.IsTrue(100.Try(WillException).IsSuccess());
//    }

//    [Test]
//    public void Try_WithException_ExpectFailure()
//    {
//        int WillException(int value) => throw new NotImplementedException();

//        Assert.IsTrue(100.Try(WillException).IsFailure());
//    }
//}