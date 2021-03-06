using Confluent.Kafka;
using ConsoleApp1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserPaymentController : ControllerBase
    {
        private IProducer<Null, string> _producer;
        

        [HttpPost]
        public async Task<bool> Post(ClientNamespace.ClientPayment p)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Payment payment = new Payment();

            payment.ClientId = p.ClientId;
            payment.Value = p.Value;
            payment.AccountNumberFrom = p.AccountNumberFrom;
            payment.AccountNumberTo = p.AccountNumberTo;
            payment.Currency = p.Currency;

            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9093"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();

            string data = JsonConvert.SerializeObject(payment);

            await _producer.ProduceAsync("demo", new Message<Null, string>()
            {
                Value = data
            }, token);

            return true;
        }
    }
}
