using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.Spec_Sets
{
    public partial class CommunitySpecSetsPage
    {
        public void ExpandSpecSetGroup(string SpecSetGroup)
        {
            Button ExpandSpecSetGroup_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSetsPanel']//tbody/tr/td/b[contains(text(),'{SpecSetGroup}')]//ancestor::td//preceding-sibling::td/input");
            ExpandSpecSetGroup_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            System.Threading.Thread.Sleep(4000);
        }

        public CommunitySpecSetsPage FindItemInGridWithTextContains(string columnName, string value)
        {
            int numberOfPage = SpecSetGroup_Grid.GetTotalPages;
            for (int i = 1; i <= numberOfPage; i++)
            {
                if (!i.Equals(1))
                {
                    SpecSetGroup_Grid.NavigateToPage(i);
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
                }
                if (SpecSetGroup_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value))
                    break;
            }
            return this;
        }
    }
}
