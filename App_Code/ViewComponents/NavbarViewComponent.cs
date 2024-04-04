using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco13BlogSite.App_Code.Models.ViewComponentModels;

namespace Umbraco13BlogSite.App_Code.ViewComponents
{
    [ViewComponent]
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ILogger<NavbarViewComponent> _logger;
        private readonly UmbracoHelper _umbracoHelper;  // Find the Root of that content page is another method we can also use  IUmbracoContextAccessor _Context;  here
        public NavbarViewComponent(ILogger<NavbarViewComponent> logger, UmbracoHelper umbracoHelper)
        {
            _logger = logger;
            _umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke()
        {
            NavbarView navbarView = new NavbarView();

            try
            {
                var homepage = _umbracoHelper?.ContentAtRoot()?.FirstOrDefault(x=>x.IsDocumentType("home") && x.IsVisible()) as Home;   // Here x.IsVisible() we used to Documnet is published 
                if (homepage == null) return View(navbarView);
                navbarView.SiteName = homepage?.SiteName;
                foreach(var item in  homepage.Children) 
                {
                    if (item?.Value<bool>("displayOnNavbar") ?? false)
                    {
                        navbarView.navbarChildren.Add(
                        new NavbarChild
                        {
                            Name = item?.Name,
                            Url = item?.Url()
                        }


                        );

                    }

                    
                
                
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While Processing {nameof(NavbarViewComponent)}");
            }
            return View(navbarView);
        }


    }
}
