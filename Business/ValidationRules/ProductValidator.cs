using Core.Validation;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validaton
{
    public class ProductValidator : BaseValidator<Product>
    {
        public override void ValidateRules(Product product)
        {
            NotEmpty(product.Code, "Stok kodu boş olamaz");
            MaxLength(product.Code, 50, "Stok kodu en fazla 50 karakter olabilir");

            NotEmpty(product.Name, "Stok adı boş olamaz");
            MaxLength(product.Name, 200, "Stok adı en fazla 200 karakter olabilir");

            Range(product.SellVat ?? 0, 0, 100, "Vergi oranı 0 - 100 arasında olmalıdır");

            NotEmpty(product.Unit, "Birim adı boş olamaz");

            MaxLength(product.SpeCode, 50, "Özel Kod 1 en fazla 50 karakter olabilir");
            MaxLength(product.SpeCode2, 50, "Özel Kod 2 en fazla 50 karakter olabilir");
            MaxLength(product.SpeCode3, 50, "Özel Kod 3 en fazla 50 karakter olabilir");
            MaxLength(product.SpeCode4, 50, "Özel Kod 4 en fazla 50 karakter olabilir");
            MaxLength(product.SpeCode5, 50, "Özel Kod 5 en fazla 50 karakter olabilir");

        }
    }
}
