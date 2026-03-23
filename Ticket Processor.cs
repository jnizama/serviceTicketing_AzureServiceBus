using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Shared;

var connectionString = "<CONNECTION_STRING>";
var inputQueue = "tickets";
var outputQueue = "processed-tickets";

await using var client = new ServiceBusClient(connectionString);

var processor = client.CreateProcessor(inputQueue);
var sender = client.CreateSender(outputQueue);

processor.ProcessMessageAsync += async args =>
{
    var body = args.Message.Body.ToString();
    var ticket = JsonSerializer.Deserialize<Ticket>(body);

    // 🔥 realistic business logic
    if (ticket.Description.Contains("server", StringComparison.OrdinalIgnoreCase))
        ticket.Priority = "High";
    else if (ticket.Description.Contains("payment"))
        ticket.Priority = "Medium";

    Console.WriteLine($"🧠 Processed ticket: {ticket.Title} | Priority: {ticket.Priority}");

    var json = JsonSerializer.Serialize(ticket);
    await sender.SendMessageAsync(new ServiceBusMessage(json));

    await args.CompleteMessageAsync(args.Message);
};

processor.ProcessErrorAsync += args =>
{
    Console.WriteLine($"❌ Error: {args.Exception.Message}");
    return Task.CompletedTask;
};

await processor.StartProcessingAsync();
Console.ReadKey();
