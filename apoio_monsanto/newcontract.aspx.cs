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

namespace apoio_monsanto
{
    public partial class newcontract : System.Web.UI.Page
    {
        commom com = new commom();
        protected System.Web.UI.HtmlControls.HtmlInputFile File;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                DataSet dsPesq = new DataSet();
                dsPesq = com.searcCustomers(null, null, Request.QueryString["id"], "");
                string userGr = "";
                string userRTV = "";

                if (! Page.IsPostBack)
                {
                    if (dsPesq != null)
                    {
                        txName.InnerText = dsPesq.Tables[0].Rows[0][1].ToString();
                        txDoc.InnerText = dsPesq.Tables[0].Rows[0][2].ToString();                      

                        try
                        {
                            if (!String.IsNullOrEmpty(userGr))
                            {
                                if (com.valueValidation(userGr, 4))
                                {
                                    ddGR.SelectedValue = userGr;
                                    //ddGR.Enabled = false;
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
                                    //ddRTV.Enabled = false;
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

                }
            }

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"].ToString() != "5")
                {
                    divEspec.Visible = true;
                }
                //divEspec.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id_del"] != null)
                {
                    string path = Server.MapPath("Data");
                    string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                    com.deleteDoc(Request.QueryString["id_del"].ToString());
                    bool directoryExists = Directory.Exists(saveLocation);
                    //if (directoryExists)
                        //Directory.Delete(saveLocation, true);
                }

                if (Request.QueryString["id_contract"] != null && Request.QueryString["type"] != null)
                {
                    DataSet dsCont = new DataSet();

                    dsCont = com.selectContract(Request.QueryString["id_contract"].ToString());

                    //SELECT id, type_contract, id_client, ISNULL(CONVERT(VARCHAR(10),dt_receb,120),'') dt_receb, safra, obs, status, 
                    //ISNULL(CONVERT(VARCHAR(10),dt_status,120),'') dt_status, user_conf, criteria, id_user_rtv, id_user_gr, 
                    //ISNULL(CONVERT(VARCHAR(10),dt_digital,120),'') dt_digital, ISNULL(CONVERT(VARCHAR(10),dt_archive,120),'') dt_archive, 
                    //ISNULL(CONVERT(VARCHAR(10),dt_approv,120),'') dt_approv, keeper

                    if (dsCont.Tables.Count > 0)
                    {
                        bool readOnly = false;
                        if (Convert.ToInt64(Session["typelogin"]) > 2)
                            readOnly = true;
                        try
                        {

                            ddRTV.Enabled = true;
                            if( ! string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["id_user_rtv"].ToString()))
                                ddRTV.SelectedValue = dsCont.Tables[0].Rows[0]["id_user_rtv"].ToString();

                            ddGR.Enabled = true;
                            if( ! string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["id_user_gr"].ToString()))
                                ddGR.SelectedValue = dsCont.Tables[0].Rows[0]["id_user_gr"].ToString();

                            txDtRec.Text = dsCont.Tables[0].Rows[0]["dt_receb"].ToString();
                            if (!String.IsNullOrEmpty(txDtRec.Text))
                                txDtRec.Text = Convert.ToDateTime(txDtRec.Text).ToString("dd/MM/yyyy");
                            txDtRec.ReadOnly = readOnly;

                            string safra = dsCont.Tables[0].Rows[0]["safra"].ToString();
                            if(!string.IsNullOrEmpty(safra))
                                ddSafra.SelectedValue = safra;

                            txObsCon.Text = dsCont.Tables[0].Rows[0]["obs"].ToString();
                            txObsCon.ReadOnly = readOnly;

                            ddStatus.SelectedValue = dsCont.Tables[0].Rows[0]["status"].ToString();

                            txDtStatus.Text = dsCont.Tables[0].Rows[0]["dt_status"].ToString();
                            if (!String.IsNullOrEmpty(txDtStatus.Text))
                                txDtStatus.Text = Convert.ToDateTime(txDtStatus.Text).ToString("dd/MM/yyyy");
                            txDtStatus.ReadOnly = readOnly;

                            txConfPor.Text = Session["namelogin"].ToString();
                            txConfPor.ReadOnly = readOnly;

                            string lstR = dsCont.Tables[0].Rows[0]["criteria"].ToString();
                            string[] words = lstR.Split(',');
                            lstRight.Items.Clear();
                            foreach (string word in words)
                            {
                                lstRight.Items.Add(word);
                                lstLeft.Items.Remove(word);
                            }

                            if (readOnly)
                            {
                                lstRight.Attributes["class"] += " aspNetDisabled";
                                lstLeft.Attributes["class"] += " aspNetDisabled";
                            }

                            //ddRTV.SelectedValue = dsCont.Tables[0].Rows[0]["id_user_rtv"].ToString();
                            //ddGR.SelectedValue = dsCont.Tables[0].Rows[0]["id_user_gr"].ToString();

                            txDtDigit.Text = dsCont.Tables[0].Rows[0]["dt_digital"].ToString();
                            if (!String.IsNullOrEmpty(txDtDigit.Text))
                                txDtDigit.Text = Convert.ToDateTime(txDtDigit.Text).ToString("dd/MM/yyyy");
                            txDtDigit.ReadOnly = readOnly;

                            txArqui.Text = dsCont.Tables[0].Rows[0]["dt_archive"].ToString();
                            if (!String.IsNullOrEmpty(txArqui.Text))
                                txArqui.Text = Convert.ToDateTime(txArqui.Text).ToString("dd/MM/yyyy");
                            txArqui.ReadOnly = readOnly;

                            txAprov.Text = dsCont.Tables[0].Rows[0]["dt_approv"].ToString();
                            if (!String.IsNullOrEmpty(txAprov.Text))
                                txAprov.Text = Convert.ToDateTime(txAprov.Text).ToString("dd/MM/yyyy");
                            txAprov.ReadOnly = readOnly;

                            txCKeepers.Text = dsCont.Tables[0].Rows[0]["keeper"].ToString();
                            txCKeepers.ReadOnly = readOnly;

                            rbTpProd.SelectedValue = dsCont.Tables[0].Rows[0]["type_doc_prdsem"].ToString();

                            string tptermo = dsCont.Tables[0].Rows[0]["term"].ToString();
                            if (!string.IsNullOrEmpty(tptermo))
                                rbTpTermo.SelectedValue = tptermo;

                            txDtContrato.Text = dsCont.Tables[0].Rows[0]["dt_contrato"].ToString();
                            txVigencia.Text = dsCont.Tables[0].Rows[0]["vigencia"].ToString();
                            txRsVolTotal.Text = dsCont.Tables[0].Rows[0]["rs_vol_total"].ToString();
                            txRsVolTestadaMais.Text = dsCont.Tables[0].Rows[0]["rs_vol_testada_mais"].ToString();
                            txRsVolTestadaMenos.Text = dsCont.Tables[0].Rows[0]["rs_vol_testada_menos"].ToString();
                            txBxCredito.Text = dsCont.Tables[0].Rows[0]["baixa_credito"].ToString();
                            txFixacao.Text = dsCont.Tables[0].Rows[0]["fixacao"].ToString();
                            txRsValorFixado.Text = dsCont.Tables[0].Rows[0]["rs_valor_fixado"].ToString();
                            txRsValorOutrosPartic.Text = dsCont.Tables[0].Rows[0]["rs_vol_outros_partic"].ToString();
                            txValorTaxas.Text = dsCont.Tables[0].Rows[0]["valor_taxas"].ToString();
                            txBonusSemestral.Text = dsCont.Tables[0].Rows[0]["bonus_semestral"].ToString();
                            txReajuste.Text = dsCont.Tables[0].Rows[0]["reajuste"].ToString();
                            txValorAdiantamento.Text = dsCont.Tables[0].Rows[0]["rs_valor_adiantamento"].ToString();

                            area_produtor_hectares.Text = dsCont.Tables[0].Rows[0]["area_produtor_hectares"].ToString();
                            valor_hectare.Text = dsCont.Tables[0].Rows[0]["valor_hectare"].ToString();

                            txUp.Disabled = readOnly;
                            btUp.Disabled = readOnly;

                            btGravar.Disabled = readOnly;
                        }
                        catch (Exception ex)
                        {

                            error.InnerText = "Erro ao salvar dados: " + ex.Message;
                        }

                    }

                }
            }
            else
            {
                error.Visible = false;
                error.InnerText = "";
            }
        }
        protected void btGrava_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsVal = new DataSet();
                String id_contract = Request.QueryString["id_contract"].ToString();
                bool cont = true;
                dsVal = com.selectDocument(id_contract);
                if ((dsVal == null || dsVal.Tables.Count == 0) && ddStatus.SelectedValue != "2")
                {
                    error.Visible = true;
                    error.InnerText = "É necessário fazer o upload dos documentos antes de salvar o processo!";
                    cont = false;
                    divUp.Attributes["class"] += " focusedInput";
                    divUp.Focus();
                }

