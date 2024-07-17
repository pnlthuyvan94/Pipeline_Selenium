using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity.AddCommunity;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity
{
    public partial class DivisionCommunityPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            DivisionCommunity_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']", 1000);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return DivisionCommunity_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            DivisionCommunity_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']", 1000);
        }

        public void OpenDivisionCommunityModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbShowAddCommunity']").Click();
            DivisionCommunityModal = new DivisionCommunityModal();
            CommonHelper.WaitUntilElementVisible(5, "//h1[text()='Add Communities']");
        }

        public void WaitGridLoad()
        {
            DivisionCommunity_Grid.WaitGridLoad();
        }

        /// <summary>
        /// Assign Community to Division by Name
        /// </summary>
        /// <param name="communityName"></param>
        public void AssignCommunityToDivision(string[] communityName)
        {
            foreach (string community in communityName)
            {
                // Filter community name
                FilterItemInGrid("Name", GridFilterOperator.EqualTo, community);
                
                if (!IsItemInGrid("Name", community))
                {
                    // Assign if community doesn't exist on the grid view
                    OpenDivisionCommunityModal();
                    DivisionCommunityModal.SelectDivisionCommunityByName(community);
                    DivisionCommunityModal.Save();
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community '{community}' was assigned to Division before.");
                }
            }
        }
    }

}
