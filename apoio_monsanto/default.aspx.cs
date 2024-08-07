using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto
{
    public partial class _default : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Session.Add("keyclient", dbAcess.Decrypt(Request.QueryString["id"].ToString()));
                Session.Add("keytype", dbAcess.Decrypt(Request.QueryString["type"].ToString()));
                Session.Add("keycontract", dbAcess.Decrypt(Request.QueryString["id_contract"].ToString()));
            }
            //com.sentEmail("david.branco@hotmail.com", "mensagem teste", "TESTE");
            if (Session["namelogin"] != null && Session["maillogin"] != null && Session["keycontract"] == null)
            {
                Response.Redirect("/contract");
            }
            
        }

        protected void btForget_click(object sender, EventArgs e)
        {
            try
            {
                error.InnerText = com.passForget(loginForget.Value);
            }
            catch (Exception ex)
            {

                error.InnerText = "Erro: " + ex.Message;
            }
           
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            DataSet dsDados = new DataSet();
            commom com = new commom();

            if ( ! String.IsNullOrEmpty(pass.Value) && ! String.IsNullOrEmpty(login.Value))
            {
                // valido o permissionamento como administrador
                com.updatePermission(login.Value, "VALIDATE");

                dsDados = com.login(login.Value, pass.Value);
                if (dsDados != null && dsDados.Tables.Count > 0)
                {
                    Session["namelogin"] = dsDados.Tables[0].Rows[0][0].ToString();
                    Session["maillogin"] = dsDados.Tables[0].Rows[0][1].ToString();
                    Session["login"] = dsDados.Tables[0].Rows[0][2].ToString();
                    Session["typelogin"] = dsDados.Tables[0].Rows[0][3].ToString();
                    Session["changePass"] = dsDados.Tables[0].Rows[0][4].ToString();
                    Session["cooperantes"] = dsDados.Tables[0].Rows[0][5].ToString();
                    Session["coopvisual"] = dsDados.Tables[0].Rows[0][6].ToString();
                    Session["coopunidade"] = dsDados.Tables[0].Rows[0][7].ToString();
                    Session["efetividade"] = dsDados.Tables[0].Rows[0]["EFETIVIDADE"].ToString();

                    if(ddSelect.SelectedValue == "2")
                    {
                        Response.Redirect("/records/contract");
                    }

                    if (ddSelect.SelectedValue == "3")
                    {
                        Response.Redirect("/glaapoio/contract");
                    }

                    if (Session["cooperantes"].ToString() == "S")
                    {
                        if (Session["changePass"].ToString() == "N")
                            Response.Redirect("/cooperantes/profile");
                        else
                            Response.Redirect("/cooperantes/contract");
                    }

                    if (Session["keyclient"] != null)
                    {
                        Response.Redirect("/process?id="+ Session["keyclient"].ToString() + "&type="+ Session["keytype"] + "&id_contract=" + Session["keycontract"].ToString());
                    }

                    if (Session["changePass"].ToString() == "N")
                        Response.Redirect("/profile");
                    else
                        Response.Redirect("/contract");

                }
                else
                {
                    error.InnerText = "Login ou Senha inválidos!";
                }
                
            }
            else
            {
                error.InnerText = "Todos os campos precisam ser preenchidos!";
            }
            
        }
    }
}