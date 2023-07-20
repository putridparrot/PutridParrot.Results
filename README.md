# PutridParrot.Results

[![Build PutridParrot.Results](https://github.com/putridparrot/PutridParrot.Results/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/putridparrot/PutridParrot.Results/actions/workflows/dotnet-core.yml)
[![NuGet version (PutridParrot.Results)](https://img.shields.io/nuget/v/PutridParrot.Results.svg?style=flat-square)](https://www.nuget.org/packages/PutridParrot.Results/)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/putridparrot/PutridParrot.Results/blob/master/LICENSE.md)
[![GitHub Releases](https://img.shields.io/github/release/putridparrot/PutridParrot.Results.svg)](https://github.com/putridparrot/PutridParrot.Results/releases)
[![GitHub Issues](https://img.shields.io/github/issues/putridparrot/PutridParrot.Results.svg)](https://github.com/putridparrot/PutridParrot.Results/issues)

_Note: This is a total re-write of the Results code-base to simplify everything._

There are several ways to return a result and/or error code from a method/function. For example, we might return an 
integer value, if the value is negative it represents an error. We might return an integer error code (such as some
of the Windows API functions do) and the actual return value is returned via out parameters or similar. 
Or we return something like a tuple with a value to indicate the error or success along with a returned value.

The IResult is an interface for implementing a better tuple version of this, in that we will return a Success or
Failure object and we can using type pattern matching to determine whether the return is a success or failure and
then access a return value accordingly.

```
IResult<string> DoSomething()
{
    // do some stuff
    // if failed
    // return new Failure<string>("Failed");


    // if suceeded
    return new Success<string>("Completed");
}
```