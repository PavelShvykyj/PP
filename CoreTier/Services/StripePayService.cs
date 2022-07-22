using AutoMapper;
using CoreTier.Interfaces;
using DataTier.Models;
using Repository.Interfaces;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    static class PaymentStatus 
    {
        public const string Success = "succeeded";
        public const string Canceled = "canceled";
        public const string Processing = "processing";

    }

    public class StripePayService : IPayService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StripePayService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        private void UpdateProcess(ref OrderPaymentProces? process) 
        {

            if (process == null) return;
            
            var session = GetSession(process.ExternalId);
            if (DateTime.Compare(session.ExpiresAt,process.Expired) !=0) 
            {
                process.Expired = session.ExpiresAt;
            }
            
            var status = session.PaymentIntent.Status;
            switch (status)
            {
                case PaymentStatus.Success:
                    // cant pay delete process, add detail
                    PayFinishSuccess(process);
                    break;

                case PaymentStatus.Canceled:
                    // can pay delete process
                    _unitOfWork.OrderPaymentProcess.Remove(process);
                    _unitOfWork.SaveAsync();
                    process = null;
                    break;

                case PaymentStatus.Processing:
                    // cant pay nothing to do
                    break;
                default:
                    if (DateTime.Compare(process.Expired, DateTime.Now) <= 0)
                    {
                        // can pay delete process
                        _unitOfWork.OrderPaymentProcess.Remove(process);
                        _unitOfWork.SaveAsync();
                        process = null;
                    }
                    break;
            }
        }

        private Session GetSession(string Id)
        {
            var options = new SessionGetOptions();
            options.AddExpand("payment_intent");
            var service = new SessionService();
            return service.Get(Id, options);
        }

        public bool CanPay(int Id)
        {
            var process = _unitOfWork.Orders.GetPaymentProces(Id);
            UpdateProcess(ref process);
            if (process is not null)
            {
                return false;
            }
            var detail = _unitOfWork.Orders.GetPaymentDitail(Id);
            return detail is null;
        }

        public void PayFinishFail(int Id)
        {
            /// may be add fail last error in process
            var process = _unitOfWork.Orders.GetPaymentProces(Id);
            if (process is not null)
            {
                PayFinishFail(process);
            }
        }

        public void PayFinishFail(OrderPaymentProces process)
        {
            /// may be add fail last error in process instead of deleting
            _unitOfWork.OrderPaymentProcess.Remove(process);
            _unitOfWork.SaveAsync();

        }

        public void PayFinishSuccess(int Id)
        {
            var process = _unitOfWork.Orders.GetPaymentProces(Id);
            if (process is not null) 
            {
                PayFinishSuccess(process);
            }
        }

        public void PayFinishSuccess(OrderPaymentProces process) 
        {
            var paymentDetail = new OrderPaymentDitail()
            {
                Order = process.Order,
                OrderId = process.OrderId,
                ExternalID = process.ExternalId,
                Amaunt = process.Order.Goods.Select(r => new { OrderId = r.OrderId, Summ = r.Summ })
                                  .GroupBy(r => r.OrderId)
                                  .Select(g => new { Summ = g.Sum(e => e.Summ) })
                                  .SingleOrDefault()
                                  .Summ


            };

            _unitOfWork.OrderPaymentDitails.Create(paymentDetail);
            _unitOfWork.OrderPaymentProcess.Remove(process);
            _unitOfWork.SaveAsync();
        }


        public void StartPay(int Id)
        {
            /// Get oreder with properties
            var order = _unitOfWork.Orders.GetOrderWithProperties(Id);
            if (order is null)
            {
                return;
            }
            
            /// Create Stripe session on order data
            var service = new SessionService();
            var LineItems = new List<SessionLineItemOptions>();
            foreach (var orderGood in order.Goods)
            {
                LineItems.Add(
                    new SessionLineItemOptions()
                    {
                        Quantity = (long)orderGood.Quantity,
                        PriceData = new SessionLineItemPriceDataOptions() {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Description = orderGood.Good.Name,
                                Name = orderGood.Good.Name
                            },
                            UnitAmount = (long)orderGood.Price*100
                        }
                    });
            }

            ///// ???????????????????????????????
            var cancelURL  = "";
            var sucsessURL = "";

            var options = new SessionCreateOptions()
            {
                LineItems = LineItems,
                Mode = "payment",
                CustomerEmail = order.Customer.Email,
                ExpiresAt = DateTime.Now.AddHours(2),
                Locale = "en",
                CancelUrl = "https://" + cancelURL,
                SuccessUrl = "https://" + sucsessURL,
                Expand = new List<string> { "payment_intent" },
                Metadata = new Dictionary<string, string>() { { "OrderID", Id.ToString() } }
            };

            Session session = service.Create(options);

            // create OrderPaymentProcess on session data
            var process = new OrderPaymentProces()
            {
                OrderId = Id,
                Expired = session.ExpiresAt,
                ExternalId = session.Id,
                ExternalName = "Stripe"
            };
            _unitOfWork.OrderPaymentProcess.Create(process);
            _unitOfWork.SaveAsync();

            ///// ???????????????????????????????
            //return session.Url;
        }
    }
}
