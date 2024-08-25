using System.Text.Json;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebhookToMessageTest.Types;

namespace WebhookToMessageTest.Fixtures;

public class AccountActivationFixture : IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusReceiver _receiver;
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public AccountActivationFixture()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
            
        var credential = new AzureCliCredential();
        _serviceBusClient = new ServiceBusClient("your-service-bus-namespace.servicebus.windows.net", credential);
        _receiver = _serviceBusClient.CreateReceiver("dev.accountactivatedevent", "integration_tests");
    }

    public async Task SendWebhookAsync(AccountActivatedWebhook webhook)
    {
        var json = JsonSerializer.Serialize(webhook, JsonOptions);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        await _client.PostAsync("/webhook/account-activated", content);
    }

    public async Task<AccountActivatedEvent> PollForAccountActivatedEventAsync()
    {
        var message = await _receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(30));
        if (message == null)
        {
            throw new TimeoutException("No message received within the timeout period.");
        }

        var json = message.Body.ToString();
        return JsonSerializer.Deserialize<AccountActivatedEvent>(json, JsonOptions);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _receiver.DisposeAsync();
        await _serviceBusClient.DisposeAsync();
        _client.Dispose();
        await _factory.DisposeAsync();
    }
}