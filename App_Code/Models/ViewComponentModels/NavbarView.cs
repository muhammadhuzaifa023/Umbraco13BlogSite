namespace Umbraco13BlogSite.App_Code.Models.ViewComponentModels
{
    public class NavbarView
    {
        public string? SiteName { get; set; }
        public List<NavbarChild> navbarChildren  { get; set; } = new List<NavbarChild>();
      
    }

    public class NavbarChild
    {
        public string?  Name { get; set; }
        public string? Url { get; set; }


    }
}
