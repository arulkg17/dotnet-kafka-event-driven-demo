namespace OrderAPI.Configuration;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;

    public string OrderCreatedTopic { get; set; } = string.Empty;
}