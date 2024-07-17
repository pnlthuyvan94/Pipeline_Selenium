namespace Pipeline.Testing.Pages.Estimating.StyleImportRules.AddRule
{
    public partial class AddRuleModal
    {
        public AddRuleModal IsActive(bool data)
        {
            Active_chk.SetCheck(data);
            return this;
        }

        public string SelectDefaultStyle(string data)
        {
            return DefaultStyle_ddl.SelectItemByValueOrIndex(data, 2);
        }

        public string SelectStyles(string data)
        {
            
            return Style_chklst.SelectItemByValueOrIndex(data, 2);
        }

        public string Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgRules']/div[1]");
            return GetLastestToastMessage(2);
        }

    }
}