using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace apoio_monsanto.glaapoio
{
    public partial class register : System.Web.UI.Page
    {
        newmom com = new newmom();
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

                ddRegional.DataTextField = "GLA_VAL";
                ddRegional.DataValueField = "GLA_VAL";
                ddRegional.DataSource = com.selectAllGlaCad("re");
                ddRegional.DataBind();

                ddUnidade.DataTextField = "GLA_VAL";
                ddUnidade.DataValueField = "GLA_VAL";
                ddUnidade.DataSource = com.selectAllGlaCad("un");
                ddUnidade.DataBind();

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
                            txNam.Text = dsPesq.Tables[0].Rows[0]["name"].ToString();
                            txDc.Text = dsPesq.Tables[0].Rows[0]["document"].ToString();
                            //txSap.Text = dsPesq.Tables[0].Rows[0]["sap"].ToString();
                            //txSapFilial.Text = dsPesq.Tables[0].Rows[0]["sap_filial"].ToString();
                            if (!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["regional"].ToString() ) )
                            {
                                ddRegional.SelectedValue = dsPesq.Tables[0].Rows[0]["regional"].ToString();
                            }

                            if (!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["unidade"].ToString()))
                            {
                                ddUnidade.SelectedValue = dsPesq.Tables[0].Rows[0]["unidade"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            error.Visible = true;
                            error.InnerText = "Unidade e/ou Regional não informados para esse cliente.";
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
            btAtual.Visible = false;
            btDel.Visible = false;
            btGravar.Visible = true;
            btPesq.Visible = true;
            btCan.Visible = false;
            //txSap.Text = "";
            //txSapFilial.Text = "";
        }

        protected void btReg_Click(object sender, EventArgs e)
        {
            String ret = "";
            error.Visible = true;

            string agric = "1";


            if (!String.IsNullOrEmpty(txDc.Text) && !String.IsNullOrEmpty(txNam.Text))
            {
                ret = com.customer_add(2, txNam.Text, txDc.Text, null, null, ddRegional.SelectedValue, ddUnidade.SelectedValue);
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
                ret = com.changeCustomer(Convert.ToInt64(Request.QueryString["update"].ToString()), txNam.Text, txDc.Text, string.Empty, string.Empty, string.Empty, ddRegional.SelectedValue, ddUnidade.SelectedValue);

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
                ret = com.changeCustomer(Convert.ToInt64(Request.QueryString["delete"]), txNam.Text, txDc.Text,"*", string.Empty, string.Empty, "", "");
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
            string delete = "<a href='/glaapoio/register?delete=" + Eval("id").ToString() + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Cliente'></a>";
            string edit = "<a href='/glaapoio/register?update=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-pencil tooltips'  data-original-title='Editar Cliente'></a>&nbsp;";

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