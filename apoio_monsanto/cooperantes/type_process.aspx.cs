using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto.cooperantes
{
    public partial class type_process : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();

            btUpd.Visible = false;
            //btPesq.Visible = false;

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["upd"] != null)
                {
                    btInc.Visible = false;
                    btUpd.Visible = true;
                    DataSet dsProcess = new DataSet();
                    id_process.Text = Request.QueryString["upd"].ToString();

                    dsProcess = com.selectTipoProcesso(id_process.Text, "");

                    if (dsProcess != null)
                    {
                        txTipo.Text = dsProcess.Tables[0].Rows[0]["name"].ToString();
                        ddActive.SelectedValue = dsProcess.Tables[0].Rows[0]["active"].ToString();
                    }
                }
            }
        }

        protected void btInc_Click(object sender, EventArgs e)
        {
            try
            {
                com.insertTipoProcesso(txTipo.Text, ddActive.SelectedValue);
                gvCriteria.DataBind();
                txTipo.Text = "";
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar tipo de processo: " + ex.Message;
            }
        }

        protected void btCanc_Click(object sender, EventArgs e)
        {
            try
            {
                //com.insertTipoProcesso(txTipo.Text, ddActive.SelectedValue);
                id_process.Text = "";
                ddActive.SelectedValue = "";
                txTipo.Text = "";
                Response.Redirect("/cooperantes/tipos");
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao cancelar: " + ex.Message;
            }
        }

        protected void btUpd_Click(object sender, EventArgs e)
        {
            try
            {
                com.updateTipoProcesso(Request.QueryString["upd"].ToString(), ddActive.SelectedValue, txTipo.Text);
                gvCriteria.DataBind();
                txTipo.Text = "";
                btUpd.Visible = false;
                btInc.Visible = true;
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao excluir tipo de processo: " + ex.Message;
            }
        }

        public string getActs()
        {
            string update = "<a href='/cooperantes/tipos?upd=" + Eval("id_process") + "' class='btn btn-warning btn - xs fa fa-pencil tooltips'  data-original-title='Atualizar Processo'></a>&nbsp;";
            return update;
        }

    }
}