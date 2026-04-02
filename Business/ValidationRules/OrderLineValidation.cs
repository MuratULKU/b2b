using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public static class OrderLineValidation
    {
        public static List<string> Validation(this OrdLine ordLine)
        {

            var result = new List<string>();
            if (ordLine == null)
            {
                result.Add("Sipariş Satırı Boş Olamaz");
            }
            if(ordLine.Id == Guid.Empty)
            {
                result.Add("Sipariş Satır Id Boş Olamaz");
            }
            if (ordLine.Unit == null)
            {
                result.Add("Birim Boş Olamaz");
            }
            if (ordLine.Amount < 1)
            {
                result.Add("Sipariş Miktarı 1 den Küçük Olamaz");
            }
            if (ordLine.Price < 1)
            {
                result.Add("Birim Fiyat 1 den Küçük Olamaz");
            }
            return result;

        }
    }
}