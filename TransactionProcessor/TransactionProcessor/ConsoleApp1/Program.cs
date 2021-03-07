using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BankNamespace.Models;
using System;

namespace BankNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var db = new PaymentContext();
                var per = new Payment { ClientId = 0, Value = 2.00m, AccountNumberFrom = "123", AccountNumberTo = "456", Currency = "USD" };
                db.Payments.Add(per);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }



            CreateHostBuilder(args).Build().Run();

           
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, collection) =>
                {
                    collection.AddHostedService<KafkaConsumerHostedService>();
                });
    }
}