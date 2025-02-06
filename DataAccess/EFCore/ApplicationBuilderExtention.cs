using DataAccess.Helpers.Database;
using Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;


namespace DataAccess.EFCore
{
    public static class ApplicationBuilderExtention
    {

        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            using (RepositoryContext context = scope.ServiceProvider.GetRequiredService<RepositoryContext>())
            {
                try
                {
                    Debug.WriteLine(context.Database.GetConnectionString());
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                //seed data
                SeedData(context);
            }
            return app;
        }


        private static void SeedData(RepositoryContext dataContext)
        {
            Guid userId = dataContext.Users.First().Id;
            if(!dataContext.FirmParams.Any())
            {
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 1,
                    Key = "FirmName",
                    Value = Encoding.UTF8.GetBytes("ülkü bilgisayar"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                    
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 2,
                    Key = "Address1",
                    Value = Encoding.UTF8.GetBytes("sahabiye mah"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 3,
                    Key = "Address2",
                    Value = Encoding.UTF8.GetBytes("otak sk. no: 21/4"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 4,
                    Key = "Town",
                    Value = Encoding.UTF8.GetBytes("kocasinan"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 5,
                    Key = "City",
                    Value = Encoding.UTF8.GetBytes("kayseri"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 6,
                    Key = "Country",
                    Value = Encoding.UTF8.GetBytes("türkiye"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 7,
                    Key = "MailAdress",
                    Value = Encoding.UTF8.GetBytes("murat@ulkubilgisayar.com"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 8,
                    Key = "LastSync",
                    Value = Encoding.UTF8.GetBytes(DateTime.Parse("01.01.1980").ToString()),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                }); 
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 9,
                    Key = "Vkn",
                    Value = Encoding.UTF8.GetBytes("9060441879"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 10,
                    Key = "DefaultPos",
                    Value = Encoding.UTF8.GetBytes(""),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 11,
                    Key = "SmtpServer",
                    Value = Encoding.UTF8.GetBytes(""),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 12,
                    Key = "SmtpPort",
                    Value = Encoding.UTF8.GetBytes("587"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 13,
                    Key = "SmtpSSL",
                    Value = Encoding.UTF8.GetBytes("True"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 14,
                    Key = "SmtpUserName",
                    Value = Encoding.UTF8.GetBytes(""),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 15,
                    Key = "SmtpPassword",
                    Value = Encoding.UTF8.GetBytes(""),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 16,
                    Key = "PublishMode",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 17,
                    Key = "ViewMode",
                    Value = Encoding.UTF8.GetBytes("0"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 18,
                    Key = "SyncTime",
                    Value = Encoding.UTF8.GetBytes("5"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 19,
                    Key = "Phone1",
                    Value = Encoding.UTF8.GetBytes("0352 221 36 76"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 20,
                    Key = "Phone2",
                    Value = Encoding.UTF8.GetBytes("0352 231 83 83"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 21,
                    Key = "Mappath",
                    Value = Encoding.UTF8.GetBytes("https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3112.6729993258527!2d35.485860074704455!3d38.72531597176134!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x152b1367dc345b17%3A0x657995ad0d27d9f9!2zw5xsxLfDvCB5YXrEsWzEsW0gYmlsZ2lzYXlhciBsdGQuIMWedGku!5e0!3m2!1str!2str!4v1701262406643!5m2!1str!2str"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 22,
                    Key = "ResetSync",
                    Value = Encoding.UTF8.GetBytes("0"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 23,
                    Key = "StockAmountShow",
                    Value = Encoding.UTF8.GetBytes("1"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 24,
                    Key = "StockWarningAmount",
                    Value = Encoding.UTF8.GetBytes("5"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 25,
                    Key = "ProductPageCount",
                    Value = Encoding.UTF8.GetBytes("10"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 26,
                    Key = "RiskTakibi",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 27,
                    Key = "LoadImage",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 28,
                    Key = "ShowProducerCode",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 29,
                    Key = "ShowGroupCode",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 30,
                    Key = "ShowSpeCode",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 31,
                    Key = "ShowSpeCode2",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 32,
                    Key = "ShowSpeCode3",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 33,
                    Key = "ShowSpeCode4",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No =34,
                    Key = "ShowSpeCode5",
                    Value = Encoding.UTF8.GetBytes("False"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 35,
                    Key = "RetailClientCode",
                    Value = Encoding.UTF8.GetBytes("120.00.0001"),
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
            }
            if (!dataContext.CardBrands.Any())
            {
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 0,
                    Name = "Standart",
                    
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 2,
                    Name = "Axess",
                 
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 3,
                    Name = "Bankkart",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 4,
                    Name = "Bonus",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 5,
                    Name = "CardFinans",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 6,
                    Name = "Maximum",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,  
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 7,
                    Name = "Miles&Smiles",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 9,
                    Name = "Sağlam Kart",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 10,
                    Name = "World",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 11,
                    Name = "Bank24",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 12,
                    Name = "Paraf",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 13,
                    Name = "Hasat Kart",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 14,
                    Name = "Üreten Kart",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 15,
                    Name = "Shop&Fly",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 16,
                    Name = "Wings",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 17,
                    Name = "Neo",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 18,
                    Name = "Tosla",
                    CreateUser = userId,
                    CreateDate = DateTime.Now,
                    UpdateUser = userId,
                    UpdateDate = DateTime.Now,
                });
            }
            if (!dataContext.BankCards.Any())
            {
                IOrderedEnumerable<BankNames> bankNames = Enum.GetValues(typeof(BankNames)).Cast<BankNames>().OrderBy(b => b.GetDisplayName());
                foreach (BankNames bankName in bankNames)
                {
                    //skip if exists
                    if (dataContext.BankCards.Any(b => b.SystemName.Equals(bankName)))
                    {
                        continue;
                    }

                    dataContext.BankCards.Add(new BankCard
                    {
                        LogoPath = $"/media/banks/{bankName.ToString().ToLower()}.jpg",
                        Name = bankName.GetDisplayName(),
                        SystemName = bankName.ToString(),
                        BankCode = (int)bankName,
                        CreateUser = userId,
                        CreateDate = DateTime.Now,
                        UpdateUser = userId,
                        UpdateDate = DateTime.Now,
                        Active = true,
                       
                     

                    });

                }
                dataContext.SaveChanges();
                
            }
                            
            if(!dataContext.CreditCardPrefixes.Any())
            {
                using (StreamReader sreader = new StreamReader(@"./wwwroot/banklist.json"))
                {
                    List<CardPrefix> items = JsonSerializer.Deserialize<List<CardPrefix>>(sreader.ReadToEnd());
                    foreach (var item in items)
                    {
                        var bankCard = dataContext.BankCards.FirstOrDefault(x => x.BankCode == Convert.ToInt32(item.eftCode));
                        if (bankCard != null)
                        {
                           CreditCard creditcard = dataContext.CreditCards.FirstOrDefault(x => x.BankCardId == bankCard.Id && x.BrandCode == Convert.ToInt32(item.brand) && x.isBusiness == item.isBusinessCard);
                            if (creditcard == null)
                            {

                                string bname = bankCard.Name+" ";
                                bname += item.brandName +" ";
                                bname += item.isBusinessCard ? "Business" : "";
                                creditcard = dataContext.CreditCards.Add(new CreditCard
                                {
                                    BankCardId = bankCard.Id,
                                    Name = bname,
                                    Active = true,
                                    ManufacturerCard = false,
                                    CampaignCard = false,
                                    Deleted = false,
                                    CreateDate = DateTime.Now,
                                    UpdateDate = DateTime.Now,
                                    BrandCode = Convert.ToInt32(item.brand),
                                    isBusiness = item.isBusinessCard,
                                    CardBrandId = dataContext.CardBrands.FirstOrDefault(x => x.Code == Convert.ToInt32(item.brand)).Id

                                }).Entity;
                                dataContext.SaveChanges();
                            }
                            dataContext.CreditCardPrefixes.Add(new CreditCardPrefix
                            {
                                Prefix = item.prefixNo.ToString(),
                                Active = true,
                                Deleted = false,
                                CreditCardId = creditcard.Id,
                                BankCode = Convert.ToInt32(item.eftCode),
                                BrandCode = Convert.ToInt32(item.brand),
                                isInstallment = item.isInstallment,
                                Business = item.isBusinessCard,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                            });
                        }
                    }
                    dataContext.SaveChanges();
                }
            }
        }
    }
}
