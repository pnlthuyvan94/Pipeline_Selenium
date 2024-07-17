using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B04_RT_01024 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        QuantityBuildingPhaseRuleData _data;
        QuantityBuildingPhaseRuleData _newdata;
        [SetUp]
        public void GetTestData()
        {
            _data = new QuantityBuildingPhaseRuleData()
            {
                Priority = "1",
                LevelCondition = "'1ST_FLOOR",
                OriginalBuildingPhase = "1163-QA Only Phase-2",
                NewBuildingPhase = "1162-QA ONLY - Phase 1",
            };
        }

        [Test]
        [Category("Section_III")]
        public void B04_Estimating_AddQuantityBuildingPhaseRule()
        {
            // Step 1: navigate to this URL:http://dev.bimpipeline.com/Dashboard/BuilderBom/PhaseRules/Default.aspx
            QuantityBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.QuanttityPhaseRules);

            // Step 2: click on "+" Add button
            QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();
            if(QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase Rules modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>New Building Phase Rules modal is displayed</b></font>");
            }
            // Create Building Phase Rule - Click 'Save' Button
            ////Select item in List. IF data is'nt in List then select item By index 
            _newdata = QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_data);

            string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Building Phase Rule created successfully!";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _newdata.Priority} and Original Building Phase {_newdata.OriginalBuildingPhase} and New Building Phase {_data.NewBuildingPhase}.");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _newdata.Priority} and LEVEL condition {_newdata.LevelCondition} and Original Building Phase {_data.OriginalBuildingPhase} and New Building Phase {_data.NewBuildingPhase} successfully.");
                QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
            }
            // Close the modal
           // QuantityBuildingPhaseRulePage.Instance.CloseModal();

            // Insert name to filter and click filter by Contain value
            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("Original Phase", GridFilterOperator.Contains, _newdata.OriginalBuildingPhase);

            if(QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _newdata.OriginalBuildingPhase) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Building Phase Rule {_newdata.OriginalBuildingPhase} was not display on grid.</font>");
            }


            // 7. Select item and click deletete icon
            QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("Original Phase", _newdata.OriginalBuildingPhase);
            QuantityBuildingPhaseRulePage.Instance.WaitGridLoad();
            string successfulMess = $"Building Phase Rule deleted successfully!";
            if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
            }
            else
            {
                if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _newdata.OriginalBuildingPhase))
                    ExtentReportsHelper.LogFail("Building Phase Rule could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");

            }
        }
    }
}
