using Microsoft.Extensions.DependencyInjection;
using MovieTickets.BillLogger;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.JsonDataFeed;
using MovieTickets.TransactionProcessor;
using System;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var services = ConfigureServices();
                var serviceProvider = services.BuildServiceProvider();

                var analyser = serviceProvider.GetService<BatchCostAnalyzer>();
                await analyser.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.ToString()}");
            }
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<ITransactionFeed, JsonTicketFeed>();
            services.AddTransient<ITransactionProcessor, TransactionBillProcessor>();
            services.AddTransient<IBillObserver, ConsoleLogger>();
            services.AddTransient<IConsoleWriter, ConsoleWriter>();

            services.AddTransient<BatchCostAnalyzer>();
            return services;
        }
    }
}
