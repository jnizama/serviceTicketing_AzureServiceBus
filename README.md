# 🎫 Support Ticket System - Azure Service Bus (.NET)

This project is part of a real-company support ticket system based in Spain, using Azure Service Bus and .NET.

## 🧠 Architecture

Event-driven microservices:

1. **TicketPublisher**

   * Simulates ticket creation
   * Sends messages to Service Bus queue

2. **TicketProcessor**

   * Consumes tickets
   * Applies business logic (priority classification)
   * Sends processed tickets to another queue

3. **NotificationService**

   * Receives processed tickets
   * Simulates sending notifications (email/logging)

---

## 🔄 Flow

```
[Publisher] → (tickets queue) → [Processor] → (processed-tickets queue) → [Notification]
```

---

## ⚙️ Setup

1. Create an Azure Service Bus namespace

2. Create the following queues:

   * `tickets`
   * `processed-tickets`

3. Update connection string in all projects:

```
var connectionString = "<YOUR_CONNECTION_STRING>";
```

---

## ▶️ Run

Start services in this order:

```
1. TicketProcessor
2. NotificationService
3. TicketPublisher
```

---

## 📦 Example Output

```
📤 Ticket sent: Server down
🧠 Processed ticket: Server down | Priority: High
🔔 Notification sent to client1@email.com
```

---

## 🚀 Future Improvements

* Retry policies (Polly)
* Dead-letter queue monitoring
* Azure Functions integration
* Email integration (SendGrid)
* REST API layer (ASP.NET Core)

---

## 📄 License

MIT
