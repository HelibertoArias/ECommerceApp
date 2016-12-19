namespace ECommerceApp.ViewModels
{
    public class MenuItemViewModel
    { 

        public MenuItemViewModel(string icon, string pageName, string title)
        {
            this.Icon = icon;
            this.PageName = pageName;
            this.Title = title;
        }

        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }

    }
}
