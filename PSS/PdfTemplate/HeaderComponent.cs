using Entity;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Net;


namespace PSS.PdfTemplate
{
    public class HeaderComponent : IComponent
    {
        private string Title { get; }
        private ShipAdress Address { get; }
        public HeaderComponent(string Title, ShipAdress Adress)
        {

            Title = Title;
            Address = new ShipAdress();
        }
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(2);

                column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();
            });
        }
    }
}
