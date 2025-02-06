using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validaton
{
    public static class CreditCardPrefixValidation
    {
        public static List<string> Validation(this CreditCardPrefix creditCardPrefix)
        {
            var result = new List<string>();
            if (creditCardPrefix.BankCode == 0)
                result.Add("Banka Hesap Kodu Boş Olamaz.");
            if (creditCardPrefix.BrandCode == 0)
                result.Add("Kredi Kartı Üye Programı Boş Olamaz.");
            if (creditCardPrefix.Prefix.Length < 6)
                result.Add("Prefix 6 Karakterden Az Olmaz.");
            if (creditCardPrefix.Prefix.Length > 8)
                result.Add("Prefix 8 Karakterden Büyük Olamaz.");
            if (creditCardPrefix.Prefix.Length == 7)
                result.Add("Prefix 6 Karakter veya 8 Karakter Olmalı.");

            return result;
        }
    }
}
