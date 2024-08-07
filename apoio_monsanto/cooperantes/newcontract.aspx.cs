using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.IO;
using Util;
using System.Diagnostics;

namespace apoio_monsanto.cooperantes
{
    public partial class newcontract : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        Funcoes func = new Funcoes();
        protected System.Web.UI.HtmlControls.HtmlInputFile File;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit;
        string visualiza = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["coopvisual"] != null)
                visualiza = Session["coopvisual"].ToString();

            // fix para inicio do flux
            com.fixStartflux();

            if (Request.QueryString["id"] != null)
            {

                if(Request.QueryString["contact"] != null)
                {
                    com.deleteContactJoined(Request.QueryString["del_and"].ToString());

                    Response.Redirect("/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "");
                }

                if (Request.QueryString["del_and"] != null)
                {
                    com.deleteContact(Request.QueryString["del_and"].ToString());

                    Response.Redirect("/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "");
                }

                if (Request.QueryString["upd_and"] != null)
                {
                    com.updateContact(Request.QueryString["upd_and"].ToString());

                    Response.Redirect("/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "");
                }

                DataSet dsPesq = new DataSet();
                dsPesq = com.searcCustomers(null, null, Request.QueryString["id"], "");
                string userGr = "";
                string userRTV = "";

                // específicos para DISTRATO
                if(Request.QueryString["type"] == "4" || Request.QueryString["type"] == "5")
                {
                    // fluxo
                    btAnali.Text = "Análise do Distrato do Cooperante";
                    btElab.Text = "Elaboração do Distrato";
                    btEnvia.Text = "Enviado ao Cooperante";
                    btRes.Text = "Recebido do Cooperante";
                    btMon.Text = "Enviado a Monsanto";
                    btApr.Text = "Arquivado com AR";

                    // field text
                    lbDtContrato.InnerText = "Data do Distrato";
                    txObsCon.Attributes.Remove("placeholder");
                    txObsCon.Attributes.Add("placeholder", "Razão do Distrato");
                }

                if (! Page.IsPostBack)
                {
                    txDtFlux.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    if (dsPesq != null)
                    {
                        // populo os dropdown
                        ddGr.DataBind();
                        ddRTV.DataBind();

                        txName.InnerText = dsPesq.Tables[0].Rows[0][1].ToString();
                        txDoc.InnerText = dsPesq.Tables[0].Rows[0]["telefone"].ToString() + " | " + dsPesq.Tables[0].Rows[0]["email"].ToString();
                        userGr = dsPesq.Tables[0].Rows[0][3].ToString();
                        userRTV = dsPesq.Tables[0].Rows[0][4].ToString();
                        txDtContrato.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        try
                        {
                            dsPesq = com.retDistByRTV(userRTV);
                           
                            if (!String.IsNullOrEmpty(userGr))
                            {
                                if (com.valueValidation(userGr, 4))
                                {
                                    ddGr.SelectedValue = userGr;
                                    ddGr.Enabled = false;
                                }
                                else
                                {

                                }
                                
                            }
                        }
                        catch (System.IndexOutOfRangeException gr)
                        {

                            ddGr.Enabled = true;
                            ddGr.SelectedValue = "";
                            //error.Visible = true;
                            //error.InnerText = " Gerente da Planta não encontrado para o cliente. Ele pode ter sido removido ou não foi cadastrado!";
                        }

                        try
                        {
                            if (!String.IsNullOrEmpty(userRTV))
                            {
                                if (com.valueValidation(userRTV, 3))
                                {
                                    ddRTV.SelectedValue = userRTV;
                                    ddRTV.Enabled = false;
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

                }
            }

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"].ToString() == "3")
                {
                    divEspec.Visible = true;
                }
            }

            if (Request.QueryString["statflux"] != null)
            {
                if (updateStatusFlux(Request.QueryString["statflux"], Request.QueryString["currentstat"], Request.QueryString["dateflux"]));
                {
                    if (Request.QueryString["statflux"] == "4")
                    {
                        try
                        {
                            //error.Visible = true;
                            //error.InnerText = com.insertAndamento(Request.QueryString["id_contract"], "O Contrato nº: " + Request.QueryString["id_contract"] + "<br /> foi aprovado com ressalvas", DateTime.Now.ToString("dd/MM/yyyy"), "S", DateTime.Now.ToString("dd/MM/yyyy"));
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }


                    Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" +
                    Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"]);
                }

                //error.Visible = true;
                //error.InnerText = com.insertAndamento(Request.QueryString["id_contract"], "O Contrato nº: " + Request.QueryString["id_contract"] + "<br /> foi aprovado com ressalvas", DateTime.Now.ToString(), "S", DateTime.Now.ToString());

            }

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id_del"] != null)
                {
                    string path = Server.MapPath("Data");
                    string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                    com.deleteDoc(Request.QueryString["id_del"].ToString());
                    bool directoryExists = Directory.Exists(saveLocation);
                    if (directoryExists)
                        Directory.Delete(saveLocation, true);
                }

                if (Request.QueryString["id_contract"] != null && Request.QueryString["type"] != null)
                {
                    DataSet dsCont = new DataSet();
                    List<string> statList = new List<string>();
                    string safra = "";
                    string tpsafra = "";

                    try
                    {
                        statList = com.statusAtualContract(Request.QueryString["id_contract"]);

                        dsCont = com.selectContract(Request.QueryString["id_contract"], "").Copy();
                        txDtContrato.Text = Convert.ToDateTime(dsCont.Tables[0].Rows[0]["dtemis"].ToString()).ToString("dd/MM/yyyy");

                        tpsafra = dsCont.Tables[0].Rows[0]["tpsafra"].ToString();
                        if (!string.IsNullOrEmpty(tpsafra))
                            ddTpSafra.SelectedValue = tpsafra;

                        safra = dsCont.Tables[0].Rows[0]["safra"].ToString();
                        if (!string.IsNullOrEmpty(safra))
                            ddSafra.SelectedValue = safra;

                        // definido o tipo de documento
                        rbTpCont.SelectedValue = Request.QueryString["type"];

                        string unidade = dsCont.Tables[0].Rows[0]["unidade"].ToString();

                        //error.InnerText = unidade;

                        if (!string.IsNullOrEmpty(unidade))
                            ddUnidades.SelectedValue = unidade;

                        txObsCon.Text = dsCont.Tables[0].Rows[0]["obs"].ToString();
                        ddStatus.SelectedValue = statList[3];


                        // validação dos status do fluxo
                        // se está cancelado
                        if (statList[3] == "4")
                        {
                            btAnali.CssClass = "btn btn-danger flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-danger flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-danger flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-danger flux";
                            btRes.Enabled = false;
                            btMon.CssClass = "btn btn-danger flux";
                            btMon.Enabled = false;
                            btFis.CssClass = "btn btn-danger flux";
                            btFis.Enabled = false;
                            btApr.Text = "Cancelado";
                            btApr.CssClass = "btn btn-danger flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-danger flux";
                            //btDev.Enabled = false;

                            btGravar.Visible = false;
                        }

                        if(statList[4] != "1")
                        {
                            btRollBacks.Visible = true;
                        }

                        if(statList[4]=="2" && statList[3]=="1") // 2 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-warning flux";
                            btElab.Enabled = true;
                            btEnvia.CssClass = "btn btn-default flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-default flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;
                        }

                        // andamento
                        if (statList[4] == "3" && statList[3] == "1") // 3 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-warning flux";
                            btEnvia.Enabled = true;
                            btRes.CssClass = "btn btn-default flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;
                        }

                        if (statList[4] == "4" && statList[3] == "1") // 4 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-warning flux";
                            btRes.Enabled = true;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;
                        }

                        if (statList[4] == "5" && statList[3] == "1") // 5 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-warning flux";
                            btFis.Enabled = true;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;
                        }

                        if (statList[4] == "6" && statList[3] == "1") // 5 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-warning flux";
                            btMon.Enabled = true;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;
                        }

                        if (statList[4] == "7" && statList[3] == "1") // 5 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-success flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-warning flux";
                            btApr.Enabled = true;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;
                        }

                        /*if (statList[4] == "8" && statList[3] == "1") // 5 em andamento
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-success flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-success flux";
                            btApr.Enabled = true;
                            //btDev.CssClass = "btn btn-warning flux";
                            //btDev.Enabled = false;
                        }*/

                        if (statList[4] == "7" && statList[3] == "2") // 6 e aprovado
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-success flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-success flux";
                            btApr.Enabled = false;

                            btDev.CssClass = "btn btn-warning flux";
                            btDev.Enabled = true;

                            ddStatus.SelectedValue = "2";
                            ddStatus.Enabled = false;
                        }


                        // reprovado
                        if (statList[4] == "1" && statList[3] == "3") 
                        {
                            //lbAnaliDate.Text = com.retDateFromFlux("1", Request.QueryString["id_contract"], "2");
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-danger flux";
                            btAnali.Enabled = true;
                            btElab.CssClass = "btn btn-default flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-default flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-default flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "2" && statList[3] == "3")
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-danger flux";
                            btElab.Enabled = true;
                            btEnvia.CssClass = "btn btn-default flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-default flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "3" && statList[3] == "3")
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-danger flux";
                            btEnvia.Enabled = true;
                            btRes.CssClass = "btn btn-default flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "4" && statList[3] == "3")
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-danger flux";
                            btRes.Enabled = true;
                            btFis.CssClass = "btn btn-default flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "5" && statList[3] == "3")
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-danger flux";
                            btFis.Enabled = true;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "5" && statList[3] == "3")
                        {
                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-danger flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-default flux";
                            btMon.Enabled = true;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = false;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "6" && statList[3] == "3")
                        {

                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-danger flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-default flux";
                            btApr.Enabled = true;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        if (statList[4] == "7" && statList[3] == "3")
                        {

                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-success flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-danger flux";
                            btApr.Enabled = true;
                            //btDev.CssClass = "btn btn-default flux";
                            //btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }

                        /*if (statList[4] == "8" && statList[3] == "3")
                        {

                            // atualiza tela                            
                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-success flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-success flux";
                            btApr.Enabled = true;
                            btDev.CssClass = "btn btn-danger flux";
                            btDev.Enabled = false;

                            ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }*/

                        //aprovado
                        if (statList[4] == "8" && statList[3] == "2")
                        {

                            btAnali.CssClass = "btn btn-success flux";
                            btAnali.Enabled = false;
                            btElab.CssClass = "btn btn-success flux";
                            btElab.Enabled = false;
                            btEnvia.CssClass = "btn btn-success flux";
                            btEnvia.Enabled = false;
                            btRes.CssClass = "btn btn-success flux";
                            btRes.Enabled = false;
                            btFis.CssClass = "btn btn-success flux";
                            btFis.Enabled = false;
                            btMon.CssClass = "btn btn-success flux";
                            btMon.Enabled = false;
                            btApr.CssClass = "btn btn-success flux";
                            btApr.Enabled = false;

                            btDev.CssClass = "btn btn-success flux";
                            btDev.Enabled = false;

                            //ddStatus.SelectedValue = "3";
                            //ddStatus.Enabled = false;
                        }




                        lbAnaliDate.Text = com.retDateFromFlux("1", Request.QueryString["id_contract"], "2");
                        lbElabDate.Text = com.retDateFromFlux("2", Request.QueryString["id_contract"], "2");
                        lbEnviaDate.Text = com.retDateFromFlux("3", Request.QueryString["id_contract"], "2");
                        lbResDate.Text = com.retDateFromFlux("4", Request.QueryString["id_contract"], "2");
                        lbViaFisica.Text = com.retDateFromFlux("5", Request.QueryString["id_contract"], "2");
                        lbMonDate.Text = com.retDateFromFlux("6", Request.QueryString["id_contract"], "2");
                        lbAprDate.Text = com.retDateFromFlux("7", Request.QueryString["id_contract"], "2");
                        lbDevDate.Text = com.retDateFromFlux("8", Request.QueryString["id_contract"], "2");

                        lbAnaliDate.Text = !string.IsNullOrEmpty(lbAnaliDate.Text) ? Convert.ToDateTime(lbAnaliDate.Text).ToString("dd/MM/yyyy") : "";
                        lbElabDate.Text = !string.IsNullOrEmpty(lbElabDate.Text) ? Convert.ToDateTime(lbElabDate.Text).ToString("dd/MM/yyyy") : "";
                        lbEnviaDate.Text = !string.IsNullOrEmpty(lbEnviaDate.Text) ? Convert.ToDateTime(lbEnviaDate.Text).ToString("dd/MM/yyyy") : "";
                        lbResDate.Text = !string.IsNullOrEmpty(lbResDate.Text) ? Convert.ToDateTime(lbResDate.Text).ToString("dd/MM/yyyy") : "";
                        lbViaFisica.Text = !string.IsNullOrEmpty(lbViaFisica.Text) ? Convert.ToDateTime(lbViaFisica.Text).ToString("dd/MM/yyyy") : "";
                        lbMonDate.Text = !string.IsNullOrEmpty(lbMonDate.Text) ? Convert.ToDateTime(lbMonDate.Text).ToString("dd/MM/yyyy") : "";
                        lbAprDate.Text = !string.IsNullOrEmpty(lbAprDate.Text) ? Convert.ToDateTime(lbAprDate.Text).ToString("dd/MM/yyyy") : "";
                        lbDevDate.Text = !string.IsNullOrEmpty(lbDevDate.Text) ? Convert.ToDateTime(lbDevDate.Text).ToString("dd/MM/yyyy") : "";
                        
                        string lstR = dsCont.Tables[0].Rows[0]["documentos"].ToString();
                        string[] words = lstR.Split(',');
                        lstRight.Items.Clear();
                        foreach (string word in words)
                        {
                            if (string.IsNullOrEmpty(word))
                            {
                                lstRight.Items.Remove(word);
                                lstLeft.Items.Remove(word);
                            }
                            else
                            {
                                lstRight.Items.Add(word);
                                lstLeft.Items.Remove(word);
                            }

                        }

                        ddRTV.SelectedValue = dsCont.Tables[0].Rows[0]["tecnico"].ToString();
                        ddGr.SelectedValue = dsCont.Tables[0].Rows[0]["gerente"].ToString();
                        ddSuper.SelectedValue = dsCont.Tables[0].Rows[0]["supervisor"].ToString();
                        txNumContBase.Text = dsCont.Tables[0].Rows[0]["num_cont_base"].ToString();
                        txNumContMae.Text = dsCont.Tables[0].Rows[0]["num_cont_mae"].ToString();
                        txFazenda.Text = dsCont.Tables[0].Rows[0]["fazenda"].ToString();
                        txDFazenda.Text = dsCont.Tables[0].Rows[0]["end_fazenda"].ToString();
                        txArea.Text = dsCont.Tables[0].Rows[0]["area"].ToString().Replace(",", "").Replace(".", ",");
                        txIncra.Text = dsCont.Tables[0].Rows[0]["inscricao"].ToString();
                        txDesagio.Text = dsCont.Tables[0].Rows[0]["desagio"].ToString().Replace(",", "").Replace(".", ",");
                        txTeto.Text = dsCont.Tables[0].Rows[0]["teto"].ToString().Replace(",", "").Replace(".", ",");
                        txMatricula.Text = dsCont.Tables[0].Rows[0]["matricula"].ToString();

                        if (! string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["dt_ini_pla"].ToString()))
                        {
                            txDtIni.Text = Convert.ToDateTime(dsCont.Tables[0].Rows[0]["dt_ini_pla"].ToString()).ToString("dd/MM/yyyy");
                        }


                        if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["dt_fim_pla"].ToString()))
                        {
                            txDtFim.Text = Convert.ToDateTime(dsCont.Tables[0].Rows[0]["dt_fim_pla"].ToString()).ToString("dd/MM/yyyy");
                        }

                        if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["data_folha_rosto"].ToString()))
                        {
                            txDataFolha.Text = Convert.ToDateTime(dsCont.Tables[0].Rows[0]["data_folha_rosto"].ToString()).ToString("dd/MM/yyyy");
                        }

                        ddUrgente.SelectedValue = dsCont.Tables[0].Rows[0]["urgente"].ToString();


                        txGarantiaS.Text = dsCont.Tables[0].Rows[0]["garantias"].ToString().Replace(",", "").Replace(".", ",");
                        txGarantiaR.Text = dsCont.Tables[0].Rows[0]["garantiar"].ToString().Replace(",", "").Replace(".", ",");
                    }
                    catch (Exception ex)
                    {
                        error.Visible = true;
                        //var lineNumber = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                        error.InnerText = "Error (1): " + ex.Message;
                    }

                }



            }
            else
            {
                error.Visible = false;
                error.InnerText = "";
            }

            /*error.Visible = true;
               error.InnerText += "postabacked";*/
  
            if (Request.QueryString["acomp"] != null)
            {
                btAcomp.Visible = true;
            }


            if (visualiza == "S")
            {
                btAnali.Enabled = false;
                btElab.Enabled = false;
                btEnvia.Enabled = false;
                btRes.Enabled = false;
                btMon.Enabled = false;
                btApr.Enabled = false;

                btGravar.Visible = false;
                btUp.Visible = false;
                btAcomp.Visible = false;
            }

            if(string.IsNullOrEmpty(txDtDocUpd.Text))
            {
                txDtDocUpd.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            //error.Visible = true;
        }
        protected void btNewAnda_Click(object sender, EventArgs e)
        {
            try
            {
                if (txNewAnda.Text != "" && txNewDtAnda.Text != "")
                {
                    string lembrar = (cbLembAnd.Checked ? "S" : "N");

                    if (lembrar == "S" && String.IsNullOrEmpty(txDtNewLemb.Text))
                    {
                        error.Visible = true;
                        error.InnerText = "A data do lembrete precisa ser informada";
                    }
                    else
                    {
                        error.InnerText = com.insertAndamento(Request.QueryString["id_contract"], txNewAnda.Text, txNewDtAnda.Text, lembrar, txDtNewLemb.Text);
                        gvAnda.DataBind();
                    }



                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText = ex.Message;
            }
        }

        protected void btNewCont_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> statList = com.statusAtualContract(Request.QueryString["id_contract"]);
                error.InnerText = com.insertContactJoined(int.Parse(Request.QueryString["id_contract"].ToString()),
                    int.Parse(ddContact.SelectedValue), int.Parse(statList[4]), txDtContact.Text);
                gvContactoPrev.DataBind();
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText = ex.Message;
            }
        }

        public string getAndAction(string type)
        {
            if(type == "anda")
            {
                string delete = "<a href='/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&del_and=" + Eval("id").ToString() + "' class='btn btn-danger btn - xs fa fa-trash-o tooltips confirmation'  data-original-title='Excluir Andamento' ></a>&nbsp;";
                string confirm = "<a href='/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&upd_and=" + Eval("id").ToString() + "' class='btn btn-success btn - xs fa fa fa-check-square tooltips'  data-original-title='Marcar como Lido' ></a>&nbsp;";

                return confirm + delete;
            }

            if(type == "cont")
            {
                string delete = "<a href='/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&del_and=" + Eval("id_contact_joined").ToString() + "&contact=true' class='btn btn-danger btn - xs fa fa-trash-o tooltips confirmation'  data-original-title='Excluir Contato' ></a>&nbsp;";

                return delete;
            }

            return "";
            
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void btGrava_Click(object sender, EventArgs e)
        {
            List<String> TableFields = new List<string>();
            List<String> TableValues = new List<string>();
            String id_contract = Request.QueryString["id_contract"].ToString();

            try
            {
                // cancelar contrato
                if(ddStatus.SelectedValue == "4")
                {
                    /*
                        retList.Add(dsValue.Tables[0].Rows[d]["ID_REGISTRO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["NOME_FLUXO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["DATA_FLUXO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["STATUS_FLUXO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["ID_FLUXO"].ToString());*/

                    List<string> statList = new List<string>();
                    statList = com.statusAtualContract(id_contract);

                    TableValues.Add(statList[4]);
                    TableValues.Add(id_contract);
                    TableValues.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                    TableValues.Add(ddStatus.SelectedValue);

                    com.Main_CUD("insert", "COOP_FLUX_DATA", null, TableValues, null, null, false);
                }
                DataSet dsVal = new DataSet();
                bool cont = true;

                error.InnerText = "Instance Data! ";

                if (cont)
                {
                    string rightSelectedItems = Request.Form[lstRight.UniqueID];
                    string safra = ddSafra.SelectedValue;
                    string unidade = ddUnidades.SelectedValue;

                    if (unidade.Contains("Selecione"))
                        unidade = "";

                    if (safra == "- Ano - ")
                        safra = "";                  

                    error.Visible = true;
                    error.InnerText += "Updating Data! ";
                    error.InnerText += com.updateContract(id_contract,
                        txDtContrato.Text,
                        ddTpSafra.SelectedValue,
                        safra,
                        unidade,
                        txObsCon.Text,
                        rightSelectedItems,
                        ddRTV.SelectedValue,
                        ddGr.SelectedValue,
                        txNumContBase.Text,
                        txNumContMae.Text,
                        txFazenda.Text,
                        txDFazenda.Text,
                        txArea.Text.Replace(".", "").Replace(",","."),
                        txIncra.Text,
                        txDesagio.Text.Replace(".", "").Replace(",", "."),
                        txTeto.Text.Replace(".", "").Replace(",", "."),
                        txDtIni.Text,
                        txDtFim.Text,
                        txGarantiaS.Text.Replace(".", "").Replace(",", "."),
                        txGarantiaR.Text.Replace(".", "").Replace(",", "."),
                        txMatricula.Text,
                        ddSuper.SelectedValue,
                        rbTpCont.SelectedValue,
                        txDataFolha.Text,
                        ddUrgente.SelectedValue);

                    if( ! error.InnerText.Contains("Erro"))
                    {
                        error.InnerText += "Dados salvos com sucesso!";
                        Response.Redirect("/cooperantes/contract?view=" + Request.QueryString["id"]);
                    }                 
                                    

                    /*if (txStatus != "")
                    {
                        if (txStatus == "2")
                        {
                            string nameContract = com.retNameCont(id_contract);
                            string htmlMail = @"<table>
                                                <tr>
                                                    <td colspan='2' style='background-color:#C6702B;'><strong>Atualização Monsanto</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan='2'>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan='2'>
                                                        O contrato <strong><a href='https://www.apoioflc.com.br?id={3}&type={4}&id_contract={5}'>{0}</a></strong> foi {1} por <strong>{2}</strong>.<br />
                                                        Para mais informações clique no contrato ou entre em contato com a equipe de apoio.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan='2'>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan='2'>
                                                        <span style='font-size:10px'>E-mail automático. Por favor, não responda. Powered by Apoio Monsanto</span>
                                                    </td>
                                                </tr>
                                                <tr>
    	                                            <td colspan='2' style='background-color:#005135'>&nbsp;</td>
                                                </tr>
                                            </table>";

                            string htmlSub = "Atualização Contrato {0} - {1}";
                            string stat = "";

                            if (txStatus == "1")
                                stat = "APROVADO";
                            else
                                stat = "REPROVADO";

                            htmlMail = String.Format(htmlMail, nameContract, stat, Session["namelogin"].ToString(), dbAcess.Encrypt(Request.QueryString["id"]), dbAcess.Encrypt(Request.QueryString["type"]), dbAcess.Encrypt(id_contract));
                            htmlSub = String.Format(htmlSub, nameContract, stat);

                            try
                            {
                                if (RTV != "")
                                {
                                    error.Visible = true;
                                    error.InnerText = "Enviando Email para RTV...";
                                    com.sentEmail(com.retUserMail(RTV), htmlMail, htmlSub);
                                    //error.Visible = false;
                                }

                                if (GR != "")
                                {
                                    error.Visible = true;
                                    error.InnerText = "Enviando Email para GR...";
                                    com.sentEmail(com.retUserMail(GR), htmlMail, htmlSub);
                                    //error.Visible = false;
                                }
                            }
                            catch (Exception ex)
                            {

                                error.Visible = true;
                                error.InnerText = "Erro ao enviar dados (UPD): " + ex.Message;
                            }
                           
                        }
                    }*/

                    //string path = Server.MapPath("data");
                    //com.memowrit(path + "/arquivo_log.txt", error.InnerText);
                }

            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText += "Erro ao salvar dados (UPD): " + ex.Message + Environment.NewLine;
                error.InnerText += ex.InnerException + Environment.NewLine;
                error.InnerText += ex.Source;
            }
        }
        protected void btCanc_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/contract?view=" + Request.QueryString["id"]);
        }

        protected void btRollBack_Click(object sender, EventArgs e)
        {
            com.rollbackFlux(Request.QueryString["id_contract"]);
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"]);
        }

        protected void btAcomp_Click(object sender, EventArgs e)
        {
            com.updateProgress(Request.QueryString["acomp"]);
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"]);
        }

        override protected void OnInit(EventArgs e)
        {
            // 
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            // 
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btUp.ServerClick += new System.EventHandler(this.btUp_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=report_andamento.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvAnda.AllowPaging = false;
                gvAnda.DataBind();

                gvAnda.RenderControl(hw);
                Response.Write(sw.ToString());

                Response.End();
            }

        }

        private void btUp_ServerClick(object sender, System.EventArgs e)
        {
            if ((txUp.PostedFile != null) && (txUp.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(txUp.PostedFile.FileName);
                string archiveName = "";
                if (rbTpCont.SelectedValue != "")
                {
                    archiveName = rbTpCont.SelectedItem.Text;
                }
                else if (rbTpCont.SelectedValue != "")
                {
                    archiveName = rbTpCont.SelectedItem.Text;
                }
                else
                {
                    archiveName = txUp.PostedFile.FileName;
                }
                string path = Server.MapPath("data");
                string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                try
                {
                    Directory.CreateDirectory(saveLocation);
                    txUp.PostedFile.SaveAs(saveLocation + "\\" + fn);
                    com.insertDocument(Request.QueryString["id_contract"].ToString(), Request.QueryString["id"].ToString(), archiveName, "\\cooperantes\\data\\" + Request.QueryString["id_contract"].ToString() + "\\" + fn, txDtDocUpd.Text, txDocObs.Text);
                    gvDoc.DataBind();
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerText = "Erro ao realizar o upload do arquivo! Sistema indisponível";
                    //Note: Exception.Message returns a detailed message that describes the current exception. 
                    //For security reasons, we do not recommend that you return Exception.Message to end users in 
                    //production environments. It would be better to return a generic error message. 
                }
            }
            else
            {
                error.Visible = true;
                error.InnerText = "Selecione um arquivo";
            }
        }

        public string getAction()
        {
            string delete = "<a href='/cooperantes/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&id_del=" + Eval("id").ToString() + "#docs' class='btn btn-danger btn - xs fa fa-trash-o tooltips confirmation'  data-original-title='Excluir Documento'></a>&nbsp;";
            string view = "<a href='" + Eval("path").ToString() + "' target='_blank' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Documento'></a>&nbsp;";
            return delete + view;
        }

        public bool testeAllTextBoxes()
        {
            // valida se tá tudo preenchido
            /*foreach (TextBox dr in this.Page.Master.FindControl("cont_pages").Controls.OfType<TextBox>())
            {
                if (String.IsNullOrEmpty(dr.Text))
                {
                    error.Visible = true;
                    error.InnerText = "Todos os campos são obrigatórios quando o contrato está aprovado.";
                    return false;
                }
                    
            }*/

            return true;
        }

        protected void btAnali_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=1&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);

        }

        protected void btElab_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=2&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }

        protected void btEnvia_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=3&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }

        protected void btRes_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=4&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }

        protected void btFis_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=5&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }

        protected void btMon_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=6&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }

        protected void btApr_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=7&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }

        public bool updateStatusFlux(string status, string currentstat, string dateflux)
        {
            List<string> statList = new List<string>();
            List<string> TableValues = new List<string>();
            List<string> TableField = new List<string>();
            string id_contract = Request.QueryString["id_contract"];
            statList = com.statusAtualContract(id_contract);

            /*
            retList.Add(dsValue.Tables[0].Rows[d]["ID_REGISTRO"].ToString());
            retList.Add(dsValue.Tables[0].Rows[d]["NOME_FLUXO"].ToString());
            retList.Add(dsValue.Tables[0].Rows[d]["DATA_FLUXO"].ToString());
            retList.Add(dsValue.Tables[0].Rows[d]["STATUS_FLUXO"].ToString());
            retList.Add(dsValue.Tables[0].Rows[d]["ID_FLUXO"].ToString());
            */

            //aprovacao de status 8
            if(status == "8")
            {
                insertStatFlux("8");

                statList = com.statusAtualContract(id_contract);


                com.updateStatusFlux(statList[0], "2", dateflux);

                com.updateFluxContract(id_contract, "8");

                return true;

            }

            if(currentstat == "1")
            {
                error.Visible = true;
                error.InnerText = "É necessário definir o status do fluxo atual antes de confirmar";
                return false;
            }

            if (currentstat == "4")
            {
                error.Visible = true;
                error.InnerText = "Para cancelar o contrato, selecione a opção CONTRATO CANCELADO e clique em SALVAR ALTERAÇÕES!";
                return false;
            }

            /*TableField.Add("id_flux");
            TableField.Add("id_contract");
            TableField.Add("dateupd");
            TableField.Add("stat");

            TableValues.Add(status.ToString());
            TableValues.Add(id_contract);
            TableValues.Add(DateTime.Now.ToString("dd/MM/yyyy"));
            TableValues.Add(ddStatus.SelectedValue);*/

            try
            {
                //com.Main_CUD("update", "COOP_FLUX_DATA", TableField, TableValues, "id", statList[0], false);
                com.updateStatusFlux(statList[0], currentstat, dateflux);
                com.updateFluxContract(id_contract, statList[4]);
                if (statList[4] != "7" && currentstat != "3")
                {
                    // insere novo registro no controle
                    insertStatFlux((Convert.ToInt32(statList[4]) + 1).ToString());
                }
                
            }
            catch (Exception e)
            {

                error.Visible = true;
                error.InnerText = e.Message;
                return false;
            }            

            return true;
        }

        protected bool insertStatFlux(string id_flux)
        {
            List<string> statList = new List<string>();
            List<string> TableValues = new List<string>();
            List<string> TableFields = new List<string>();
            string id_contract = Request.QueryString["id_contract"];

            TableFields.Add("id_flux");
            TableFields.Add("id_contract");
            TableFields.Add("stat");

            TableValues.Add(id_flux);
            TableValues.Add(id_contract);
            TableValues.Add("1");

            com.Main_CUD("insert", "COOP_FLUX_DATA", TableFields, TableValues, null, null, false);

            return true;
        }

        protected void btDev_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cooperantes/process?new=true&id=" + Request.QueryString["id"] + "&type=" + Request.QueryString["type"] + "&id_contract=" + Request.QueryString["id_contract"] + "&statflux=8&currentstat=" + ddStatus.SelectedValue + "&dateflux=" + txDtFlux.Text);
        }
    }
}
