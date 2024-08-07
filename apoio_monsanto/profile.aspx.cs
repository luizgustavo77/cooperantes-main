using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto
{
    public partial class profile : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["changePass"] != null)
            {
                if (Session["changePass"].ToString() == "N")
                {
                    requisitos.Attributes["class"] = "alert alert-danger";
                    requisitos.InnerHtml += "*** OBRIGATÓRIO ALTERAR A SENHA NO PRIMEIRO LOGIN ***";
                }
            }
                
        }

        protected void btReg_Click(object sender, EventArgs e)
        {
            error.Visible = true;
            if (txPass.Text != txCPass.Text)
            {
                error.InnerText = "A senha e a confirmação de senha precisam ser iguais";
            }
            else if ( ! txCPass.Text.ToCharArray().Any(x => char.IsNumber(x)) || 
                ! txCPass.Text.ToCharArray().Any(x => char.IsUpper(x)) ||
                txCPass.Text.Length < 6 )
            {
                error.InnerText = "A senha precisa seguir os padrões citados acima! Verifique!";
            }
            else
            {
                try
                {
                    com.changePass(Session["login"].ToString(), txCPass.Text, txPass.Text);

                    error.InnerText = "Senha alterada!";
                }
                catch (Exception ex)
                {

                    error.InnerText = "Erro ao alterar a senha: " + ex.Message;
                }
            }
        }
    }
}