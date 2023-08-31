using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mensajeria_Windows.Infrastructure.Attributes;

namespace Mensajeria_Windows.Infrastructure.Filters.AppInsights
{
    /// <summary>
    /// Filter to obtain request and repost types
    /// </summary>
    public class RequestLoggerFilter : IActionFilter
    {
        public void OnActionExecuted (ActionExecutedContext context)
        {
            var entity = context.Result as ObjectResult;
            var type = entity?.Value?.GetType();
            context.HttpContext.Items.Add(MagicStrings.AppInsights.ResponseBodyType, type);

        }
        /// <summary>
        /// Esto es para cuando se ejecute un controlador bindea los modelos
        /// Guarda en el contexto del requesto bodytype
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting (ActionExecutingContext context)
        {
            //devuelve el primer dato que encuentre o null
            var parameter = context.ActionDescriptor.Parameters
                .FirstOrDefault(x => x.BindingInfo.BindingSource.DisplayName == BindingSource.Body.DisplayName
                && x.BindingInfo.BindingSource.IsFromRequest);
            if (parameter != null)
            {
                //Se recoge el tipo
                context.HttpContext.Items.Add(MagicStrings.AppInsights.RequestBodyType, parameter.ParameterType);
            }

            //Seleccionar si el body va a ser seguido(trace)
            var traceRequestInfo = context.ActionDescriptor.EndpointMetadata.OfType<TraceRequestAttribute>().SingleOrDefault();

            var SkipTraceResponse = traceRequestInfo?.SkipTraceResponseBody ?? false;
            var SkipTraceRequest = traceRequestInfo?.SkipTraceRequestBody ?? false;

            context.HttpContext.Items.Add(MagicStrings.AppInsights.SkipTraceResponse, SkipTraceResponse);
            context.HttpContext.Items.Add(MagicStrings.AppInsights.SkipTraceRequest, SkipTraceRequest);

        }
    }
}
