using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PaymentTransaction:BaseEntity
    {
        public Guid OrderNumber { get; set; }
        public string? TransactionNumber { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? UserIpAddress { get; set; }
        public string? UserAgent { get; set; }
        public int BankCardId { get; set; }
        public string? CardPrefix { get; set; }
        public string? CardHolderName { get; set; }
        public int Installment { get; set; }
        public int? ExtraInstallment { get; set; }
        public decimal TotalAmount { get; set; }
        public string? BankErrorMessage { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? ClientCode { get; set; }

        public int StatusId { get; set; }
        public bool Deleted { get; set; } = false;
        public string? BankRequest { get; set; }
        public string? BankResponse { get; set; }
        public string? MaskedCardNumber { get; set; }
        public BankCard Bank { get; set; }

        public PaymentStatus Status
        {
            get => (PaymentStatus)StatusId;
            set => StatusId = (int)value;
        }

        public void MarkAsCreated()
        {
            CreateDate = DateTime.Now;
            Status = PaymentStatus.Pending;
        }

        public void MarkAsPaid(DateTime paidDate)
        {
            PaidDate = paidDate;
            Status = PaymentStatus.Paid;
            BankErrorMessage = null;
        }

        public void MarkAsFailed(string bankErrorMessage, string bankResponse)
        {
            Status = PaymentStatus.Failed;
            BankErrorMessage = bankErrorMessage;
            BankResponse = bankResponse;
        }
    }
}
