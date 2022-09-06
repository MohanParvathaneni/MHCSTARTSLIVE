using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MHCStars
{
    public partial class ThankYou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAnother_Click(object sender, EventArgs e)
        {
            Int32 organizationId;
            Int32.TryParse(Request.QueryString["OrganizationId"], out organizationId);
            if (!(organizationId == 0))
            {
                Response.Redirect("./?OrganizationId=" + organizationId);
            }
        }
    }
}