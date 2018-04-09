using Automatonymous;
using System;

namespace BulkAccept.Saga
{
    public class AcceptSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public State CurrentState { get; set; }

        public int RefNo { get; set; }
        public string VinNo { get; set; }
    }
}