                string tpProd = "";
                if (rbTpProd.SelectedValue == "" && Request.QueryString["type"] != null)
                {
                    if (Request.QueryString["type"].ToString() == "3")
                    { 
                        error.Visible = true;
                        error.InnerText = "Para o tipo de contrato informado, é necessário selecionar um tipo de documento!";
                        rbTpProd.Focus();
                        cont = false;
                    }
                }
                else
                {
                    tpProd = rbTpProd.SelectedValue;
                }
                /*if(cont)
                {
                    if (ddStatus.SelectedValue == "1" && Request.QueryString["type"].ToString() != "5")
                        if (!testeAllTextBoxes())
                            cont = false;

                }*/

                if (cont)
                {
                    string rightSelectedItems = Request.Form[lstRight.UniqueID];
                    lstRight.Items.Clear();
                    if (!string.IsNullOrEmpty(rightSelectedItems))
                    {
                        foreach (string item in rightSelectedItems.Split(','))
                        {
                            lstRight.Items.Add(item);
                        }
                    }

                    //dt_receb = '{0}', safra = '{1}', obs = '{2}', status = '{3}', dt_status = '{4}', user_conf = '{5}', criteria = '{6}', id_user_rtv = '{7}', id_user_gr = '{8}', dt_digital = '{9}', dt_archive = '{10}', dt_approv = '{11}', keeper = '{12}'";
                    String txSafra = "";

                    if (!ddSafra.SelectedValue.Contains("Safra"))
                    {
                        txSafra = ddSafra.SelectedValue;
                    }

                    String txStatus = "";
                    if (!ddStatus.SelectedValue.Contains("Status"))
                    {
                        txStatus = ddStatus.SelectedValue;
                    }

                    String RTV = "";
                    if (ddRTV.SelectedValue != "")
                    {
                        RTV = ddRTV.SelectedValue;
                    }

                    String GR = "";
                    if (ddGR.SelectedValue != "")
                    {
                        GR = ddGR.SelectedValue;
                    }

                    DataSet dsConsAnt = new DataSet();
                    DataSet dsConsDep = new DataSet();

                    error.Visible = true;
                    error.InnerText = com.updateContract(id_contract, txDtRec.Text,
                       txSafra, txObsCon.Text,
                       txStatus,
                       txDtStatus.Text, txConfPor.Text, rightSelectedItems,
                       RTV, GR,
                       txDtDigit.Text, txArqui.Text, txAprov.Text,
                       txCKeepers.Text, Session["login"].ToString(), tpProd, rbTpTermo.SelectedValue,
                       txDtContrato.Text, txVigencia.Text, txRsVolTotal.Text, txRsVolTestadaMais.Text, txRsVolTestadaMenos.Text,
                       txBxCredito.Text, txFixacao.Text, txRsValorFixado.Text, txRsValorOutrosPartic.Text, txValorTaxas.Text, txBonusSemestral.Text, 
                       txReajuste.Text, txValorAdiantamento.Text, area_produtor_hectares.Text, valor_hectare.Text);


                    if (txStatus != "")
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
                                    //com.sentEmail(com.retUserMail(RTV), htmlMail, htmlSub);
                                    //error.Visible = false;
                                }

