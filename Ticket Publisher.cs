using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Shared;

var connectionString = "<CONNECTION_STRING>";
var queueName = "tickets";

await using var client = new ServiceBusClient(connectionString);
var sender = client.CreateSender(queueName);

var tickets = new List<Ticket>
{
    new Ticket { Title = "Server down", Description = "Production server is not responding", CustomerEmail = "client1@email.com" },
    new Ticket { Title = "Login issue", Description = "User cannot login", CustomerEmail = "client2@email.com" },
    new Ticket { Title = "Payment failed", Description = "Transaction error", CustomerEmail = "client3@email.com" }
};

foreach (var ticket in tickets)
{
    var json = JsonSerializer.Serialize(ticket);

    var message = new ServiceBusMessage(json)
    {
        MessageId = ticket.Id
    };

    await sender.SendMessageAsync(message);
    Console.WriteLine($"📤 Ticket sent: {ticket.Title}");
}
