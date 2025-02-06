namespace CoreUI.Data
{
    public class MenuItemModel
    {
        public int id { get; set; }
        public int parent { get; set; } = default!;
        public bool isCollapsed { get; set; }
        public string menuName { get; set; } = default!;
        public List<MenuItemModel> items { get; set; } = default!;
        public bool isSelected { get; set; }
    }
}
