using Business.Abstract;
using Business.SingletonServices;
using Entity;
using PSS.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PSS.PdfTemplate
{
    public class OfferDocument : IDocument
    {
        private OrdFiche Model { get; }
        private Client Client { get; }
        private FirmParam FirmParam { get; }
        private FirmParameter FirmParamService { get; }


        public OfferDocument(OrderFormModel orderFormModel)
        {
            Model = orderFormModel.OrdFiche;
            FirmParam = orderFormModel.FirmParam;
            Client = orderFormModel.Client;

        }



        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;
        public void Compose(IDocumentContainer container)
        {


            container
           .Page(page =>
           {
               page.Margin(50);

               page.Header()
                   .Height(50)
                   .AlignMiddle()
                   .Text("Teklif Formu")
                   .SemiBold()
                   .FontSize(20)
                   .FontColor(Colors.Blue.Medium);
               page.Content()
                        .Padding(20)
                        .Column(column =>
                        {
                            column.Spacing(20);

                            // 1. Adres Bilgileri
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Spacing(5);
                                    col.Item().Text("Gönderen").Bold();
                                    //col.Item().Text(FirmParamService.ToString(1));
                                    //col.Item().Text(FirmParamService.ToString(2));
                                    //col.Item().Text(FirmParamService.ToString(3));
                                    //col.Item().Text(FirmParamService.ToString(4) + "/" + FirmParamService.ToString(5));
                                    //col.Item().Text(FirmParamService.ToString(19) + "/" + FirmParamService.ToString(20));

                                });

                                row.RelativeItem().Column(col =>
                                {
                                    col.Spacing(5);
                                    col.Item().Text("Alıcı").Bold().FontSize(10);
                                    col.Item().Text(Client.Code).FontSize(10);
                                    col.Item().Text(Client.Name).FontSize(10);
                                    col.Item().Text(Client.Address1).FontSize(10);
                                    col.Item().Text(Client.Address2).FontSize(10);
                                    col.Item().Text(Client.Town + "/" + Client.City).FontSize(10);
                                    col.Item().Text(Client.Phone1).FontSize(10);
                                    col.Item().Text(Client.MailAdress).FontSize(10);



                                });
                            });

                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2); // Ürün Adı
                                    columns.RelativeColumn(4); // Ürün Adı
                                    columns.RelativeColumn(1); // Miktar
                                    columns.RelativeColumn(2); // Birim Fiyat
                                    columns.RelativeColumn(2); // Toplam
                                });

                                // Başlıklar
                                table.Header(header =>
                                {
                                    header.Cell().Text("Ürün Kodu").Bold().FontSize(8);
                                    header.Cell().Text("Ürün Adı").Bold().FontSize(8);
                                    header.Cell().AlignRight().Text("Miktar").Bold().FontSize(8);
                                    header.Cell().AlignRight().Text("Birim Fiyat").Bold().FontSize(8);
                                    header.Cell().AlignRight().Text("Toplam").Bold().FontSize(8);
                                });

                                // Satırlar
                                foreach (var item in Model.Lines)
                                {
                                    table.Cell().Text(item.Product.Code).FontSize(8);
                                    table.Cell().Text(item.Product.Name).FontSize(8);
                                    table.Cell().AlignRight().Text(item.Amount.ToString()).FontSize(8);
                                    table.Cell().AlignRight().Text($"{item.Price:C}").FontSize(8);
                                    table.Cell().AlignRight().Text($"{item.Total:C}").FontSize(8);
                                }
                            });

                            // 3. Toplam Bilgileri Tablosu
                            column.Item().PaddingTop(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3); // Boş alan
                                    columns.RelativeColumn(4); // Etiket
                                    columns.RelativeColumn(3); // Değer
                                });


                                table.Cell().Text("");
                                table.Cell().AlignRight().Text("Ara Toplam:").Bold().FontSize(8);
                                table.Cell().AlignRight().Text($"{Model.GrossTotal:C}").FontSize(8);


                                table.Cell().Text("");
                                table.Cell().AlignRight().Text("KDV (%18):").Bold().FontSize(8);
                                table.Cell().AlignRight().Text($"{Model.TotalVat:C}").FontSize(8); 


                                table.Cell().Text("");
                                table.Cell().AlignRight().Text("Genel Toplam:").Bold().FontSize(8);
                                table.Cell().AlignRight().Text($"{Model.Total:C}").Bold().FontSize(8);
                            });

                            page.Footer()
                               .Height(30)
                               .AlignCenter()
                               .Text(x =>
                               {
                                   x.Span("Sayfa ").FontSize(8);
                                   x.CurrentPageNumber().FontSize(8);
                                   x.Span(" / ").FontSize(8);
                                   x.TotalPages().FontSize(8);
                               });

                        });

           });
        }
    }
}
