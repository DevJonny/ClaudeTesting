using FluentAssertions;
using WebhookToMessageTest.Fixtures;
using WebhookToMessageTest.Types;

namespace WebhookToMessageTest
{
    public class AccountActivationTests : IClassFixture<AccountActivationFixture>
    {
        private readonly AccountActivationFixture _fixture;

        public AccountActivationTests(AccountActivationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Receive_AccountActivatedEvent_When_AccountActivated_Webhook_Is_Sent()
        {
            // Arrange
            var accountId = Guid.NewGuid().ToString();
            var webhook = new AccountActivatedWebhook
            {
                Body = new WebhookBody
                {
                    Id = accountId,
                    CorrelationId = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow.ToString("o"),
                    Details = new AccountDetails
                    {
                        CountryOfRegistration = "US",
                        Name = "Test Account",
                        Timezone = "America/New_York"
                    },
                    MarketId = "US",
                    PrimaryOwnerPersonaId = Guid.NewGuid().ToString(),
                    ProductId = "TestProduct",
                    SettlementDetails = new List<SettlementDetail>
                    {
                        new SettlementDetail
                        {
                            Currency = "USD",
                            AccountName = "Test Account",
                            Ibans = new List<Iban>
                            {
                                new Iban { IbanNumber = "US123456789", Bic = "TESTBIC" }
                            }
                        }
                    },
                    Status = "Active",
                    Type = "Business",
                    UpdatedAt = DateTime.UtcNow.ToString("o"),
                    MessageId = Guid.NewGuid().ToString(),
                    WebhookEventTypeName = "AccountActivated"
                }
            };

            // Act
            await _fixture.SendWebhookAsync(webhook);
            var receivedEvent = await _fixture.PollForAccountActivatedEventAsync();

            // Assert
            receivedEvent.Should().BeEquivalentTo(new AccountActivatedEvent { AccountId = accountId });
        }
    }
}
