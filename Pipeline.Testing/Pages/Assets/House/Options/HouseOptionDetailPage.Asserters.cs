using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.House.Options
{
    public partial class HouseOptionDetailPage
    {
        public bool IsItemInElevationGridWithTextContains(string columnName, params string[] values)
        {
            bool isPassed = true;
            foreach (var value in values)
                if (!Elevation_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value))
                    isPassed = false;
            return isPassed;
        }

        public bool IsItemInOptionGridWithTextContains(string columnName, params string[] values)
        {
            bool isPassed = true;
            foreach (var value in values)
                if (!Option_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value))
                    isPassed = false;
            return isPassed;
        }
        public bool IsItemInOptionGrid(string columnName,  string values)
        {
            return Option_Grid.IsItemOnCurrentPage(columnName, values);
        }
        public void WaitAddOptionModalLoadingIcon()
        {
            Label loadingIC = new Label(FindType.Xpath, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
            if(loadingIC.IsDisplayed())
            {
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
            }
            return;
        }
    }
}
