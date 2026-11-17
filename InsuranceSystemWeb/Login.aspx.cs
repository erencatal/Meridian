using System;
using System.Web.UI;

namespace InsuranceSystemWeb
{
    // Simple advisor login; a successful login is remembered in the session
    public partial class Login : Page
    {
        public const string SessionKey = "AdvisorLoggedIn";

        private const string AdvisorUsername = "advisor";
        private const string AdvisorPassword = "1234";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool isValid = txtUsername.Text.Trim() == AdvisorUsername
                        && txtPassword.Text == AdvisorPassword;

            if (!isValid)
            {
                lblLoginMsg.Text = "Invalid username or password.";
                return;
            }

            Session[SessionKey] = true;
            Response.Redirect("Advisor.aspx");
        }
    }
}
