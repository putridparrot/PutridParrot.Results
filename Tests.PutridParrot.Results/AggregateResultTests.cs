using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using PutridParrot.Results;

namespace Tests.PutridParrot.Results;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AggregateResultTests
{
    [Test]
    public void IsSuccess_WhenOnlySuccessExists_ExpectTrue()
    {
        var success1 = Result.Success();
        var success2 = Result.Success();

        var aggregateResult = new AggregateResult(success1, success2);

        Assert.IsTrue(aggregateResult.IsSuccess());
    }

    [Test]
    public void IsSuccess_WhenFailureExists_ExpectFalse()
    {
        var success = Result.Success();
        var failure = Result.Failure();

        var aggregateResult = new AggregateResult(success, failure);

        Assert.IsFalse(aggregateResult.IsSuccess());
    }


    [Test]
    public void IsFailure_WhenNoFailuresExists_ExpectFalse()
    {
        var success1 = Result.Success();
        var success2 = Result.Success();

        var aggregateResult = new AggregateResult(success1, success2);

        Assert.IsFalse(aggregateResult.IsFailure());
    }

    [Test]
    public void IsFailure_WhenFailureExists_ExpectTrue()
    {
        var success = Result.Success();
        var failure = Result.Failure();

        var aggregateResult = new AggregateResult(success, failure);

        Assert.IsTrue(aggregateResult.IsFailure());
    }

    [Test]
    public void IsSuccess_WhenOnlySuccessAndSuccessTExists_ExpectTrue()
    {
        var success1 = Result.Success();
        var success2 = Result.Success(true);

        var aggregateResult = new AggregateResult(success1, success2);

        Assert.IsTrue(aggregateResult.IsSuccess());
    }

    [Test]
    public void IsSuccess_WhenOnlySuccessTExists_ExpectTrue()
    {
        var success1 = Result.Success(true);
        var success2 = Result.Success(true);

        var aggregateResult = new AggregateResult(success1, success2);

        Assert.IsTrue(aggregateResult.IsSuccess());
    }

    [Test]
    public void IsSuccess_WhenFailureTExists_ExpectFalse()
    {
        var success = Result.Success(true);
        var failure = Result.Failure(true);

        var aggregateResult = new AggregateResult(success, failure);

        Assert.IsFalse(aggregateResult.IsSuccess());
    }

    [Test]
    public void AggregateMultipleResults_WithFailure_ExpectIsSuccessToBeFalse()
    {
        var aggregateResult = new AggregateResult();

        aggregateResult = new AggregateResult(aggregateResult, Result.Success(true));
        aggregateResult = new AggregateResult(aggregateResult, Result.Failure(true));

        Assert.IsFalse(aggregateResult.IsSuccess());
    }

    [Test]
    public void AggregateMultipleResults_WithSuccess_ExpectIsSuccessToBeTrue()
    {
        var aggregateResult = new AggregateResult();

        aggregateResult = new AggregateResult(aggregateResult, Result.Success(true));
        aggregateResult = new AggregateResult(aggregateResult, Result.Success(true));

        Assert.IsTrue(aggregateResult.IsSuccess());
    }

    [Test]
    public void AggregateMultipleResultsViaEnumerableCtor_WithFailure_ExpectIsSuccessToBeFalse()
    {
        var list = new List<IResult>
        {
            Result.Success(true),
            Result.Failure(true)
        };

        var aggregateResult = new AggregateResult(list);

        Assert.IsFalse(aggregateResult.IsSuccess());
    }

    [Test]
    public void AggregateMultipleResultsViaEnumerableCtor_WithSuccess_ExpectIsSuccessToBeTrue()
    {
        var list = new List<IResult>
        {
            Result.Success(true),
            Result.Success(true)
        };

        var aggregateResult = new AggregateResult(list);

        Assert.IsTrue(aggregateResult.IsSuccess());
    }

    [Test]
    public void AggregateSingleResults_WithFailure_ExpectIsSuccessToBeFalse()
    {
        var aggregateResult = new AggregateResult(Result.Failure(true));

        Assert.IsFalse(aggregateResult.IsSuccess());
    }

    [Test]
    public void AggregateSingleResults_WithSuccess_ExpectIsSuccessToBeTrue()
    {
        var aggregateResult = new AggregateResult(Result.Success(true));

        Assert.IsTrue(aggregateResult.IsSuccess());
    }

}