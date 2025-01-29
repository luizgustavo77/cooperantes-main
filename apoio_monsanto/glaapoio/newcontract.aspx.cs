using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto.glaapoio
{
    public partial class newcontract : System.Web.UI.Page
    {
        newmom com = new newmom();
        protected System.Web.UI.HtmlControls.HtmlInputFile File;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["namelogin"] != null)
            {
                txConfPor.Text = Session["namelogin"]?.ToString();
            }

            if (Request.QueryString["type"] != null)
            {
                /*if (Request.QueryString["type"].ToString() == "3")
                {
                    divEspec.Visible = true;
                }*/
                //divEspec.Visible = true;
            }

            //loadMarca();

            if (Page.IsPostBack)
            {

            }
            else if (!Page.IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    DataSet dsPesq = new DataSet();
                    dsPesq = com.searcCustomers(null, null, Request.QueryString["id"], "");
                    string userGr = "";
                    string userRTV = "";

                    if (!Page.IsPostBack)
                    {
                        if (dsPesq != null)
                        {
                            txName.InnerText = dsPesq.Tables[0].Rows[0]["name"].ToString();
                            txDoc.InnerText = dsPesq.Tables[0].Rows[0]["document"].ToString();
                        }

                    }
                }
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

                            data_recebimento.Text = dsCont.Tables[0].Rows[0]["dt_digital"].ToString();

                            data_contrato.Text = dsCont.Tables[0].Rows[0]["dt_contract"].ToString();

                            //data_arquivamento.Text = dsCont.Tables[0].Rows[0]["dt_archive"].ToString();

                            caixa.Text = dsCont.Tables[0].Rows[0]["keeper"].ToString();

                            ddStatus.SelectedValue = string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["status"].ToString()) ? "" : dsCont.Tables[0].Rows[0]["status"].ToString();

                            if (!string.IsNullOrWhiteSpace(dsCont.Tables[0].Rows[0]["user_conf"].ToString()))
                            {
                                txConfPor.Text = dsCont.Tables[0].Rows[0]["user_conf"].ToString();
                            }

                            string lstR = dsCont.Tables[0].Rows[0]["obs"].ToString();
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

                            try
                            {
                                txtAcordo.Text = dsCont.Tables[0].Rows[0]["numero_acordo"].ToString();

                                ddlTipoAcordo.SelectedValue = string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["tipo_acordo"].ToString()) ? "" : dsCont.Tables[0].Rows[0]["tipo_acordo"].ToString();

                            }
                            catch (Exception)
                            {
                                //faz nada so tratando devido a subida nova, pode ser removido no futuro.
                            }

                            btGravar.Disabled = readOnly;
                        }
                        catch (Exception ex)
                        {
                            error.Visible = true;
                            error.InnerText = "Erro ao carregar dados: " + ex.Message;
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

        protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddStatus.SelectedValue == "A")
            {
                rfvCaixa.Enabled = true;
                caixa.Enabled = true;
                caixa.BackColor = Color.White;

                lstLeft.Enabled = false;
                lstLeft.BackColor = Color.LightGray;
                lstRight.Enabled = false;
                lstRight.BackColor = Color.LightGray;

                rfvTipoAcordo.Enabled = true;
            }
            else
            {
                rfvCaixa.Enabled = false;
                caixa.Enabled = false;
                caixa.BackColor = Color.LightGray;
                caixa.Text = string.Empty;

                lstLeft.Enabled = true;
                lstLeft.BackColor = Color.White;
                lstRight.Enabled = true;
                lstRight.BackColor = Color.White;

                rfvTipoAcordo.Enabled = false;
            }
        }

        protected void btGrava_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {

                    DataSet dsVal = new DataSet();
                    String id_contract = Request.QueryString["id_contract"].ToString();
                    bool cont = true;
                    /*dsVal = com.selectDocument(id_contract);
                    if ((dsVal == null || dsVal.Tables.Count == 0))
                    {
                        error.Visible = true;
                        error.InnerText = "É necessário fazer o upload dos documentos antes de salvar o processo!";
                        cont = false;
                        divUp.Attributes["class"] += " focusedInput";
                        divUp.Focus();
                    }*/

                    if (cont)
                    {
                        //dt_receb = '{0}', safra = '{1}', obs = '{2}', status = '{3}', dt_status = '{4}', user_conf = '{5}', criteria = '{6}', id_user_rtv = '{7}', id_user_gr = '{8}', dt_digital = '{9}', dt_archive = '{10}', dt_approv = '{11}', keeper = '{12}'";
                        DataSet dsConsAnt = new DataSet();
                        DataSet dsConsDep = new DataSet();

                        error.Visible = true;
                        error.InnerText = "";/*com.updateContract(id_contract, txDtRec.Text,
                       txSafra, txObsCon.Text,
                       "",
                       "", txConfPor.Text, rightSelectedItems,
                       RTV, GR,
                       txDtDigit.Text, "", "",
                       txCKeepers.Text, Session["login"].ToString(), tpProd, rbTpTermo.SelectedValue);*/

                        List<String> tableFields = new List<string>();
                        List<String> tableValues = new List<string>();

                        tableFields.Add("type_contract");
                        tableValues.Add(Request.QueryString["type"].ToString());


                        string rightSelectedItems = Request.Form[lstRight.UniqueID];
                        lstRight.Items.Clear();
                        if (!string.IsNullOrEmpty(rightSelectedItems))
                        {
                            foreach (string item in rightSelectedItems.Split(','))
                            {
                                lstRight.Items.Add(item);
                            }
                        }

                        tableFields.Add("obs");
                        tableValues.Add(rightSelectedItems);

                        tableFields.Add("user_conf");
                        tableValues.Add(txConfPor.Text);

                        if (!string.IsNullOrEmpty(data_recebimento.Text))
                        {
                            tableFields.Add("dt_digital");
                            tableValues.Add(data_recebimento.Text);
                        }

                        if (!string.IsNullOrEmpty(data_contrato.Text))
                        {
                            tableFields.Add("dt_contract");
                            tableValues.Add(data_contrato.Text);
                        }

                        /*if (!string.IsNullOrEmpty(data_arquivamento.Text))
                        {
                            tableFields.Add("dt_archive");
                            tableValues.Add(data_arquivamento.Text);
                        }*/

                        tableFields.Add("keeper");
                        tableValues.Add(caixa.Text);

                        tableFields.Add("status");
                        tableValues.Add(ddStatus.SelectedValue);



                        tableFields.Add("numero_acordo");
                        tableValues.Add(txtAcordo.Text);

                        tableFields.Add("tipo_acordo");
                        tableValues.Add(ddlTipoAcordo.SelectedValue);

                        com.Main_CUD("update", "NEW_CONTRACT", tableFields, tableValues, "id", id_contract, false);

                        error.Visible = false;

                        //string path = Server.MapPath("data");
                        //com.memowrit(path + "/arquivo_log.txt", error.InnerText);
                    }
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

            if (Session["namelogin"] != null)
            {
                txConfPor.Text = Session["namelogin"]?.ToString();
            }
        }

        private void btUp_ServerClick(object sender, System.EventArgs e)
        {
            if ((txUp.PostedFile != null) && (txUp.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(txUp.PostedFile.FileName);
                string archiveName = fn;
                bool tpProd = false;

                string path = Server.MapPath("data");
                string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                try
                {
                    Directory.CreateDirectory(saveLocation);
                    txUp.PostedFile.SaveAs(saveLocation + "\\" + fn);
                    com.insertDocument(Request.QueryString["id_contract"].ToString(), Request.QueryString["id"].ToString(), archiveName, "\\glaapoio\\data\\" + Request.QueryString["id_contract"].ToString() + "\\" + fn);
                    //com.valDocCont(Request.QueryString["id_contract"].ToString(), Path.GetFileNameWithoutExtension(archiveName));
                    gvDoc.DataBind();
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerText = "Error while upload file: " + ex.Message;
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

        public string getAct()
        {
            string delete = "<a href='/glaapoio/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&id_del=" + Eval("id").ToString() + "#docs' class='btn btn-danger btn - xs fa fa-trash-o tooltips confirmation'  data-original-title='Excluir Documento'></a>&nbsp;";
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
