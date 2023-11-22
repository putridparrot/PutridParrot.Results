#nullable enable
using System;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using PutridParrot.Results;

namespace Tests.PutridParrot.Results;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ResultTests
{
    [Test]
    public void WhenSuccessCreated_IsSuccess_ReturnsTrue()
    {
        IResult result = Result.Success();
        Assert.IsTrue(result.IsSuccess());
    }

    [Test]
    public void WhenFailureCreated_IsSuccess_ReturnsFalse()
    {
        IResult result = Result.Failure();
        Assert.IsFalse(result.IsSuccess());
    }

    [Test]
    public void WhenSuccessCreate_IsFailure_ReturnsTrue()
    {
        IResult result = Result.Failure();
        Assert.IsTrue(result.IsFailure());
    }

    [Test]
    public void WhenFailureCreate_IsFailure_ReturnsFalse()
    {
        IResult result = Result.Success();
        Assert.IsFalse(result.IsFailure());
    }

    [Test]
    public void WhenSuccessTCreated_IsSuccess_ReturnsTrue()
    {
        IResult<bool> result = Result.Success(true);
        Assert.IsTrue(result.IsSuccess());
    }

    [Test]
    public void WhenFailureTCreated_IsSuccess_ReturnsFalse()
    {
        IResult<bool> result = Result.Failure(true);
        Assert.IsFalse(result.IsSuccess());
    }


    [Test]
    public void WhenFailureTCreated_IsFailure_ReturnsFalse()
    {
        IResult<bool> result = Result.Failure(true);
        Assert.IsTrue(result.IsFailure());
    }

    [Test]
    public void WhenSuccessTCreated_IsFailure_ReturnsFalse()
    {
        IResult<bool> result = Result.Success(true);
        Assert.IsFalse(result.IsFailure());
    }

    [Test]
    public void WhenFailureCreated_FailureMessage_ShouldNotBeEmpty()
    {
        IResult result = Result.Failure("This failed");
        Assert.AreEqual("This failed", result.FailureMessage());
    }

    [Test]
    public void WhenFailureTCreated_FailureMessage_ShouldNotBeEmpty()
    {
        IResult<bool> result = Result.Failure(true,"This failed");
        Assert.AreEqual("This failed", result.FailureMessage());
    }

    [Test]
    public void WhenSuccessCreated_FailureMessage_ShouldBeEmpty()
    {
        IResult result = Result.Success();
        Assert.AreEqual("", result.FailureMessage());
    }

    [Test]
    public void WhenSuccessTCreated_FailureMessage_ShouldBeEmpty()
    {
        IResult<bool> result = Result.Success(true);
        Assert.AreEqual("", result.FailureMessage());
    }

    [Test]
    public void WhenSuccessTCreated_ExpectValueToMatch()
    {
        IResult<string> result = Result.Success("Scooby Doo");
        Assert.AreEqual("Scooby Doo", result.Value);
    }

    [Test]
    public void WhenFailureTCreated_ExpectValueToMatch()
    {
        IResult<string> result = Result.Failure<string>("Scooby Doo");
        Assert.AreEqual("Scooby Doo", result.Value);
    }

    [Test]
    public void ToResult_NoException_ExpectSuccess()
    {
        var f = () => "Scooby Doo";

        var result = f.ToResult();

        Assert.IsTrue(result.IsSuccess());
        Assert.IsTrue(result is IResult<string>);
        Assert.AreEqual("Scooby Doo", ((IResult<string>)result).Value);
    }

    private string DoSomething(int _)
    {
        throw new NotImplementedException();
    }

    [Test]
    public void ToResult_FromValueWithException_ExpectFailure()
    {
        var result = 123.ToResult(DoSomething);

        Assert.IsTrue(result.IsFailure());
        Assert.IsTrue(result is IResult<Exception>);
        Assert.IsTrue(((Failure<Exception>)result).Value is NotImplementedException);
    }

    [Test]
    public void ToResult_FromException_ExpectFailure()
    {
        var result = new NotImplementedException().ToResult();

        Assert.IsTrue(result.IsFailure());
        Assert.IsTrue(result is IResult<NotImplementedException>);
        Assert.IsTrue(((Failure<NotImplementedException>)result).Value is NotImplementedException);
    }

    [Test]
    public void ToResult_WithException_ExpectFailure()
    {
        Func<string> f = () => throw new NotImplementedException();

        var result = f.ToResult();

        Assert.IsTrue(result.IsFailure());
        Assert.IsTrue(result is IResult<Exception>);
        Assert.IsTrue(((Failure<Exception>)result).Value is NotImplementedException);
    }

    [Test]
    public void ToSuccess_EnsureValueIsEncapsulatedWithinASuccess()
    {
        var result = "Scooby Doo".ToSuccess();
        Assert.IsTrue(result.IsSuccess());
        Assert.AreEqual("Scooby Doo", result.Value);
    }

    [Test]
    public void ToFailure_EnsureValueIsEncapsulatedWithinASuccess()
    {
        var result = "Scooby Doo".ToFailure();
        Assert.IsTrue(result.IsFailure());
        Assert.AreEqual("Scooby Doo", result.Value);
    }

    [Test]
    public void OnSuccessOnFailure_WhenSuccessful_ExpectOnSuccessToBeCalled()
    {
        var wasSuccessCalled = false;
        var wasFailureCalled = false;
        "Scooby Doo".ToSuccess()
            .OnSuccess(() => wasSuccessCalled = true)
            .OnFailure(() => wasFailureCalled = true);

        Assert.IsTrue(wasSuccessCalled);
        Assert.IsFalse(wasFailureCalled);
    }

    [Test]
    public void OnSuccessOnFailure_WhenFailure_ExpectOnFailureToBeCalled()
    {
        var wasSuccessCalled = false;
        var wasFailureCalled = false;
        "Scooby Doo".ToFailure()
            .OnSuccess(() => wasSuccessCalled = true)
            .OnFailure(() => wasFailureCalled = true);

        Assert.IsFalse(wasSuccessCalled);
        Assert.IsTrue(wasFailureCalled);
    }

    [Test]
    public void ResultAsHttpError()
    {
        var error = 404.ToFailure("Page not found");

        Assert.IsTrue(error.IsFailure());
        Assert.AreEqual("Page not found", error.FailureMessage());
    }

    [Test]
    public void ResultAsHttpSuccess()
    {
        var error = 200.ToSuccess();

        Assert.IsTrue(error.IsSuccess());
    }
}