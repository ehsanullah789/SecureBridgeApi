using Braintree;

namespace secureBridge_Services.Services.BrainTreeService
{
    public interface IBraintreeService
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}