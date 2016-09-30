using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Kenobi.Common.ErrorModel;
using Kenobi.Common.Errors.Hotel;
using Newtonsoft.Json;
using Kenobi.Common.Translator.Json;

namespace kenobi.TripsExtension.Filters
{
    public class ExceptionResponseFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var baseApplicationException = actionExecutedContext.Exception as BaseApplicationException;
            actionExecutedContext.Response = baseApplicationException != null
                ? GetBaseApplicationException(baseApplicationException) : GetInternalServerErrorAsResponse();
        }

        private static HttpResponseMessage GetBaseApplicationException(BaseApplicationException baseApplicationException)
        {
            return new HttpResponseMessage(baseApplicationException.HttpStatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(baseApplicationException.GetErrorInfo(),
                                        new ErrorInfoTranslator(), new InfoTranslator())),
            };
        }

        private static HttpResponseMessage GetInternalServerErrorAsResponse()
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new ErrorInfo(FaultCodes.Application, ErrorMessages.Application()),
                                            new ErrorInfoTranslator(), new InfoTranslator()))
            };
        }
    }
}