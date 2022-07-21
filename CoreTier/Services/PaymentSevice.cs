using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;

namespace CoreTier.Services
{
    public class PaymentSevice
    {
        public Session? _session;


        public string CreateSession(string sucsessURL, string cancelURL) 
        {
            var service = new SessionService();
            var options = new SessionCreateOptions()
            {
                LineItems = new List<SessionLineItemOptions>() {
                    new SessionLineItemOptions()
                    {
                        PriceData = new SessionLineItemPriceDataOptions() {
                         Currency = "USD",
                         ProductData = new SessionLineItemPriceDataProductDataOptions()
                         {
                            Description = "Test good description",
                            Name = "Test good"
                         },
                         UnitAmount = 100
                        },
                        Quantity = 1
                        
                    }
                },
                Mode = "payment",
                CustomerEmail = "myserver1978@gmail.com",
                ExpiresAt = DateTime.Now.AddHours(2),
                //AfterExpiration
                Locale = "en",
                CancelUrl = "https://"+cancelURL,
                SuccessUrl = "https://" + sucsessURL
            };

            options.AddExpand("payment_intent");
            Session session = service.Create(options);
            _session = session;
            return session.Url;

        }

        public void RefreshSession() {
            var options = new SessionGetOptions();
            options.AddExpand("payment_intent");

            var service = new SessionService();
            _session = service.Get(_session.Id, options);


        }

    }
}
