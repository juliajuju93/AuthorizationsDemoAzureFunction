# AuthorizationsDemoAzureFunction
Web APIs have experienced an exponential increase in popularity and usage in the past few years. APIs exist at the intersection of business, products, and technologies and have transformed the way businesses interact with each other and the way they provide value to their customers. Web APIs allow businesses to access 3rd-party data, allow for cross-platform communication and seamless integration anywhere and anytime it's required, offering unmatched data processing efficiencies and cost savings.

## Azure API Management
Azure API Management accelerates the deployment, monitoring, security, and sharing of APIs in a dedicated network. It is a way to create consistent and modern API gateways for back-end services. Authorizations (along with authentication) is an important security component of your development process because it enables organizations to keep their networks secure by permitting only authenticated users (or processes) to access protected resources. Implementing authentication requires to understand these concepts and is not only very time consuming but also comes with its challenges.

## API Management Authorizations 🚀
Authorizations in API Management is a simple and reliable way to unbundle and abstract authorizations from web APIs. It greatly simplifies the process of authenticating and authorizing user's across one (or) more backend or SaaS services. With Authorizations you can easily configure OAuth, Consent, acquire tokens, cache tokens and refresh tokens without writing a single line of code. It allows you to delegate authentication to your API Management instance. This feature enables APIs to be exposed with or without a subscription key, and the authorization to the backend service uses OAuth 2.0., and reduces development costs in ramping up, implementing and maintaining security features with service integrations. API Management does all the heavy lifting for you, while you can focus on the application/domain logic.

## Authorization scenario - Time triggered Azure Function ⏳
In this repo, we will talk about an unattended scenario with Azure Functions. With our [Blog Post: Use Static Web Apps API and API Management Authorizations to integrate third party services](https://link-url-here.org](https://techcommunity.microsoft.com/t5/apps-on-azure-blog/use-static-web-apps-api-and-api-management-authorizations-to/ba-p/3603755), users are able to post a GitHub issue to a repository. We now want to implement a timer triggered function with Azure Functions that will GET the count of GitHub issues and POST about it in a Microsoft Teams channel. This will create a reminder notification in Teams about how many issues are still open:

### Prerequisites

- A running Azure API Management service instance. Check out our [Quickstart: Create a new Azure API Management service instance by using the Azure portal](https://learn.microsoft.com/en-us/azure/api-management/get-started-create-service-instance). *Note: Managed system-assigned identity must be enabled for the API Management instance.*
- [Visual Studio 2022](https://azure.microsoft.com/downloads/) (Make sure you select the **Azure development workload** during installation) or [Visual Studio Code](https://code.visualstudio.com/)

### STEP 1 - Configure Authorizations in Azure API Management
