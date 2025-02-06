
using Business.Abstract;
using Business.Concrete;
using CoreUI.Data;
using DataAccess.Abstract;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;


namespace PSS.Components.CartPanel
{
    public class CartService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IProductServices productServices;
        private readonly IOrderService orderService;
        private readonly IDocumentNoService documentNoService;
        private readonly IUnitofWork unitofWork;

        public OrdFiche _ordFiche;
        public Client Client { get; set; }
        public User User { get; set; }




        public CartService(ProtectedLocalStorage sessionStroge, IProductServices productServices, IOrderService orderService, IDocumentNoService documentNo, IUnitofWork unitofWork)
        {

            _localStorage = sessionStroge;

            this.productServices = productServices;
            this.orderService = orderService;
            this.documentNoService = documentNo;
            this.unitofWork = unitofWork;
        }



        private DocumentNo _documentNo;



        public EventHandler<CardEventArg> CardChanged;

      

        public async void AddFiche(string ClientCode, Guid UserId)
        {
            if (_ordFiche == null)
            {

                _ordFiche = new OrdFiche
                {
                    Id = Guid.NewGuid(),
                    Docode = "11",
                    Date_ = DateTime.Now,
                    Send = 0,
                    Active = true,
                    ClientCode = ClientCode,
                    FicheNo = await documentNoService.GetDocNo(11), //fiş numarası 
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    UpdateUser = UserId,
                    CreateUser = UserId,
                    UserId = UserId,
                    TrCode = 1,
                    CurrencyId = 1 //döviz kuru seçildiğinde değişecek
                };


                _ordFiche.Lines = new();
            }
        }
        public async void AddCart(Product product, double Amount, double Price, double Discount, Guid UserId)
        {
            AddFiche(Client.Code, UserId);

            OrdLine ordLine = _ordFiche.Lines?.FirstOrDefault(x => x.ProductId == product.Id);
            if (ordLine == null)
            {
                ordLine = new OrdLine
                {
                    Id = Guid.NewGuid(),
                    StockRef = product.LogicalRef,
                    Amount = Amount,
                    Price = Price,
                    Total = Amount * Price,
                    Product = product,
                    ProductId = product.Id,
                    UomRef = product.UomRef,
                    UsRef = product.UsRef,
                    AvailableStock = 0,
                    Discper = Discount,
                    LineType = 0,
                    LineNo = (short)(_ordFiche.Lines.Count + 1),  // Assuming Lines is always initialized
                    TrCode = 1,
                    Vat = product.SellVat ?? 0,
                    VatMatrah = Math.Round((Amount * Price - Discount) / (1 + (product.SellVat ?? 0) / 100), 2),
                    VatAmnt = Math.Round(((Amount * Price - Discount) * (product.SellVat ?? 0)) / 100, 2),
                    Unit = product.Unit,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateUser = UserId,
                    UpdateUser = UserId,
                    OrdFicheId = _ordFiche.Id,
                    Date_ = _ordFiche.Date_,
                    CurrencyId = 1
                };

                _ordFiche.Lines.Add(ordLine);
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
            }
            var s = unitofWork.ChangedEntity(ordLine);
            _ordFiche.GrossTotal = Math.Round(_ordFiche.Lines.Sum(x => x.Total), 2);
            _ordFiche.TotalDiscounted = Math.Round(_ordFiche.Lines.Sum(x => x.Distdisc), 2);
            _ordFiche.TotalVat = Math.Round(_ordFiche.Lines.Sum(x => x.VatAmnt), 2);
            _ordFiche.Total = Math.Round(_ordFiche.Lines.Sum(x => x.Total - x.Distdisc + x.VatAmnt), 2);

        }

        public async void ReadCart(Guid id)
        {
            _ordFiche = await orderService.GetOrderFiche(id);

        }

        public Task Save()
        {
            //documentNoService.Update(_documentNo);
            foreach (OrdLine line in _ordFiche.Lines)
            {
                var s = unitofWork.ChangedEntity(line);
            }
            return orderService.Save(_ordFiche);
        }

        public void RemoveCart(OrdLine ordLine)
        {
            var test = EntityState.Added.Equals(ordLine);
            if (!test)
            {
                _ordFiche.Lines?.Remove(ordLine);
            }
        }



      
    }

    public class CardEventArg
    {

    }
}
