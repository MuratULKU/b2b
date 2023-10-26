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
using System.Threading.Tasks;

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
            dataContext.SaveChanges();
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
        }
    }
}
