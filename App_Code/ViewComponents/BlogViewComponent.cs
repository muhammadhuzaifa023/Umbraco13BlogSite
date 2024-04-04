using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco13BlogSite.App_Code.Models.ViewComponentModels;

namespace Umbraco13BlogSite.App_Code.ViewComponents
{
    [ViewComponent]
    public class BlogViewComponent: ViewComponent
    {
        private readonly ILogger<BlogViewComponent> _logger;
        private readonly UmbracoHelper _umbracoHelper;

        public BlogViewComponent(ILogger<BlogViewComponent> logger, UmbracoHelper umbracoHelper)
        {
            _logger = logger;
            _umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke()
        {
            List<BlogView> blogView = new();
            try
            {
                var content = _umbracoHelper?.ContentAtRoot()?
                    .FirstOrDefault(x => x.IsDocumentType("home"))?
                    .Children()?
                    .FirstOrDefault(x => x.IsVisible()
                    && x.IsDocumentType("blog")) as Blog;

                if (content == null) return View(blogView);

                foreach (Blog item in content?.Children() ?? new List<Blog>())
                {
                    blogView?.Add(new BlogView
                    {
                        Title = item?.Name,
                        SubTitle = item?.SubTitle,
                        BlogUrl = item?.Url(),
                        CreatedBy = item?.CreatorName(),
                        //AuthorUrl = item?.AuthorPageUrl?.Url(),
                        CreatedDate = item?.CreateDate.ToString("D")
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While Processing {nameof(BlogViewComponent)}");
            }


            return View(blogView);
        }


    }
}
