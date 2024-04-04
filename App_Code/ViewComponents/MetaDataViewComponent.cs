using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco13BlogSite.App_Code.Models.ViewComponentModels;

namespace Umbraco13BlogSite.App_Code.ViewComponents
{
    [ViewComponent]
    public class MetaDataViewComponent : ViewComponent
    {
        private readonly ILogger<MetaDataViewComponent> _logger;
        private readonly IUmbracoContextAccessor _Context;

        public MetaDataViewComponent(ILogger<MetaDataViewComponent> logger, IUmbracoContextAccessor Context)
        {

            _logger = logger;
            _Context = Context;


        }
        public IViewComponentResult Invoke()
        {

            MetaDataView metaData = new MetaDataView();
            try
            {
                var context = _Context?.GetRequiredUmbracoContext()?.PublishedRequest?.PublishedContent;
                if (context == null) return View(metaData);
                metaData.Author = context?.Value<string>("author");
                metaData.Description = context?.Value<string>("description");
                metaData.Title = context?.Value<string>("title");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While Processing {nameof(MetaDataViewComponent)}");

            }
            return View(metaData);
        }


    }
}
