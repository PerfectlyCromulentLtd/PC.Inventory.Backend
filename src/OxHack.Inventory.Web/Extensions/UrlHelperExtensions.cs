using Microsoft.AspNetCore.Mvc.Routing;
using System;

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
