using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment
{
    public interface IPaymentProviderFactory
    {
        IPaymentProvider Create(BankNames bankName);
        IPaymentProvider Create(VirtualPosSystem posSystem);
        string CreatePaymentFormHtml(IDictionary<string, object> parameters, Uri actionUrl, bool appendFormSubmitScript = true);
    }
}
