using Business.Abstract;

using Business.ValidationRules;
using Core.Abstract;
using Core.Concrete;

using Entity;

namespace B2B.Data
{
    public class CartService:IDisposable
    {
        private readonly IProductService productServices;
        private readonly IOrderService orderService;
        private readonly ICompanyService companyService;
        private readonly IUserService userService;
        private readonly IDocumentNoService documentNoService;
     

        public OrdFiche _ordFiche;
        public Company company { get; set; }
        public User user { get; set; }



        public CartService(IProductService productServices, IOrderService orderService, ICompanyService companyService, IUserService userService, IDocumentNoService documentNoService)
        {
            this.productServices = productServices;
            this.orderService = orderService;
            this.companyService = companyService;
            this.userService = userService;
            this.documentNoService = documentNoService;
          
        }




        public async Task<List<OrdLine>> ReadCart(Guid userId)
        {
            user = await userService.GetUser(userId);
            company = await companyService.Get(user.CompanyId ?? default);
            if (user != null && company != null)
            {
                _ordFiche = await orderService.GetOrderFiche(0, userId);
                if (_ordFiche == null)
                {
                    _ordFiche = new();
                    _ordFiche.Lines = new();
                    _ordFiche.Id = Guid.Empty;
                    _ordFiche.Docode = await documentNoService.GetDocNo(1);
                    _ordFiche.Date_ = DateTime.Now;
                    _ordFiche.Send = 0;
                    _ordFiche.Active = true;
                    _ordFiche.ClientCode = company.ProgramCode;
                    _ordFiche.FicheNo = _ordFiche.Docode;
                    _ordFiche.UpdateDate = DateTime.Now;
                    _ordFiche.CreateDate = DateTime.Now;
                    _ordFiche.UpdateUser = userId;
                    _ordFiche.CreateUser = userId;
                    _ordFiche.UserId = userId;
                    _ordFiche.TrCode = 1;
                    _ordFiche.CurrencyId = 1;
                    _ordFiche.Lines = new();
                }
                return _ordFiche.Lines;
            }
            else
                return null;


        }





