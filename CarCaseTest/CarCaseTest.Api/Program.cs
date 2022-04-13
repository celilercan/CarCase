using CarCaseTest.Queue.Consumers;
using CarCaseTest.Queue.Services;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace CarCaseTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //configure logging first
            ConfigureLogging();

            //then create the host, so that if the host fails we can log errors
            CreateHost(args);
        }

		private static void CreateHost(string[] args)
		{
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (System.Exception ex)
			{
				Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
				throw;
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
					.ConfigureServices((hostContext, services) =>
                    {
						var config = hostContext.Configuration.GetSection("RabbitMQ");
						//services.RegisterModelServices();
						services.AddMassTransit(x => {
							x.AddConsumers(typeof(AdvertVisitConsumer).Assembly);
							x.UsingRabbitMq((context, cfg) =>
							{
								cfg.Host(config["HostName"], x => 
								{
									x.Username(config["UserName"]);
									x.Password(config["Password"]);
								});
								cfg.UseMessageRetry(r => r.Interval(3, 1000));
								cfg.Durable = true;
								cfg.AutoDelete = false;
								cfg.ReceiveEndpoint(config["QueueName"], e =>
								{
									e.ConfigureConsumer<AdvertVisitConsumer>(context);
								});
							});
						});
						services.AddHostedService<MassTransitService>();
					})
					.ConfigureWebHostDefaults(webBuilder =>
					{
						webBuilder.UseStartup<Startup>();
					})
					.ConfigureAppConfiguration(configuration =>
					{
						configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
						configuration.AddJsonFile(
							$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
							optional: true);
					})
					.UseSerilog();

		private static void ConfigureLogging()
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(
					$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
					optional: true)
				.Build();

			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithMachineName()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
				.Enrich.WithProperty("Environment", environment)
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
		{
			return new ElasticsearchSinkOptions(new Uri(configuration.GetConnectionString("ElasticSearch")))
			{
				AutoRegisterTemplate = true,
				IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
			};
		}
	}
}
