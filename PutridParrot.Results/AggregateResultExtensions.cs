using System.Linq;

namespace PutridParrot.Results;

public static class AggregateResultExtensions
{
    public static bool IsSuccess(this IAggregateResult aggregateResult) =>
        !aggregateResult.IsFailure();

    public static bool IsFailure(this IAggregateResult aggregateResult) =>
        aggregateResult.Results.Any(result => ResultExtensions.IsFailure(result));
}