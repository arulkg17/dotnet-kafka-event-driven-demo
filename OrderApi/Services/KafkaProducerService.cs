using Confluent.Kafka;
using System.Text.Json;

namespace OrderAPI.Services;

public class KafkaProducerService
{
    private readonly IConfiguration _configuration;

    public KafkaProducerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublishOrderCreatedAsync<T>(T message)
    {
        var bootstrapServers =
            _configuration["Kafka:BootstrapServers"];

        var topic =
            _configuration["Kafka:OrderCreatedTopic"];

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };

        using var producer =
            new ProducerBuilder<Null, string>(config).Build();

        var json = JsonSerializer.Serialize(message);

        var result = await producer.ProduceAsync(
            topic,
            new Message<Null, string>
            {
                Value = json
            });

        Console.WriteLine(
            $"Order event published. Topic: {result.Topic}, Partition: {result.Partition}, Offset: {result.Offset}");
    }
}