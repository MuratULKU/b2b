using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUnitofWork:IDisposable
    {
        IBankCardRepository BankCards { get; }
        IBrandCardRepository BrandCards { get; }
        ICreditCardInstallmentRepository CreditCardInstallment { get; }
        ICreditCardRepository CreditCards { get; }
        ICreditCardPrefixRepository CreditCardPrefixs { get; }
        IVirtualPosRepository VirtualPoses { get; }
        IVirtualPosParameterRepository VirtualPosParameter { get; }
        IPaymentRepository Payment { get; }
        ICategoryRepository Category { get; }
        ICharSetRepository CharSet { get; }
        ICharAsgnRepository CharAsgn { get; }
        ICharCodeRepository CharCode { get; }
        ICharValRepository CharVal { get; } 
        IPriceListRepository PriceList { get; }
        IProductAmountRepository ProductAmount { get; }
        IClientRepository Client { get; }
        IOrdFicheRepository OrdFiche { get; }
        IOrdLineRepository OrdLine { get; }
        IDocumentNoRepository DocumentNo { get; }
        IFirmParamRepository FirmParam { get; }
        ICompanyRepository Company { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole { get; }
        IClFicheRepository ClFiche { get; }
        public void BeginTransaction();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
        public IEnumerable<EntityEntry> ChangedEntries();
        public EntityState ChangedEntity<TEntity>(TEntity entity);
        Task<int> CommitAsync();
    }
}
