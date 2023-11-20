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
    public class FirmDocRepository : IFirmDocRepository
    {
        private readonly RepositoryContext _dataContext;

        public FirmDocRepository(RepositoryContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Delete(FirmDoc doc)
        {
            _dataContext.FirmDocs.Remove(doc);
            _dataContext.SaveChanges();
        }

        public void Insert(FirmDoc doc)
        {
            _dataContext.FirmDocs.Add(doc);
            _dataContext.SaveChanges();
        }

        public void Update(FirmDoc doc)
        {
            _dataContext.FirmDocs.Update(doc);
            _dataContext.SaveChanges();
        }
    }
}
