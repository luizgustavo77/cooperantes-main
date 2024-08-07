using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto
{
    public partial class contact : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();

            if(!Page.IsPostBack)
            {
                if (Request.QueryString["upd"] != null)
                {
                    id_contact.Text = Request.QueryString["upd"].ToString();
                    try
                    {
                        DataSet dsCon = new DataSet();
                        dsCon = com.selectContactRegister(Request.QueryString["upd"].ToString());
                        
                        if(dsCon != null)
                        {
                            txCrite.Text = dsCon.Tables[0].Rows[0]["description"].ToString();
                            btUpd.Visible = true;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        error.Visible = true;
                        error.InnerText = "Erro ao atualizar dados do contato: " + ex.Message;
                    }
                }
                if (Request.QueryString["del"] != null)
                {
                    id_contact.Text = Request.QueryString["del"].ToString();
                    try
                    {
                        com.deleteContactPrev(Request.QueryString["del"].ToString());
                        gvCriteria.DataBind();
                    }
                    catch (Exception ex)
                    {

                        error.Visible = true;
                        error.InnerText = "Erro ao excluir contato: " + ex.Message;
                    }
                }
            }
            
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertContactPrev(txCrite.Text.ToUpper());
                gvCriteria.DataBind();
                txCrite.Text = "";
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar contato: " + ex.Message;
            }
        }

        protected void btUpd_Click(object sender, EventArgs e)
        {
            try
            {
                com.updateContactPrev(Request.QueryString["upd"].ToString(), txCrite.Text);
                gvCriteria.DataBind();
                txCrite.Text = "";
                btUpd.Visible = false;
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar contato: " + ex.Message;
            }
        }

        public string getAct()
        {
            string update = "<a href='/cooperantes/contact?upd=" + Eval("id_coop_contact") + "' class='btn btn-warning btn - xs fa fa-pencil tooltips'  data-original-title='Atualizar Contato'></a>&nbsp;";
            string delete = "<a href='/cooperantes/contact?del=" + Eval("id_coop_contact") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Contato'></a>&nbsp;";
            return update+delete;
        }

        public string retPerfil()
        {
           return "PERFIL PADRÃO";
        }
    }
}