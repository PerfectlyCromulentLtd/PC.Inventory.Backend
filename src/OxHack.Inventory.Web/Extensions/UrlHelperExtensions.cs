using Microsoft.AspNet.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Extensions
{
    public static class UrlHelperExtensions
    {
		public static string Content(this UrlHelper urlHelper, string path, bool asAbsolute = false)
		{
			var contentPath = urlHelper.Content(path);
			var url = new Uri(contentPath);

			return asAbsolute ? url.AbsoluteUri : path;
		}
	}
}
