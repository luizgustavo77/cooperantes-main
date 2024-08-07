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

namespace apoio_monsanto.gla
{
    public partial class report : System.Web.UI.Page
    {
        newmom gla = new newmom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ddCY.DataTextField = "GLA_VAL";
                ddCY.DataValueField = "GLA_VAL";
                ddCY.DataSource = gla.selectAllGlaCad("cy");
                ddCY.DataBind();

                ddCY.SelectedValue = "";
            }
        }
        protected void btPes_Click(object sender, EventArgs e)
        {

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=report_gla.xls");
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