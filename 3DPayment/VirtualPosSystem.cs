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
        [Description("Denizbank Ödeme Sistemi")]
        InterVPOS =2,
        [Description("Finansbank Ödeme Sistemi")]
        PayFor =3,
        [Description("Garanti Bankası")]
        GVP =4,
        [Description("Kuveyt Türk")]
        KuveytTurk =5,
        [Description("Vakıfbank")]
        GET724 =6,
        [Description("Yapı Kredi Bankası")]
        Posnet =7,
        [Description("Innova Ödeme Sistemi")]
        Innova =8

    }
}
