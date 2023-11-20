using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DocumentNoRepository : IDocumentNoRepository
    {
        private readonly RepositoryContext _dataContext;

        public DocumentNoRepository(RepositoryContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DocumentNo GetDocNo(int doctype)
        {
            var db =  _dataContext.DocumentNo.Where(x => x.DocType == doctype).FirstOrDefault();
            if (db == null)
            {
                db = new DocumentNo() {
                    DocType = doctype,
                    Prefix = string.Empty,
                    DocNo = 1
                };
                return _dataContext.DocumentNo.Add(db).Entity;
            }
            db.DocNo += 1;
            return _dataContext.DocumentNo.Update(db).Entity;
            
        }

        public void Insert(DocumentNo docno)
        {
            _dataContext.DocumentNo.Add(docno);
            _dataContext.SaveChanges();
        }

        public void Update(DocumentNo docno)
        {
            _dataContext.DocumentNo.Update(docno);
            _dataContext.SaveChanges(); 
        }
    }
}
