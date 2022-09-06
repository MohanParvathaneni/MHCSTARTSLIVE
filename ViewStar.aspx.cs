using MHCStars.Classes;
using MHCStars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace MHCStars
{
    public partial class ViewStar : System.Web.UI.Page
    {
        private MHCC_StarsEntities1 _db = new MHCC_StarsEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 starSentId;
            Int32.TryParse(Request.QueryString["StarSentId"], out starSentId);
            phStarSentIdNotDefined.Visible = starSentId == 0;
            phBody.Visible = starSentId != 0;

            if (starSentId != 0)
            {
                var qry = from x in _db.StarSents where (x.StarSentID == starSentId) select x;
                if (qry.Any())
                {
                    foreach (var item in qry)
                    {
                        lblRecipient.Text = item.RecieverName;
                        lblYourAStar.Text = " - You're a Star!!";
                        lblComments.Text = item.Comment;
                        lblName.Text = string.Format("Thank you {0}", item.SenderName);
                        lblManager.Text = item.ManagerName;
                    }
                }
                else
                {
                    string myMessage = string.Format("Could not find the starSentId {0}", starSentId);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myMessage + "');", true);
                }
            }
        }
    }
}