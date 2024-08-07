using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class delegation : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["del"] != null)
                {
                    try
                    {
                        com.deletePermission(Request.QueryString["del"].ToString());
                        gvPerm.DataBind();
                    }
                    catch (Exception ex)
                    {

                        error.Visible = true;
                        error.InnerText = "Erro ao excluir permissão: " + ex.Message;
                    }
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertPermission(ddUsers.SelectedValue, txDe.Text, txAte.Text, ddPerfil.SelectedValue);
                gvPerm.DataBind();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao incluir permissão: " + ex.Message;
            }

        }

        public string getAction()
        {
            string delete = "<a href='/delegation?del=" + Eval("id_user") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Permissão'></a>&nbsp;";
            return delete;
        }
    }
}