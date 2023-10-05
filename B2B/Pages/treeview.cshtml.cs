using B2B.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B2B.Pages
{
    public class treeviewModel : PageModel
    {
        public void OnGet()
        {
        }

        private List<TreeviewItem> _sampleData = new List<TreeviewItem>()
        {
            new TreeviewItem("Item 1", new List<TreeviewItem>()
              {
                new TreeviewItem("Item 1.1"),
                new TreeviewItem("Item 1.2")
            }),
            new TreeviewItem("Item 2"),
            new TreeviewItem("Item 3", new List<TreeviewItem>()
            {
                new TreeviewItem("Item 3.1"),
                new TreeviewItem("Item 3.2", new List<TreeviewItem>()
                {
                    new TreeviewItem("Item 3.2.1"),
                    new TreeviewItem("Item 3.2.2"),
                    new TreeviewItem("Item 3.2.3")
                }),
                new TreeviewItem("Item 3.3")
            }),
            new TreeviewItem("Item 4"),
            new TreeviewItem("Item 5", new List<TreeviewItem>()
            {
                new TreeviewItem("Item 5.1"),
                new TreeviewItem("Item 5.1"),
                new TreeviewItem("Item 5.1"),
                new TreeviewItem("Item 5.1"),
            })
        };
    }

   
}
