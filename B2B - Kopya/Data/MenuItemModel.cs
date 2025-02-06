
namespace B2B.Data
{
    public class MenuItemModel
    {
        public int id { get; set; }
        public int parent { get; set; }
        public bool isCollapsed { get; set; }
        public string menuName { get; set; }
        public List<MenuItemModel> items { get; set; }
        public bool isSelected {get;set;}
    }
}
