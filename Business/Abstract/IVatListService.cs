using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVatListService
    {
        public Task<double> GetbyVatNo(byte VatNo);
        public Task<VatList> GetbyVat(byte VatNo);
        public Task<IResult> Add(VatList vatList);
        public Task<IResult> Update(VatList vatList);
        public Task<double[]> GetVat();
    }
}
