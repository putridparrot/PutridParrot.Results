using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PutridParrot.Results;

public interface IAggregateResult : IResult
{
    IReadOnlyCollection<IResult> Results { get; }
}

[DebuggerDisplay("Count = {InnerResultsCount}")]
public class AggregateResult : IAggregateResult
{
    private readonly IResult[] _results;
    // taken from the way AggregateException works to save recreating the ReadOnlyCollection
    private ReadOnlyCollection<IResult>? _resultsView;

    public AggregateResult() :
        this(Array.Empty<IResult>(), false)
    {
    }

    public AggregateResult(IResult result) :
        this(new[] { result }, false)
    {
    }

    public AggregateResult(IEnumerable<IResult> results) :
        this(new List<IResult>(results ?? throw new ArgumentNullException(nameof(results))).ToArray(), false)
    {
    }

    public AggregateResult(AggregateResult aggregateResult, params IResult[] results)
    {
        if (aggregateResult == null) 
            throw new ArgumentNullException(nameof(aggregateResult));

        var temp = new List<IResult>(aggregateResult.Results);
        temp.AddRange(results);

        _results = temp.ToArray();
    }


    public AggregateResult(params IResult[] results) :
        this(new List<IResult>(results ?? throw new ArgumentNullException(nameof(results))).ToArray(), false)
    {
    }

    private AggregateResult(IResult[] results, bool cloneResults)
    {
        _results = cloneResults ? new IResult[results.Length] : results;
    }

    public IReadOnlyCollection<IResult> Results => _resultsView ??= new ReadOnlyCollection<IResult>(_results);

    /// <summary>
    /// This helper property is used by the DebuggerDisplay.
    ///
    /// Note that we don't want to remove this property and change the debugger display to {InnerExceptions.Count}
    /// because DebuggerDisplay should be a single property access or parameterless method call, so that the debugger
    /// can use a fast path without using the expression evaluator.
    ///
    /// See https://docs.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal int InnerResultsCount => Results.Count;
}
