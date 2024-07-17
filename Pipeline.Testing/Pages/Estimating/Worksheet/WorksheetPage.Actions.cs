using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail;

namespace Pipeline.Testing.Pages.Estimating.Worksheet
{
    public partial class WorksheetPage
    {

        public void ClickAddWorksheetIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return WorksheetPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            WorksheetPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheets']/div[1]");
        }

        public WorksheetPage EnterWorksheetNameToFilter(string columnName, string worksheetName)
        {
            WorksheetPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, worksheetName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheets']", 1000);
            return this;
        }

        public void DeleteWorkSheet(string name)
        {
            DeleteItemInGrid("Name", name);
            WaitGridLoad();
            string successfulMess = $"Worksheet {name} deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Worksheet deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", name))
                    ExtentReportsHelper.LogFail("<font color ='red'>Worksheet could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Worksheet deleted successfully!</b></font>");
            }
        }

        /// <summary>
        /// Create new Worksheet
        /// </summary>
        /// <param name="data"></param>
        public void CreateNewWorksheet(WorksheetData data)
        {
            // Click Add button
            ClickAddWorksheetIcon();

            // Open Works detail page
            Label Titltle_lbl = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblWStitle' and text() = 'New Worksheet']");
            if (!Titltle_lbl.IsDisplayed())
            {
                ExtentReportsHelper.LogFail("<font color='red'>Add New Worksheet page doesn't display or title is incorrect." +
                    "<br>Expected title: 'New Worksheet'</font>");
            }

            WorksheetDetailPage.Instance.EnterWorksheetName(data.Name)
               .EnterWorksheetCode(data.Code).Save();

            // No toast message
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbSaveContinue']");
            PageLoad();

            // Verify new worksheet title
            if (!WorksheetDetailPage.Instance.IsSaveWorksheetSuccessful(data.Name))
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to create Worksheet." +
                    $"<br>Expected Name:{data.Name}" +
                    $"<br>Expected Code:{data.Code}</font>");
            }
            else
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Create successful Worksheet</b></font>");
        }

        /// <summary>
        /// Open Worksheet detail page by name
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SelectItemInGrid(string columnName, string value)
        {
            WorksheetPage_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

    }
}
