using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto.cooperantes
{
    public partial class admin : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        DataSet dsRet = new DataSet();
        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool readOnlyButs = false;
            String id_action = "";
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();

            btgrav.Disabled = readOnlyButs;
            //1. ADMIN
            //2. USER
            //3. RTV
            //4. GR
            //5. SERVICE
            //6. CARE
            //. JURIDICO
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["type"] != null)
                {
                    if (Request.QueryString["type"].ToString() == "3")
                    {
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Técnico de Campo";
                        //txLogin.Enabled = false;
                    }

                    if (Request.QueryString["type"].ToString() == "4")
                    {
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Gerente da Planta";
                        //txLogin.Enabled = false;
                    }

                    if (Request.QueryString["type"].ToString() == "5")
                    {
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Supervisor de Campo";
                        //txLogin.Enabled = false;
                    }

                    if (Request.QueryString["type"].ToString() == "2")
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Célula de Apoio";
                    /*
                                        if (Request.QueryString["type"].ToString() == "5")
                                            h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Customer Service";
                                        if (Request.QueryString["type"].ToString() == "6")
                                            h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Customer Care";
                                        if (Request.QueryString["type"].ToString() == "7")
                                            h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Jurídico - Crédito e Cobrança";
                    */

                    if (Request.QueryString["view"] != null || Request.QueryString["edit"] != null || Request.QueryString["del"] != null)
                    {
                        String login = "";
                        bool readOnly = false;
                        if (Request.QueryString["view"] != null)
                        {
                            login = Request.QueryString["view"];
                            readOnly = true;
                            btcan.Visible = true;
                            btedit.Visible = false;
                            btdel.Visible = false;
                            btgrav.Visible = false;
                            btpesq.Visible = false;

                        }
                        else if (Request.QueryString["edit"] != null)
                        {
                            login = Request.QueryString["edit"];
                            btcan.Visible = true;
                            btedit.Visible = true;
                            btdel.Visible = false;
                            btgrav.Visible = false;
                            btpesq.Visible = false;
                        }
                        else
                        {
                            login = Request.QueryString["del"];
                            readOnly = true;
                            btcan.Visible = true;
                            btedit.Visible = false;
                            btdel.Visible = true;
                            btgrav.Visible = false;
                            btpesq.Visible = false;
                        }

                        dsRet = com.selectDatUsers(login, Request.QueryString["type"].ToString());

                        if (dsRet != null)
                        {
                            txNam.Text = dsRet.Tables[0].Rows[0][0].ToString();
                            txNam.ReadOnly = readOnly;
                            txEmail.Text = dsRet.Tables[0].Rows[0][1].ToString();
                            txEmail.ReadOnly = readOnly;
                            txLogin.Text = dsRet.Tables[0].Rows[0][2].ToString();
                            txLogin.ReadOnly = true;
                            string unidade = dsRet.Tables[0].Rows[0][9].ToString();
                            if (!string.IsNullOrEmpty(unidade))
                                txUnidades.Text = unidade;
                            //ddUnidade.Enabled = readOnly;
                            txCont1.Text = dsRet.Tables[0].Rows[0][11].ToString();
                            txCont1.ReadOnly = readOnly;
                            txCont2.Text = dsRet.Tables[0].Rows[0][12].ToString();
                            txCont2.ReadOnly = readOnly;
                            if (dsRet.Tables[0].Rows[0][5].ToString() == "1")
                                userActive.Checked = true;
                            else
                                userActive.Checked = false;
                            if (dsRet.Tables[0].Rows[0]["visualiza"].ToString() == "S")
                                userVisual.Checked = true;
                            else
                                userVisual.Checked = false;

                        }
                    }
                }
            }

            if (visualiza == "S")
            {
                btcan.Visible = true;
                btedit.Visible = false;
                btdel.Visible = false;
                btgrav.Visible = false;
                btpesq.Visible = true;
            }
        }

        protected void btGrava_Click(object sender, EventArgs e)
        {
            bool cont = true;
            //show errors
            error.Visible = true;
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"].ToString() != "7")
                {
                    if (txNam.Text == "" ||
                        txEmail.Text == "" ||
                        txLogin.Text == "")
                    {
                        error.Visible = true;
                        error.InnerText = "Todos os campos são obrigatórios!";
                        cont = false;
                    }
                }
                else
                {
                    if (txNam.Text == "" ||
                       txEmail.Text == "" ||
                       txLogin.Text == "")
                    {
                        error.Visible = true;
                        error.InnerText = "Os campos de Nome e Email são obrigatórios!";
                        cont = false;
                    }
                }
            }

            if (cont)
            {
                try
                {
                    string active = "1";
                    if (!userActive.Checked)
                        active = "0";
                    string typeGLA = "1";

                    string visual = "N";
                    if (userVisual.Checked)
                        visual = "S";

                    error.InnerText = com.insertUser(txNam.Text, txEmail.Text, txLogin.Text, Request.QueryString["type"].ToString(), active,
                       txUnidades.Text, "", txCont1.Text, txCont2.Text, "", typeGLA, visual);
                    txNam.Text = "";
                    txNam.ReadOnly = false;
                    txEmail.Text = "";
                    txEmail.ReadOnly = false;
                    txLogin.Text = "";
                    txLogin.ReadOnly = false;
                    ddUnidade.Enabled = true;
                    txCont1.Text = "";
                    txCont1.ReadOnly = false;
                    txCont2.Text = "";
                    txCont2.ReadOnly = false;
                    gvUsers.DataBind();
                    txUnidades.Text = "";

                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerText = ex.Message;
                }
            }
        }

        protected void btCan_Click(object sender, EventArgs e)
        {

            btdel.Visible = false;
            btedit.Visible = false;
            btpesq.Visible = true;
            btcan.Visible = false;
            btgrav.Visible = true;

            error.InnerText = "";

            txNam.Text = "";
            txNam.ReadOnly = false;
            txEmail.Text = "";
            txEmail.ReadOnly = false;
            txLogin.Text = "";
            txLogin.ReadOnly = false;
            ddUnidade.DataBind();
            txCont1.Text = "";
            txCont1.ReadOnly = false;
            txCont2.Text = "";
            txCont2.ReadOnly = false;
            txUnidades.Text = "";

        }

        protected void btEdit_Click(object sender, EventArgs e)
        {
            if (txNam.Text == "" ||
                       txEmail.Text == "" ||
                       txLogin.Text == "")
                error.InnerText = "Os campos de Nome e Email são obrigatórios!";
            try
            {
                //show errors
                error.Visible = true;
                string active = "1";
                if (!userActive.Checked)
                    active = "0";

                string visual = "N";
                if (userVisual.Checked)
                    visual = "S";

                error.InnerText = com.updateUser(txNam.Text, txEmail.Text, txLogin.Text, Request.QueryString["type"].ToString(), active,
                    txUnidades.Text, "", txCont1.Text, txCont2.Text, "", visual);
                gvUsers.DataBind();

                btcan.Visible = true;
                btedit.Visible = true;
                btdel.Visible = false;
                btgrav.Visible = false;
                btpesq.Visible = false;
                ddUnidade.DataBind();
                txUnidades.Text = "";

                Response.Redirect("/cooperantes/admin?type=" + Request.QueryString["type"].ToString());

            }
            catch (Exception ex)
            {

                error.InnerText = ex.Message;
            }
        }

        protected void btDel_Click(object sender, EventArgs e)
        {
            try
            {
                //show errors
                error.Visible = true;
                string active = "";
                if (!userActive.Checked)
                    active = "0";

                com.deleteUser(txLogin.Text);

                error.InnerText = "Registro removido!";

                txNam.Text = "";
                txNam.ReadOnly = false;
                txEmail.Text = "";
                txEmail.ReadOnly = false;
                txLogin.Text = "";
                txLogin.ReadOnly = false;
                ddUnidade.Enabled = true;
                txCont1.Text = "";
                txCont1.ReadOnly = false;
                txCont2.Text = "";
                txCont2.ReadOnly = false;

                gvUsers.DataBind();

                btcan.Visible = true;
                btedit.Visible = false;
                btdel.Visible = true;
                btgrav.Visible = false;
                btpesq.Visible = false;
                ddUnidade.DataBind();
                txUnidades.Text = "";

                Response.Redirect("/cooperantes/admin?type=" + Request.QueryString["type"].ToString());
            }
            catch (Exception ex)
            {

                error.InnerText = ex.Message;
            }
        }

        protected string getAction()
        {
            string view = "<a href='/cooperantes/admin?type=" + Request.QueryString["type"].ToString() + "&view=" + Eval("login") + "' class='btn btn-info btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Dados do Usuário'></a>&nbsp;";
            string edit = "<a href='/cooperantes/admin?type=" + Request.QueryString["type"].ToString() + "&edit=" + Eval("login") + "' class='btn btn-primary btn - xs fa fa-pencil tooltips'  data-original-title='Editar Usuário'></a>&nbsp;";
            string delete = "<a href='/cooperantes/admin?type=" + Request.QueryString["type"].ToString() + "&del=" + Eval("login") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Usuário'></a>&nbsp;";

            return view + edit + delete;
        }

        protected void btAddUni_Click(object sender, EventArgs e)
        {
            txUnidades.Text += ddUnidade.SelectedValue + ";";
        }

        protected void btDelUni_Click(object sender, EventArgs e)
        {
            txUnidades.Text = "";
        }
    }
}