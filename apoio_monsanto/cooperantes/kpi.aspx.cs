using System;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace apoio_monsanto.cooperantes
{
    public partial class kpi : System.Web.UI.Page
    {
        coopcom com = new coopcom();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsEtapas = new DataSet();

           
            if (!Page.IsPostBack)
            {
                //
            }

        }

        protected int kpi3;
        protected int kpi4;
        protected int kpi14;
        protected int kpi22;
        protected int kpi30;
        protected int kpi31;

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
            gvKPI.DataSourceID = "obKPI";
            //com.retDataToKPIGraph(ref kpi3, ref kpi4, ref kpi14, ref kpi22, ref kpi30, ref kpi31);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ExportToExcel2(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=report_apoio_kpi_cooperantes.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvKPI.AllowPaging = false;
                gvKPI.DataBind();

                gvKPI.RenderControl(hw);
                Response.Write(sw.ToString());

                Response.End();
            }

        }

        protected string getTime(int sequence)
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
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 6, 7);
            else if (sequence == 7)
            {
                retInfo = com.getTimeFluxById(int.Parse(Eval("CONTRACT").ToString()), 1, 7);
            }
                
            
            return retInfo;
        }

    }
}