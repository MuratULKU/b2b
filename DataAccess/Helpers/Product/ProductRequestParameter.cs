using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers.Product
{
    public class ProductRequestParameter
    {
        /// <summary>
        /// Mevcut Sayfa Numarası
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Sayfadaki Satır Sayısı
        /// </summary>
        public int PageSize { get; set; }
        public int CategoryId { get; set; }
        /// <summary>
        /// Toplam Sayfa Sayısı
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// Toplam Satır Sayısı
        /// </summary>
        public int RowCount { get; set; }
        public string Filtre { get; set; }
        public ProductRequestParameter()
        {
                CurrentPage = 1;
                PageSize = 10;
        }
    }
}
