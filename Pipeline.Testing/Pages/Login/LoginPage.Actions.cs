

namespace Pipeline.Testing.Pages.Login
{
    public partial class LoginPage
    {

        public LoginPage LoginWithUser(string userName)
        {
            UserName_txt.SetText(userName);
            return this;
        }

        public LoginPage LoginWithPassword(string pwd)
        {
            Password_txt.SetText(pwd);
            return this;
        }

        public LoginPage SignIn()
        {
            Login_btn.Click();
            return this;
        }
    }
}
