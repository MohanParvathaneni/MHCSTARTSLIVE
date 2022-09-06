using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MHCStars.Entities;
using MHCStars.Classes;
using System.Net.Mail;
using System.Text;

namespace MHCStars
{
    public partial class _Default : Page
    {
        private MHCC_StarsEntities1 _db = new MHCC_StarsEntities1();
        private MHCC_EMailEntities _dbEmail = new MHCC_EMailEntities();
        private MHCC_ADEntities _dbAD = new MHCC_ADEntities();

        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        private EmailMessage msg = new EmailMessage();

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 dllOrganizationId;
            Int32.TryParse(ddlOrganization.SelectedValue, out dllOrganizationId);
            if (dllOrganizationId > 1)
            {
                Response.Redirect(@"./?OrganizationId=" + dllOrganizationId.ToString());
            }
            Int32 organizationId;
            Int32.TryParse(Request.QueryString["OrganizationId"], out organizationId);
            if (organizationId == 0)
            {
                var qry = from x in _db.Organizations where (x.Active == true) select x;
                if (qry.Any())
                {
                    List<ListItem> organizationList = new List<ListItem>();
                    foreach (var item in qry)
                    {
                        organizationList.Add(new ListItem(item.OrganizationName, item.OrganizationId.ToString()));
                    }
                    List<ListItem> organizationListSorted = organizationList.Distinct().OrderBy(x => x.Text).ToList();
                    ddlOrganization.Items.AddRange(organizationListSorted.ToArray());
                    ddlOrganization.DataBind();
                }

                //Have them chose from drop down list the organization
                //Hide the main body till they choose the location
                phBody.Visible = false;
                phChooseOrganization.Visible = true;

                if (ddlOrganization.SelectedItem.Text == "Choose One")
                {

                }
                else
                {
                    phBody.Visible = true;
                    phChooseOrganization.Visible = false;
                }

            }

            
            else if (organizationId > 0)
            {
                lblName.Text = (!(string.IsNullOrWhiteSpace(_user.DisplayName))) ? _user.DisplayName : string.Format("{0}, {1} {2}", _user.LastName, _user.FirstName, _user.MiddleName);

                var qry = from x in _db.OrganizationOrangizationADGroups where (x.OrganizationId == organizationId) select x;
                if (qry.Any())
                {
                    List<ListItem> managerList = new List<ListItem>();
                    foreach (var item in qry)
                    {
                        //Fill in Manager Listing
                        if (!(string.IsNullOrWhiteSpace(item.OrganizationADGroup.ADGroupName)))
                        {
                            var myManagerList = McUser.GetUsersFromGroup(item.OrganizationADGroup.ADGroupName);
                            if (myManagerList.Any())
                            {
                                foreach (var manager in myManagerList)
                                {
                                    if (string.IsNullOrWhiteSpace(manager.DisplayName))
                                    {
                                        managerList.Add(new ListItem(string.Format("{0}, {1} {2}", manager.LastName, manager.FirstName, manager.MiddleName), manager.Username));
                                    }
                                    else
                                    {
                                        managerList.Add(new ListItem(manager.DisplayName, manager.Username));
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Put up message OrganizaitonManagerADGroup needs to be defined
                            string myMessage = "The organizationADGroup is not defined in the AD.";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myMessage + "');", true);
                        }
                    }
                    List<ListItem> managerListSorted = managerList.Distinct().OrderBy(x => x.Text).ToList();
                    ddlManager.Items.AddRange(managerListSorted.ToArray());
                }
                var qryOrg = from x in _db.Organizations where (x.OrganizationId == organizationId) select x;
                if (qryOrg.Any())
                {
                    foreach (var item in qryOrg)
                    {
                        //Fill in Receiver Listing
                        if (!(string.IsNullOrWhiteSpace(item.OrganizationOU)))
                        {
                            Int32 myOULength = item.OrganizationOU.Length;

                            List<ListItem> recipientList = new List<ListItem>();
                            var qryAD = from x in _dbAD.Colleagues where x.DistinguishedName.Substring((x.DistinguishedName.Length - myOULength), myOULength) == item.OrganizationOU select x;
                            if (qryAD.Any())
                            {
                                foreach (var user in qryAD)
                                {
                                    if (string.IsNullOrWhiteSpace(user.DisplayName))
                                    {
                                        recipientList.Add(new ListItem(string.Format("{0}, {1} {2}", user.LastName, user.FirstName, user.MiddleName), user.Username));
                                    }
                                    else
                                    {
                                        recipientList.Add(new ListItem(user.DisplayName, user.Username));
                                    }
                                }
                            }
                            List<ListItem> recipientListSort = recipientList.Distinct().OrderBy(x => x.Text).ToList();
                            ddlRecipient.Items.AddRange(recipientListSort.ToArray());
                            ddlRecipient.DataBind();
                        }
                        else
                        {
                            //Put up message OrganizaitonOU needs to be defined
                            string myMessage = "The organizationOU is not defined in the Organization table.";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myMessage + "');", true);
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(ddlOrganization.SelectedItem.Text))
            {
                isValid = false;
                lblError.Text = "Organization name can not be blank.";
            }

            if (string.IsNullOrWhiteSpace(ddlRecipient.SelectedItem.Text))
            {
                isValid = false;
                lblError.Text = "Recipient can not be blank.";
            }

            if (string.IsNullOrWhiteSpace(ddlManager.SelectedItem.Text))
            {
                isValid = false;
                lblError.Text = "Manager can not be blank.";
            }

            if (string.IsNullOrWhiteSpace(tbComments.Text))
            {
                isValid = false;
                if (string.IsNullOrWhiteSpace(lblError.Text))
                {
                    lblError.Text = "Comments Text can not be blank.";
                }
                else
                {
                    lblError.Text = lblError.Text + "<br/>Comments can not be blank.";
                }
            }

            //Insert record
            if (isValid)
            {
                Int32 organizationId;
                Int32.TryParse(Request.QueryString["OrganizationId"], out organizationId);

                McUser recipient = new McUser().GetUserByUsername(ddlRecipient.SelectedItem.Value);
                McUser manager = new McUser().GetUserByUsername(ddlManager.SelectedItem.Value);

                string recipientName = (!(string.IsNullOrWhiteSpace(recipient.DisplayName))) ? recipient.DisplayName : string.Format("{0}, {1} {2}", recipient.LastName, recipient.FirstName, recipient.MiddleName);
                string managerName = (!(string.IsNullOrWhiteSpace(manager.DisplayName))) ? manager.DisplayName : string.Format("{0}, {1} {2}", manager.LastName, manager.FirstName, manager.MiddleName);
                string senderName = (!(string.IsNullOrWhiteSpace(_user.DisplayName))) ? _user.DisplayName : string.Format("{0}, {1} {2}", _user.LastName, _user.FirstName, _user.MiddleName);

                if ((string.IsNullOrWhiteSpace(recipient.Email)) || (string.IsNullOrWhiteSpace(manager.Email)) || (string.IsNullOrWhiteSpace(_user.Email)))
                {
                    LiteralControl liTop = new LiteralControl();
                    liTop.Text = string.Format("{0}", @"<div class=""col-md-12""><button type=""button"" class=""btn btn-danger"">");

                    LiteralControl liBottom = new LiteralControl();
                    liBottom.Text = string.Format("{0}", @"</button></div>");

                    LiteralControl licEmailError = new LiteralControl();
                    if (string.IsNullOrWhiteSpace(recipient.Email))
                    {
                        licEmailError.Text = string.Format("{0} does not have an email address defined. So we can not send the email to them.<br/>", recipient.DisplayName);
                    }
                    if (string.IsNullOrWhiteSpace(manager.Email))
                    {
                        string l = string.Empty;
                        l = string.Format("{0} does not have an email address defined. So we can not send the email to them.<br/>", manager.DisplayName);
                        if (string.IsNullOrWhiteSpace(licEmailError.Text))
                        {
                            licEmailError.Text = l;
                        }
                        else
                        {
                            licEmailError.Text += l;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(_user.Email))
                    {
                        string l = string.Empty;
                        l = string.Format("{0} does not have an email address defined. So we can not send the email to them.<br/>", _user.DisplayName);
                        if (string.IsNullOrWhiteSpace(licEmailError.Text))
                        {
                            licEmailError.Text = l;
                        }
                        else
                        {
                            licEmailError.Text += l;
                        }
                    }

                    phEmailError.Controls.Add(liTop);
                    phEmailError.Controls.Add(licEmailError);
                    phEmailError.Controls.Add(liBottom);
                }
                else
                {
                    phEmailError.Controls.Clear();

                    StarSent StarSentInsert = new StarSent()
                    {
                        OrganizationId = organizationId,

                        Comment = tbComments.Text,

                        SenderGUID = _user.Guid,
                        SenderName = senderName,
                        SenderLogin = _user.Username,

                        RecieverGUID = recipient.Guid,
                        RecieverName = recipientName,
                        RecieverLogin = recipient.Username,

                        ManagerGUID = manager.Guid,
                        ManagerName = managerName,
                        ManagerLogin = manager.Username,

                        CreatedDateTime = DateTime.Now
                    };

                    _db.StarSents.Add(StarSentInsert);
                    _db.SaveChanges();
                    Int32 starSentId = StarSentInsert.StarSentID; //This is so we know the ID number

                    //Send email
                    MailMessage message = new MailMessage();

                    MailAddress mFromAddress = new MailAddress(_user.Email, senderName);
                    message.From = mFromAddress;

                    MailAddress mToAddress = new MailAddress(recipient.Email, recipientName);
                    message.To.Add(mToAddress);

                    MailAddress mCCAddress = new MailAddress(manager.Email, managerName);
                    message.To.Add(mCCAddress);
                    message.To.Add(mFromAddress);

                    string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                    string strURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
                    string strAppPath = HttpContext.Current.Request.ApplicationPath.ToString();

                    if (strAppPath == "/")
                    {
                        strAppPath = string.Empty;
                    }

                    StringBuilder mBody = new StringBuilder();
                    mBody.AppendFormat("{0}{1}{2}{3}{4}{5}", @"<a href = '", @strURL, @strAppPath, @"/ViewStar.aspx?StarSentID=", starSentId, @"'>Click here to see the Shooting Star.</a>");

                    message.Subject = string.Format("{0} has received a Shooting Star from {1}", recipientName, senderName);

                    SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["MailSMTPServer"]);
                    smtpClient.Send(message);

                    smtpClient.Dispose();

                    //Log to database mail sent
                    Mail MailInsert = new Mail()
                    {
                        EmailDate = DateTime.Now,
                        EmailFrom = _user.Email,
                        EmailTo = recipient.Email,
                        EmailToCC = manager.Email,
                        EmailToBCC = null,
                        EmailSubject = message.Subject,
                        EmailBody = mBody.ToString()
                    };
                    _dbEmail.Mails.Add(MailInsert);
                    _dbEmail.SaveChanges();

                    Response.Redirect("./ThankYou.aspx?OrganizationId=" + organizationId.ToString());

                }
            }
        }
    }
}