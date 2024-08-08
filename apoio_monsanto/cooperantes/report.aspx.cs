using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apoio_monsanto.cooperantes
{
    public partial class report : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected decimal totFolha;
        protected decimal totAnal;
        protected decimal totReceb;
        protected decimal totEnv;
        protected decimal totRes;
        protected decimal totMon;
        protected decimal totApr;
        protected decimal percFolha;
        protected decimal percAnal;
        protected decimal percReceb;
        protected decimal percEnv;
        protected decimal percRes;
        protected decimal percMon;
        protected decimal percApr;
        protected string labelSafra;
        protected string labelAnoSafra;

        public static class JavaScript
        {
            public static string Serialize(object o)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(o);
            }
        }
        protected void btPes_Click(object sender, EventArgs e)
        {
            DataSet dsEtapas = new DataSet();
            Decimal totalPizza = 1;

            labelSafra = (ddTpSafra.SelectedValue == "V") ? "Verão" : "Inverno";
            labelAnoSafra = ddSafra.SelectedValue;

            string etapa = "";
            decimal total = 0;
            string contractType = "";

            foreach (ListItem listItem in ddTipoCont.Items)
            {
                if (listItem.Selected && listItem.Value != "" && listItem.Value != "0")
                {
                    contractType += "'" + listItem.Value + "',";
                }
            }

            if (contractType != "")
                contractType = contractType.Substring(0, (contractType.Length - 1));

            totalPizza = com.retTotalToPizza(contractType, ddUnidade.SelectedValue, txDc.Text, ddEtapa.SelectedValue, txNam.Text, ddTpSafra.SelectedValue, ddSafra.SelectedValue, Convert.ToBoolean(ddCanc.SelectedValue));

            try
            {
                dsEtapas = com.retTotByEtapa(contractType, ddUnidade.SelectedValue, txDc.Text, ddEtapa.SelectedValue, txNam.Text, ddTpSafra.SelectedValue, ddSafra.SelectedValue, Convert.ToBoolean(ddCanc.SelectedValue));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (dsEtapas != null)
            {
                totFolha = 0;
                totAnal = 0;
                totReceb = 0;
                totEnv = 0;
                totRes = 0;
                totMon = 0;
                totApr = 0;

                percFolha = 0;
                percAnal = 0;
                percReceb = 0;
                percEnv = 0;
                percRes = 0;
                percMon = 0;
                percApr = 0;

                for (int i = 0; i < dsEtapas.Tables[0].Rows.Count; i++)
                {
                    etapa = dsEtapas.Tables[0].Rows[i]["ETAPA"].ToString();
                    total = decimal.Parse(dsEtapas.Tables[0].Rows[i]["TOTAL"].ToString());

                    if (etapa == "1")
                    {
                        percFolha = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totFolha = total;
                    }
                    else if (etapa == "2")
                    {
                        percAnal = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totAnal = total;

                    }
                    else if (etapa == "3")
                    {
                        percEnv = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totEnv = total;
                    }
                    else if (etapa == "4")
                    {
                        percRes = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totRes = total;

                    }
                    else if (etapa == "5")
                    {
                        percReceb = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totReceb = total;
                    }
                    else if (etapa == "6")
                    {
                        percMon = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totMon = total;
                    }
                    else if (etapa == "7")
                    {
                        percApr = (total > 0) ? Math.Round(((total / totalPizza) * 100), 2) : 0;
                        totApr = total;
                    }


                }
            }

            try
            {
                DataSet dsReport = new DataSet();
                dsReport = com.selectProcessToReport(contractType, ddUnidade.SelectedValue, txDc.Text, ddEtapa.SelectedValue, txNam.Text, Session["coopunidade"].ToString(),
                    Convert.ToBoolean(ddCanc.SelectedValue), ddTpSafra.SelectedValue, ddSafra.SelectedValue, contrato_mae.Text, contrato_base.Text);

                if (dsReport != null)
                {
                    gvDetail.DataSource = dsReport;
                    gvDetail.DataBind();
                }
            }
            catch (Exception)
            {

                gvDetail.DataSource = null;
                gvDetail.DataBind();
            }


        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=report_cooperantes.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvDetail.AllowPaging = false;
                //gvDetail.DataBind();

                gvDetail.RenderControl(hw);
                Response.Write(sw.ToString());

                Response.End();
            }
        }

        protected string getTempo(int sequence)
        {
            string retInfo = "";
            if (sequence == 1)
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 1, 2);
            else if (sequence == 2)
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 2, 3);
            else if (sequence == 3)
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 3, 4);
            else if (sequence == 4)
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 4, 5);
            else if (sequence == 5)
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 5, 6);
            else if (sequence == 6)
            {
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 1, 6);
            }


            return retInfo;
        }

        public void memowrit(string path, string data)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                sw.Write(data);
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = e.Row.Cells[5];

                string path = HttpContext.Current.Server.MapPath("data");
                memowrit(path + "/cell5", cell.Text);
                memowrit(path + "/cell2", e.Row.Cells[5].Text);

                string status = (cell.Text);
                if (status == "Aprovado")
                {
                    cell.BackColor = Color.Green;
                    cell.ForeColor = Color.White;
                }
                else if (status == "Enviado para Assinatura do Cooperante")
                {
                    cell.BackColor = Color.Red;
                    cell.ForeColor = Color.White;
                }
                else if (status == "Aprovado Com Ressalvas")
                {
                    cell.BackColor = Color.Yellow;
                }
                else if (status == "Enviado a Monsanto")
                {
                    cell.BackColor = Color.Blue;
                    cell.ForeColor = Color.White;
                }
                else if (status.Contains("Recebimento"))
                {
                    cell.BackColor = Color.HotPink;
                    //cell.ForeColor = Color.White;
                }
                else if (status.Contains("Elabora"))
                {
                    cell.BackColor = Color.Gray;
                    //cell.ForeColor = Color.White;
                }

                //cell.
            }
        }
    }
}