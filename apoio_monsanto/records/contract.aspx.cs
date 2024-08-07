﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace apoio_monsanto.records
{
    public partial class contract : System.Web.UI.Page
    {
        glamom com = new glamom();
        string id_customer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            error.InnerText = "";
                        
            if (!Page.IsPostBack)
            {
                ddCY.DataTextField = "GLA_VAL";
                ddCY.DataValueField = "GLA_VAL";
                ddCY.DataSource = com.selectAllGlaCad("cy");
                ddCY.DataBind();

                ddCY.SelectedValue = "";

                ddMarca.DataTextField = "GLA_VAL";
                ddMarca.DataValueField = "GLA_VAL";
                ddMarca.DataSource = com.selectAllGlaCad("ma");
                ddMarca.DataBind();

                ddMarca.SelectedValue = "";

                ddRegional.DataTextField = "GLA_VAL";
                ddRegional.DataValueField = "GLA_VAL";
                ddRegional.DataSource = com.selectAllGlaCad("re");
                ddRegional.DataBind();

                ddRegional.SelectedValue = "";

                ddUnidade.DataTextField = "GLA_VAL";
                ddUnidade.DataValueField = "GLA_VAL";
                ddUnidade.DataSource = com.selectAllGlaCad("un");
                ddUnidade.DataBind();

                ddUnidade.SelectedValue = "";

                if (Request.QueryString["view"] != null)
                {
                    DataSet dsPesq = new DataSet();
                    dsPesq = com.searcCustomers(null, null, Request.QueryString["view"], "");

                    if (dsPesq != null)
                    {
                        txNam.Text = dsPesq.Tables[0].Rows[0][2].ToString();
                        txDc.Text = dsPesq.Tables[0].Rows[0][3].ToString();
                        txSap.Text = dsPesq.Tables[0].Rows[0]["sap"].ToString();

                        if(!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["unidade"].ToString()))
                            ddUnidade.SelectedValue = dsPesq.Tables[0].Rows[0]["unidade"].ToString();

                        if (!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["regional"].ToString()))
                            ddRegional.SelectedValue = dsPesq.Tables[0].Rows[0]["regional"].ToString();

                        /*if (!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["marca"].ToString()))
                            ddMarca.SelectedValue = dsPesq.Tables[0].Rows[0]["marca"].ToString();*/
                    }

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

                    Response.Redirect("/records/contract?view=" + Request.QueryString["view"].ToString());
                }
            }
            else
            {
                if (divCont.Visible)
                {
                    ddCY.Visible = true;
                    ddCultura.Visible = true;
                    ddTpContrato.Visible = true;
                }
            }

            Session["message"] = null;

            
        }

        protected void btPes_Click(object sender, EventArgs e)
        {

        }

        protected string getTypeContract()
        {
            string ret = "";

            /*<asp:ListItem Value="1">Contrato de Distribuição Comercial</asp:ListItem>
                                        <asp:ListItem Value="2">Termo de Participação no Programa de Vendas</asp:ListItem>
                                        <asp:ListItem Value="3">Carta de Quitação</asp:ListItem>
                                        <asp:ListItem Value="4">Contrato ATS</asp:ListItem>
                                        <asp:ListItem Value="5">Contratos Qion Implantação</asp:ListItem>
                                        <asp:ListItem Value="6">Contratos Qion Licenciamento</asp:ListItem>
                                        <asp:ListItem Value="7">Outros (Documentos Diversos)</asp:ListItem> */

            if (Eval("type_contract").ToString() == "1")
                ret = "Contrato de Distribuição Comercial";
            else if (Eval("type_contract").ToString() == "2")
                ret = "Termo de Participação no Programa de Vendas";
            else if (Eval("type_contract").ToString() == "3")
                ret = "Carta de Quitação";
            else if (Eval("type_contract").ToString() == "4")
                ret = "Contrato ATS";
            else if (Eval("type_contract").ToString() == "5")
                ret = "Contratos Qion Implantação";
            else if (Eval("type_contract").ToString() == "6")
                ret = "Contratos Qion Licenciamento";
            else if (Eval("type_contract").ToString() == "7")
                ret = "Outros (Documentos Diversos)";
            else if (Eval("type_contract").ToString() == "8")
                ret = "FCPA";
            else
                ret = "";

            return ret;
        }

        protected string getActions()
        {
            string create = "<a data-toggle='modal' data-placement='top' data-original-title='Novo Processo' href='contract?customer#modTipo' onclick='return clickTo(" + Eval("id") + ");' class='btn btn-success btn - xs fa fa-plus tooltips'></a>&nbsp;";
            string view = "<a href='/records/contract?view=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Processos do Cliente'></a>";

            if (Convert.ToInt64(Session["typelogin"]) > 2 )
                return view;
            else
                return create + view;
        }

        protected string getActionCont()
        {
            string view = "<a href='/records/process?id=" + Eval("id_client").ToString() + "&type=" + Eval("type_contract") + "&id_contract=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Contrato'></a>&nbsp;"; ;
            string delete = "<a href='/records/contract?view=" + Eval("id_client").ToString() + "&id_contract=" + Eval("id").ToString() + "&del=true' class='btn btn-danger btn - xs fa fa-trash-o tooltips tooltips confirmation' data-placement='top' data-original-title='Excluir Contrato'></a>&nbsp;";

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

                Response.Redirect("/records/process?new=true&id=" + id_customer + "&type=" + ddTipoCont.SelectedValue + "&id_contract=" + id_contract);
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText = ex.Message;
            }

        }
    }
}
 