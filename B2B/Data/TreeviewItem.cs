namespace B2B.Data
{
    public class TreeviewItem
    {
        public string? Text { get; set; }
        public List<TreeviewItem>? Children { get; set; }
        public bool IsCollapsed { get; set;}
        public bool IsSelelected { get; set; }
        public bool HasChildren => Children != null && Children.Any();

        public TreeviewItem(string? text, List<TreeviewItem>? children = null, bool isCollapsed = false, bool isSelelected = false)
        {
            Text = text;
            Children = children;
            IsCollapsed = isCollapsed;
            IsSelelected = isSelelected;
        }
    }
}