                                if (GR != "")
                                {
                                    error.Visible = true;
                                    error.InnerText = "Enviando Email para GR...";
                                    //com.sentEmail(com.retUserMail(GR), htmlMail, htmlSub);
                                    //error.Visible = false;
                                }
                            }
                            catch (Exception ex)
                            {

                                error.Visible = true;
                                error.InnerText = "Erro ao enviar dados (UPD): " + ex.Message;
                            }
                           
                        }
                    }


                    error.Visible = false;

                    //string path = Server.MapPath("data");
                    //com.memowrit(path + "/arquivo_log.txt", error.InnerText);
                }

            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerText = "Erro ao salvar dados (UPD): " + ex.Message;
            }
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

        private void btUp_ServerClick(object sender, System.EventArgs e)
        {
            if ((txUp.PostedFile != null) && (txUp.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(txUp.PostedFile.FileName);
                string archiveName = "";
                bool tpProd = false;

                if (rbTpProd.SelectedValue != "")
                {
                    archiveName = rbTpProd.SelectedItem.Text;
                    tpProd = true;
                }
                else
                {
                    archiveName = txUp.PostedFile.FileName;
                }

                if ( rbTpTermo.SelectedValue != "" )
                {
                    if(tpProd)
                        archiveName = rbTpProd.SelectedItem.Text + " - " + rbTpTermo.SelectedItem.Text;
                    else
                        archiveName = rbTpTermo.SelectedItem.Text;
                }
                
                string path = Server.MapPath("Data");
                string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                try
                {
                    Directory.CreateDirectory(saveLocation);
                    txUp.PostedFile.SaveAs(saveLocation + "\\" + fn);
                    com.insertDocument(Request.QueryString["id_contract"].ToString(), Request.QueryString["id"].ToString(), archiveName, "\\data\\" + Request.QueryString["id_contract"].ToString() + "\\" + fn);
                    com.valDocCont(Request.QueryString["id_contract"].ToString(), Path.GetFileNameWithoutExtension(archiveName));
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
            string delete = "<a href='/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&id_del=" + Eval("id").ToString() + "#docs' class='btn btn-danger btn - xs fa fa-trash-o tooltips confirmation'  data-original-title='Excluir Documento'></a>&nbsp;";
            string view = "<a href='" + Eval("path").ToString() + "' target='_blank' class='btn btn-primary btn - xs fa fa-eye tooltips' data-placement='top' data-original-title='Ver Documento'></a>&nbsp;";
            if (Convert.ToInt64(Session["typelogin"]) > 2)
                return view;
            else
                return view + delete;
        }

        public bool testeAllTextBoxes()
        {
            // valida se tá tudo preenchido
            foreach (TextBox dr in this.Page.Master.FindControl("cont_pages").Controls.OfType<TextBox>())
            {
                if (String.IsNullOrEmpty(dr.Text))
                {
                    error.Visible = true;
                    error.InnerText = "Todos os campos são obrigatórios quando o contrato está aprovado.";
                    return false;
                }
                    
            }

            return true;
        }

    }
}
