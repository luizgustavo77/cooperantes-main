using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace apoio_monsanto.cooperantes
{
    public partial class contract : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        string id_customer = "";
        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            error.InnerText = "";
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();

            if (!Page.IsPostBack)
            {
                try
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
                    }
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerText = ex.Message;
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
            string create = "<a data-toggle='modal' data-placement='top' data-original-title='Novo Processo' href='/cooperantes/contract?customer#modTipo' onclick='return clickTo(" + Eval("id") + ");' class='btn btn-success btn - xs fa fa-plus tooltips'></a>&nbsp;";
            string view = "<a href='/cooperantes/contract?view=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Processos do Cliente'></a>";

            if(visualiza == "S")
                return view;
            else
                return create + view;

        }

        protected string getActionCont()
        {
            string view = "<a href='/cooperantes/process?id=" + Eval("id_coop").ToString() + "&type=" + Eval("type") + "&id_contract=" + Eval("id").ToString() + "' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Contrato'></a>&nbsp;"; ;
            string delete = "<a href='/cooperantes/contract?view=" + Eval("id_coop").ToString() + "&id_contract=" + Eval("id").ToString() + "&del=true' class='btn btn-danger btn - xs fa fa-trash-o tooltips tooltips confirmation' data-placement='top' data-original-title='Excluir Contrato'></a>&nbsp;";

            if (visualiza == "S")
                return view;
            else
                return view + delete;
        }

        protected void btNew_Cont_Click(object sender, EventArgs e)
        {
            List<String> TableFields = new List<string>();
            List<String> TableValues = new List<string>();

            try
            {
                /*
                    id
                    id_coop
                    dtemis
                    tpsafra
                    safra
                    unidade
                    obs
                    documentos
                    tecnico
                    gerente
                    num_cont_base
                    num_cont_mae
                    fazenda
                    end_fazenda
                    area
                    inscricao
                    desagio
                    dt_ini_pla
                    dt_fim_pla
                    garantiar
                    garantias
                */

                id_customer = hidden.Text;
                String id_contract = "";
                com.insertContract(ddTipoCont.SelectedValue, id_customer, ref id_contract, Session["coopunidade"].ToString());
                com.insertStartFlux(id_contract);
               
                /*TableFields.Add("id_flux");
                TableFields.Add("id_contract");
                TableFields.Add("dateupd");
                TableFields.Add("stat");

                TableValues.Add("1");
                TableValues.Add(id_contract);
                TableValues.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                TableValues.Add("1");

                com.Main_CUD("insert", "COOP_FLUX_DATA", TableFields, TableValues, null, null, false);*/
                // inclui registro de pagamentos
                //error.Visible = true;
                //error.InnerText = com.Main_CUD("insert", "COOP_PROCESS", null, TableValues, "", "", false);

                Response.Redirect("/cooperantes/process?new=true&id=" + id_customer + "&type=" + ddTipoCont.SelectedValue + "&id_contract=" + id_contract);
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText = ex.Message;
            }

        }

        public string getNameCont()
        {
            return com.selectTipoProcessoBase(Eval("type").ToString(), "") != null ? com.selectTipoProcessoBase(Eval("type").ToString(), "").Tables[0].Rows[0]["name"].ToString() : "";
        }

        public string getEtapaCont()
        {
            /*
                1	Análise da Documentação do Cooperante
                2	Elaboração do Contrato
                3	Enviado para Assinatura do Cooperante
                4	Aprovado Com Ressalvas
                5	Enviado a Monsanto
                6	Aprovado*/
            coopcom com = new coopcom();

            if (com.vldContractCanceled(Eval("id").ToString()))
            {
                return "Documento Cancelado";
            }

            if (Eval("etapa").ToString() == "1")
                return "Recebimento da Folha de Rosto";
            else if (Eval("etapa").ToString() == "2")
                return "Análise e Elaboração do Contrato";
            else if (Eval("etapa").ToString() == "3")
                return "Enviado para Assinatura do Cooperante";
            else if (Eval("etapa").ToString() == "4")
                return "Aprovado Com Ressalvas";
            else if (Eval("etapa").ToString() == "5")
                return "Recebimento de Via Física";
            else if (Eval("etapa").ToString() == "6")
                return "Enviado a Monsanto";
            else if (Eval("etapa").ToString() == "7")
                return "Aprovado";
            else if (Eval("etapa").ToString() == "8")
                return "Devolvido ao Cooperante";
            else
                return "";

        }

        public string getSafraCont()
        {
            /*
                V Verão
                I Inverno*/

            if (Eval("tpsafra").ToString() == "V")
                return "Verão";
            else if (Eval("tpsafra").ToString() == "I")
                return "Inverno";
            else
                return "";

        }
    }
}