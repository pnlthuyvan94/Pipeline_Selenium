using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;

namespace Pipeline.Testing.Pages.Assets.CustomOptions
{
    public partial class CustomOptionPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CustomOptionPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CustomOptionPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemWithTextContainsOnCurrentPage(string columnName, string value)
        {
            return CustomOptionPage_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CustomOptionPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            //CustomOptionPage_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomOptions']/div[1]");
        }
        public void SelectItemInGrid(string columnName, string valueToFind)
        {
            CustomOptionPage_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }

        public void CreateCustomOption(CustomOptionData customOption)
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            // Create CO - Click 'Save' Button
            CustomOptionDetailPage.Instance.AddCustomOption(customOption);

            string _expectedMessage = $"Custom Option {customOption.Code} already exists";
            string actualMsg = CustomOptionDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage.Equals(actualMsg))
            {
                // Actual: failed to create customer
                ExtentReportsHelper.LogFail(null, $"<font color = 'green'><b>Custom Option with name { customOption.Code} created successfully.</b></font>");
            }
            else
            {
                // There is no toast message after clicking save button, then reload page and check the title
                RefreshPage();
                string breadcrumName = CustomOptionDetailPage.Instance.SubHeadTitle();
                if (!breadcrumName.Equals(customOption.Code))
                {
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Could not create Custom Option with code {customOption.Code}.</font>" +
                        $"<br>Actual message: {actualMsg}" +
                        $"<br>Expected message: {_expectedMessage}</br>");
                }
                else
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Custom Option with code {customOption.Code} created successfully!</b></font>");
            }
        }

        public void DeleteCustomOption(CustomOptionData customOption)
        {
            FilterItemInGrid("Code", GridFilterOperator.Contains, customOption.Code);
            if (IsItemInGrid("Code", customOption.Code))
            {
                // 7. Select item and click deletete icon
                DeleteItemInGrid("Code", customOption.Code);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomOptions']/div[1]");
                string successfulMess = $"Custom Option {customOption.Code} deleted successfully!";
                if (successfulMess == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Custom Option deleted successfully!</b></font>");
                    CloseToastMessage();
                }
                else
                {
                    if (IsItemInGrid("Code", customOption.Code))
                        ExtentReportsHelper.LogWarning(null, "Custom Option could not be deleted - Possible constraint preventing deletion.");
                    else
                        ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Custom Option deleted successfully!</b></font>");
                }
            }
            else
            {
                ExtentReportsHelper.LogInformation($"<b>Can't find Custom Option with code {customOption.Code} to delete.</b>");
            }
        }
    }

}
