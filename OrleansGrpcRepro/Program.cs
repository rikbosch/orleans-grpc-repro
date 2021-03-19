using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using Orleans.Serialization;

namespace OrleansGrpcRepro
{
    class Program
    {

        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(app =>
                    {
                        app.Run(async httpContext =>
                        {
                            var client = httpContext.RequestServices.GetRequiredService<IClusterClient>();
                            var result = await client.GetGrain<ITestGrain>("some-grain-id").DoSomething(new Event1()
                            {
                                Quantity = 299
                            });

                            await httpContext.Response.WriteAsJsonAsync(result);
                        });
                    });
                })
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder.ConfigureServices(x => x.AddSingleton<IExternalSerializer, ProtobufSerializer>());
                    siloBuilder.UseLocalhostClustering();
                    siloBuilder.AddOutgoingGrainCallFilter<OutgoingGrainCallFilter>();
                    siloBuilder.AddIncomingGrainCallFilter<IncomingGrainCallFilter>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });
    }


}
