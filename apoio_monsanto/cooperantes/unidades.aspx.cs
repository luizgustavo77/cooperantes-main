using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class unidades : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();

            if (Request.QueryString["del"] != null)
            {
                try
                {
                    com.deleteUnidades(Request.QueryString["del"].ToString());
                    gvCriteria.DataBind();
                }
                catch (Exception ex)
                {

                    error.Visible = true;
                    error.InnerText = "Erro ao excluir unidade: " + ex.Message;
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertUnidades(txCrite.Text.ToUpper());
                gvCriteria.DataBind();
                txCrite.Text = "";
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar unidade: " + ex.Message;
            }
        }

        public string getAct()
        {
            string delete = "<a href='/cooperantes/unidades?del=" + Eval("id_unidade") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Unidade'></a>&nbsp;";
            return delete;
        }

        public string retPerfil()
        {
           return "PERFIL PADRÃO";
        }
    }
}