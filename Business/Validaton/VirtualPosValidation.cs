using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validaton
{
    public static class VirtualPosValidation
    {
        public static List<string> Validation(this VirtualPos virtualPos)
        {
            var result = new List<string>();
            if (virtualPos.Id == Guid.Empty)
                 result.Add("Id Boş Olmaz.");
            if (virtualPos.BankCardId == Guid.Empty)
                result.Add("Banka Seçilmemiş.");
            if (virtualPos.CardBrandId == Guid.Empty)
                result.Add("Kart Tipi Seçilmemiş.");
            if (virtualPos.Name == null)
                result.Add("Sanal Pos Adı Girilmemiş.");
            if (virtualPos.Name?.Length < 3)
                result.Add("Sanal Pos Adı En Az 3 Karakter Olmalıdır.");
            return result;

        }
    }
}
