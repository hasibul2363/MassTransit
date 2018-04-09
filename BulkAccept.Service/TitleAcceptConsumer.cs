using BulkAccept.Messaging;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace BulkAccept.Service
{
    public class TitleAcceptConsumer : IConsumer<ITitleAcceptedEvent>
    {
        public async Task Consume(ConsumeContext<ITitleAcceptedEvent> context)
        {
            var message = context.Message;

            await Console.Out.WriteLineAsync($"Accept title : Vin No {message.VinNo} Order id: {message.RefNo} is received.");

            //do something..
            if(message.RefNo>10)
            {
                await Console.Out.WriteLineAsync($"Accept Title  Ref No >=10: {message.RefNo} is received.");
                await context.Publish<ITitleAcceptedEvent>(
              new { CorrelationId = context.Message.CorrelationId, OrderId = message.RefNo });
            }
          else
            {
                await Console.Out.WriteLineAsync($"Accept Title Ref No <10: {message.RefNo} is received.");
                await context.Publish<ITitleFailedEvent>(
              new { CorrelationId = context.Message.CorrelationId, RefNo = message.RefNo, Reason ="Ref No should >10"});

            }
        }
    }
}