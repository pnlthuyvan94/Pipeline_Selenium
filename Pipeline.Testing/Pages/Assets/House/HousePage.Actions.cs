using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;

namespace Pipeline.Testing.Pages.Assets.House
{
    public partial class HousePage
    {
        public void ClickAddToHouseIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            HousePage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            HousePage_Grid.WaitGridLoad();
        }

        public HousePage EnterHouseNameToFilter(string columnName, string houseName)
        {
            HousePage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, houseName);
            HousePage_Grid.WaitGridLoad();
            return this;
        }

        public void SelectItemInGrid(int columIndex, int rowIndex)
        {
            HousePage_Grid.ClickItemInGrid(columIndex, rowIndex);
            JQueryLoad();
        }

        public void SelectItemInGridWithTextContains(string columName, string value)
        {
            System.Threading.Thread.Sleep(3000);
            HousePage_Grid.ClickItemInGrid(columName, value);
            JQueryLoad();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            HousePage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");
            //HousePage_Grid.WaitGridLoad();
        }
        public void WaitGridLoad()
        {
            HousePage_Grid.WaitGridLoad();
        }

        public void OpenHouseDetailPageOnNewTab(string houseName)
        {
            Label houseNameOnGrid = new Label(FindType.XPath, $"//*[contains(@id, '_hypHouseName') and text()='{houseName}']");
            CommonHelper.OpenLinkInNewTab(houseNameOnGrid.GetAttribute("href"));
        }

        /// <summary>
        /// Create new house
        /// </summary>
        /// <param name="houseData"></param>
        public void CreateHouse(HouseData houseData)
        {
            ClickAddToHouseIcon();
            PageLoad();
            HouseDetailPage.Instance.CreateUpdateAHouse(houseData);
            string updateMsg = $"House {houseData.HouseName} saved successfully!";
            if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                ExtentReportsHelper.LogPass(updateMsg);
            HouseDetailPage.Instance.RefreshPage();
            // Verify items
            HouseDetailPage.Instance.IsSavedWithCorrectValue(houseData);
            // 5. Verify new House in header
            if (HouseDetailPage.Instance.IsHouseNameDisplaySuccessfullyOnBreadScrumb(houseData.HouseName, houseData.PlanNumber))
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>House with name '{houseData.HouseName}' display correctly on the breadscrum.</b></font>");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"House with name '{houseData.HouseName}' DOESN'T display correctly on the breadscrum." +
                    $"<br>Expected:<font color='red'><b>{houseData.HouseName}</b></font>.");
        }

        /// <summary>
        /// Delete house by name
        /// </summary>
        /// <param name="houseName"></param>
        public void DeleteHouse(string houseName)
        {
            SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Insert name to filter and click filter by Contain value
            FilterItemInGrid("Name", GridFilterOperator.Contains, houseName);
            if (IsItemInGrid("Name", houseName))
            {
                DeleteItemInGrid("Name", houseName);
                string successfulMess = $"House {houseName} deleted successfully!";
                string actualMsg = GetLastestToastMessage();
                if (successfulMess.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b>House deleted successfully!</b></font>");
                    CloseToastMessage();
                }
                else
                {
                    if (IsItemInGrid("Name", houseName))
                        ExtentReportsHelper.LogWarning("The House could not be deleted - Possible constraint preventing deletion.");
                    else
                        ExtentReportsHelper.LogPass(null, "<font color ='green'><b>House deleted successfully!</b></font>");
                }
            }
        }

        public void ClickCopyFromHouse(string houseName)
        {
            Button copyBtn = new Button(FindType.XPath, $"//a[text() = '{houseName}']/parent::td/following-sibling::td/input[@title='Copy']");
            copyBtn.Click();
            WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']");
        }
        public void ClickCopyInCopyHouseModal()
        {
            copyButtonInCopyModal.Click();

        }
        public void EnterNewHouseName(string newHouseName)
        {
            enterNewHouseNameInCopyModal.Clear();
            enterNewHouseNameInCopyModal.SetText(newHouseName);
        }
    }
}
