using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Purchasing.CostType;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class E02_RT_01095 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        CostTypeData _costType;

        [SetUp]
        public void GetData()
        {
           _costType = new CostTypeData()
            {
                Name = "RT-QA_Cost Type",
                Description = "Cost Type Test",
            };
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void E02_Purchasing_AddCostType()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Purchasing/CostCodes/CostTypes.aspx
            CostTypePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostTypes);

            // Step 3: Populate all values

            // Create Cost Type - Click 'Save' Button
            CostTypePage.Instance.CreateCostType(_costType);

            // 7. Select item and click deletete icon
            CostTypePage.Instance.DeleteCostType(_costType.Name);
        }
        #endregion
    }
}
