using _3DPayment.Request;
using _3DPayment.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment
{
    public interface IPaymentProvider
    {
        Task<PaymentGatewayResult> ThreeDGatewayRequest(PaymentGatewayRequest request);
        Task<VerifyGatewayResult> VerifyGateway(VerifyGatewayRequest request, PaymentGatewayRequest gatewayRequest, IFormCollection form);
        Task<CancelPaymentResult> CancelRequest(CancelPaymentRequest request);
        Task<RefundPaymentResult> RefundRequest(RefundPaymentRequest request);
        Task<PaymentDetailResult> PaymentDetailRequest(PaymentDetailRequest request);
        Dictionary<string, string> TestParameters { get; }
    }
}
