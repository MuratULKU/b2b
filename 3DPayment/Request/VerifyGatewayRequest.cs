using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment.Request
{
    public class VerifyGatewayRequest
    {
        public string CustomerIpAddress { get; set; }
        public bool ManufacturerCard { get; set; }
        public BankNames BankName { get; set; }
        public Dictionary<string, string> BankParameters { get; set; } = new Dictionary<string, string>();
    }
}
