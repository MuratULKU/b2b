using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IClFicheService
    {
        Task<IResult> Insert(ClFiche clFiche);
        Task<IResult> Update(ClFiche clFiche);
        Task<List<ClFiche>> GetClFicheFiche(int trCode, byte send);
    }
}
