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

namespace apoio_monsanto.gla
{
    public partial class newcontract : System.Web.UI.Page
    {
        newmom com = new newmom();
        protected System.Web.UI.HtmlControls.HtmlInputFile File;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit;
        protected void Page_Load(object sender, EventArgs e)
        {
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

                ddUnidade.DataTextField = "GLA_VAL";
                ddUnidade.DataValueField = "GLA_VAL";
                ddUnidade.DataSource = com.selectAllGlaCad("un");
                ddUnidade.DataBind();

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
                            //userGr = dsPesq.Tables[0].Rows[0][3].ToString();
                            //userRTV = dsPesq.Tables[0].Rows[0][4].ToString();
                            if (!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["regional"].ToString()))
                            {
                                ddRegional.SelectedValue = dsPesq.Tables[0].Rows[0]["regional"].ToString();
                            }

                            if (!string.IsNullOrEmpty(dsPesq.Tables[0].Rows[0]["unidade"].ToString()))
                            {
                                ddUnidade.SelectedValue = dsPesq.Tables[0].Rows[0]["unidade"].ToString();
                            }
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

                            txDtDigit.Text = dsCont.Tables[0].Rows[0]["dt_digital"].ToString();

                            txDtRec.Text = dsCont.Tables[0].Rows[0]["dt_contract"].ToString();

                            txCKeepers.Text = dsCont.Tables[0].Rows[0]["keeper"].ToString();

                            if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["cultura"].ToString()))
                                ddCultura.SelectedValue = dsCont.Tables[0].Rows[0]["cultura"].ToString();

                            if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["distrito"].ToString()))
                                ddMarca.SelectedValue = dsCont.Tables[0].Rows[0]["distrito"].ToString();
                            else
                                ddMarca.SelectedValue = "";

                            if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["regional"].ToString()))
                                ddRegional.SelectedValue = dsCont.Tables[0].Rows[0]["regional"].ToString();

                            if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["unidade"].ToString()))
                                ddUnidade.SelectedValue = dsCont.Tables[0].Rows[0]["unidade"].ToString();

                            if (!string.IsNullOrEmpty(dsCont.Tables[0].Rows[0]["cy"].ToString()))
                                ddCY.SelectedValue = dsCont.Tables[0].Rows[0]["cy"].ToString();
                            else
                                ddCY.SelectedValue = "";

                            txConfPor.Text = dsCont.Tables[0].Rows[0]["user_conf"].ToString();

                            txObsCon.Text = dsCont.Tables[0].Rows[0]["obs"].ToString();

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

        protected void loadMarca ()
        {
            if(ddCultura.SelectedValue.ToLower() == "milho e sorgo")
            {
                ddMarca.Items.Clear();
                ddMarca.Items.Add("AGROCERES");
                ddMarca.Items.Add("DEKALB");
                ddMarca.Items.Add("AGROESTE");
                ddMarca.Items.Add("MONSANTO");
                ddMarca.DataBind();
            }
            else if (ddCultura.SelectedValue.ToLower() == "crop")
            {
                ddMarca.Items.Clear();
                ddMarca.Items.Add("CROP");
                ddMarca.DataBind();
            }
            else if (ddCultura.SelectedValue.ToLower() == "soja")
            {
                ddMarca.Items.Clear();
                ddMarca.Items.Add("SOJA");
                ddMarca.DataBind();
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
                if ((dsVal == null || dsVal.Tables.Count == 0))
                {
                    error.Visible = true;
                    error.InnerText = "É necessário fazer o upload dos documentos antes de salvar o processo!";
                    cont = false;
                    divUp.Attributes["class"] += " focusedInput";
                    divUp.Focus();
                }

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

                    tableFields.Add("cy");
                    tableValues.Add(ddCY.SelectedValue);

                    tableFields.Add("obs");
                    tableValues.Add(txObsCon.Text);

                    tableFields.Add("cultura");
                    tableValues.Add(ddCultura.SelectedValue);

                    tableFields.Add("user_conf");
                    tableValues.Add(txConfPor.Text);

                    tableFields.Add("unidade");
                    tableValues.Add(ddUnidade.SelectedValue);

                    tableFields.Add("regional");
                    tableValues.Add(ddRegional.SelectedValue);

                    tableFields.Add("distrito");
                    tableValues.Add(ddMarca.SelectedValue);

                    if (!string.IsNullOrEmpty(txDtDigit.Text))
                    {
                        tableFields.Add("dt_digital");
                        tableValues.Add(txDtDigit.Text);
                    }

                    if (!string.IsNullOrEmpty(txDtRec.Text))
                    {
                        tableFields.Add("dt_contract");
                        tableValues.Add(txDtRec.Text);
                    }

                    tableFields.Add("keeper");
                    tableValues.Add(txCKeepers.Text);

                    com.Main_CUD("update", "GLA_CONTRACT", tableFields, tableValues, "id", id_contract, false);

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
                string archiveName = fn;
                bool tpProd = false;
                
                string path = Server.MapPath("data");
                string saveLocation = path + "\\" + Request.QueryString["id_contract"].ToString();

                try
                {
                    Directory.CreateDirectory(saveLocation);
                    txUp.PostedFile.SaveAs(saveLocation + "\\" + fn);
                    com.insertDocument(Request.QueryString["id_contract"].ToString(), Request.QueryString["id"].ToString(), archiveName, "\\gla\\data\\" + Request.QueryString["id_contract"].ToString() + "\\" + fn);
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
            string delete = "<a href='/gla/process?id=" + Request.QueryString["id"].ToString() + "&id_contract=" + Request.QueryString["id_contract"].ToString() + "&type=" + Request.QueryString["type"] + "&id_del=" + Eval("id").ToString() + "#docs' class='btn btn-danger btn - xs fa fa-trash-o tooltips confirmation'  data-original-title='Excluir Documento'></a>&nbsp;";
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
