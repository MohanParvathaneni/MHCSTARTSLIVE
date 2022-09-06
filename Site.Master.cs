using MHCStars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MHCStars
{
    public partial class SiteMaster : MasterPage
    {
        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            //string h = HttpContext.Current.User.Identity.Name;
            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string strURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            string strAppPath = HttpContext.Current.Request.ApplicationPath.ToString();

            if (strAppPath == "/")
            {
                strAppPath = string.Empty;
            }

            bool developers = HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers);
            //bool overAllAdmins = HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin);

            lblName.Text = _user.DisplayName;

            //Get admin tab
            //if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin))
            //if (developers || overAllAdmins)
            if (developers)
            {
                HtmlGenericControl _li = new HtmlGenericControl("li");
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", strURL + strAppPath + "/Views/Admin/Index.aspx");
                anchor.InnerText = "Admin";
                _li.Controls.Add(anchor);
                phMenu.Controls.Add(_li);

                _li = new HtmlGenericControl("li");
                anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", strURL + strAppPath + "/Views/Admin/Reportlisting.aspx");
                anchor.InnerText = "Report";
                _li.Controls.Add(anchor);
                phMenu.Controls.Add(_li);
            }

        }
    }
}