using DataAccess.Abstract;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SanalMagaza.DataAccess.Concrete;


namespace DataAccess.Concrete
{
    public class UnitofWork : IUnitofWork
    {
        private IDbContextTransaction _transaction;
        private readonly RepositoryContext _context;
        private IBankCardRepository? _bankCardRepository;
        private IBrandCardRepository? _brandCardRepository;
        private ICreditCardInstallmentRepository? _creditCardInstallmentRepository;
        private ICreditCardRepository? _creditCardRepository;
        private ICreditCardPrefixRepository? _creditCardPrefixRepository;
        private IVirtualPosRepository? _virtualPosRepository;
        private IVirtualPosParameterRepository? _virtualPosParameterRepository;
        private IPaymentRepository? _paymentRepository;
        private ICategoryRepository? _categoryRepository;
        private ICharSetRepository? _charsetRepository;
        private ICharAsgnRepository? _charAsgnRepository;
        private ICharCodeRepository? _charCodeRepository;
        private IPriceListRepository? _priceListRepository;
        private IProductAmountRepository? _productAmountRepository;
        private ICharValRepository? _charValRepository;
        private IClientRepository? _clientRepository;
        private IOrdLineRepository? _ordLineRepository;
        private IOrdFicheRepository? _ordFicheRepository;
        private IDocumentNoRepository? _documentNoRepository;
        private IFirmParamRepository? _firmParamRepository;
        private ICompanyRepository? _companyRepository;
        private IUserRepository? _userRepository;
        private IRoleRepository? _roleRepository;
        private IUserRoleRepository? _userRoleRepository;
        public UnitofWork(RepositoryContext context)
        {
            this._context = context;
        }

        public IBankCardRepository BankCards => _bankCardRepository = _bankCardRepository ?? new BankCardRepository(_context);

        public IBrandCardRepository BrandCards => _brandCardRepository = _brandCardRepository ?? new BrandCardRepository(_context);
        public ICreditCardInstallmentRepository CreditCardInstallment => _creditCardInstallmentRepository = _creditCardInstallmentRepository ?? new CreditCardInstallmentRepository(_context);
        public IVirtualPosRepository VirtualPoses => _virtualPosRepository = _virtualPosRepository ?? new VirtualPosRepository(_context);
        public ICreditCardPrefixRepository CreditCardPrefixs => _creditCardPrefixRepository = _creditCardPrefixRepository ?? new CreditCardPrefixRepository(_context);
        public ICreditCardRepository CreditCards => _creditCardRepository = _creditCardRepository ?? new CreditCardRepository(_context);
        public IVirtualPosParameterRepository VirtualPosParameter => _virtualPosParameterRepository = _virtualPosParameterRepository ?? new VirtualPosParameterRepository(_context);

        public IPaymentRepository Payment => _paymentRepository = _paymentRepository ?? new PaymentRepository(_context);

        public ICategoryRepository Category => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public ICharSetRepository CharSet => _charsetRepository = _charsetRepository ?? new CharSetRepository(_context);

        public ICharAsgnRepository CharAsgn => _charAsgnRepository = _charAsgnRepository ?? new CharAsgnRepository(_context);

        public ICharCodeRepository CharCode => _charCodeRepository = _charCodeRepository ?? new CharCodeRepository(_context);

        public IPriceListRepository PriceList => _priceListRepository = _priceListRepository ?? new PriceListRepository(_context);

        public IProductAmountRepository ProductAmount => _productAmountRepository = _productAmountRepository ?? new ProductAmountRepository(_context);

        public ICharValRepository CharVal => _charValRepository = _charValRepository ?? new CharValRepository(_context);

        public IClientRepository Client => _clientRepository = _clientRepository ?? new ClientRepository(_context);

        public IOrdFicheRepository OrdFiche => _ordFicheRepository = _ordFicheRepository ?? new OrdFicheRepository(_context);

        public IOrdLineRepository OrdLine => _ordLineRepository = _ordLineRepository ?? new OrdLineRepository(_context);

        public IDocumentNoRepository DocumentNo => _documentNoRepository = _documentNoRepository ?? new DocumentNoRepository(_context);

        public IFirmParamRepository FirmParam => _firmParamRepository = _firmParamRepository ?? new FirmParamRepository(_context);

        public ICompanyRepository Company => _companyRepository = _companyRepository ?? new CompanyRepository(_context);

        public IUserRepository User => _userRepository = _userRepository ?? new UserRepository(_context);

        public IRoleRepository Role => _roleRepository = _roleRepository ?? new RoleRepository(_context);

        public IUserRoleRepository UserRole => _userRoleRepository = _userRoleRepository ?? new UserRoleRepository(_context);

        // Commit changes asynchronously
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
            
        }

        // Begin a transaction
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        // Commit a transaction asynchronously
        public async Task CommitTransactionAsync()
        {
            _context.SaveChanges();
            await _transaction.CommitAsync();
        }

        // Rollback a transaction asynchronously
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public IEnumerable<EntityEntry> ChangedEntries()
        {
            var changedEntries =  _context.ChangeTracker.Entries()
                .Where(x=>x.State != EntityState.Unchanged);
            return changedEntries;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public EntityState ChangedEntity<TEntity>(TEntity entity)
        {
            var test =  _context.Entry(entity);
            return test.State;
        }
    }
}
