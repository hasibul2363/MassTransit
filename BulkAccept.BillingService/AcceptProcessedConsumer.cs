using System;
using System.Threading.Tasks;
using BulkAccept.Messaging;
using MassTransit;

namespace BulkAccept.BillingService
{
    public class AcceptProcessedConsumer : IConsumer<IAcceptProcessedEvent>
    {
        public async Task Consume(ConsumeContext<IAcceptProcessedEvent> context)
        {
            var message = context.Message;

            await Console.Out.WriteLineAsync($"Ref No: {message.RefNo} is being billed.");
        }
    }
}