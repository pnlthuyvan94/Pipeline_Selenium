using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House.Communities
{
    public partial class HouseCommunities
    {
       
        public HouseCommunities InsertCommunities()
        {
            Insert_Btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");
            return this;
        }

        public HouseCommunities AddButtonCommunities()
        {
            Add_Btn.Click(false);
            if (!AddCommunityModalTitle_lbl.WaitForElementIsVisible(15))
                ExtentReportsHelper.LogFail("Add Communities modal is NOT display after 15s.");
            return this;

        }
        public void CloseButtonCommunities()
        {
            CloseModal_Btn.Click();
        }
        public void FillterOnGrid(string columnName, string valueToFind)
        {
            HouseCommunities_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, valueToFind);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");
        }

        public bool IsValueOnGrid(string columnName, string valueToFind)
        {
            System.Threading.Thread.Sleep(3000);
            return HouseCommunities_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public bool IsValueOnGridWithTextContain(string columnName, string valueToFind)
        {
            return HouseCommunities_Grid.IsItemWithTextContainsOnCurrentPage(columnName, valueToFind);
        }

        public IList<string> GetListNameCommunities()
        {
            IList<string> nameCommunities = new List<string>();
            IList<IWebElement> optionCommunities = driver.FindElements(By.XPath("//div[contains(@id, 'cbProperty_DropDown')]//ul/li/label"));
            foreach (var item in optionCommunities)
            {
                string addItem = item.Text;
                nameCommunities.Add(addItem);
            }
            return nameCommunities;
        }

        public IList<string> GetListNameCommunitiesOnGird()
        {
            IList<string> nameCommunities = new List<string>();
            IList<IWebElement> optionCommunities = driver.FindElements(By.XPath("//div[contains(@id,'rlbCommunitiesPanel')]//div[contains(@id,'rlbCommunities')]//div//ul//li//span"));
            foreach (var item in optionCommunities)
            {
                string addItem = item.Text;
                nameCommunities.Add(addItem);
            }
            return nameCommunities;
        }

        public IList<string> GetOnlyNameCommunities(params string[] listCommunities)
        {
            IList<string> nameCommunities = new List<string>();
            foreach (var item in listCommunities)
            {
                string[] addItem = item.Split('-');
                nameCommunities.Add(addItem[addItem.Length - 1]);
            }
            return nameCommunities;
        }

        public string GetAndCheckHyperLink(string community)
        {
            Link communitiesLink = new Link(FindType.XPath, $"//table[contains(@id,'Communities')]//tbody//tr//td//a[contains(text(),'{community}')]");
            communitiesLink.WaitForElementIsVisible(5);
            return communitiesLink.GetAttribute("href");
        }

        public HouseCommunities SelectCommunityOnAddCommunityModal(string community)
        {
            CommunityFieldOfAddCommunityModal_btn.Click();
            CommunityFieldOfAddCommunityModal_txt.SetText(community);
            Link item = new Link(FindType.XPath, $"//div[contains(@id,'cbProperty_DropDown')]//ul/li/label[contains(text(),'{community}')/input]");
            item.WaitForElementIsVisible(5);
            item.Click();
           // CommunityFieldOfAddCommunityModal_txt.SetText(community);

           // if(GetListNameCommunities().Contains(community))
           // {
           //     //Link item = new Link(FindType.XPath, $"//div[contains(@id, 'cbProperty_DropDown')]//ul/li/label[contains(text(),'{community}')/input]");
           //     Link item = new Link(FindType.XPath, $"//*[@id='ctl00_CPH_Content_cbProperty_DropDown']/div/ul/li[@class='rcbHovered']/label/input");
           //     item.WaitForElementIsVisible(5);
           //     item.Click();
           // }
           //else
           // {
           //     CloseButtonCommunities();
           //     ExtentReportsHelper.LogFail($"Not found item {community} on list Community");
           // }
            return this;
            
        }

    }
}
