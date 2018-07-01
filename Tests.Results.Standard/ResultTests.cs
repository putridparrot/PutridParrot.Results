using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using PutridParrot.Results;

namespace Tests.Results
{
#if !NETSTANDARD1_6
    [ExcludeFromCodeCoverage]
#endif
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void Success_DefaultProperties_ExpectSuccess()
        {
            var result = new Result<int>(ResultStatus.Success);

            Assert.AreEqual(ResultStatus.Success, result.Status);
        }

        [Test]
        public void Failure_DefaultProperties_ExpectFailure()
        {
            var result = new Result<int>(ResultStatus.Failure);

            Assert.AreEqual(ResultStatus.Failure, result.Status);
        }

        [Test]
        public void Undefined_DefaultProperties_ExpectNotSet()
        {
            var result = new Result<int, string>();

            Assert.AreEqual(ResultStatus.Undefined, result.Status);
        }

        [Test]
        public void Success_WithValue_ExpectSuccess()
        {
            var result = new Result<int, string>(ResultStatus.Success, 123);

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual(123, result.Value);
        }

        [Test]
        public void Failure_WithValue_ExpectFailure()
        {
            var result = new Result<int, string>(ResultStatus.Failure, 123);

            Assert.AreEqual(ResultStatus.Failure, result.Status);
            Assert.AreEqual(123, result.Value);
        }

        [Test]
        public void Undefined_WithValue_ExpectNotSet()
        {
            var result = new Result<int, string>(123);

            Assert.AreEqual(ResultStatus.Undefined, result.Status);
            Assert.AreEqual(123, result.Value);
        }

        [Test]
        public void Message_WithSuccess_ByDefaultEmptyAndNotNull()
        {
            var result = new Result<int, string>(ResultStatus.Success);

            Assert.IsEmpty(result.Messages);
        }

        [Test]
        public void Message_WithFailure_ByDefaultEmptyAndNotNull()
        {
            var result = new Result<int, string>(ResultStatus.Failure);

            Assert.IsEmpty(result.Messages);
        }

        [Test]
        public void Message_WithUndefined_ByDefaultEmptyAndNotNull()
        {
            var result = new Result<int, string>(ResultStatus.Undefined);

            Assert.IsEmpty(result.Messages);
        }

        [Test]
        public void ToString_SingleMessage_ExpectSameMessageReturned()
        {
            var result = new Result<int, string>(ResultStatus.Success);
            result.Messages.Add("This is a test message");

            Assert.AreEqual("This is a test message", result.ToString());
        }

        [Test]
        public void ToString_MultipleMessages_ExpectSameMessageReturned()
        {
            var result = new Result<int, string>(ResultStatus.Success);
            result.Messages.Add("Message1");
            result.Messages.Add("Message2");
            result.Messages.Add("Message3");

            Assert.AreEqual("Message1\r\nMessage2\r\nMessage3", result.ToString());
        }

        [Test]
        public void ToString_MultipleMessagesUsingStatusCodeObject_ExpectSameMessageReturned()
        {
            var result = new Result<int, StatusCode>(ResultStatus.Success);
            result.Messages.Add(new StatusCode(102, "Message1"));
            result.Messages.Add(new StatusCode(103, "Message2"));
            result.Messages.Add(new StatusCode(104, "Message3"));

            Assert.AreEqual("102:Message1\r\n103:Message2\r\n104:Message3", result.ToString());
        }


        [Test]
        public void Success_OnNonValueResult_DefaultProperties_ExpectSuccess()
        {
            var result = new Result(ResultStatus.Success);

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual(default(object), result.Value);
        }

        [Test]
        public void Failure_OnNonValueResult_DefaultProperties_ExpectFailure()
        {
            var result = new Result(ResultStatus.Failure);

            Assert.AreEqual(ResultStatus.Failure, result.Status);
            Assert.AreEqual(default(object), result.Value);
        }

        [Test]
        public void Undefined_OnNonValueResult_DefaultProperties_ExpectNotSet()
        {
            var result = new Result();

            Assert.AreEqual(ResultStatus.Undefined, result.Status);
            Assert.AreEqual(default(object), result.Value);
        }
    }
}

