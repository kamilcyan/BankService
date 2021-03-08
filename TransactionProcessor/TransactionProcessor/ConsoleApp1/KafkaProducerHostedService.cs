using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankNamespace
{
    public class KafkaConsumerHostedService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<KafkaConsumerHostedService> _logger;
        //private readonly IInterfejs inte;
        private readonly ClusterClient _cluster;
        //private PaymentContext paymentContext;

        public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger/*, IInterfejs inte*/, IServiceScopeFactory scopeFactory /*PaymentContext pc*/)
        {
            this.scopeFactory = scopeFactory;
            //paymentContext = pc;
            _logger = logger;
            //this.inte = inte;
            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9093"
            }, new ConsoleLogger());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = scopeFactory.CreateScope();
            Models.Payment p = new Models.Payment();
            _cluster.ConsumeFromLatest("demo");
            _cluster.MessageReceived += record =>
            {
                _cluster.Produce("demo", p);
                _logger.LogInformation($"Received: {Encoding.UTF8.GetString(record.Value as byte[])}");
                var dbContext = scope.ServiceProvider.GetRequiredService<PaymentContext>();
                dbContext.Add(p);
                dbContext.SaveChanges();
            };

            
            //paymentContext.Payments.Add(p);
            //paymentContext.SaveChanges();
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster?.Dispose();
            return Task.CompletedTask;
        }
    }
}
