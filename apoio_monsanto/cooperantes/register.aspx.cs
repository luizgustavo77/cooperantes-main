using Canducci.Zip;
using System;
using System.Data;
using System.Web.UI;

namespace apoio_monsanto.cooperantes
{
    public partial class register : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        mon mon = new mon();
        ZipCodeLoad zipLoad = new ZipCodeLoad();

        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            /*
             * ddRTV = Técnico de Campo
             * ddGR = Gerente de Planta
             * 
             * 
             * */
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();
            bool readOnly = false;
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
                            txNam.Text = dsPesq.Tables[0].Rows[0]["name"].ToString();
                            txDc.Text = dsPesq.Tables[0].Rows[0]["document"].ToString();
                            userGr = dsPesq.Tables[0].Rows[0]["gr"].ToString();
                            userRTV = dsPesq.Tables[0].Rows[0]["rtv"].ToString();

                            ZipCode zipCode = null;
                            if (ZipCode.TryParse(dsPesq.Tables[0].Rows[0]["cep"].ToString(), out zipCode))
                            {
                                var zipCodeInfo = zipLoad.FindAsync(zipCode).GetAwaiter().GetResult();

                                txRua.Text = zipCodeInfo.Value.Address;
                                txBairro.Text = zipCodeInfo.Value.District;
                                txCidade.Text = zipCodeInfo.Value.City;
                                txEstado.Text = zipCodeInfo.Value.Uf;
                            }
                            else
                            {
                                txRua.Text = dsPesq.Tables[0].Rows[0]["rua"].ToString();
                                txBairro.Text = dsPesq.Tables[0].Rows[0]["bairro"].ToString();
                                txCidade.Text = dsPesq.Tables[0].Rows[0]["cidade"].ToString();
                                txEstado.Text = dsPesq.Tables[0].Rows[0]["estado"].ToString();
                            }

                            //txRua.Text = dsPesq.Tables[0].Rows[0]["rua"].ToString();
                            txNum.Text = dsPesq.Tables[0].Rows[0]["numero"].ToString();
                            txCompl.Text = dsPesq.Tables[0].Rows[0]["complemento"].ToString();
                            //txBairro.Text = dsPesq.Tables[0].Rows[0]["bairro"].ToString();
                            //txCidade.Text = dsPesq.Tables[0].Rows[0]["cidade"].ToString();
                            //txEstado.Text = dsPesq.Tables[0].Rows[0]["estado"].ToString();
                            txMail.Text = dsPesq.Tables[0].Rows[0]["email"].ToString();
                            txTel.Text = dsPesq.Tables[0].Rows[0]["telefone"].ToString();

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
                                        //error.Visible = true;
                                        //error.InnerText = " Gerente de Planta não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                                    }

                                }
                            }
                            catch (System.IndexOutOfRangeException gr)
                            {

                                ddGR.Enabled = true;
                                ddGR.SelectedValue = "";
                                //error.Visible = true;
                                //error.InnerText = " Gerente de Planta não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
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
                                        //error.Visible = true;
                                        //error.InnerText = " Técnico de Campo não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                                    }
                                }
                            }
                            catch (System.IndexOutOfRangeException rtv)
                            {

                                ddRTV.Enabled = true;
                                ddRTV.SelectedValue = "";
                                //error.Visible = true;
                                //error.InnerText = " Técnico de Campo não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                            }
                        }
                        catch (Exception ex)
                        {
                            error.Visible = true;
                            error.InnerText = "Error! Informe a mensagem a seguir pro Suporte: " + ex.Message;
                        }

                    }

                }
            }
            else
            {

            }

            if (visualiza == "S")
            {
                btAtual.Visible = false;
                btGravar.Visible = false;
                btPesq.Visible = true;
                btCan.Visible = true;
            }

        }

        protected void btCep_Click(object sender, EventArgs e)
        {


            ZipCode zipCode = null;
            if (ZipCode.TryParse(txCep.Text, out zipCode))
            {
                var zipCodeInfo = zipLoad.FindAsync(zipCode).GetAwaiter().GetResult();

                txRua.Text = zipCodeInfo.Value.Address;
                txBairro.Text = zipCodeInfo.Value.District;
                txCidade.Text = zipCodeInfo.Value.City;
                txEstado.Text = zipCodeInfo.Value.Uf;
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
            txRua.Text = "";
            txBairro.Text = "";
            txCidade.Text = "";
            txEstado.Text = "";
            txMail.Text = "";
            txTel.Text = "";
            txBairro.Text = "";
        }

        protected void btReg_Click(object sender, EventArgs e)
        {
            String ret = "";
            error.Visible = true;

            string agric = "0";

            if (!String.IsNullOrEmpty(txDc.Text) && !String.IsNullOrEmpty(txNam.Text))
            {
                ret = com.customer_add(2, txNam.Text, txDc.Text, ddGR.SelectedValue, ddRTV.SelectedValue, agric, txRua.Text, txNum.Text, txCompl.Text, txBairro.Text, txCidade.Text, txEstado.Text, txTel.Text, txMail.Text);
                gvCustomers.DataBind();

                Session["message"] = "OK";

                returnInitialState();
            }
            else
                ret = "Os campos Nome e Documento são obrigatórios!";

            error.InnerText = ret;
        }

        protected void btUpd_Click(object sender, EventArgs e)
        {

            String ret = "";
            error.Visible = true;
            if (!String.IsNullOrEmpty(txDc.Text) && !String.IsNullOrEmpty(txNam.Text))
            {
                ret = com.changeCustomer(Convert.ToInt16(Request.QueryString["update"]), txNam.Text, txDc.Text, String.Empty, ddRTV.SelectedValue.ToString(), ddGR.SelectedValue.ToString(), txMail.Text, txTel.Text, txCep.Text, txNum.Text);

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
                ret = com.changeCustomer(Convert.ToInt64(Request.QueryString["delete"]), txNam.Text, txDc.Text, "*", ddRTV.SelectedValue.ToString(), ddGR.SelectedValue.ToString(), txMail.Text, txTel.Text, txCep.Text, txNum.Text);
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
            Response.Redirect("/cooperantes/register");

        }

        protected void btPes_Click(object sender, EventArgs e)
        {

        }

        protected string getActionC()
        {
            string delete = "<a href='/cooperantes/register?delete=" + Eval("id").ToString() + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips'  data-original-title='Excluir Cliente'></a>";
            string edit = "<a href='/cooperantes/register?update=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-pencil tooltips'  data-original-title='Editar Cliente'></a>&nbsp;";

            if (visualiza != "S")
                return edit + delete;
            else
                return "";
        }
    }
}