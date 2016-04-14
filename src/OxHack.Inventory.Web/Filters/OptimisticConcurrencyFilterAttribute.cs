using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Filters
{
	public class OptimisticConcurrencyFilterAttribute : ActionFilterAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			//// only for PUT where if-match header contains a value
			//if (context.Request.Method.Method != HttpMethod.Put.Method ||
			//	context.Request.Headers.IfMatch == null ||
			//	context.Request.Headers.IfMatch.Count == 0)

			//	return;
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			throw new NotImplementedException();
		}
	}
}
