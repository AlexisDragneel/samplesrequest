using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Services
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _accesor;

        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider, IServiceProvider serviceProvider,
            IHttpContextAccessor accessor) {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _accesor = accessor;
        }

        public async Task<string> RenderViewToStringAsync(string name, object model)
        {
            var actionContext = GetActionContext();
            var viewEngine = _razorViewEngine.FindView(actionContext,name,false);

            if (!viewEngine.Success)
            {
                throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", name));
            }

            var view = viewEngine.View;

            using (var output = new StringWriter())
            {
                var vieContext = new ViewContext(actionContext,
                    view,
                    new ViewDataDictionary<object>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                try
                {
                    await view.RenderAsync(vieContext);
                }
                catch(Exception ex)
                {

                }

                return output.ToString();
            }
        }

        private ActionContext GetActionContext()
        {

            var context = _accesor.HttpContext;
            context.RequestServices = _serviceProvider;
            return new ActionContext(context, new RouteData(), new ActionDescriptor());
        }
    }
}
