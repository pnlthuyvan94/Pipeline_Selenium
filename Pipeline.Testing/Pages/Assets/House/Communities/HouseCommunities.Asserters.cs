using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House.Communities
{
    public partial class HouseCommunities
    {
        public IList<string> AddAndVerifyCommunitiesToHouse(string communityUsingForTestCase2, params int[] indexs)
        {
            // Expand community list 
            Button community_btn = new Button(FindType.XPath, "//button[@class='rcbActionButton']");
            community_btn.Click(false);
            System.Threading.Thread.Sleep(5000);
            // Get selected communities at index from params list
            IList<string> communitiesName = new List<string>();
            IList<IWebElement> communityList = driver.FindElements(By.XPath("//div[contains(@id, 'cbProperty_DropDown')]//ul/li/label"));

            // Check if the dropdown panel doesn't display then click again.
            IWebElement communityPanel = FindElementHelper.FindElement(FindType.XPath, "//div[@id='ctl00_CPH_Content_cbProperty_DropDown']//*[@class='rcbList']");
            if (communityPanel.Displayed == false)
                community_btn.JavaScriptClick(false);

            // Split community name and code
            if(indexs != null)
            {
                foreach (int i in indexs)
                {
                    string name = communityList[i].Text.Substring(communityList[i].Text.IndexOf("-") + 1);
                    communitiesName.Add(name);
                }
            }

            // Add community with name 'Community_RegressionTest_Auto' to house - using for second test script
            if (!communitiesName.Contains(communityUsingForTestCase2))
                communitiesName.Add(communityUsingForTestCase2);

            CheckBox selectedCommunity;
            foreach (var item in communitiesName)
            {
                selectedCommunity = new CheckBox(FindType.XPath, $"//div[contains(@id, 'cbProperty_DropDown')]//ul/li/label[contains(text(),'{item}')]/input");
                if (selectedCommunity.IsNull())
                    ExtentReportsHelper.LogFail($"The community with name <font color='red'><b>{item}</b></font> is not displayed on community list.");
                else
                    selectedCommunity.Check(false);
            }
            InsertCommunities();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");

            // Verify toast message
            string expectedMsg = "Each selected community successfully added to the house.";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Add community is successfully</b></font>");
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The toast message is NOT same as the expectation." +
                $"<br>Expected: {expectedMsg}" +
                $"<br>Actual: {actualMsg}</font>");
            }

            // Close modal
            if (CloseModal_Btn.WaitForElementIsVisible(5) == true)
                CloseModal_Btn.Click();

            return communitiesName;
        }

        public void VerifyFilterCommunity(string columnName, string valueToFind)
        {
            //FillterOnGrid(columnName, valueToFind);
            if (IsValueOnGridWithTextContain(columnName, valueToFind) is true)
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Filtered successfully the Community</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='red'>Filtered unsuccessfully the Community</font>");
            //HouseCommunities_Grid.FilterByColumn(columnName, GridFilterOperator.NotIsEmpty, string.Empty);
            //HouseCommunities_Grid.WaitGridLoad();
        }

        public void DeleteAndVerifyDeleteCommunity(string columnName, string valueToFind)
        {
            HouseCommunities_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");
            string actualMess = "Community successfully removed from this House";
            string actualMsg = GetLastestToastMessage();
            if (actualMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass(null, $"Community <font color='green'><b>{valueToFind}</b></font> successfully removed the this House");
                //CloseToastMessage();
                System.Threading.Thread.Sleep(5000);
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail($"Community <font color='red'<b>{valueToFind}</b></font> unsuccessfully removed the this House");
                //CloseToastMessage();
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
