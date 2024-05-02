namespace Umbraco13BlogSite.App_Code.Models.ViewComponentModels
{
    public class FooterView
    {
        // Social Media + Media link 

        public List<FooterLink> FooterLinks { get; set; } = new();


        public List<SocialMedia> socialMedias { get; set; } = new ();
    }

    public class FooterLink
    {
        public string?  Name { get; set; }
        public string? Url { get; set; }
        public string? Target { get; set; }

    }

    public class SocialMedia
    {
        public string? Icon { get; set; }

        public string? SocialUrl { get; set; }
        public string? Target { get; set; }


    }
}
