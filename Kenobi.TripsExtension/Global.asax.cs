using System;
using System.Web;
using System.Web.Http;
using Kenobi.TripsExtension.Core.Infrastructure;

namespace kenobi.TripsExtension
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exceptionHandler = new LogHandler();
            var ex = Server.GetLastError();
            exceptionHandler.LogException(ex);
        }
    }
}