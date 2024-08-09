using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class mon : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["namelogin"] == null && Session["maillogin"] == null || Session["cooperantes"].ToString() == "S")
            {
                Session.Clear();
                Response.Redirect("/default?logoff=true");
            }
            else
            {
                nameuser.InnerText = Session["namelogin"].ToString();
            }

            string menu = @"    <li id='crit' runat='server'><a href='/motivoreprova'>&nbsp;&nbsp;<span class='font11'>Motivos de Reprovação</span></a></li>
                                <li id='crit' runat='server'><a href='/criteria'>&nbsp;&nbsp;<span class='font11'>Critérios de Avaliação</span></a></li>
                                <li id='reg' runat='server'><a href='/region'>&nbsp;&nbsp;<span class='font11'>Regionais</span></a></li>
                                <li id='dele' class='dele' runat='server'><a href='/delegation'>&nbsp;&nbsp;<span class='font11'>Delegação de Permissão</span></a></li>
                                <li id='logs' runat='server'><a href='/logs'>&nbsp;&nbsp;<span class='font11'>Consulta de Logs</span></a></li>
                                <li id='saf' runat='server'><a href='/safra'>&nbsp;&nbsp;<span class='font11'>Safra</span></a></li>";

            //<li id='rela' runat='server'><a href='/report'>&nbsp;&nbsp;<span class='font11'>Relatório</span></a></li>           

            if (Convert.ToInt64(Session["typelogin"]) == 1)
            {
                liAdmin.Visible = true;
                menuAdmin.InnerHtml = menu;
            }
            SetActiveNavLink();
        }

        protected void logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session["typeGLA"] = "";
            Response.Redirect("/default?exit=true");
        }

        private void SetActiveNavLink()
        {
            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("register"))
            {
                cad.Attributes.Add("class", "active");

            }
            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("contract"))
            {
                con.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("profile"))
            {
                userbut.Attributes["class"] += " open";
                con.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("logs"))
            {
                ope.Attributes.Add("class", "active");
                
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("criteria"))
            {
                ope.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("motivoreprova"))
            {
                ope.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("delegation"))
            {
                ope.Attributes.Add("class", "active");
            }

            if(HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("region"))
            {
                ope.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("safra"))
            {
                ope.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("report"))
            {
                rel.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("admin"))
            {
                if (Request.QueryString["type"] != null)
                {
                    cad.Attributes.Add("class", "active");
                }

            }

        }
    }
}