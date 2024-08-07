using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto
{
    public partial class gla : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            coopcom com = new coopcom();
            if (Session["namelogin"] == null && Session["maillogin"] == null)
            {
                Session.Clear();
                Response.Redirect("/default?logoff=true");
            }
            else
            {
                if(Session["cooperantes"].ToString() == "S" && ! HttpContext.Current.Request.Url.AbsolutePath.Contains("cooperantes"))
                {
                    Session.Clear();
                    Response.Redirect("/default?logoff=true");
                }   
                else
                {
                    nameuser.InnerText = Session["namelogin"].ToString();

                }
                    
            }

            /*            string menu = @"    <li id='crit' runat='server'><a href='/criteria'>&nbsp;&nbsp;<span class='font11'>Critérios de Avaliação</span></a></li>
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
            */
            dtbase.InnerText = "Data e Hora do Sistema: " + DateTime.Now.ToString("dd/MM/yyy hh:mm:ss");
            DataSet dsRem = new DataSet();
            dsRem = null;

            if (dsRem != null)
            {
                if (dsRem.Tables.Count > 0)
                {
                    /*
                     <i class='fa fa-tasks'></i><span class='badge bg-theme'>4</span>
                           
                    */
                    string htmlCab = "<div class='notify-arrow notify-arrow-green'></div>";
                    string htmlRem = "";
                    string badge = "<i class='fa fa-envelope'></i><span class='badge {0}'>{1}</span>";
                    int numPend = dsRem.Tables[0].Rows.Count;
                    string classCss = (numPend > 0 ? "bg-red" : "bg-theme");
                    badge = string.Format(badge, classCss, numPend.ToString());
                    //badgetag.InnerHtml = badge;
                    htmlCab += "<li><p class='green'>Há " + numPend.ToString() + " andamento(s) em lembrete! (Top 10)</p></li>";

                    for (int i = 0; i < dsRem.Tables[0].Rows.Count; i++)
                    {
                        if(i==11)
                            break;

                        htmlRem += @"<li>
                                        <a href='{2}'>
                                            <span class='subject'>
                                            <span class='time'>{0}</span>
                                            </span>
                                            <span class='message'>
                                                {1}
                                            </span>
                                        </a>
                                    </li>";
                        string hrefLink = "/cooperantes/process?id=" + dsRem.Tables[0].Rows[i]["id_coop"].ToString() + "&type=" + dsRem.Tables[0].Rows[i]["type"].ToString() + "&id_contract=" + dsRem.Tables[0].Rows[i]["id_contract"].ToString() + "&acomp=" + dsRem.Tables[0].Rows[i]["ID_ACOMP"].ToString();
                        htmlRem = String.Format(htmlRem, Convert.ToDateTime(dsRem.Tables[0].Rows[i]["dtandamento"].ToString()).ToString("dd/MM/yyyy"), dsRem.Tables[0].Rows[i]["andamento"].ToString(), hrefLink);
                    }

                    //andamentos.InnerHtml = htmlCab + htmlRem;
                }

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

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("delegation"))
            {
                ope.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("region"))
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

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("kpi"))
            {
                kpi.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("admin"))
            {
                if (Request.QueryString["type"] != null)
                {
                    cad.Attributes.Add("class", "active");
                }

            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("general"))
            {
                cad.Attributes.Add("class", "active");
            }
            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("register"))
            {
                cad.Attributes.Add("class", "active");
            }

            if (HttpContext.Current.CurrentHandler.ToString().ToLower().Contains("contact"))
            {
                cad.Attributes.Add("class", "active");
            }

        }
    }
}