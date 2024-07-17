using LinqToExcel;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingTrade;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B16_RT_01157 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        #region"Test Case"
        [Test]
        [Category("Section_III")]
        [Ignore("The Building Trade was hidden from the page, so this test will skip.")]
        public void B16_Estimating_AddABuildingTrade()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingTrades/Default.aspx
            BuildingTradePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingTrades);

            // Step 2: click on "+" Add button
            BuildingTradePage.Instance.ClickAddToOpenCreateBuildingTradeModal();
            Assert.That(BuildingTradePage.Instance.AddBuildingTradeModal.IsModalDisplayed, "Create Building Trade modal is not displayed.");

            // Step 3: Populate all values
            Row TestData = ExcelFactory.GetRow(BuildingTradePage.Instance.AddBuildingTradeModal.TestData_RT01157, 1);
            BuildingTradeData _trade = new BuildingTradeData()
            {
                Code = TestData["Code"],
                Name = TestData["Name"],
                Description = TestData["Description"],
            };

            // Create Building Trade - Click 'Save' Button
            BuildingTradePage.Instance.AddBuildingTradeModal.AddBuildingTrade(_trade);
            string _actualMessage = BuildingTradePage.Instance.GetLastestToastMessage();
            string _expectedMessage = $"Building Trade {_trade.Code} {_trade.Name} saved successfully!";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Could not Create Building Trade with name { _trade.Name} and Code {_trade.Code}.");
                Assert.Fail($"Could not create Building Trade.");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Create Building Trade with name { _trade.Name} and Code {_trade.Code} successfully.");
                BuildingTradePage.Instance.CloseToastMessage();
            }

            // Step 5. Create with existing name
            BuildingTradePage.Instance.AddBuildingTradeModal.AddBuildingTrade(_trade);
            _expectedMessage = $"Not able to create Building Trade {_trade.Code} {_trade.Name}";
            bool isDuplicated = false;
            if (BuildingTradePage.Instance.GetLastestToastMessage() != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Created duplicated Building Trade with name { _trade.Name} and Code {_trade.Code}.");
                isDuplicated = true;
            }
            ExtentReportsHelper.LogPass("The Building Trade could not create with the existed number. The message is dispalyed as expected: " + _actualMessage);
            BuildingTradePage.Instance.CloseToastMessage();

            BuildingTradePage.Instance.CloseModal();

            // Insert name to filter and click filter by Contain value
            BuildingTradePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _trade.Name);

            bool isFound = BuildingTradePage.Instance.IsItemInGrid("Name", _trade.Name);
            Assert.That(isFound, string.Format($"New Building Trade \"{_trade.Name} \" was not display on grid."));

            // 7. Select item and click deletete icon
            BuildingTradePage.Instance.DeleteItemInGrid("Name", _trade.Name);
            BuildingTradePage.Instance.WaitGridLoad();
            string successfulMess = $"Building Trade {_trade.Code} {_trade.Name} deleted successfully!";
            ClassicAssert.AreEqual(successfulMess, BuildingTradePage.Instance.GetLastestToastMessage(), "The delete message is not as expected.");

            ExtentReportsHelper.LogPass("Building Trade deleted successfully!");
            BuildingTradePage.Instance.CloseToastMessage();

            if (isDuplicated)
            {
                ExtentReportsHelper.LogInformation("Delete the duplicated data.");
                BuildingTradePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _trade.Name);
                BuildingTradePage.Instance.DeleteItemInGrid("Name", _trade.Name);
            }
        }
        #endregion
    }
}
