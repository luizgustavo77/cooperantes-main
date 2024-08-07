using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto
{
    public partial class admin : System.Web.UI.Page
    {
        commom com = new commom();
        DataSet dsRet = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            bool readOnlyButs = false;
            if (Convert.ToInt64(Session["typelogin"]) >= 2 )
                readOnlyButs = true;
            String id_action = "";

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
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;RTV";
                    if (Request.QueryString["type"].ToString() == "4")
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;GR";
                    if (Request.QueryString["type"].ToString() == "2")
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Contratos - Células de Apoio";
                    if (Request.QueryString["type"].ToString() == "5")
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Customer Service";
                    if (Request.QueryString["type"].ToString() == "6")
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Customer Care";
                    if (Request.QueryString["type"].ToString() == "7")
                        h4Title.InnerHtml = "<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;&nbsp;Jurídico - Crédito e Cobrança";

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
                            txDistrict.Text = dsRet.Tables[0].Rows[0][9].ToString();
                            txDistrict.ReadOnly = readOnly;
                            ddRegional.SelectedValue = dsRet.Tables[0].Rows[0][10].ToString();
                            ddRegional.Enabled = readOnly;
                            txCont1.Text = dsRet.Tables[0].Rows[0][11].ToString();
                            txCont1.ReadOnly = readOnly;
                            txCont2.Text = dsRet.Tables[0].Rows[0][12].ToString();
                            txCont2.ReadOnly = readOnly;
                            txEnd.Text = dsRet.Tables[0].Rows[0][13].ToString();
                            txEnd.ReadOnly = readOnly;
                            if (dsRet.Tables[0].Rows[0][5].ToString() == "1")
                                userActive.Checked = true;
                            else
                                userActive.Checked = false;
                            if (dsRet.Tables[0].Rows[0][14].ToString() == "1")
                                userGLA.Checked = true;
                            else
                                userGLA.Checked = false;

                        }
                    }
                }
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
                        txLogin.Text == "" ||
                        txEnd.Text == "" ||
                        txDistrict.Text == "" ||
                        ddRegional.SelectedValue == "" ||
                        txCont1.Text == "" ||
                        txCont2.Text == "")
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
                       txLogin.Text == "" )
                    {
                        error.Visible = true;
                        error.InnerText = "Nome, Email e Login são obrigatórios!";
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
                    if (!userGLA.Checked)
                        typeGLA = "0";

                    error.InnerText = com.insertUser(txNam.Text, txEmail.Text, txLogin.Text, Request.QueryString["type"].ToString(), active,
                        txDistrict.Text, ddRegional.SelectedValue, txCont1.Text, txCont2.Text, txEnd.Text, typeGLA);
                    txNam.Text = "";
                    txNam.ReadOnly = false;
                    txEmail.Text = "";
                    txEmail.ReadOnly = false;
                    txLogin.Text = "";
                    txLogin.ReadOnly = false;
                    txDistrict.Text = "";
                    txDistrict.ReadOnly = false;
                    ddRegional.SelectedValue = "";
                    ddRegional.Enabled = true;
                    txCont1.Text = "";
                    txCont1.ReadOnly = false;
                    txCont2.Text = "";
                    txCont2.ReadOnly = false;
                    txEnd.Text = "";
                    txEnd.ReadOnly = false;
                    userGLA.Checked = false;

                    gvUsers.DataBind();


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
            txDistrict.Text = "";
            txDistrict.ReadOnly = false;
            ddRegional.SelectedValue = "";
            ddRegional.Enabled = true;
            txCont1.Text = "";
            txCont1.ReadOnly = false;
            txCont2.Text = "";
            txCont2.ReadOnly = false;
            txEnd.Text = "";
            txEnd.ReadOnly = false;
        }

        protected void btEdit_Click(object sender, EventArgs e)
        {
            if (txNam.Text == "" ||
               txEmail.Text == "" ||
               txLogin.Text == "" ||
               txEnd.Text == "" ||
               txDistrict.Text == "" ||
               ddRegional.SelectedValue == "" ||
               txCont1.Text == "" ||
               txCont2.Text == "")
                error.InnerText = "Todos os campos são obrigatórios!";
            try
            {
                //show errors
                error.Visible = true;
                string active = "1";
                if (!userActive.Checked)
                    active = "0";

                error.InnerText = com.updateUser(txNam.Text, txEmail.Text, txLogin.Text, Request.QueryString["type"].ToString(), active,
                    txDistrict.Text, ddRegional.SelectedValue, txCont1.Text, txCont2.Text, txEnd.Text);
                gvUsers.DataBind();

                btcan.Visible = true;
                btedit.Visible = true;
                btdel.Visible = false;
                btgrav.Visible = false;
                btpesq.Visible = false;

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
                txDistrict.Text = "";
                txDistrict.ReadOnly = false;
                ddRegional.SelectedValue = "";
                ddRegional.Enabled = true;
                txCont1.Text = "";
                txCont1.ReadOnly = false;
                txCont2.Text = "";
                txCont2.ReadOnly = false;
                txEnd.Text = "";
                txEnd.ReadOnly = false;

                gvUsers.DataBind();

                btcan.Visible = true;
                btedit.Visible = false;
                btdel.Visible = true;
                btgrav.Visible = false;
                btpesq.Visible = false;
            }
            catch (Exception ex)
            {

                error.InnerText = ex.Message;
            }
        }

        protected string getAction()
        {
            string view = "<a href='/admin?type=" + Request.QueryString["type"].ToString() + "&view="+Eval("login")+ "' class='btn btn-info btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Dados do Usuário'></a>&nbsp;";
            string edit = "<a href='/admin?type=" + Request.QueryString["type"].ToString() + "&edit=" + Eval("login") + "' class='btn btn-primary btn - xs fa fa-pencil tooltips'  data-original-title='Editar Usuário'></a>&nbsp;";
            string delete = "<a href='/admin?type=" + Request.QueryString["type"].ToString() + "&del=" + Eval("login") + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Usuário'></a>&nbsp;";

            // Admin = 1
            if (Convert.ToInt64(Session["typelogin"]) == 1)
                return view + edit + delete;
            // Celulas = 2
            else if (Convert.ToInt64(Session["typelogin"]) == 2)
                return view + edit;
            else
                return view;
        }
    }
}