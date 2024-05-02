using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco13BlogSite.App_Code.Models.Pagination;
using Umbraco13BlogSite.App_Code.Models.ViewComponentModels;

namespace Umbraco13BlogSite.App_Code.ViewComponents
{
    [ViewComponent]
    public class BlogsViewComponent: ViewComponent
    {
        private readonly ILogger<BlogsViewComponent> _logger;
        private readonly UmbracoHelper _umbracoHelper;

        public BlogsViewComponent(ILogger<BlogsViewComponent> logger, UmbracoHelper umbracoHelper)
        {
            _logger = logger;
            _umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke(int? page=0)
        {
            List<BlogsView> blogsView = new();
            int pageSize = 1;   // ya mujhy ya batha raha haii ka Ek page pr mujhy kitnay Blogs post dhekhaynay haii aghr may eski 3 kerdoo to ya mujhy ek page pr 3 blog post dhekhayga 
            try
            {
                var content = _umbracoHelper?.ContentAtRoot()?
                    .FirstOrDefault(x => x.IsDocumentType("home"))?
                    .Children()?
                    .FirstOrDefault(x => x.IsVisible()
                    && x.IsDocumentType("blogs")) as Blogs;

                if (content == null) return View(blogsView);
                pageSize = content?.NumberOfBlogs?.Equals(0) ?? false ?
                                    pageSize :
                                    Convert.ToInt32(content?.NumberOfBlogs); // Aghr to page size zero haii ya nh diya hoya to PageSize ko 1 kerdo NH to joo client nay field may diya hoya hai uskoo 

                foreach (Blog item in content?.Children() ?? new List<Blog>())
                {
                    blogsView?.Add(new BlogsView
                    {
                        Title = item?.Name,
                        SubTitle = item?.SubTitle,
                        BlogUrl = item?.Url(),
                        CreatedBy = item?.CreatorName(),
                        AuthorUrl = item?.AuthorPageUrl.Url(),
                        CreatedDate = item?.CreateDate.ToString("D")
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While Processing {nameof(BlogsViewComponent)}");
            }


            return View(PaginatedList<BlogsView>.Create(blogsView.AsQueryable(),page??1,pageSize));  // If page is null or 0 then it will go to page 1
        }


    }
}
