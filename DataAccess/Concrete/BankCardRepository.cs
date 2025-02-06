using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;
using DataAccess.EFCore;
using Core.Abstract;
using Core.Concrete;
using System.Xml.XPath;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class BankCardRepository : Repository<BankCard>,IBankCardRepository
    {
        public BankCardRepository(RepositoryContext context)
            : base(context)
        { }
    }
}