        public async Task<Core.Abstract.IResult> AddUserCart(Guid productId, double Price, double Amount, double Discount, Guid userId)
        {
            user = await userService.GetUser(userId);

            company = await companyService.Get(user.CompanyId ?? default);
            if (company != null)
            {
                _ordFiche = await orderService.GetOrderFiche(0, userId);
                if (_ordFiche == null)
                {
                    _ordFiche = new();
                    _ordFiche.Lines = new();
                    _ordFiche.Id = Guid.Empty;
                    _ordFiche.Docode = await documentNoService.GetDocNo(1);
                    _ordFiche.Date_ = DateTime.Now;
                    _ordFiche.Send = 0;
                    _ordFiche.Active = true;
                    _ordFiche.ClientCode = company.ProgramCode;
                    _ordFiche.FicheNo = _ordFiche.Docode;
                    _ordFiche.UpdateDate = DateTime.Now;
                    _ordFiche.CreateDate = DateTime.Now;
                    _ordFiche.UpdateUser = userId;
                    _ordFiche.CreateUser = userId;
                    _ordFiche.UserId = userId;
                    _ordFiche.TrCode = 1;
                    _ordFiche.CurrencyId = 1; //döviz kuru seçildiğinde değişecek
                    _ordFiche.CompanyId = user.CompanyId ?? default;
                }
                OrdLine ordLine =  _ordFiche.Lines?.FirstOrDefault(x => x.ProductId == productId);
                if (ordLine == null)
                {
                    var product = await productServices.GetByGuid(productId);
                    ordLine = new();
                    ordLine.Id = Guid.NewGuid();
                    ordLine.StockRef = product.LogicalRef;
                    ordLine.Amount = Amount;
                    ordLine.Price = Price;
                    ordLine.Total = ordLine.Amount * ordLine.Price;
                    short lineno;
                    short.TryParse(_ordFiche.Lines?.Count.ToString(), out lineno);
                    ordLine.LineNo = (short)(lineno + (short)1);
                    ordLine.TrCode = 1;

                    ordLine.UomRef = product.UomRef;
                    ordLine.UsRef = product.UsRef;
                    ordLine.AvailableStock = 0;
                    ordLine.Discper = Discount;
                    ordLine.LineType = 0;
                    if (Discount > 0)
                    {
                        ordLine.Distdisc = ordLine.Total * ordLine.Discper / 100;
                    }
                    else
                    {
                        ordLine.Distdisc = 0;
                    }
                    ordLine.VatMatrah = Math.Round((ordLine.Total - ordLine.Distdisc), 2);
                    ordLine.Vat = product.SellVat ?? 0;
                    ordLine.VatAmnt = Math.Round((ordLine.VatMatrah * ordLine.Vat / 100), 2);
                    ordLine.Unit = product.Unit;

                    ordLine.CreateDate = DateTime.Now;
                    ordLine.UpdateDate = DateTime.Now;
                    ordLine.CreateUser = userId;
                    ordLine.UpdateUser = userId;
                    ordLine.OrdFicheId = _ordFiche.Id;
                    ordLine.ProductId = product.Id;
                    ordLine.Date_ = _ordFiche.Date_;
                    ordLine.CurrencyId = 1;
                    _ordFiche.Lines.Add(ordLine);
                    var valid = ordLine.Validation();
                    if (valid.Count == 0)
                    {
                        await orderService.AddLine(ordLine);
                    }
                    else
                    {
                        string msg = "";
                        foreach (var item in valid)
                        {
                            msg += item.ToString();
                        }

                        return new Result(ResultStatus.Error, msg);
                    }
                }
                else
                {
                    ordLine.Amount += Amount;
                    ordLine.Price = Price;
                    ordLine.Total = Math.Round((ordLine.Amount * ordLine.Price), 2);
                    if (Discount > 0)
                    {
                        ordLine.Distdisc = ordLine.Total * Discount / 100;
                    }
                    else
                    {
                        ordLine.Distdisc = 0;
                    }
                    ordLine.VatMatrah = Math.Round((ordLine.Total - ordLine.Distdisc), 2);
                    ordLine.VatAmnt = Math.Round((ordLine.VatMatrah * ordLine.Vat / 100), 2);
                    await orderService.UpdateLine(ordLine);
                    
                }
                CalculateFiche();
               
                await orderService.Save(_ordFiche);

                
            }
          return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
        }

        private void CalculateFiche()
        {
            _ordFiche.GrossTotal = Math.Round(_ordFiche.Lines.Sum(x => x.Total), 2);
            _ordFiche.TotalDiscounted = Math.Round(_ordFiche.Lines.Sum(x => x.Distdisc), 2);
            _ordFiche.TotalVat = Math.Round(_ordFiche.Lines.Sum(x => x.VatAmnt), 2);
            _ordFiche.Total = Math.Round(_ordFiche.Lines.Sum(x => x.Total - x.Distdisc + x.VatAmnt), 2);
            _ordFiche.UpdateDate = DateTime.Now;
            _ordFiche.UpdateUser = user.Id;
        }


        public async void RemoveCart(OrdLine ordLine)
        {
            if (ordLine.Id != Guid.Empty)
            {
                _ordFiche.Lines?.Remove(ordLine);
                await orderService.DeleteLine(ordLine);
                CalculateFiche();
                //fişdeki toplam değişikliği için
               await orderService.Save(_ordFiche);
            }
        }

        public void RemoveCartGuid(Guid pruductGuid)
        {

        }
        public async Task<Core.Abstract.IResult> OrderSend()
        {
            _ordFiche.Send = 1;
           return await orderService.Save(_ordFiche);
        }

        public void Dispose()
        {
           _ordFiche = null;
            company = null;
            user = null;
        }
    }
}
