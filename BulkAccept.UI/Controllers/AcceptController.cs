using System;
using System.Configuration;
using System.Web.Mvc;
using BulkAccept.Common;
using BulkAccept.UI.Models;
using MassTransit;

namespace BulkAccept.UI.Controllers
{
    public class AcceptController : Controller
    {
        private readonly ISendEndpoint _bus;

        public AcceptController()
        {
            var busControl = BusConfigurator.Instance.ConfigureBus();
            var sendToUri = new Uri($"{MqConstants.RabbitMQUri}{ConfigurationManager.AppSettings["SagaQueueName"]}");

            _bus = busControl.GetSendEndpoint(sendToUri).Result;
        }

        // GET: Order
        public ActionResult Index(AcceptModel orderModel)
        {
            if (orderModel.RefNo > 0)
                CreateOrder(orderModel);

            return View();
        }

        private void CreateOrder(AcceptModel orderModel)
        {
            _bus.Send(orderModel).Wait();
        }
    }
}