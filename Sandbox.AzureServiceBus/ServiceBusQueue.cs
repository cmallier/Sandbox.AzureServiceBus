
using Azure.Messaging.ServiceBus;

namespace Sandbox.AzureServiceBus;

internal class ServiceBusQueue
{
    const string QueueName = "QueueName";

    // Public methods
    public async Task SendMessageAsync()
    {
        Console.WriteLine( "SendMessageAsync" );

        // Create a Service Bus client here
        // By leveraging "await using", the DisposeAsync method will be called automatically once the client variable goes out of scope. 
        // In more realistic scenarios, you would want to store off a class reference to the client (rather than a local variable) so that it can be used throughout your program.
        await using var client = new ServiceBusClient( Constants.ServiceBusConnectionString );

        // Create sender
        await using ServiceBusSender sender = client.CreateSender( QueueName );

        try
        {
            var message = new ServiceBusMessage( "Some message" );

            Console.WriteLine( $"Sending to Azure : Body: {message.Body} - ContentType:{message.ContentType}" );

            await sender.SendMessageAsync( message );
        }
        catch( Exception exception )
        {
            Console.WriteLine( $"Exception: {exception.Message}" );
        }
        finally
        {
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }


    public async Task ReceiveMessageAsync1()
    {
        Console.WriteLine( "ReceiveMessageAsync" );

        await using var client = new ServiceBusClient( Constants.ServiceBusConnectionString );

        //var options = new ServiceBusReceiverOptions
        //{
        //    ReceiveMode = ServiceBusReceiveMode.PeekLock,
        //    PrefetchCount = 10
        //};

        // Receiver
        ServiceBusReceiver receiver =  client.CreateReceiver( QueueName );
        ServiceBusReceivedMessage serviceBusReceivedMessage = await receiver.ReceiveMessageAsync();

        Console.WriteLine( $"Received from Azure : Body: {serviceBusReceivedMessage.Body.ToString()} - ContentType: {serviceBusReceivedMessage.ContentType}" );

        // For options ServiceBusReceiveMode.PeekLock
        // await receiver.CompleteMessageAsync( serviceBusReceivedMessage );
    }


    public async Task ReceiveMessageAsync2()
    {
        Console.WriteLine( "Receive message from Azure" );

        // Create a Service Bus client here
        // By leveraging "await using", the DisposeAsync method will be called automatically once the client variable goes out of scope. 
        // In more realistic scenarios, you would want to store off a class reference to the client (rather than a local variable) so that it can be used throughout your program.
        await using var client = new ServiceBusClient( Constants.ServiceBusConnectionString );

        // Create the options to use for configuring the processor
        var processorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 1,
            AutoCompleteMessages = false
        };

        await using ServiceBusProcessor processor = client.CreateProcessor( QueueName, processorOptions );

        // Configure the message and error handler to use
        processor.ProcessMessageAsync += ProcessMessageHandler;
        processor.ProcessErrorAsync += ProcessErrorHandler;

        // Start processing
        await processor.StartProcessingAsync();

        Console.Read();

        // Close the processor here
        await processor.CloseAsync();
    }


    // Private methods
    private async Task ProcessMessageHandler( ProcessMessageEventArgs args )
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine( $"Received: {body}" );

        // Complete the message.
        // Messages is deleted from the queue. 
        await args.CompleteMessageAsync( args.Message );
    }

    private Task ProcessErrorHandler( ProcessErrorEventArgs args )
    {
        Console.WriteLine( args.Exception.ToString() );
        return Task.CompletedTask;
    }
}
