using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WinetechReports.ReportViewer
{
  public partial class ReportViewer : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report1.rdlc");
      ReportViewer1.LocalReport.Refresh();
    }
  }
}