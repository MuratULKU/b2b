using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment.Results
{
    public class CancelPaymentResult
    {
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public static CancelPaymentResult Successed(string transactionId, string referenceNumber, string message = null)
        {
            return new CancelPaymentResult
            {
                Success = true,
                TransactionId = transactionId,
                ReferenceNumber = referenceNumber,
                Message = message
            };
        }

        public static CancelPaymentResult Failed(string errorMessage, string errorCode = null)
        {
            return new CancelPaymentResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode
            };
        }
    }
}


