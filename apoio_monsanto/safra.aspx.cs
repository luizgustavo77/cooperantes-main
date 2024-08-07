using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class safra : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["del"] != null)
            {
                try
                {
                    com.deleteSafra(Request.QueryString["del"].ToString());
                    gvsafra.DataBind();
                }
                catch (Exception ex)
                {

                    error.Visible = true;
                    error.InnerText = "Erro ao excluir Safra: " + ex.Message;
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertSafra(txCrite.Text);
                txCrite.Text = "";
                gvsafra.DataBind();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar Safra: " + ex.Message;
            }
        }

        public string getAction()
        {
            string delete = "<a href='/safra?del=" + Eval("ID") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Safra'></a>&nbsp;";
            return delete;
        }

    }
}