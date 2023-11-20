using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDocumentNoRepository
    {
        void Insert(DocumentNo docno);
        void Update(DocumentNo docno);
        DocumentNo GetDocNo(int doctype);
    }
}
