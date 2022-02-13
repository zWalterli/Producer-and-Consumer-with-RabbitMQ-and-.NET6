using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Producer.Models;
using RabbitMQ.Client;

namespace Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly string QUEUE_NAME = "email";
    private readonly ConnectionFactory _factory;
    public MessageController()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost"
            // Uri = new Uri("amqp://guest:guest@localhost:15672")
        };
    }

    [HttpPost]
    public IActionResult Post([FromBody] MessageInputModel message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: QUEUE_NAME,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var stringFiedMessage = JsonConvert.SerializeObject(message);
        var bytesMessage = Encoding.UTF8.GetBytes(stringFiedMessage);

        channel.BasicPublish(
            exchange: "",
            routingKey: QUEUE_NAME,
            basicProperties: null,
            body: bytesMessage
        );

        return Ok();
    }
}
