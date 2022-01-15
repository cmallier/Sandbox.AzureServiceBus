using Sandbox.AzureServiceBus;

Console.WriteLine( "Start" );


// Service Bus Queue
Console.WriteLine( "-- Service Bus Queue" );
var serviceBusQueue = new ServiceBusQueue();

await serviceBusQueue.SendMessageAsync();
await serviceBusQueue.ReceiveMessageAsync1();
//await serviceBusQueue.ReceiveMessageAsync2();



// Service Bus Topics
Console.WriteLine( "-- Service Bus Topics" );
var serviceBusTopics = new ServiceBusTopics();

await serviceBusTopics.SendMessageAsync();
await serviceBusTopics.ReceiveMessageAsync();