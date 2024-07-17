
namespace Pipeline.Testing.Pages.Settings.Saberis
{
    public partial class SaberisPage
    {
        public SaberisPage EnterInformation(string rootUrl, string username, string password)
        {
            RootUrl_Txt.SetText(rootUrl);
            UserName_Txt.SetText(username);
            Password_Txt.SetText(password);
            return this;
        }

        public SaberisPage RunningSaberis(bool run = true)
        {
            if (run)
                Running_Btn.JavaScriptClick();
            else
                Paused_Btn.JavaScriptClick();
            return this;
        }

        public void SaveSetting()
        {
            Save_Btn.Click();
        }

        public void LoginSaberis(string userName, string password)
        {
            UserNameSaberis_Txt.SetText(userName);
            PasswordSaberis_Txt.SetText(password);
            LogInSaberis_Btn.Click();
            PageLoad();
        }
    }

}
