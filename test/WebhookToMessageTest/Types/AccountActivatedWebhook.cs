namespace WebhookToMessageTest.Types;

public class AccountActivatedWebhook
{
    public WebhookBody Body { get; set; }
}

public class WebhookBody
{
    public string CorrelationId { get; set; }
    public string CreatedAt { get; set; }
    public AccountDetails Details { get; set; }
    public string Id { get; set; }
    public string MarketId { get; set; }
    public string PrimaryOwnerPersonaId { get; set; }
    public string ProductId { get; set; }
    public List<SettlementDetail> SettlementDetails { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string UpdatedAt { get; set; }
    public string MessageId { get; set; }
    public string WebhookEventTypeName { get; set; }
}

public class AccountDetails
{
    public string CountryOfRegistration { get; set; }
    public string FourthLine { get; set; }
    public string Name { get; set; }
    public string Timezone { get; set; }
}

public class SettlementDetail
{
    public string Currency { get; set; }
    public string SortCode { get; set; }
    public string AccountNumber { get; set; }
    public List<Iban> Ibans { get; set; }
    public string AccountName { get; set; }
    public string BankName { get; set; }
    public string BankAddress { get; set; }
    public string BalanceReference { get; set; }
}

public class Iban
{
    public string IbanNumber { get; set; }
    public string Bic { get; set; }
}