# Sandbox.AzureServiceBus

\<namespace-name\>.servicebus.windows.net


## Concepts

- __Queue__: Allows for Sending and Receiving of messages. Often used for point-to-point communication.
- __Topic__: As opposed to Queues, Topics are better suited to publish/subscribe scenarios. A topic can be sent to, but requires a subscription, of which there can be multiple in parallel, to consume from.
- __Subscription__: The mechanism to consume from a Topic. Each subscription is independent, and receives a copy of each message sent to the topic. Rules and Filters can be used to tailor which messages are received by a specific subscription.



## Info

[Implement message-based communication workflows with Azure Service Bus](https://docs.microsoft.com/en-us/learn/modules/implement-message-workflows-with-service-bus/)
[Azure.Messaging.ServiceBus](https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Messaging.ServiceBus/7.2.1/api/index.html)
