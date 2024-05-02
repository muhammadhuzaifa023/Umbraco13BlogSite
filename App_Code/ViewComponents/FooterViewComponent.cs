using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco13BlogSite.App_Code.Models.ViewComponentModels;

namespace Umbraco13BlogSite.App_Code.ViewComponents
{
    [ViewComponent]
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILogger<FooterViewComponent> _logger;
        //private readonly IUmbracoContextAccessor _Context;
        private readonly UmbracoHelper _umbracoHelper;  // Find the Root of that content page is another method we can also use  IUmbracoContextAccessor _Context;  here

        public FooterViewComponent(ILogger<FooterViewComponent> logger, UmbracoHelper umbracoHelper)
        {

            _logger = logger;
            _umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke()
        {
            FooterView footerView = new FooterView();
            try
            {


                var homepage = _umbracoHelper?.ContentAtRoot()?.FirstOrDefault(x => x.IsDocumentType("home") && x.IsVisible()) as Home;   // Here x.IsVisible() we used to Documnet is published 
                if (homepage == null) return View(footerView);
                foreach (var item in homepage.FooterLink)
                {
                    footerView?.FooterLinks?.Add(
                        new FooterLink
                        {

                            Name = item?.Name,
                            Url = item?.Url,
                            Target = item?.Target,

                        });

                }
                // Blocklist 
                foreach (var item in homepage.SocialMediaPicker)
                {
                    var socialMediaElement = item.Content as SocialMediaElement;
                    footerView?.socialMedias?.Add(
                         new SocialMedia
                         {
                             Icon = socialMediaElement?.Icon.ToString(),
                             SocialUrl = socialMediaElement?.SocialMediaUrl,
                             Target = socialMediaElement?.Target
                         });



                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While Processing {nameof(FooterViewComponent)}");

            }
            return View(footerView);
        }

    }
}
