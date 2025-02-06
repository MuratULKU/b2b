using Entity;


namespace Core.Extantaion
{
    public static class ProductExtension
    {
        public static string ToImage(this Product product)
        {
            if (product.firmDocs?.Count > 0)
            {
                var result = "data:image/jpg;base64,";
                result += Convert.ToBase64String(product.firmDocs[0].LData);
                return result;
            }
            return "data:image/jpeg;base64,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";
        }

        public static string ToMiktar1(this Product product, int StockAmountShow, int StockWarningAmount)
        {
            if (StockAmountShow == 1)
            {
                string returnText = "";

                if (product.ProductAmounts != null && product.ProductAmounts.Count > 0 && product.ProductAmounts[0].OnHand > 0)
                {
                    if (product.ProductAmounts[0].OnHand <= 0)
                        returnText = $"<i class ='fa fa-circle' style='color:red'></i>";
                    else if (product.ProductAmounts[0].OnHand <= StockWarningAmount)
                        returnText = $"<i class ='fa fa-circle' style='color:yellow'></i>";
                    else if (product.ProductAmounts[0].OnHand >= StockWarningAmount)
                        returnText = $"<i class ='fa fa-circle' style='color:green'></i>";
                }
                else
                    returnText = $"<i class ='fa fa-circle' style='color:red'></i>";
                return returnText;
            }
            else
                return product.ProductAmounts.Count > 0 ? product.ProductAmounts[0].OnHand.ToString() + " " + product.Unit : string.Empty;
        }

        public static string ToFiyat1(this Product product, double Discount)
        {
            if (product.PriceLists.Count > 0)
            {
                double price = (double)product.PriceLists[0].Price;
                double discount = price * Discount / 100; ;

                if (product.PriceLists[0].IncVat == 1)
                {
                    return ((price - discount) / (1 + ((double)product.SellVat / 100))).ToString("N2") + " " + product.PriceLists[0].Currency.CurrencySymbol;
                }
                else
                {
                    return (price - discount).ToString("N2") + " " + product.PriceLists[0].Currency.CurrencySymbol;
                }
            }
            return product.PriceLists.Count > 0 ? product.PriceLists[0].Price.ToString("N2") + " " + product.PriceLists[0].Currency.CurrencySymbol : string.Empty;
        }
        public static string ToFiyat2(this Product product, double Discount)
        {
            if (product.PriceLists.Count > 0)
            {
                double price = (double)product.PriceLists[0].Price;
                double discount = price * Discount / 100;

                if (product.PriceLists[0].IncVat == 1)
                {
                    return (price - discount).ToString("N2") + " " + product.PriceLists[0].Currency.CurrencySymbol;
                }
                else
                {
                    return ((price - discount) * (1 + ((double)product.SellVat / 100))).ToString("N2") + " " + product.PriceLists[0].Currency.CurrencySymbol;
                }
            }
            else
                return string.Empty;
        }
    }


}
