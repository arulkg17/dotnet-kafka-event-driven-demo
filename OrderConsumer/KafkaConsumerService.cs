using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace OrderConsumer;

public class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;

    private readonly IConfiguration _configuration;

    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var bootstrapServers =
            _configuration["Kafka:BootstrapServers"];

        var topic =
            _configuration["Kafka:OrderCreatedTopic"];

        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = "order-consumer-group-v2",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer =
            new ConsumerBuilder<Ignore, string>(config)
                .Build();

        consumer.Subscribe(topic);

        _logger.LogInformation(
            "Kafka consumer started. Topic: {Topic}",
            topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(
                    stoppingToken);

                _logger.LogInformation(
                    "Order event received: {Message}",
                    result.Message.Value);

                // Later we will deserialize and process
                // the order event here.
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation(
                "Kafka consumer stopping...");
        }
        finally
        {
            consumer.Close();
        }

        await Task.CompletedTask;
    }
}