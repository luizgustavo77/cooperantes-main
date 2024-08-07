using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto.glaapoio
{
    public partial class form_general : System.Web.UI.Page
    {
        newmom com = new newmom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["del"] != null)
            {
                try
                {
                    com.deleteGlaCad(Request.QueryString["del"].ToString());
                    gvCadastro.DataBind();
                }
                catch (Exception ex)
                {

                    error.Visible = true;
                    error.InnerText = "Erro ao excluir Cadastro: " + ex.Message;
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertGlaCad(ddType.SelectedValue, txValue.Text);
                txValue.Text = "";
                gvCadastro.DataBind();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar Cadastro: " + ex.Message;
            }
        }

        public string getAction()
        {
            string delete = "<a href='/glaapoio/general?del=" + Eval("id_gla_cad_generic") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Cadastro'></a>&nbsp;";
            return delete;
        }

    }
}