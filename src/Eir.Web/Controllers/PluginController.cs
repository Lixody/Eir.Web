using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Eir.Web.Controllers
{
    [Route("api/plugins/wp")]
    public class PluginController : Controller
    {
        [HttpGet("{plugin}")]
        public async Task<IActionResult> GetPluginByName(string plugin)
        {
            var scraper = new WPPluginInformationScraper();

            try
            {
                var url = string.Format("https://wordpress.org/plugins/{0}/", plugin);
                var result = await scraper.ScrapeInformation(url);

                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}