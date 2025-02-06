using B2B.Data;
using Blazorise;
using Business.Abstract;
using Business.Concrete;
using Entity;
using Microsoft.AspNetCore.Components;

namespace B2B.Components.Products
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
            return "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCAcHCg0KCgsMDAwMBwkODw0MDgsMDAz/2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCAAoAFYDASIAAhEBAxEB/8QAGwABAAMBAQEBAAAAAAAAAAAAAAYHCAUJAwT/xAAsEAABBAIBAwQCAgEFAAAAAAACAQMEBQYHAAgREgkTFCEiIxUWMRckMzRB/8QAGAEBAQEBAQAAAAAAAAAAAAAAAAIBAwT/xAAvEQEAAgAEBAUBCAMAAAAAAAABAhEAITFBAxJRYSIycYGhkhMjM5GisbLwQlJy/9oADAMBAAIRAxEAPwD3845jX1ocK/1G13pGj/qGH59/Jbap2f69lT3s01t/tpy+1JP40rxbXt37/Hd+0T8f/UoHXu5Mw6ZumBnDtdY/MxHIMw3rYYnZ4jhDNSjmsA+M7LKBUfyxtVppJGMEhl2QDLZpaqYxUNRZ5HBl9pz5eWXKb3+F8feluYVmjKI1xY8nL3OZ2orir8cPTJby8rj1I45heNcdXFn09TI5Veza+TUZqIjMfcwpvYV1jZw0NSaADex4ZTU5z219xGUciMEqCD5IRRe86rZVpmErNMNvn7r4/TBa5XUZJe45Ws5A5Lalj4OSDCK34fmKKcYBGMpj39r6ReZxuLHhiy0BX0OFLi6ekWPaVmzjY8OUkI7sT6uIcMvpnIl/z3yx6IccwCzN33a7ag4y91GZNFi5Pqs9hyJELEaAJFTYsOttrFgK7EcAK9z5YqTcoZUhEjtoMoVUyPVfQ3uu06k+i/U2wrtmLHuM4xCqvZzcYVFkH5MRp5xARVVUHyNeyKq9k56XgyCS/wCLT9U4/wAuHI9h0THnjxoyY1uX+mEv24kfnpi0+OOOcsdcOOOOMMOOOOMMcvJMJpszcrTuKirtip5oWNeUyKD6wZQIQg+15IvtuihkiGPYkQl7L9rzh5Z0+YDn1PlFde4Ph91X5sbTmRRp9NHks35NA220UsDBUkKANNCKuIXiLYInZBTtUfqM9Tlv0sUupbivl3EetutjVtLeM1VC7dzJ1e6xLJxlqKwy9IMiJttf0Arn4/Sonflc4D6kE7KNy9RVnEKcOFa3oMbl1UDOqx/AGK1+Us4ZUiW/aRWZDMT9bRm8TbqCDR+y24f6z5xYyhOW0VH6YSV7JKJbudjFS5oziboJ7ylEPURew3i8X/Ty0DJ1ozhbmjdPOYdFsCtmaIsMrlrGppN+2UkY/s+0jyt/griD5KP137fXJuWi8JNkWyw3FSbGjXGUFaljxSpXt3r+3h/1V7J+j/j+k/HmVcQ9WC02V0fbE2hjOP6evHtU3smpyBYmz3peOSmWobMsXq60i1TxzHHEkx2xZWK0XukbffyFEP67c9VrJdH4zkH8/pt4ctxnE8NyGbj0bKGjd+Xf27lYVcLxsA35xjb7+6SiDqr4r7SJ587MVeV35ffmi8vrcbOx4WrrExjdMeqelTjFz28bH182heNajrbHQtGZw0FKM2PWlTtSEgte61BJRIool490YVQBVbRfFVEV7fSc/ViWI1OAYtW0dDV19LSU8ZuFAr4EYI0WCw2KA2000CIINiKIIiKIiIiIiInMn0+79jUvXy23mkGPUvQdLT76biVFlZ2NOspq4AW3G5EtmE0rxMogq6600geZCpqCeaxbCvWkj5Rrfa9pEx3XOdXWsFx9wY+ttlMZRV3DVvNKG02E84sYGZTbjbqky6CD29pfdQXPIZjJeHz9eZTXTiThtrchSur3xORJjWQxL080ISNdMpRjn0O2N08cyLfepRlmqY2XVuwNX0tLl+J3mJQCgU2XnaQZULILAYLMpJRwWDF1lxJHmyrPZfZHxdVD8h72Feo0WWuYfVOYScDLrjNL3FL+octvrGo9Qkg5VkTisorzRNBDcbFADzSxY/JO/dUqBk6GvauW16Bzxt0LzSmqz/u7UmjqpFoM2srsvTnHMpad9RXKNgzNW5Fdavr6HVe75BRsMvY2VfPtvJyK9NhHZQPiNtRW5MaO4QqzLlEBmyBiimShMvTy6ssy62tD1OyL7XlTr/G8qgsTqBhvKFt7GSBeaOlIaGI00wPkIq0oPOkYGiuAwaK2lckvEf663kj0R0e2tZ1WDkX6e43SdTJzMstcX3xxxycMQXdvT7Tb6mYS/cSrSMWB5NGyqvSG4AI9KYbebAHfIC8mlR4u6D4l3ROxJ99693X6duG7xyvN76ddZZV3WauY7K+ZXyIyFSy6KUcqvlRQdYcbVwXj8iF8Xmz8RRQ7d0VxzIlFRyz5ssvFQWOycsaTMoTPCWbb0r2ta7lrlvbiG33pOU19ikmGW3txN3FhnsXY9heE9SyJdjbRoTERhXWHq1yGrIfGZfBlI6A2802YICNtiMgz700cX2xLuJ2VZln19dZBV43V2Vm87XMPy0o7U7SK8oMQ22RccfcUXfBsQUERABtfy445cZJVbInZIkSulRAy2DoYKuvSvU5ubPr4vFnuru4knUF0GYP1NZZk9rlLl49/bsDl67sIkaULLC10l9HzcHsHmL6Gidi8/FE/yC/55EYHph4/LHMZGT7G2hnFznMehiWVpcSKxt8Wqae7Ohgy1EhMR2k9x0hNBaTzFO/04pOE45MfDHlNP2uTJr1Vb79MJeK73r4Ih/CP0mJRuboGw/eWaZJe21lkseXlD+MyJQRJDANtlQWLlhD8EJklRDecIXe6r5AiIPtr+SwvSXRjIx/1Od3bks6mTAqcipauiom3ZwPx57qsNraTwYE19lXUjVsYvMRM/wCO7r3DwXjjiOWnf9QRfziA7e9YOYnWvhs/JP6Y7mm/Tgx3T2R4uf8AeNi5NiuvX5EnCsTupUFypw83W3WA+MTMVuW8jMd96OykyRIRps+w/kIkMg1z0SUupdA6t1zjuXbAqKPVEyFJhPRLUI8q6bi+faJPJtoQejOef7GxAELxH/Hbjjm269x94ti9UXV98HO73E9myjoZuR1XXF0cccczDH//2Q==";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="StockAmountShow">Stok Gösterim Sekli 1 ise simge ile göster</param>
        /// <param name="StockWarningAmount">Stok Simge Değişimi Miktarı</param>
        /// <returns></returns>
        public static string ToAvaliableAmount(this double column, int StockAmountShow, int StockWarningAmount)
        {
            if (StockAmountShow == 1)
            {
                if (column <= 0)
                    return  $"<i class ='fa fa-circle' style='color:red'></i>";
                else if (column <= StockWarningAmount)
                    return  $"<i class ='fa fa-circle' style='color:yellow'></i>";
                else if (column >= StockWarningAmount)
                    return $"<i class ='fa fa-circle' style='color:green'></i>";
                else
                    return $"<i class ='fa fa-circle' style='color:red'></i>";
            }
            else
                return column > 0 ? column.ToString() : "0";
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
