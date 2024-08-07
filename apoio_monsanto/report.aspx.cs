using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Configuration;

namespace apoio_monsanto
{
    public partial class report : System.Web.UI.Page
    {
        commom com = new commom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ddSafra.DataBind();
                ddSafra.SelectedValue = "";
            }
                
            // categorias de acordo com a safra
            String catType = catGraph(ddSafra.SelectedValue);

            // preencho primeiro os aprovados no período
            DataSet dsAproved = new DataSet();

            // aproved
            retDataGraph(com.selectContAproved(ddGR.SelectedValue, ddRTV.SelectedValue, ddSafra.SelectedValue,
                ddRegional.SelectedValue, txDc.Text, ddTipoCont.SelectedValue, ddTpDoc.SelectedValue), catType, ref aaproved);

            // reproved
            retDataGraph(com.selectContReproved(ddGR.SelectedValue, ddRTV.SelectedValue, ddSafra.SelectedValue,
                ddRegional.SelectedValue, txDc.Text, ddTipoCont.SelectedValue, ddTpDoc.SelectedValue), catType, ref areproved);

            // sent
            retDataGraph(com.selectContAll(ddGR.SelectedValue, ddRTV.SelectedValue, ddSafra.SelectedValue,
                ddRegional.SelectedValue, txDc.Text, ddTipoCont.SelectedValue, ddTpDoc.SelectedValue), catType, ref asent);

            if (Session["typelogin"] != null)
            {
                if (Session["typelogin"].ToString() == "3")
                {
                    ddRTV.SelectedValue = Session["namelogin"].ToString();
                    ddRTV.Attributes.Add("disabled", "disabled");
                }
                if (Session["typelogin"].ToString() == "4")
                {
                    ddGR.SelectedValue = Session["namelogin"].ToString();
                    ddGR.Attributes.Add("disabled", "disabled");
                }
            }
        }

        protected string[] acategorie;
        protected int[] aaproved = new int[12];
        protected int[] areproved = new int[12];
        protected int[] asent = new int[12];

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
            
        }

        public String catGraph(string safra)
        {
            if(!string.IsNullOrEmpty(safra))
            {
                string[] splitedSafra = safra.Split('/');
                if (splitedSafra[0] == splitedSafra[1])
                {
                    acategorie = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
                    return "Safra";
                }
                else
                {
                    acategorie = new string[] { "Setembro", "Outubro", "Novembro", "Dezembro", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto" };
                    return "Entre-Safra";
                }
            }
            else
            {
                acategorie = new string[] { "Setembro", "Outubro", "Novembro", "Dezembro", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto" };
                return "Sem Safra Definida";
            }
            
        }

        public void retDataGraph (DataSet data, String catType, ref int[] iVal)
        {
            int[] auxVal = new int[12];
            for (int i = 0; i <= 11; i++)
            {
                auxVal[i] = 0;
            }

            if (data != null)
            {
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    auxVal[Convert.ToInt16(data.Tables[0].Rows[i][0]) - 1] = Convert.ToInt16(data.Tables[0].Rows[i][1]);
                }

                // de acordo com os meses do ano partindo de setembro a agosto
                iVal[0] = auxVal[8];
                iVal[1] = auxVal[9];
                iVal[2] = auxVal[10];
                iVal[3] = auxVal[11];
                iVal[4] = auxVal[0];
                iVal[5] = auxVal[1];
                iVal[6] = auxVal[2];
                iVal[7] = auxVal[3];
                iVal[8] = auxVal[4];
                iVal[9] = auxVal[5];
                iVal[10] = auxVal[6];
                iVal[11] = auxVal[7];
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
            Response.AddHeader("content-disposition", "attachment;filename=report_apoio_monsanto.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvDetail.AllowPaging = false;
                gvDetail.DataBind();

                gvDetail.RenderControl(hw);
                Response.Write(sw.ToString());

                Response.End();
            }

        }
    }
}