using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B05_RT_01082 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private BOMBuildingPhaseRuleData _data;
        private BOMBuildingPhaseRuleData _Newdata;
        [SetUp]
        public void GetTestData()
        {
            _data = new BOMBuildingPhaseRuleData()
            {
                OriginalProductBuildingPhase = "1161-QA Only Phase-0",
                OriginalSubcomponentBuildingPhase = "1162-QA ONLY - Phase 1",
                NewSubcomponentBuildingPhase = "1163-QA Only Phase-2",
            };
        }

        [Test]
        [Category("Section_III")]
        public void B05_Estimating_AddBOMBuildingPhaseRule()
        {
            // Step 1: navigate to this URL:http://dev.bimpipeline.com/Dashboard/BuilderBom/BOMPhaseRules/Default.aspx
            BOMBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);

            // Verify the created item and delete if it's existing
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase) is true)
            {
                BOMBuildingPhaseRulePage.Instance.DeleteBOMBuildingPhaseRule(_data);
            }

            // Step 2: click on "+" Add button
            BOMBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();
            if(BOMBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.IsModalDisplayed()is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase Rules modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>New Building Phase Rules modal is displayed.</b></font>");
            }

            // Step 3: Create Building Phase Rule - Click 'Save' Button
            ////Select item in List. IF data is'nt in List then select item By index 
            _Newdata = BOMBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_data);
          
            string _actualMessage = BOMBuildingPhaseRulePage.Instance.GetLastestToastMessage();
            string _expectedMessage = "New BOM Building Phase Rule successfully inserted";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Original Product Building Phase { _Newdata.OriginalProductBuildingPhase} .");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Create BOM Building Phase Rule with Original Product Building Phase { _Newdata.OriginalProductBuildingPhase} .");
                BOMBuildingPhaseRulePage.Instance.CloseToastMessage();
            }

            // Close the modal
            //BOMBuildingPhaseRulePage.Instance.CloseModal();

            // Insert name to filter and click filter by Contain value
            //BOMBuildingPhaseRulePage.Instance.FilterItemInGrid("Original Product Building Phase", GridFilterOperator.Contains, _data.OriginalProductBuildingPhase);

            if(BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _Newdata.OriginalProductBuildingPhase) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>BOM Building Phase Rulet {_Newdata.OriginalProductBuildingPhase} was not display on grid.</font>");
            }
        }

        [TearDown]
        public void DeleteData()
        {
            // 7. Select item and click deletete icon
            BOMBuildingPhaseRulePage.Instance.DeleteBOMBuildingPhaseRule(_Newdata);
        }
    }
}
