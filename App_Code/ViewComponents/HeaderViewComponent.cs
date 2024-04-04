using Microsoft.AspNetCore.Mvc;
using Polly;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco13BlogSite.App_Code.Models.ViewComponentModels;

namespace Umbraco13BlogSite.App_Code.ViewComponents
{
    [ViewComponent]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILogger<HeaderViewComponent> _logger;
        private readonly IUmbracoContextAccessor _Context;
        public HeaderViewComponent(ILogger<HeaderViewComponent> logger, IUmbracoContextAccessor Context)
        {

            _logger = logger;
            _Context = Context;

        }

        public IViewComponentResult Invoke()
        {
            HeaderView headerView = new HeaderView();



            try
            {
                var context = _Context?.GetRequiredUmbracoContext()?.PublishedRequest?.PublishedContent;

                if (context is null) return View(headerView);
                
                headerView.Title = context?.Value<string>("title");
                headerView.SubTitle = context?.Value<string>("subTitle");
                headerView.Imageurl = context?.Value<IPublishedContent>("pageBanner")?.Url();
                headerView.IsBlog = context?.IsDocumentType("blog");
                headerView.CreatedBy = context?.CreatorName();
                headerView.AuthorUrl = context?.Value<IPublishedContent>("authorPageUrl")?.Url();
                headerView.CreatedDate = context?.CreateDate.ToString("D");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While Processing {nameof(HeaderViewComponent)}");

            }
            return View(headerView);
        }

    }
}
