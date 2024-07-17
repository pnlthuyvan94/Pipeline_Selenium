
using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House.HouseComparisonGroups
{
    public partial class HouseComparisonGroups
    {
        public void SortOptionInComparisonGroups()
        {
            SortOption_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
        }


        public void ExpandComparisonGroups(string Option)
        {
            Button ExpandOption_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]//ancestor::tr/td[@class='rtlL rtlL2']/input[@class='rtlExpand']");
            ExpandOption_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");

        }
        public void PageSize(int size)
        {
            ComparisonGroups_Grid.ChangePageSize(size);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
        }
    }
}
