using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class criteria : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["del"] != null)
            {
                try
                {
                    com.deleteCriteria(Request.QueryString["del"].ToString());
                    gvCriteria.DataBind();
                }
                catch (Exception ex)
                {

                    error.Visible = true;
                    error.InnerText = "Erro ao excluir permissão: " + ex.Message;
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                String gla = "1";
                if (!userGLA.Checked)
                    gla = "2";

                com.insertCriteria(txCrite.Text, gla);
                gvCriteria.DataBind();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar critério: " + ex.Message;
            }
        }

        public string getAction()
        {
            string delete = "<a href='/criteria?del=" + Eval("id") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Critério'></a>&nbsp;";
            return delete;
        }

        public string retPerfil()
        {
           return "PERFIL PADRÃO";
        }
    }
}