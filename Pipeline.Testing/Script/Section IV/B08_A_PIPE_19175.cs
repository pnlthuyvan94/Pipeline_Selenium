using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Estimating.Calculations;
using Pipeline.Testing.Pages.Estimating.Calculations.CalculationDetail;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;

namespace Pipeline.Testing.Script.Section_IV
{
    class B08_A_PIPE_19175 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private const string CALCULATION_EDITED_SUCCESSFULLY = "Calculation edited successfully.";
        private const string CALCULATION_DETAIL_SUBCOMPONENT = "QTY * 196";
        private const string CALCULATION_DETAIL_STYLE_CONVERSION = "QTY * 196";
        private const string CALCULATION_DETAIL_PRODUCT_CONVERSION = "QTY * 196";

        CalculationData calculationData, calculationDataUpdate;
        ProductData productData;
        SpecSetData specSet, specSetProductConversion, specSetStyleConversion;
        StyleData styleData;
        ManufacturerData manuData;

        [SetUp]
        public void SetupData()
        {
            manuData = new ManufacturerData()
            {
                Name = "QA_RT_Automation_Manufacturer_19175"
            };
            styleData = new StyleData()
            {
                Name = "QA_RT_Automation_Style_19175",
                Manufacturer = manuData.Name
            };
            calculationData = new CalculationData
            {
                Description = "RT_QA_Automation_Calculation",
                Calculation = "QTY*195"

            };
            calculationDataUpdate = new CalculationData
            {
                Description = "RT_QA_Automation_Calculation",
                Calculation = "QTY*196"
            };
            productData = new ProductData
            {
                Name = "QA_RT_Automation_Product_19175"
            };
            specSet = new SpecSetData
            {
                GroupName = "Automation_RT_QA_Specset_Group",
                SpectSetName = "RT_QA_Automation_SpecSet"
            };
            specSetProductConversion = new SpecSetData
            {
                ProductCalculation = calculationDataUpdate.Description + $" ({calculationDataUpdate.Calculation})"
            };
            specSetStyleConversion = new SpecSetData
            {
                StyleCalculation = calculationDataUpdate.Description + $" ({calculationDataUpdate.Calculation})"
            };
        }

        [Test]
        [Category("Section_IV")]

        public void B08_A_Estimating_Detail_Pages_Calculations_Calculation()
        {
            //Step 1: Go to calculation page and pick a one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> step 1: Go to calculation page and pick a calculation </font>");
            OptionPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.Contains, calculationData.Description);
            //Step 2: Edit the calculation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 2: Edit the calculation</font>");
            if (CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description))
            {
                CalculationPage.Instance.SelectItemInGrid("Description", calculationData.Description);
            }
            CalculationDetailPage.Instance.UpdateCalculation(calculationDataUpdate);
            string urlCal = CalculationDetailPage.Instance.CurrentURL;
            //Step 3: Verify toast message after editing
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 3: Verify toast message after editing</font>");
            //Verify toast message
            if (CalculationDetailPage.Instance.GetLastestToastMessage().Trim().Equals(CALCULATION_EDITED_SUCCESSFULLY))
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'> Calculation edited successfully</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, "<font color='red'> Calculation edited not successfully</font>");
            }

            //Step 4: Go to Product/Subcomponent
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 4: Go to Product/Subcomponent</font>");
            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData.Name);
            ProductPage.Instance.SelectItemInGrid("Product Name", productData.Name);
            ProductPage.Instance.LeftMenuNavigation("Subcomponents");
            //Step 5: Verify the value of updated calculation is now appeared on subcomponent
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 5: Verify the value of updated calculation is now appeared on subcomponent</font>");          
            ProductSubcomponentPage.Instance.VerifyItemInGrid("Calculation", calculationDataUpdate.Description + " (" + calculationDataUpdate.Calculation + ")");
            //Step 5: Back to Calculation page to verify the updated calculation is now appeared on product subcomponent calculation culumn
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 5: Back to Calculation page to verify the updated calculation is now appeared on product subcomponent calculation culumn</font>");
            CommonHelper.OpenURL(urlCal);
            CalculationDetailPage.Instance.IsIemInProductSubcomponentGrid("Calculation", CALCULATION_DETAIL_SUBCOMPONENT);
            //Step 6: Go to specset to verify the updated values of calculation on Product and Style coversions
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 6: Go to specset to verify the updated values of calculation on Product and Style coversions</font>");
            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSet.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSet.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSet.GroupName);
                SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(specSet.SpectSetName, "Expand");
                SpecSetDetailPage.Instance.IsIemOnProductConversionGrid(specSet.SpectSetName, "Calculation", specSetProductConversion.ProductCalculation);
                SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSet.SpectSetName, "Calculation", specSetStyleConversion.StyleCalculation);
            }
            //Step 7: Go back to calculation page to verify the style conversion calculation and product conversion calculation grids
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 7: Go back to calculation page to verify the style conversion calculation and product conversion calculation grids</font>");
            CommonHelper.OpenURL(urlCal);
            CalculationDetailPage.Instance.IsIemInStyleConversionGrid("Calculation", CALCULATION_DETAIL_STYLE_CONVERSION);
            CalculationDetailPage.Instance.IsIemInProductConversionGrid("Calculation", CALCULATION_DETAIL_PRODUCT_CONVERSION);
            //Set the calculation to origin
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Back the calculation to origin</font>");
            CommonHelper.OpenURL(urlCal);
            CalculationDetailPage.Instance.UpdateCalculation(calculationData);            

        }
       
    }
}
