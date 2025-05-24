using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;




namespace UserForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("Tu Nombre");

            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}