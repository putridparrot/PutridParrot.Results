using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Results;

namespace Tests.Results
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ResultBuilderTests
    {
        [Test]
        public void Success_ExepectResultWithSuccess()
        {
            var result = ResultBuilder<int, string>.Success().Build();

            Assert.AreEqual(ResultStatus.Success, result.Status);
        }

        [Test]
        public void Failure_ExepectResultWithFailure()
        {
            var result = ResultBuilder<int, string>.Failure().Build();

            Assert.AreEqual(ResultStatus.Failure, result.Status);
        }

        [Test]
        public void Undefined_ExepectResultWithNotSet()
        {
            var result = ResultBuilder<int, string>.Undefined().Build();

            Assert.AreEqual(ResultStatus.Undefined, result.Status);
        }

        [Test]
        public void WithValue_ExpectResultToHaveSuppliedValue()
        {
            var result = ResultBuilder<int, string>.Failure()
                .WithValue(123)
                .Build();

            Assert.AreEqual(123, result.Value);
        }

        [Test]
        public void WithMessage_ExpectResultToHaveSuppliedMessage()
        {
            var result = ResultBuilder<int, string>.Failure()
                .WithMessage("This is a message")
                .Build();

            Assert.AreEqual("This is a message", result.ToString());
        }

        [Test]
        public void WithMessages_ExpectResultToHaveSuppliedMessages()
        {
            var result = ResultBuilder<int, string>.Failure()
                .WithMessages(new[] {"Message1", "Message2", "Message3"})
                .Build();

            Assert.AreEqual("Message1\r\nMessage2\r\nMessage3", result.ToString());
        }

        [Test]
        public void WithMessageAndWithValue_ExpectResultToHaveSuppliedMessage()
        {
            var result = ResultBuilder<int, string>.Failure()
                .WithMessage("This is a message")
                .WithValue(123)
                .Build();

            Assert.AreEqual("This is a message", result.ToString());
            Assert.AreEqual(123, result.Value);
        }
    }
}