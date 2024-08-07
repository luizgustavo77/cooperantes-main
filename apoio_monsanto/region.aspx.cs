using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class region : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["del"] != null)
            {
                try
                {
                    com.deleteRegion(Request.QueryString["del"].ToString());
                    gvregion.DataBind();
                }
                catch (Exception ex)
                {

                    error.Visible = true;
                    error.InnerText = "Erro ao excluir Regional: " + ex.Message;
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertRegion(txCrite.Text);
                gvregion.DataBind();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar Regional: " + ex.Message;
            }
        }

        public string getAct()
        {
            string delete = "<a href='/region?del=" + Eval("ID") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Regional'></a>&nbsp;";
            return delete;
        }

    }
}