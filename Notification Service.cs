using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Shared;

var connectionString = "<CONNECTION_STRING>";
var queueName = "processed-tickets";

await using var client = new ServiceBusClient(connectionString);
var processor = client.CreateProcessor(queueName);

processor.ProcessMessageAsync += async args =>
{
    var ticket = JsonSerializer.Deserialize<Ticket>(args.Message.Body.ToString());

    Console.WriteLine($"🔔 Notification sent to {ticket.CustomerEmail}");
    Console.WriteLine($"   Ticket: {ticket.Title} | Priority: {ticket.Priority}");

    await args.CompleteMessageAsync(args.Message);
};

processor.ProcessErrorAsync += args =>
{
    Console.WriteLine($"❌ Error: {args.Exception.Message}");
    return Task.CompletedTask;
};

await processor.StartProcessingAsync();
Console.ReadKey();
