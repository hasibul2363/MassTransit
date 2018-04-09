using System;
using BulkAccept.Messaging;

namespace BulkAccept.Saga.Messages
{
    public class AcceptReceivedEvent : IAcceptReceivedEvent
    {
        private readonly AcceptSagaState _acceptSagaState;

        public AcceptReceivedEvent(AcceptSagaState orderSagaState)
        {
            _acceptSagaState = orderSagaState;
        }

            public Guid CorrelationId => _acceptSagaState.CorrelationId;

            public string VinNo => _acceptSagaState.VinNo;

            public int RefNo => _acceptSagaState.RefNo;
    }
}