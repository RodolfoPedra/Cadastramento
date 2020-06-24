using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cadastramento.ModelData.Logic.ModelBinder;

namespace Cadastramento.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(int), new IntModelBinder());
            ModelBinders.Binders.Add(typeof(int?), new IntModelBinder());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var options = new Util.DataTables.DataTables.AspNet.Mvc5.Options()
                .EnableRequestAdditionalParameters()
                .EnableResponseAdditionalParameters();

            var binder = new Util.DataTables.DataTables.AspNet.Mvc5.ModelBinder();
            binder.ParseAdditionalParameters = Parser;

            // DataTables.AspNet registration with default options.
            Util.DataTables.DataTables.AspNet.Mvc5.Configuration.RegisterDataTables(options, binder);
        }

        public static IDictionary<string, object> Parser(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var httpMethod = controllerContext.HttpContext.Request.HttpMethod;
            var collection = httpMethod.ToUpper().Equals("POST") ? controllerContext.HttpContext.Request.Form : controllerContext.HttpContext.Request.QueryString;
            var modelKeys = collection.AllKeys.Where(m => !m.StartsWith("columns") && !m.StartsWith("order") && !m.StartsWith("search") && m != "draw" && m != "start" && m != "length" && m != "_"); //Cache bust
            var dictionary = new Dictionary<string, object>();
            foreach (string key in modelKeys)
            {
                var value = bindingContext.ValueProvider.GetValue(key).AttemptedValue;
                if (value.Length > 0)
                    dictionary.Add(key, value);
            }
            return dictionary;
        }
    }
}
