using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Eir.Web
{
    public class WPPluginInformationScraper
    {
        public async Task<PluginInformation> ScrapeInformation(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new ArgumentException("Invalid url provided", nameof(url));
            }
            var tags = await FetchMetaTags(url);
            var result = ExtractPluginInformation(tags);
            return result;
        }

        private async Task<IEnumerable<HtmlNode>> FetchMetaTags(string url)
        {
            var web = new HtmlWeb();

            var document = await web.LoadFromWebAsync(url);

            var metaNodes = document.DocumentNode.DescendantsAndSelf().Where(node => node.Name == "meta");

            return metaNodes;
        }

        private PluginInformation ExtractPluginInformation(IEnumerable<HtmlNode> nodes)
        {
            var result = new PluginInformation();
            foreach (var node in nodes)
            {
                var nameAttribute = node.Attributes.FirstOrDefault(attr => attr.Name == "name" || attr.Name == "property" || attr.Name == "itemprop");
                var contentAttribute = node.Attributes.FirstOrDefault(attr => attr.Name == "content");
                if (nameAttribute != null)
                {
                    switch (nameAttribute.Value)
                    {
                        case "og:title":
                            result.Name = contentAttribute.Value;
                            break;
                        case "og:description":
                            result.Description = contentAttribute.Value;
                            break;
                        case "og:url":
                            result.Url = contentAttribute.Value;
                            break;
                        case "softwareVersion":
                            result.Version = contentAttribute.Value;
                            break;
                    }
                }
            }

            return result;
        }
    }

    public class PluginInformation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Url { get; set; }
    }
}
