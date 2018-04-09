using Automatonymous;
using System;
using BulkAccept.Messaging;
using BulkAccept.Saga.Messages;

namespace BulkAccept.Saga
{
    public class AcceptSaga : MassTransitStateMachine<AcceptSagaState>
    {
        public State Received { get; set; }
        public State Accept { get; set; }
        public State Processed { get; set; }

        public Event<ITitleCreateCommand> TitleCreateCommand { get; set; }       
        public Event<ITitleAcceptedEvent> TitleAcceptedCommand { get; set; }
        public Event<ITitleFailedEvent> TitleFailedCommand { get; set; }
        public Event<IAcceptProcessedEvent> AcceptProcessed { get; set; }

        public AcceptSaga()
        {
            InstanceState(s => s.CurrentState);

            Event(() => TitleCreateCommand,
                cec =>
                        cec.CorrelateBy(state => state.VinNo, context => context.Message.VinNo)
                        .SelectId(selector => Guid.NewGuid()));
            Event(() => TitleFailedCommand, accept => accept.CorrelateById(s => s.Message.CorrelationId));
            Event(() => TitleAcceptedCommand, accept => accept.CorrelateById(s => s.Message.CorrelationId));

            Event(() => AcceptProcessed, cec => cec.CorrelateById(selector =>
                        selector.Message.CorrelationId));

            Initially(
                When(TitleCreateCommand)
                    .Then(context =>
                    {
                        context.Instance.VinNo = context.Data.VinNo;
                        context.Instance.RefNo = context.Data.RefNo;
                    })
                    .ThenAsync(
                        context => Console.Out.WriteLineAsync($"{context.Data.RefNo} Ref No is received..")
                    )
                    .TransitionTo(Accept)
                   .Then((s ) => accepttitle(s.Data,s.Instance,s))
                 //.Publish(context => new CheckTiltleAccept(context.Instance))
                );

            During(Accept, 
                When(TitleAcceptedCommand)               
                .Then(c => Console.Out.WriteLineAsync($"{c.Data.VinNo} Vin No is accepted"))
                .TransitionTo(Received)
                .Publish(c => new AcceptReceivedEvent(c.Instance)),

                When(TitleFailedCommand)
                .ThenAsync(
                    c=>Console.Out.WriteAsync($"{c.Data.VinNo} Vin No accept has failed. Reason: {c.Data.Reason}")                 
                ).Finalize());
            During(Received,
                When(AcceptProcessed)
                .ThenAsync(
                    context => Console.Out.WriteLineAsync($"{context.Data.RefNo} Ref No is processed.."))
                .Finalize()
                );

            SetCompletedWhenFinalized();
        }

        private void accepttitle(ITitleCreateCommand data, AcceptSagaState instance, BehaviorContext<AcceptSagaState, ITitleCreateCommand> s)
        {
            var message = data;

            Console.Out.WriteLineAsync($"Accept title : {message.VinNo} Order id: {message.RefNo} is received.");

            //do something..
            if (message.RefNo >= 10)
            {
                Console.Out.WriteLineAsync($"Accept title >=10 : {message.VinNo} Order id: {message.RefNo} is received.");
                s.Publish (new CheckTiltleAccept(instance));
            }
            else
            {
                Console.Out.WriteLineAsync($"Accept title <10: {message.VinNo} Order id: {message.RefNo} is received.");
                s.Publish(new CheckTiltleFailed(instance));
            }
        }

      
 
    }

    internal class CheckTiltleFailed : ITitleFailedEvent
    {
        private AcceptSagaState instance;

        public CheckTiltleFailed(AcceptSagaState instance)
        {
            this.instance = instance;
        }
        public DateTime AcceptedDate => DateTime.Now;

        public Guid CorrelationId => instance.CorrelationId;

        public string VinNo => instance.VinNo;
        public int RefNo => instance.RefNo;

        public string Reason => "Ref No Shoud >=10";
    }

    internal class CheckTiltleAccept: ITitleAcceptedEvent
    {
        private AcceptSagaState instance;

        public CheckTiltleAccept(AcceptSagaState instance)
        {
            this.instance = instance;
        }

        public DateTime AcceptedDate => DateTime.Now;

        public Guid CorrelationId => instance.CorrelationId;

        public string VinNo => instance.VinNo;
        public int RefNo => instance.RefNo;
    }
}