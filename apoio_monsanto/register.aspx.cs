using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto
{
    public partial class register : System.Web.UI.Page
    {
        commom com = new commom();
        mon mon = new mon();
        protected void Page_Load(object sender, EventArgs e)
        {
            bool readOnly = false;
            if (Convert.ToInt64(Session["typelogin"]) >= 3)
                readOnly = true;
            String id_action = "";

            btGravar.Disabled = readOnly;

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["update"] != null || Request.QueryString["delete"] != null)
                {

                    if (Request.QueryString["delete"] != null)
                    {
                        id_action = Request.QueryString["delete"].ToString();
                        btAtual.Visible = false;
                        btGravar.Visible = false;
                        btPesq.Visible = false;
                        btCan.Visible = true;
                        btDel.Visible = true;
                        ddGR.Enabled = false;
                        ddRTV.Enabled = false;
                        txNam.ReadOnly = true;
                        txDc.ReadOnly = true;

                    }
                    else
                    {
                        id_action = Request.QueryString["update"].ToString();
                        btAtual.Visible = true;
                        btGravar.Visible = false;
                        btPesq.Visible = false;
                        btCan.Visible = true;
                    }

                    DataSet dsPesq = new DataSet();
                    dsPesq = com.searcCustomers(null, null, id_action, null);
                    string userGr = "";
                    string userRTV = "";

                    if (dsPesq != null)
                    {
                        try
                        {
                            txNam.Text = dsPesq.Tables[0].Rows[0][1].ToString();
                            txDc.Text = dsPesq.Tables[0].Rows[0][2].ToString();
                            userGr = dsPesq.Tables[0].Rows[0][3].ToString();
                            userRTV = dsPesq.Tables[0].Rows[0][4].ToString();

                            if (dsPesq.Tables[0].Rows[0][5].ToString() == "1")
                                userAgric.Checked = true;

                            try
                            {
                                if (!String.IsNullOrEmpty(userGr))
                                {
                                    if (com.valueValidation(userGr, 4))
                                    {
                                        ddGR.SelectedValue = userGr;
                                    }
                                    else
                                    {
                                        error.Visible = true;
                                        error.InnerText = " GR não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                                    }

                                }
                            }
                            catch (System.IndexOutOfRangeException gr)
                            {

                                ddGR.Enabled = true;
                                ddGR.SelectedValue = "";
                                error.Visible = true;
                                error.InnerText = " GR não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                            }

                            try
                            {
                                if (!String.IsNullOrEmpty(userRTV))
                                {
                                    if (com.valueValidation(userRTV, 3))
                                    {
                                        ddRTV.SelectedValue = userRTV;
                                    }
                                    else
                                    {
                                        error.Visible = true;
                                        error.InnerText = " RTV não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                                    }
                                }
                            }
                            catch (System.IndexOutOfRangeException rtv)
                            {

                                ddRTV.Enabled = true;
                                ddRTV.SelectedValue = "";
                                error.Visible = true;
                                error.InnerText = " RTV não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                            }
                        }
                        catch (Exception ex)
                        {
                            error.Visible = true;
                            error.InnerText = "Informações de GR ou RTV incoerentes. Informe a mensagem a seguir pro Suporte: " + ex.Message;
                        }

                    }

                }
            }
            else
            {

            }

        }

        private void returnInitialState()
        {
            txNam.Text = "";
            txNam.ReadOnly = false;
            txDc.Text = "";
            txDc.ReadOnly = false;
            ddGR.SelectedValue = "";
            ddRTV.SelectedValue = "";
            btAtual.Visible = false;
            btDel.Visible = false;
            btGravar.Visible = true;
            btPesq.Visible = true;
            btCan.Visible = false;
        }

        protected void btReg_Click(object sender, EventArgs e)
        {
            String ret = "";
            error.Visible = true;

            string agric = "1";
            if (!userAgric.Checked)
                agric = "0";


            if (!String.IsNullOrEmpty(txDc.Text) && !String.IsNullOrEmpty(txNam.Text))
            {
                ret = com.customer_add(2, txNam.Text, txDc.Text, ddGR.SelectedValue, ddRTV.SelectedValue, agric);
                gvCustomers.DataBind();

                Session["message"] = "OK";

                returnInitialState();
            }
            else
                ret = "Todos os campos são obrigatórios!";

            error.InnerText = ret;
        }

        protected void btUpd_Click(object sender, EventArgs e)
        {

            String ret = "";
            error.Visible = true;
            if (!String.IsNullOrEmpty(txDc.Text) && !String.IsNullOrEmpty(txNam.Text))
            {
                ret = com.changeCustomer(Convert.ToInt16(Request.QueryString["update"]), txNam.Text, txDc.Text, String.Empty, ddRTV.SelectedValue.ToString(), ddGR.SelectedValue.ToString());

                gvCustomers.DataBind();

                Session["message"] = "OK";

                returnInitialState();
            }
                
            else
                ret = "Todos os campos são obrigatórios!";

            
            error.InnerText = ret;

        }
        protected void btDel_Click(object sender, EventArgs e)
        {
            String ret = "";
            error.Visible = true;
            if (!String.IsNullOrEmpty(txDc.Text) && !String.IsNullOrEmpty(txNam.Text))
            {
                ret = com.changeCustomer(Convert.ToInt64(Request.QueryString["delete"]), txNam.Text, txDc.Text, "*", ddRTV.SelectedValue.ToString(), ddGR.SelectedValue.ToString());
                gvCustomers.DataBind();

                Session["message"] = "OK";

                returnInitialState();
            }
            else
                ret = "Todos os campos são obrigatórios!";
           
            error.InnerText = ret;

        }

        protected void btCan_Click(object sender, EventArgs e)
        {
            returnInitialState();
        }

        protected void btPes_Click(object sender, EventArgs e)
        {

        }

        protected string getAction()
        {
            string delete = "<a href='/register?delete=" + Eval("id").ToString() + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Cliente'></a>";
            string edit = "<a href='/register?update=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-pencil tooltips'  data-original-title='Editar Cliente'></a>&nbsp;";

            // Admin = 1
            if (Convert.ToInt64(Session["typelogin"]) == 1 )
                return edit + delete;
            // Celulas = 2
            else if (Convert.ToInt64(Session["typelogin"]) == 2)
                return edit;
            else
                return "";
        }
    }
}