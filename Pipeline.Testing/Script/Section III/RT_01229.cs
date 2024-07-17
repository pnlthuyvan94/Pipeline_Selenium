using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Purchasing.CostCode;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class E03_RT_01229 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private CostCodeData costCodeData;

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            costCodeData = new CostCodeData()
            {
                Name = "RT-QA_Test",
                Description = "Run test case by automation"
            };
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void E03_Purchasing_AddCostCode()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Purchasing/CostCodes/CostCodes.aspx
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);

            // 2: Add new Cost Code
            string _expectedMessage = $"Cost Code {costCodeData.Name} created successfully!";
            CostCodePage.Instance.AddCostCodes(costCodeData, _expectedMessage, false);

            // 6: Verify the new Contract create successfully
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, costCodeData.Name);
            bool isFound = CostCodePage.Instance.IsItemInGrid("Name", costCodeData.Name);
            Assert.That(isFound, string.Format("New Cost Code \"{0} \" was not display on grid.", costCodeData.Name));

            // 7: Add new Cost Code with duplicate name and verify it
            string _expectedDuplicateMessage = $"Not able to create cost code {costCodeData.Name} at this time.";
            CostCodePage.Instance.AddCostCodes(costCodeData, _expectedDuplicateMessage, true);

        }
        #endregion

        [TearDown]
        public void Delete()
        {
            // 8: Delete Contract Document
            CostCodePage.Instance.DeleteCostCodesByName(costCodeData.Name);
        }
    }
}
