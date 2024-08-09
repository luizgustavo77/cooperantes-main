using System;

namespace apoio_monsanto
{
    public partial class motivoreprova : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["del"] != null)
            {
                try
                {
                    com.deleteMotivo(Request.QueryString["del"].ToString());
                    gvMotivo.DataBind();
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

                com.insertMotivoReprova(txCrite.Text, gla);
                gvMotivo.DataBind();
            }
            catch (Exception ex)
            {

                error.Visible = true;
                error.InnerText = "Erro ao gravar motivo: " + ex.Message;
            }
        }

        public string getAction()
        {
            string delete = "<a href='/motivo?del=" + Eval("id") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Motivo'></a>&nbsp;";
            return delete;
        }

        public string retPerfil()
        {
            return "PERFIL PADRÃO";
        }
    }
}