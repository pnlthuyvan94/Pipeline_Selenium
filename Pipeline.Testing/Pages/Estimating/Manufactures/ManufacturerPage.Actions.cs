using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Manufactures.ManufacturerDetail;

namespace Pipeline.Testing.Pages.Estimating.Manufactures
{
    public partial class ManufacturerPage
    {
        public void ClickAddToManufacturerIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ManufacturerPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        private void DeleteItemInGrid(string columnName, string value)
        {
            ManufacturerPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            ManufacturerPage_Grid.WaitGridLoad();
        }

        public ManufacturerPage EnterManufaturerNameToFilter(string columnName, string manufacturerName)
        {
            ManufacturerPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, manufacturerName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgManufacturers']");
            return this;
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ManufacturerPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgManufacturers']");
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            ManufacturerPage_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void DeleteManufacturer(string manuName)
        {
            DeleteItemInGrid("Name", manuName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgManufacturers']");
            string successfulMess = $"Manufacturer {manuName} deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Manufacturer deleted successfully!");
                CloseToastMessage();
            }
            else
            {
                // Can't get toast message, then verify the items on the grid
                if (IsItemInGrid("Name", manuName))
                    ExtentReportsHelper.LogFail("Manufacturer could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Manufacturer deleted successfully!");
            }
        }

        /// <summary>
        /// Create a new Manufacturer
        /// </summary>
        /// <param name="data"></param>
        public void CreateNewManufacturer(ManufacturerData data)
        {
            // Click add button to open detail page
            ClickAddToManufacturerIcon();

            string expectedURl = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_MANUFACTURER_URL;
            if (ManufacturerDetailPage.Instance.IsPageDisplayed(expectedURl) is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Manufacturer detail page isn't displayed." +
                    $"<br>Expected URL: {expectedURl}" +
                    $"Actual URL: {CurrentURL}</font>");
            else
                ExtentReportsHelper.LogPass("<font color ='green'><b>Manufacturer detail page displayed successfully.</b></font>");

            // Populate data
            ManufacturerDetailPage.Instance.EnterManufaturerName(data.Name)
                   .EnterManufaturerUrl(data.Url)
                   .EnterManufaturerDescription(data.Description)
                   .Save();
        }
    }
}
