using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Services;

namespace OrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly KafkaProducerService _kafkaProducer;

    public OrderController(KafkaProducerService kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        order.CreatedDate = DateTime.UtcNow;

        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName,
            Amount = order.Amount,
            CreatedDate = order.CreatedDate
        };

        await _kafkaProducer.PublishOrderCreatedAsync(
            orderCreatedEvent);

        return Ok(new
        {
            message = "Order created successfully",
            order
        });
    }
}