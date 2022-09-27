using Braintree;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Services.BrainTreeService
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration _config;
        public BraintreeService(IConfiguration config)
        {
            _config = config;
        }

        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.GetSection("BraintreeGateway:MerchantId").Value,
                PublicKey = _config.GetSection("BraintreeGateway:PublicKey").Value,
                PrivateKey = _config.GetSection("BraintreeGateway:PrivateKey").Value
            };

            return newGateway;
        }
        public IBraintreeGateway GetGateway()
        {
            return CreateGateway();

        }
    }
}
