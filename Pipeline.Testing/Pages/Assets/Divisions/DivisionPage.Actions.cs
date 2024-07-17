using NUnit.Framework;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;

namespace Pipeline.Testing.Pages.Assets.Divisions
{
    public partial class DivisionPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            DivisionPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgDivisions']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return DivisionPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            DivisionPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            DivisionPage_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            DivisionPage_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void DeleteDivision(DivisionData _division)
        {
            // Insert name to filter and click filter by Contain value
            FilterItemInGrid("Division", GridFilterOperator.Contains, _division.Name);

            bool isFound = IsItemInGrid("Division", _division.Name);
            if (isFound)
            {
                // Step 7 delete division
                // Back to list of Division and verify new item in grid view
                DeleteItemInGrid("Division", _division.Name);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgDivisions']/div[1]");
                string successfulMess = $"Division {_division.Name} deleted successfully!";
                string actualMsg = DivisionPage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Division {_division.Name} deleted successfully!</b></font>");
                    CloseToastMessage();
                }
                else
                {
                    if (!IsItemInGrid("Division", _division.Name))
                        ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Division {_division.Name} deleted successfully!</b></font>");
                    else
                        ExtentReportsHelper.LogFail($"<font color = 'red'>The division could not be deleted.</font>" +
                            $"<br>Actual message: <i>{actualMsg}</i>" +
                            $"<br>Expected message: <i>{successfulMess}</i>");
                }
            }
            else
                ExtentReportsHelper.LogInformation($"There is no Division with name {_division.Name} to delete.");
        }

        public void CreateDivision(DivisionData _division)
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            var _expectedCreateDivisionURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_DIVISION_URL;
            if (!DivisionDetailPage.Instance.IsPageDisplayed(_expectedCreateDivisionURL))
            {
                ExtentReportsHelper.LogFail($"<font color = 'red'>Division details page (Created page) isn't displayed or title is incorrect.</font");
            }

            // Create division - Click 'Save' Button
            DivisionDetailPage.Instance.AddDivision(_division);
            string _expectedMessage = $"Could not create division with name {_division.Name}.";
            string actualMsg = DivisionDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage.Equals(actualMsg))
                ExtentReportsHelper.LogFail($"<font color = 'red'>Could not create division with name { _division.Name}.</font");
            else
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Create Division with name { _division.Name} successfully.</b></font>");
        }
    }
}
