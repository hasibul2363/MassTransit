using System;
using System.Configuration;
using BulkAccept.Common;
using MassTransit;

namespace BulkAccept.BillingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BillingService";

            var bus = BusConfigurator.Instance
                .ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, ConfigurationManager.AppSettings["OrderQueueName"], e =>
                    {
                        e.Consumer<AcceptProcessedConsumer>();
                    });
                });

            bus.StartAsync();

            Console.WriteLine("Listening Accept processed event..");
            Console.ReadLine();
        }
    }
}