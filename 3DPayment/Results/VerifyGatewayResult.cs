﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment.Results
{
    public class VerifyGatewayResult
    {
        public bool Success { get; set; }
        public string ResponseCode { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public string OrderNumber { get; set; }
        public int Installment { get; set; }
        public int ExtraInstallment { get; set; }
        public Uri CampaignUrl { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public string ErrMsg { get; set; }

        public string CardMask { get; set; }
         

        public static VerifyGatewayResult Successed(string transactionId, string referenceNumber, string cardMask, int installment = 0, int extraInstallment = 0, 
            string message = null, string responseCode = null,
            string campaignUrl = null )
        {
            return new VerifyGatewayResult
            {
                Success = true,
                TransactionId = transactionId,
                ReferenceNumber = referenceNumber,
                Installment = installment,
                ExtraInstallment = extraInstallment,
                CardMask = cardMask,
                Message = message,
                ResponseCode = responseCode,
                CampaignUrl = !string.IsNullOrEmpty(campaignUrl) ? new Uri(campaignUrl) : null,
               
            };
        }

        public static VerifyGatewayResult Failed(string errorMessage, string errorCode = null, string responseCode = null)
        {
            return new VerifyGatewayResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode,
                ResponseCode = responseCode
            };
        }
    }
}
