Write me a C# test using xunit. The test should send an accountactivated webhook to an instance of a .net 8 application using the webapplicationfactory.

The assertion of the test should be that we poll for a AccountActivatedEvent message being received from Azure Service bus on a topic dev.accountactivatedevent with a subscription called integration_tests. The AccountActivatedEvent message should have a single property of AccountId which has the value of the AccountId property of AccountActivated webhook.

The tests should use fluentassertions beequivalentto and be named using microsoft naming conventions.

The connection to azure service bus should use the azureclicredentials instead of a connection string.

Both the webhook and the message should use the same JSON settings, and it should be using system.text.json. The settings should be ignore nulls, and property naming is camelcase.

The code for sending the webhook and polling for message should be abstracted to a collection fixture.

Use autofixture to populate the accountactivated webhook should use the following spec

```json

{
  "body": {
    "correlationId": "string",
    "createdAt": "string",
    "details": {
      "countryOfRegistration": "string",
      "fourthLine": "string",
      "name": "string",
      "timezone": "string"
    },
    "id": "string",
    "marketId": "string",
    "primaryOwnerPersonaId": "string",
    "productId": "string",
    "settlementDetails": [
      {
        "currency": "string",
        "sortCode": "string",
        "accountNumber": "string",
        "ibans": [
          {
            "iban": "string",
            "bic": "string"
          }
        ],
        "accountName": "string",
        "bankName": "string",
        "bankAddress": "string",
        "balanceReference": "string"
      },
      {
        "currency": "string",
        "ibans": [
          {
            "iban": "string",
            "bic": "string"
          }
        ],
        "accountName": "string",
        "bankName": "string",
        "bankAddress": "string",
        "balanceReference": "string"
      },
      {
        "currency": "string",
        "ibans": [
          {
            "iban": "string",
            "bic": "string"
          }
        ],
        "accountName": "string",
        "bankName": "string",
        "bankAddress": "string",
        "balanceReference": "string"
      }
    ],
    "status": "string",
    "type": "string",
    "updatedAt": "string",
    "messageId": "string",
    "webhookEventTypeName": "string"
  }
}
```