using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace apoio_monsanto
{
    public partial class contract : System.Web.UI.Page
    {
        commom com = new commom();
        string id_customer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            error.InnerText = "";

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["view"] != null)
                {
                    DataSet dsPesq = new DataSet();
                    dsPesq = com.searcCustomers(null, null, Request.QueryString["view"], "");

                    if (dsPesq != null)
                    {
                        txNam.Text = dsPesq.Tables[0].Rows[0][1].ToString();
                        txDc.Text = dsPesq.Tables[0].Rows[0][2].ToString();
                    }

                    ddSafra.DataBind(); 
                    ddSafra.SelectedValue = "";

                    divCont.Visible = true;
                    divGv.Visible = true;
                }

                if (Request.QueryString["del"] != null)
                {
                    com.deleteContract(Request.QueryString["id_contract"].ToString());

                    string path = Server.MapPath("Data");
                    string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                    try
                    {
                        com.deleteDocByContract(Request.QueryString["id_contract"].ToString());
                        bool directoryExists = Directory.Exists(saveLocation);
                        if (directoryExists)
                            Directory.Delete(saveLocation, true);
                    }
                    catch (Exception)
                    {

                    }                

                    error.InnerText = "Registro removido!";
                    gvContracts.DataBind();

                    Response.Redirect("/contract?view=" + Request.QueryString["view"].ToString());
                }
            }
            else
            {
                
            }

            Session["message"] = null;

            
        }

        protected void btPes_Click(object sender, EventArgs e)
        {

        }

        protected string getAction()
        {
            string create = "<a data-toggle='modal' data-placement='top' data-original-title='Novo Processo' href='contract?customer#modTipo' onclick='return clickTo(" + Eval("id") + ");' class='btn btn-success btn - xs fa fa-plus tooltips'></a>&nbsp;";
            string view = "<a href='/contract?view=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Processos do Cliente'></a>";

            if (Convert.ToInt64(Session["typelogin"]) > 2 )
                return view;
            else
                return create + view;
        }

        protected string getActionCont()
        {
            string view = "<a href='/process?id=" + Eval("id_client").ToString() + "&type=" + Eval("type_contract") + "&id_contract=" + Eval("NrContrato").ToString() + "' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Contrato'></a>&nbsp;"; ;
            string delete = "<a href='/contract?view=" + Eval("id_client").ToString() + "&id_contract=" + Eval("NrContrato").ToString() + "&del=true' class='btn btn-danger btn - xs fa fa-trash-o tooltips tooltips confirmation' data-placement='top' data-original-title='Excluir Contrato'></a>&nbsp;"; 
            if (Convert.ToInt64(Session["typelogin"]) >= 2 )
                return view;
            else
                return view + delete;
        }

        protected void btNew_Cont_Click(object sender, EventArgs e)
        {
            try
            {
                id_customer = hidden.Text;
                String id_contract = "";
                com.insertContract(ddTipoCont.SelectedValue, id_customer, ref id_contract, Session["login"].ToString());

                Response.Redirect("/process?new=true&id=" + id_customer + "&type=" + ddTipoCont.SelectedValue + "&id_contract=" + id_contract);
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText = ex.Message;
            }

        }
    }
}