using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Purchasing.CutoffPhase;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class E05_RT_01159 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private CutoffPhaseData _data;
        private bool isDuplicated = false;

        [SetUp]
        public void SetUp()
        {
            _data = new CutoffPhaseData()
            {
                Name = "RT-QA_Cutoff_Phase",
                Code = "RT_Auto",
                SortOrder = "0"
            };
        }

        #region"Test Case"
        [Test]
        [Category("Section_III")]
        [Ignore("The Cutoff Phase was removed from Purchasing menu - PIPE-21663, so this test sript will be ignored.")]
        public void E05_Purchasing_AddCutoffPhase()
        {
            // Step 1: navigate to this URL:http://dev.bimpipeline.com/Dashboard/Purchasing/CutoffPhases/Default.aspx
            CutoffPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CutoffPhases);

            // Delete cut off phase before creating a new one
            CutoffPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _data.Name);
            if (CutoffPhasePage.Instance.IsItemInGrid("Name", _data.Name) is true)
            {
                CutoffPhasePage.Instance.DeleteCutOffPhase(_data);
            }

            // Step 2-3-4: click on "+" Add button and create a new Cutoff Phase
            CutoffPhasePage.Instance.CreateNewCutoffPhase(_data);
            
            // Step 5. Create with existing name
            CutoffPhasePage.Instance.ClickAddToOpenCutoffPhaseModal();
            CutoffPhasePage.Instance.AddCutoffPhaseModal.CreateNewCutoffPhase(_data);
            string _expectedMessage = $"Not able to create Cutoff Phase {_data.Name} at this time.";
            string _actualMessage = CutoffPhasePage.Instance.GetLastestToastMessage();
            if (_actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Created duplicated Cutoff Phase with name { _data.Name} and Sort Order {_data.SortOrder}.");
                isDuplicated = true;
            }
            ExtentReportsHelper.LogPass(null, "The Cutoff Phase could not create with the existed number. The message is dispalyed as expected: " + _actualMessage);
            CutoffPhasePage.Instance.CloseToastMessage();

            // Close the modal
           CutoffPhasePage.Instance.CloseModal();

            // Insert name to filter and click filter by Contain value
            CutoffPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _data.Name);

            bool isFound = CutoffPhasePage.Instance.IsItemInGrid("Name", _data.Name);
            Assert.That(isFound, string.Format($"Cutoff Phase \"{_data.Name} \" was not display on grid."));

            // Open Cutoff Phase detail page and verify it have the Building Phase section
            CutoffPhasePage.Instance.ClickAnItemOnGrid("Name", _data.Name);
            if (!CutoffPhasePage.Instance.IsHaveBuildingPhaseSection)
            {
                ExtentReportsHelper.LogFail($"Building Phase section does not exist on Cutoff Phase detail page.");
                isDuplicated = true;
            }
            ExtentReportsHelper.LogPass(null, "Building Phase section is displayed on Cutoff Phase detail page.");

        }
        #endregion
        [TearDown]
        public void DeleteData ()
        {
            // Back to Cutoff Phases page
            CutoffPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CutoffPhases);
            BasePage.PageLoad();

            CutoffPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _data.Name);
            // 7. Select item and click deletete icon
            CutoffPhasePage.Instance.DeleteCutOffPhase(_data);

            if (isDuplicated)
            {
                ExtentReportsHelper.LogInformation("Delete the duplicated data.");
                CutoffPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _data.Name);
                CutoffPhasePage.Instance.DeleteItemInGrid("Name", _data.Name);
            }
        }
    }
}
