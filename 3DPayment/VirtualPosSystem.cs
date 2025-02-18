using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment
{
    public enum VirtualPosSystem
    {
        [Description("Seçiniz")]
        None = 0,
        [Description("NestPay Ödeme Sistemi")]
        NestPay =1,
        InterVPOS=2,
        PayFor=3,
        GVP=4,
        KuveytTurk=5,
        GET724=6,
        Posnet=7,
        Innova=8

    }
}
