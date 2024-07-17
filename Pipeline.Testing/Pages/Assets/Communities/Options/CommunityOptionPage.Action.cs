using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityHouseOption;
using Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityOption;
using Pipeline.Testing.Pages.Assets.Communities.Options.AddOptionCondition;

namespace Pipeline.Testing.Pages.Assets.Communities.Options
{
    public partial class CommunityOptionPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CommunityOption_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]",3000);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CommunityOption_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsCommunityOptionInGrid(string columnName, string value)
        {
            return CommunityOption_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsCommunityHouseOptionInGrid(string columnName, string value)
        {
            return CommunityHouseOption_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value);
        }

        public void DeleteCommunityOptionInGrid(string columnName, string value)
        {
            CommunityOption_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
            //CommunityOption_Grid.WaitGridLoad();
        }

        public void DeleteCommunityHouseOptionInGrid(string columnName, string value)
        {
            CommunityHouseOption_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            //CommunityHouseOption_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouseOptions']/div[1]");
        }

        public void OpenAddCommunityOptionModal()
        {
            // Click Add button
            AddCommunityOption_btn.Click();

            // Waiting until new Community Option modal displayed.
            CommonHelper.VisibilityOfAllElementsLocatedBy(3, "//*[@id='comm-opts-modal']/section/header/h1");
            AddCommunityOptionModal = new AddCommunityOptionModal();
        }

        public void OpenAddCommunityHouseOptionModal()
        {   // Click Add button
            AddCommunityHouseOption_btn.Click();

            // Waiting until new Community House Option modal displayed.
            CommonHelper.VisibilityOfAllElementsLocatedBy(3, "//*[@id='ctl00_CPH_Content_lblModalHeader']");
            AddCommunityHouseOptionModal = new AddCommunityHouseOptionModal();
        }

        public void WaitCommunityOptionGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void WaitCommunityHouseOptionGridLoad(int forceWait = 1000)
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouseOptions']/div[1]", forceWait);
        }

        public void OpenAssignConditionModal(string optionName)
        {
            string assignConditionXpath = $"//*[@id='ctl00_CPH_Content_rgHouseOptions_ctl00']/tbody/tr/td/a[starts-with(text(),'{optionName}')]/../../td/input[contains(@src, 'Images/add')]";
            IWebElement assignCondition = FindElementHelper.FindElement(FindType.XPath, assignConditionXpath);
            if (assignCondition != null)
            {
                assignCondition.Click();
                // Wait loading grid
                WaitCommunityHouseOptionGridLoad();

                // Open asigned condition grid view
                AddOptionCondition = new AddOptionConditionGrid(optionName);
            }
        }


        public void AddCommunityOption(string[] OptionData)
        {
            // Click Add (+) Community Option
            OpenAddCommunityOptionModal();
            if (AddCommunityOptionModal.IsCommunityOptionModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Add Community Option modal doesn't display.</font>");
            }
            ExtentReportsHelper.LogPass("Add Community Option modal displays correctly.");
            CommunityOptionData communityOptionData = new CommunityOptionData()
            {
                AllHouseOptions = OptionData,
                OtherMasterOptions = OptionData,
                SalePrice = "0.00"
            };

            AddCommunityOptionModal.AddCommunityOption(communityOptionData);
            WaitCommunityOptionGridLoad();

            // Verify Message
            string _actualMessage = CommunityOptionPage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Selected Options added successfully!";
            if (_actualMessage.Equals(_expectedMessage))
            {
                ExtentReportsHelper.LogPass($"Add community option successfully.");
                //CommunityOptionPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Add community option unsuccessfully. Actual messsage: {_actualMessage}");
            }
            if (AddCommunityOptionModal.IsAddCommunityOptionSuccessful(communityOptionData) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Option and price aren't cleared from the Adding modal.</font>");
            }
            // Close modal
            AddCommunityOptionModal.Close();
        }
    }
}
