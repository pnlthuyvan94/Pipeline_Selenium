using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Purchasing.Trades;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E06_PIPE_40408 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E06_Test_()-";
        private const string NewBuildingTradeCode = "RT06TST";

        private const string InvalidSpecialCharacter = "~!@#$%^&*+";
        private const string ValidSpecialCharacter = "()-_";

        [SetUp]
        public void Setup()
        {
            TradesData _trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Add Trade.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Populate trade details in the modal and save.</b></font>");
                TradesPage.Instance.CreateTrade(_trade);
                CommonHelper.RefreshPage();
            }
        }

        [Test]
        public void E06_Purchasing_Trades_Landing_Page()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Trades Landing Page.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Verify if columns are available in the grid.</b></font>");
            if(TradesPage.Instance.IsColumnFoundInGrid("Trade") is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Trade column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Trade column is not found in the grid.</b></font>");

            if (TradesPage.Instance.IsColumnFoundInGrid("Company Vendor") is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Company Vendor column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Company Vendor column is not found in the grid.</b></font>");

            if (TradesPage.Instance.IsColumnFoundInGrid("Building Phases") is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Building Phases column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Building Phases column is not found in the grid.</b></font>");

            if (TradesPage.Instance.IsColumnFoundInGrid("Scheduling Tasks") is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Scheduling Tasks column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Scheduling Tasks column is not found in the grid.</b></font>");

            if(TradesPage.Instance.IsVendorAssignmentBtnDisplayed is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor Assignments button is displayed.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor Assignments button is not displayed.</b></font>");

            if (TradesPage.Instance.IsBulkActionDisplayed is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Bulk Actions is displayed.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Bulk Actions is not displayed.</b></font>");

            if (TradesPage.Instance.IsUtilitiesDisplayed is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Utilities button is displayed.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Utilities button is not displayed.</b></font>");

            //Verify if these buttons are available in Trades default page:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Verify if these buttons are available in Trades default page.</b></font>");
            CommonHelper.CaptureScreen();

            //On “Trade” column, type in filter keyword using special characters that are not ()-_ and choose “Contain” filter option. //Verify if the filter text field clears the keyword and the search results still shows the default trade list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: On “Trade” column, type in filter keyword using special characters that are not ()-_ and choose “Contain” filter option. //Verify if the filter text field clears the keyword and the search results still shows the default trade list.</b></font>");
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, InvalidSpecialCharacter);
            if (!TradesPage.Instance.IsItemInGrid("Trade", InvalidSpecialCharacter))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The filter text field clears the keyword and the search results still shows the default trade list.</b></font>");
            }

            //On “Trade” column, type in filter keyword that consists of either ()-_ then choose “Contain” filter option. Verify if the filter results shows the correct data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: On “Trade” column, type in filter keyword that consists of either ()-_ then choose “Contain” filter option. Verify if the filter results shows the correct data.</b></font>");
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, ValidSpecialCharacter);
            if (TradesPage.Instance.IsItemInGrid("Trade", InvalidSpecialCharacter))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid special characters were successfully displayed.</b></font>");
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 Tear down test data.</b></font>");
            DeleteTrade();
        }

        private void DeleteTrade()
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                TradesPage.Instance.DeleteItemInGrid("Trade", NewBuildingTradeName);
                CommonHelper.RefreshPage();
            }
        }

    }
}
