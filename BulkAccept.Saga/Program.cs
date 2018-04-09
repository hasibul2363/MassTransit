using System;
using System.Configuration;
using Automatonymous;
using BulkAccept.Common;
using MassTransit.Saga;

namespace BulkAccept.Saga
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Saga";
            var acceptSaga = new AcceptSaga();
            var repo = new InMemorySagaRepository<AcceptSagaState>();

            var bus = BusConfigurator.Instance
                .ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, ConfigurationManager.AppSettings["SagaQueueName"], e =>
                    {
                        e.StateMachineSaga(acceptSaga, repo);
                    });
                });

            bus.StartAsync();

            Console.WriteLine("Accept saga started..");
            Console.ReadLine();
        }
    }
}