using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Results;

namespace Tests.Results
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CompositeResultTests
    {
        [Test]
        public void Add_ToEmptyComposite_ExpectSingleResult()
        {
            var composite = new CompositeResult<int, string>();

            composite.Results.Add(new Result<int, string>(ResultStatus.Success));

            Assert.AreEqual(ResultStatus.Success, composite.Status);
        }

        [Test]
        public void Add_MultipleOfSameStatus_ExpectSingleResult()
        {
            var composite = new CompositeResult<int, string>();

            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));

            Assert.AreEqual(ResultStatus.Success, composite.Status);
        }

        [Test]
        public void Add_MultipleOf_WhenFailureExists_ExpectFailure()
        {
            var composite = new CompositeResult<int, string>();

            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Failure));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));

            Assert.AreEqual(ResultStatus.Failure, composite.Status);
        }

        [Test]
        public void Add_MultipleOf_WhenUndefinedExists_ExpectUndefined()
        {
            var composite = new CompositeResult<int, string>();

            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));
            composite.Results.Add(new Result<int, string>(ResultStatus.Undefined));
            composite.Results.Add(new Result<int, string>(ResultStatus.Success));

            Assert.AreEqual(ResultStatus.Undefined, composite.Status);
        }

        [Test]
        public void Value_IfNoValue_ExpectException()
        {
            var composite = new CompositeResult<int, string>();

            Assert.Throws<Exception>(() =>
            {
                var v = composite.Value;
            });
        }

        [Test]
        public void Value_IfSameValue_ExpectValue()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(new Result<int, string>(123));
            composite.Results.Add(new Result<int, string>(123));
            composite.Results.Add(new Result<int, string>(123));
            composite.Results.Add(new Result<int, string>(123));

            Assert.AreEqual(123, composite.Value);
        }

        [Test]
        public void Value_IfOneValue_ExpectValue()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(new Result<int, string>(123));

            Assert.AreEqual(123, composite.Value);
        }

        [Test]
        public void Value_IfDifferentValue_ExpectException()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(new Result<int, string>(123));
            composite.Results.Add(new Result<int, string>(0));
            composite.Results.Add(new Result<int, string>(123));
            composite.Results.Add(new Result<int, string>(123));

            Assert.Throws<Exception>(() =>
            {
                var v = composite.Value;
            });
        }

        [Test]
        public void Values_ExpectAllValues()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(new Result<int, string>(1));
            composite.Results.Add(new Result<int, string>(2));
            composite.Results.Add(new Result<int, string>(3));
            composite.Results.Add(new Result<int, string>(4));

            Assert.AreEqual(new[] {1, 2, 3, 4}, composite.Values);
        }

        [Test]
        public void Messages_ExpectAllMessages()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(ResultBuilder<int, string>.Success().WithMessages(new[] {"a", "b"}).Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("c").Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("d").Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("e").Build());

            Assert.AreEqual(new[] {"a", "b", "c", "d", "e"}, composite.Messages);
        }

        [Test]
        public void Any_WhereStatusDoesNotExist_ExpectFalse()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(ResultBuilder<int, string>.Success().WithMessages(new[] {"a", "b"}).Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("c").Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("d").Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("e").Build());

            Assert.IsFalse(composite.Any(ResultStatus.Undefined));
        }

        [Test]
        public void Any_WhereStatusDoesExist_ExpectTrue()
        {
            var composite = new CompositeResult<int, string>();
            composite.Results.Add(ResultBuilder<int, string>.Success().WithMessages(new[] {"a", "b"}).Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("c").Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("d").Build());
            composite.Results.Add(ResultBuilder<int, string>.Failure().WithMessage("e").Build());

            Assert.IsTrue(composite.Any(ResultStatus.Failure));
        }
    }
}