using DataAccess.Helpers.Database;
using Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            if(!dataContext.FirmParams.Any())
            {
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 1,
                    Key = "FirmName",
                    Value = Encoding.UTF8.GetBytes("ülkü bilgisayar")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 2,
                    Key = "Address1",
                    Value = Encoding.UTF8.GetBytes("sahabiye mah")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 3,
                    Key = "Address2",
                    Value = Encoding.UTF8.GetBytes("otak sk. no: 21/4")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 4,
                    Key = "Town",
                    Value = Encoding.UTF8.GetBytes("kocasinan")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 5,
                    Key = "City",
                    Value = Encoding.UTF8.GetBytes("kayseri")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 6,
                    Key = "Country",
                    Value = Encoding.UTF8.GetBytes("türkiye")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 7,
                    Key = "MailAdress",
                    Value = Encoding.UTF8.GetBytes("murat@ulkubilgisayar.com")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 8,
                    Key = "LastSync",
                    Value = Encoding.UTF8.GetBytes(DateTime.Parse("01.01.1980").ToString())
                }); 
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 9,
                    Key = "Vkn",
                    Value = Encoding.UTF8.GetBytes("9060441879")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 10,
                    Key = "DefaultPos",
                    Value = Encoding.UTF8.GetBytes("")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 11,
                    Key = "SmtpServer",
                    Value = Encoding.UTF8.GetBytes("")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 12,
                    Key = "SmtpPort",
                    Value = Encoding.UTF8.GetBytes("587")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 13,
                    Key = "SmtpSSL",
                    Value = Encoding.UTF8.GetBytes("True")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 14,
                    Key = "SmtpUserName",
                    Value = Encoding.UTF8.GetBytes("")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 15,
                    Key = "SmtpPassword",
                    Value = Encoding.UTF8.GetBytes("")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 16,
                    Key = "PublishMode",
                    Value = Encoding.UTF8.GetBytes("False")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 17,
                    Key = "ViewMode",
                    Value = Encoding.UTF8.GetBytes("0")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 18,
                    Key = "SyncTime",
                    Value = Encoding.UTF8.GetBytes("5")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 19,
                    Key = "Phone1",
                    Value = Encoding.UTF8.GetBytes("0352 221 36 76")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 20,
                    Key = "Phone2",
                    Value = Encoding.UTF8.GetBytes("0352 231 83 83")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 21,
                    Key = "Mappath",
                    Value = Encoding.UTF8.GetBytes("https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3112.6729993258527!2d35.485860074704455!3d38.72531597176134!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x152b1367dc345b17%3A0x657995ad0d27d9f9!2zw5xsxLfDvCB5YXrEsWzEsW0gYmlsZ2lzYXlhciBsdGQuIMWedGku!5e0!3m2!1str!2str!4v1701262406643!5m2!1str!2str")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 22,
                    Key = "ResetSync",
                    Value = Encoding.UTF8.GetBytes("0")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 23,
                    Key = "StockAmountShow",
                    Value = Encoding.UTF8.GetBytes("1")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 24,
                    Key = "StockWarningAmount",
                    Value = Encoding.UTF8.GetBytes("5")
                });
                dataContext.FirmParams.Add(new FirmParam
                {
                    Id = Guid.NewGuid(),
                    No = 25,
                    Key = "ProductPageCount",
                    Value = Encoding.UTF8.GetBytes("10")
                });
            }
            if (!dataContext.CardBrands.Any())
            {
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 0,
                    Name = "Standart",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 2,
                    Name = "Axess",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 3,
                    Name = "Bankkart",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 4,
                    Name = "Bonus",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 5,
                    Name = "CardFinans",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 6,
                    Name = "Maximum",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 7,
                    Name = "Miles&Smiles",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 9,
                    Name = "Sağlam Kart",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 10,
                    Name = "World",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 11,
                    Name = "Bank24",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 12,
                    Name = "Paraf",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
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
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 15,
                    Name = "Shop&Fly",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 16,
                    Name = "Wings",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 17,
                    Name = "Neo",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                dataContext.CardBrands.Add(new CardBrand
                {
                    Id = Guid.NewGuid(),
                    Code = 18,
                    Name = "Tosla",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
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
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Active = true

                    });

                }
            }
            if (!dataContext.BankCards.Any())
            {
                using (StreamReader sreader = new StreamReader(@"./wwwroot/banklist.json"))
                {
                    string jsonfile = sreader.ReadToEnd();
                    List<CardPrefix> items = JsonSerializer.Deserialize<List<CardPrefix>>(jsonfile);
                    foreach (var item in items)
                    {
                        var bankCard = dataContext.BankCards.FirstOrDefault(x => x.BankCode == Convert.ToInt32(item.eftCode));
                        if (bankCard == null)
                        {
                            bankCard = dataContext.BankCards.Add(new BankCard
                            {
                                Name = item.bankName,
                                SystemName = item.bankName.Trim(),
                                BankCode = Convert.ToInt32(item.eftCode),
                                LogoPath = "",
                                UseCommonPaymentPage = false,
                                Active = true,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,

                            }).Entity;
                            dataContext.SaveChanges();
                        }

                        CreditCard creditcard = dataContext.CreditCards.FirstOrDefault(x => x.BankCardId == bankCard.Id && x.BrandCode == Convert.ToInt32(item.brand));
                        if (creditcard == null)
                        {
                            creditcard = dataContext.CreditCards.Add(new CreditCard
                            {
                                BankCardId = bankCard.Id,
                                Name = item.brandName ?? "Default",
                                Active = true,
                                ManufacturerCard = false,
                                CampaignCard = false,
                                Deleted = false,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                BrandCode = Convert.ToInt32(item.brand),
                                CardBrandId = dataContext.CardBrands.FirstOrDefault(x => x.Code == Convert.ToInt32(item.brand)).Id

                            }).Entity;
                            dataContext.SaveChanges();
                        }
                        if (dataContext.CreditCards.Any(x => x.BankCardId == ((BankCard)dataContext.BankCards.FirstOrDefault(x => x.BankCode == Convert.ToInt32(item.eftCode))).Id))
                        {
                            dataContext.CreditCardPrefixes.Add(new CreditCardPrefix
                            {
                                Prefix = item.prefixNo.ToString(),
                                Active = true,
                                Deleted = false,
                                CreditCardId = creditcard.Id,
                                BankCode = Convert.ToInt32(item.eftCode),
                                BrandCode = Convert.ToInt32(item.brand),
                                isInstallment = item.avoidAuthInstall,
                                Business = item.isBusinessCard,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                            });
                            dataContext.SaveChanges();
                        }



                    }
                }
                dataContext.SaveChanges();
            }
            if(!dataContext.CreditCardPrefixes.Any())
            {
                using (StreamReader sreader = new StreamReader(@"./banklist.json"))
                {
                    List<CardPrefix> items = JsonSerializer.Deserialize<List<CardPrefix>>(sreader.ReadToEnd());
                    foreach (var item in items)
                    {
                        var bankCard = dataContext.BankCards.FirstOrDefault(x=>x.BankCode == Convert.ToInt32(item.eftCode));
                        if(bankCard == null)
                        {
                            bankCard = dataContext.BankCards.Add(new BankCard
                            {
                                Id = Guid.NewGuid(),
                                Name = item.bankName,
                                SystemName = item.bankName.Trim(),
                                BankCode = Convert.ToInt32(item.eftCode),
                                LogoPath ="",
                                UseCommonPaymentPage=false,
                                Active = true,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                            }).Entity;
                            dataContext.SaveChanges();
                        }
                        CreditCard creditCard = dataContext.CreditCards.FirstOrDefault(
                            x => x.BankCardId == bankCard.Id && x.BrandCode == Convert.ToInt32(item.brand)
                            ); 
                        if(creditCard == null)
                        {

                            creditCard = dataContext.CreditCards.Add(new CreditCard
                            {
                                Id = Guid.NewGuid(),
                                BankCardId = bankCard.Id,
                                Name = item.brandName ?? "Default",
                                Active = true,
                                ManufacturerCard = false,
                                CampaignCard = false,
                                Deleted = false,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                BrandCode = Convert.ToInt32(item.brand)

                            }).Entity;
                            dataContext.SaveChanges();
                        }
                        if (dataContext.CreditCards.Any(x => x.BankCardId == ((BankCard)dataContext.BankCards.FirstOrDefault(x => x.BankCode == Convert.ToInt32(item.eftCode))).Id))
                        {
                            dataContext.CreditCardPrefixes.Add(new CreditCardPrefix
                            {
                                Prefix = item.prefixNo.ToString(),
                                Active = true,
                                Deleted = false,
                                CreditCardId = creditCard.Id,
                                BankCode = Convert.ToInt32(item.eftCode),
                                BrandCode = Convert.ToInt32(item.brand),
                                isInstallment = item.avoidAuthInstall,
                                Business = item.isBusinessCard,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                            });
                            dataContext.SaveChanges();
                        }
                        }
                }
            }
        }
    }
}
