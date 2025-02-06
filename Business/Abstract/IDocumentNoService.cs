using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDocumentNoService
    {
        Task<IResult> Insert(DocumentNo documentNo);
        Task<IResult> Update(DocumentNo documentNo);
        Task<string> GetDocNo(int doctype);
    }
}
