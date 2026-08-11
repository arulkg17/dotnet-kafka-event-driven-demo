namespace OrderAPI.Models;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedDate { get; set; }
}