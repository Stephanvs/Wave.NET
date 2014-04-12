using System;
using FluentAssertions;
using WaveNET.Core.Model.Document.Operation;
using WaveNET.Core.Model.Operation;
using WaveNET.Core.Model.Operation.Wave;
using WaveNET.Core.Model.Wave;
using Xunit;

namespace WaveNET.Tests.Core.Models.Operations.Wave
{
    public class ParticipantTransformTest
    {
        private static readonly RemoveParticipantOperation remove1a;
        private static readonly RemoveParticipantOperation remove2a;
        private static readonly RemoveParticipantOperation remove2b;
        private static readonly AddParticipantOperation add1a;
        private static readonly AddParticipantOperation add2a;
        private static readonly AddParticipantOperation add2b;
        private static readonly NoOp noop1;
        private static readonly NoOp noop2;
        private static readonly WaveletBlipOperation mutation;

        private static readonly ParticipantId Fred = new ParticipantId("fred@example.com");
        private static readonly ParticipantId John = new ParticipantId("john@example.com");
        private static readonly ParticipantId Marc = new ParticipantId("marc@example.com");

        static ParticipantTransformTest()
        {
            var context1 = new WaveletOperationContext(new ParticipantId("p1@google.com"), DateTime.UtcNow, 1L);
            var context2 = new WaveletOperationContext(new ParticipantId("p2@google.com"), DateTime.UtcNow, 1L);
            var contextA = new WaveletOperationContext(new ParticipantId("a@google.com"), DateTime.UtcNow, 1L);
            remove1a = new RemoveParticipantOperation(context1, new ParticipantId("a@google.com"));
            remove2a = new RemoveParticipantOperation(context2, new ParticipantId("a@google.com"));
            remove2b = new RemoveParticipantOperation(context2, new ParticipantId("b@google.com"));
            add1a = new AddParticipantOperation(context1, new ParticipantId("a@google.com"));
            add2a = new AddParticipantOperation(context2, new ParticipantId("a@google.com"));
            add2b = new AddParticipantOperation(context2, new ParticipantId("b@google.com"));
            noop1 = new NoOp(context1);
            noop2 = new NoOp(context2);
            mutation = new WaveletBlipOperation("dummy",
                new BlipContentOperation(contextA, (new DocOpBuilder()).Characters("x").Build()));
        }

        /// <summary>
        ///     Tests that the correct exception is thrown when a removed participant issues an operation.
        /// </summary>
        [Fact]
        public void RemovedAuthorException()
        {
            CheckTransformThrowsException<RemovedAuthorException>(mutation, remove2a);
        }

        /// <summary>
        ///     Tests the transformation of a participant addition with a participant removal
        /// </summary>
        [Fact]
        public void AdditionVsRemoval()
        {
            CheckTransformThrowsException<TransformException>(add1a, remove2a);
            CheckTransformThrowsException<TransformException>(remove2a, add1a);
            CheckIdentityTransform(add1a, remove2b);
            CheckIdentityTransform(remove2b, add1a);
        }

        /// <summary>
        ///     Tests that no exception is thrown in various cases.
        /// </summary>
        [Fact]
        public void NoExceptions()
        {
            CheckIdentityTransform(remove2a, mutation);
            CheckIdentityTransform(mutation, remove2b);
            CheckIdentityTransform(remove2b, mutation);
        }

        [Fact]
        public void RemovalVsRemoval()
        {
            CheckTransform(remove1a, remove2a, noop1, noop2);
            CheckIdentityTransform(remove1a, remove2b);
        }

        [Fact]
        public void AdditionVsAddition()
        {
            CheckTransform(add1a, add2a, noop1, noop2);
            CheckIdentityTransform(add1a, add2b);
        }

        private static void CheckTransformThrowsException<TException>(WaveletOperation clientOperation,
            WaveletOperation serverOperation)
            where TException : Exception
        {
            Assert.Throws<TException>(() => Transformer.Transform(clientOperation, serverOperation));
        }

        private static void CheckTransform(WaveletOperation clientOperation,
            WaveletOperation serverOperation,
            WaveletOperation transformedClientOperation,
            WaveletOperation transformedServerOperation)
        {
            OperationPair<WaveletOperation> pair = Transformer.Transform(clientOperation, serverOperation);

            transformedClientOperation.Equals(pair.ClientOperation).Should().BeTrue();
            transformedServerOperation.Equals(pair.ServerOperation).Should().BeTrue();
        }

        private static void CheckIdentityTransform(WaveletOperation clientOperation, WaveletOperation serverOperation)
        {
            CheckTransform(clientOperation, serverOperation, clientOperation, serverOperation);
        }
    }
}