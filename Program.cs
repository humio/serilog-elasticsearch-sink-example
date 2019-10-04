using System;
using System.Threading;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace serilog_elastic_sink_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerConfig = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:8080/api/v1/ingest/elastic-bulk")) // humio elastic bulk endpoint
               {
                   FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                   EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
                   ModifyConnectionSettings = x => x.BasicAuthentication(userName: "", password: "88WS28JEuB0G2WC3cYufdgTINAxOGQvliBrqH5Vqutjb")  // password is ingest token from humio
               });

            var logger = loggerConfig.CreateLogger();


            while (true)
            {
                Console.WriteLine("Logging..");
                logger.Information("This will show up in humio..");
                Thread.Sleep(1000);
            }

        }
    }
}

