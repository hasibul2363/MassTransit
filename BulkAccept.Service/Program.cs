using System;
using BulkAccept.Common;
using System.Configuration;
using MassTransit;

namespace BulkAccept.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "AcceptService";

            var bus = BusConfigurator.Instance
                .ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, ConfigurationManager.AppSettings["OrderQueueName"], e =>
                    {
                        e.Consumer<AcceptReceivedConsumer>();
                    });
                });

            bus.StartAsync();

            Console.WriteLine("Listening Accept received event..");
            Console.ReadLine();
        }
    }
}