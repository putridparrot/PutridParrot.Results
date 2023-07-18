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
        IResult result = new Success();
        Assert.IsTrue(result.IsSuccess());
    }

    [Test]
    public void WhenFailureCreated_IsSuccess_ReturnsFalse()
    {
        IResult result = new Failure();
        Assert.IsFalse(result.IsSuccess());
    }

    [Test]
    public void WhenSuccessCreate_IsFailure_ReturnsTrue()
    {
        IResult result = new Failure();
        Assert.IsTrue(result.IsFailure());
    }

    [Test]
    public void WhenFailureCreate_IsFailure_ReturnsFalse()
    {
        IResult result = new Success();
        Assert.IsFalse(result.IsFailure());
    }

    [Test]
    public void WhenSuccessTCreated_IsSuccess_ReturnsTrue()
    {
        IResult<bool> result = new Success<bool>(true);
        Assert.IsTrue(result.IsSuccess());
    }

    [Test]
    public void WhenFailureTCreated_IsSuccess_ReturnsFalse()
    {
        IResult<bool> result = new Failure<bool>(true);
        Assert.IsFalse(result.IsSuccess());
    }


    [Test]
    public void WhenFailureTCreated_IsFailure_ReturnsFalse()
    {
        IResult<bool> result = new Failure<bool>(true);
        Assert.IsTrue(result.IsFailure());
    }

    [Test]
    public void WhenSuccessTCreated_IsFailure_ReturnsFalse()
    {
        IResult<bool> result = new Success<bool>(true);
        Assert.IsFalse(result.IsFailure());
    }

    [Test]
    public void WhenFailureCreated_FailureMessage_ShouldNotBeEmpty()
    {
        IResult result = new Failure("This failed");
        Assert.AreEqual("This failed", result.FailureMessage());
    }

    [Test]
    public void WhenFailureTCreated_FailureMessage_ShouldNotBeEmpty()
    {
        IResult<bool> result = new Failure<bool>(true,"This failed");
        Assert.AreEqual("This failed", result.FailureMessage());
    }

    [Test]
    public void WhenSuccessCreated_FailureMessage_ShouldBeEmpty()
    {
        IResult result = new Success();
        Assert.AreEqual("", result.FailureMessage());
    }

    [Test]
    public void WhenSuccessTCreated_FailureMessage_ShouldBeEmpty()
    {
        IResult<bool> result = new Success<bool>(true);
        Assert.AreEqual("", result.FailureMessage());
    }

    [Test]
    public void WhenSuccessTCreated_ExpectValueToMatch()
    {
        IResult<string> result = new Success<string>("Scooby Doo");
        Assert.AreEqual("Scooby Doo", result.Value);
    }

    [Test]
    public void WhenFailureTCreated_ExpectValueToMatch()
    {
        IResult<string> result = new Failure<string>("Scooby Doo");
        Assert.AreEqual("Scooby Doo", result.Value);
    }
}