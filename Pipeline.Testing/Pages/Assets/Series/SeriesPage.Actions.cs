using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Series.AddSeries;

namespace Pipeline.Testing.Pages.Assets.Series
{
    public partial class SeriesPage
    {
        public void ClickAddToSeriesModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddSeriesModal = new AddSeriesModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            WaitGridLoad();
            return Series_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void ClickEditItemInGrid(string columnName, string value)
        {
             Series_Grid.ClickEditItemInGrid(columnName, value);
             WaitGridLoad();
        }

        /// <summary>
        /// Filter the item in grid
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="gridFilterOperator"></param>
        /// <param name="value"></param>
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Series_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSeries']/div[1]");
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Series_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSeries']/div[1]");
        }

        public void WaitGridLoad()
        {
            Series_Grid.WaitGridLoad();
        }

        public void SelectItemAndOpenDetailPage(string columnName, string valueToFind)
        {
            Series_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }

        /// <summary>
        /// Delete series by name and code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public void DeleteSeries(string code, string name)
        {
            // delete this series
            DeleteItemInGrid("Name", name);

            // verify the success toast messsage is displayed
            string expectedMsg = $"Series {name} deleted successfully!";
            string _actualMessage = GetLastestToastMessage();
            if (expectedMsg.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass($"The Series '<font color='green'><b>{name}</b></font>' is deleted successfully.");
            }
            else
            {
                if (IsItemInGrid("Name", name))
                {
                    ExtentReportsHelper.LogFail($"The Series \"{code} - {name}\" deleted unsuccessfully!");
                }
                else
                    ExtentReportsHelper.LogPass($"The Series \"{code} - {name}\" deleted successfully!");
            }
        }

        /// <summary>
        /// Create a new Series
        /// </summary>
        /// <param name="seriesData"></param>
        public void CreateSeries(SeriesData seriesData)
        {
            // Click on "+" Add button
            ClickAddToSeriesModal();
            if (AddSeriesModal.IsModalDisplayed() is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Add Series modal is not displayed or title is incorrect." +
                    $"<br>Expected title: 'Add Series'</br></font>");

            // Populate all values
            AddSeriesModal.EnterSeriesName(seriesData.Name).EnterSeriesCode(seriesData.Code).EnterSeriesDescription(seriesData.Description);
            AddSeriesModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Series " + seriesData.Name + " created successfully!";
            string _actualMessage = AddSeriesModal.GetLastestToastMessage(30);
            if (string.IsNullOrEmpty(_actualMessage) is false && _expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The message is displayed as expected." +
                    $"<br>Actual results: {_actualMessage}</b></font>");
            }
            else
            {
                FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
                if (IsItemInGrid("Name", seriesData.Name) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color ='red'>The message does not as expected." +
                       $"<br>Actual results: {_actualMessage}" +
                       $"<br>Expected results: {_expectedMessage}</br></font>");
                }
            }
        }

        public void UpdateSeries(SeriesData seriesData)
        {
            EditSeriesName_txt.SetText(seriesData.Name);
            EditCode_txt.SetText(seriesData.Code);
            EditDescription_txt.SetText(seriesData.Description);
            UpdateSeries_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSeries']/div[1]");
        }

        public void CancelUpdateSeries()
        {
            CancelSeries_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSeries']/div[1]");
        }
    }
}
