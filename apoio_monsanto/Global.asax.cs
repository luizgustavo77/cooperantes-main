using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;


namespace apoio_monsanto
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(System.Web.Routing.RouteTable.Routes);

        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }

        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            // not found
            routes.MapPageRoute("error",
            "index/404", "~/error404.aspx");

            // error
            routes.MapPageRoute("errors",
            "index/error", "~/error.aspx");

            // dashboard
            routes.MapPageRoute("dashboard",
            "dashboard",
            "~/dashboard.aspx");

            // default
            routes.MapPageRoute("default",
            "default",
            "~/default.aspx");

            // register
            routes.MapPageRoute("register",
            "register",
            "~/register.aspx");

            // contract
            routes.MapPageRoute("contract",
            "contract",
            "~/contract.aspx");

            // new contract
            routes.MapPageRoute("process",
            "process",
            "~/newcontract.aspx");

            // profile
            routes.MapPageRoute("profile",
            "profile",
            "~/profile.aspx");

            // admin
            routes.MapPageRoute("admin",
            "admin",
            "~/admin.aspx");

            // logs
            routes.MapPageRoute("logs",
            "logs",
            "~/logs.aspx");

            // report
            routes.MapPageRoute("report",
            "report",
            "~/report.aspx");

            // delegation
            routes.MapPageRoute("delegation",
            "delegation",
            "~/delegation.aspx");

            // criteria
            routes.MapPageRoute("criteria",
            "criteria",
            "~/criteria.aspx");

            // apoio_flc
            routes.MapPageRoute("apoio_flc",
            "apoio_flc",
            "~/apoio_flc/default.aspx");

            // region
            routes.MapPageRoute("region",
            "region",
            "~/region.aspx");

            // safra
            routes.MapPageRoute("safra",
            "safra",
            "~/safra.aspx");

            // ***** COOPERANTES **** //
            // contract
            routes.MapPageRoute("cooperantes/contract",
            "cooperantes/contract",
            "~/cooperantes/contract.aspx");

            // new contract
            routes.MapPageRoute("cooperantes/process",
            "cooperantes/process",
            "~/cooperantes/newcontract.aspx");

            // register
            routes.MapPageRoute("cooperantes/register",
            "cooperantes/register",
            "~/cooperantes/register.aspx");

            // report
            routes.MapPageRoute("cooperantes/report",
            "cooperantes/report",
            "~/cooperantes/report.aspx");

            // kpi
            routes.MapPageRoute("cooperantes/kpi",
            "cooperantes/kpi",
            "~/cooperantes/kpi.aspx");

            // admin
            routes.MapPageRoute("cooperantes/admin",
            "cooperantes/admin",
            "~/cooperantes/admin.aspx");

            // admin
            routes.MapPageRoute("cooperantes/unidades",
            "cooperantes/unidades",
            "~/cooperantes/unidades.aspx");

            // profile
            routes.MapPageRoute("cooperantes/profile",
            "cooperantes/profile",
            "~/cooperantes/profile.aspx");

            // contact
            routes.MapPageRoute("cooperantes/contact",
            "cooperantes/contact",
            "~/cooperantes/contact.aspx");

            // contact
            routes.MapPageRoute("cooperantes/tipos",
            "cooperantes/tipos",
            "~/cooperantes/type_process.aspx");

            // **** records **** //
            // contract
            routes.MapPageRoute("records/contract",
            "records/contract",
            "~/records/contract.aspx");

            // new contract
            routes.MapPageRoute("records/process",
            "records/process",
            "~/records/newcontract.aspx");

            // register
            routes.MapPageRoute("records/register",
            "records/register",
            "~/records/register.aspx");

            // general
            routes.MapPageRoute("records/general",
            "records/general",
            "~/records/form_general.aspx");

            // report
            routes.MapPageRoute("records/report",
            "records/report",
            "~/records/report.aspx");

            // **** gla **** //
            // contract
            routes.MapPageRoute("glaapoio/contract",
            "glaapoio/contract",
            "~/glaapoio/contract.aspx");

            // new contract
            routes.MapPageRoute("glaapoio/process",
            "glaapoio/process",
            "~/glaapoio/newcontract.aspx");

            // register
            routes.MapPageRoute("glaapoio/register",
            "glaapoio/register",
            "~/glaapoio/register.aspx");

            // general
            routes.MapPageRoute("glaapoio/general",
            "glaapoio/general",
            "~/glaapoio/form_general.aspx");

            // report
            routes.MapPageRoute("glaapoio/report",
            "glaapoio/report",
            "~/glaapoio/report.aspx");
        }
    }
}