namespace Pipeline.Testing.Pages.Assets.OptionGroup.AddOptionGroup
{
    public partial class AddOptionGroupModal
    {
        public AddOptionGroupModal EnterOptionGroupName(string groupName)
        {
            OptionGroupName_txt.WaitForElementIsVisible(5, false);
            OptionGroupName_txt.SetText(groupName);
            return this;
        }

        public AddOptionGroupModal EnterSortOrder(string sortOrder)
        {
            SortOrder_txt.SetText(sortOrder);
            return this;
        }

        public void Save()
        {
            OptionGroupSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionGroups']");
        }

        public void CloseModal()
        {
            OptionGroupClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
