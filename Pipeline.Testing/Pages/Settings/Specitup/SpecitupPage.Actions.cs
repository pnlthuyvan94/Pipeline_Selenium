
namespace Pipeline.Testing.Pages.Settings.Specitup
{
    public partial class SpecitupPage
    {
        public SpecitupPage EnterInformation(string rootUrl, string username, string password)
        {
            RootUrl_Txt.SetText(rootUrl);
            UserName_Txt.SetText(username);
            Password_Txt.SetText(password);
            return this;
        }

        public SpecitupPage RunningSpecitup(bool run = true)
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
    }

}
