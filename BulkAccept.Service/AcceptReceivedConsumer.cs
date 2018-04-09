using System;
using System.Threading.Tasks;
using BulkAccept.Messaging;
using MassTransit;

namespace BulkAccept.Service
{
    public class AcceptReceivedConsumer : IConsumer<IAcceptReceivedEvent>
    {
        public async Task Consume(ConsumeContext<IAcceptReceivedEvent> context)
        {
            var message = context.Message;

            await Console.Out.WriteLineAsync($" Vin No: {message.VinNo} Ref Id: {message.RefNo} is received.");

            //do something..

            await context.Publish<IAcceptProcessedEvent>(
                new { CorrelationId = context.Message.CorrelationId, RefNo = message.RefNo });
        }
    }
}