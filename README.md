# Results

## Synopsis

Sometimes we want to return more than just a return value. 

For example, we might tend to return an object and if the object is null a failure occurred, but in some situations a null is a valid return and so how do we return whether the method succeeded or not. Ofcourse we might use out/ref params, but they're a bit messy at times (although getting better with C# 7). So then we might return a Tuple (again getting better with C# 7) but then we have the situation of ensuring that we're consistent with which value is the success or failure status. Finally we might use exceptions to return failure, which come with good and bad points but as (in this instance) they're really acting more like part of the code's flow, they're probably best left to those "exceptional" circumstances instead of short cuts for returning failures.

Okay, it's not a big deal, we've got plenty of options and here's another which is a simple little Result class (as other classes to support it).

Let's take a look at the various parts...

## ResultStatus

The ResultStatus is an enumerated type which simply offers the options, Success, Failure and Undefined (which basically means the status has not been set).

## Result

At the heart of this is the Result object, which we can return, wrapping a return value from a method call along with a message type (for supplying more human readable status indicators). It also includes a ResultStatus.

The message type might be a string and thus allows us to return the equivalent of an exception message, but we can also use other objects as messages. In the project we have a StatusCode object which allows us to return messages with codes (i.e. message: Page not found, code: 404). The message type simply needs to override ToString as well so the Result can also aggregate messages if required.

## ResultBuilder

This class simply offers a fluent style interface to the Result objects. It's more or less a helper class for those who prefer such interfaces.

## CompositeResult

This class allows us to take multiple Result returns and aggregate them, allowing us to see if afFailure exists or all Result's succeeed, for example.

## Thanks

![Powered by NDepend](https://user-images.githubusercontent.com/7886450/27169849-9c070308-51a3-11e7-8b7e-04fe6f4d9194.jpg)
