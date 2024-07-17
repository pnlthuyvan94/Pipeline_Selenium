using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Calculations;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B13_RT_01140 : BaseTestScript
    {
        private CalculationData calculationData;

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [SetUp]
        public void GetData()
        {
            calculationData = new CalculationData()
            {
                Description = "RT-QA Auto Calculation",
                Calculation = "*4.167"
            };
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void B13_Estimating_AddProductCalculation()
        {
            // Step 1: navigate to this URL:http://dev.bimpipeline.com/Dashboard/BuilderBom/BOMPhaseRules/Default.aspx
            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);

            //Delete data berfore new data
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.Contains, calculationData.Description);
            if(CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description) is true)
            {
                CalculationPage.Instance.DeleteCalculation(calculationData);
            }

            // Step 2 - 3: Create a new calculation
            CalculationPage.Instance.CreateNewCalculation(calculationData);

            // Insert name to filter and click filter by Contain value
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.Contains, calculationData.Description);

            if(CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Calculation with description {calculationData.Description} was not display on grid.</font>");
            }

            // 4. Step 4: Select item and click deletete icon
            /*CalculationPage.Instance.DeleteItemInGrid("Description", calculationData.Description);
            CalculationPage.Instance.WaitGridLoad();
            string successfulMess = $"Calculation deleted successfully.";
            if (successfulMess == CalculationPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Calculation deleted successfully!");
                CalculationPage.Instance.CloseToastMessage();
            }
            else
            {
                if (CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description))
                    ExtentReportsHelper.LogFail("Calculation could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Calculation deleted successfully!");
            }
            */

            CalculationPage.Instance.DeleteCalculation(calculationData);
        }
        #endregion
    }
}
