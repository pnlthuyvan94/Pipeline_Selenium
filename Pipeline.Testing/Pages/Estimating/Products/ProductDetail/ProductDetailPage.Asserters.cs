using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductDetail
{
    public partial class ProductDetailPage
    {
        /*
         * Verify head title and id of new Product
         */
        public bool IsCreateSuccessfully(ProductData product)
        {
            bool isPassed = true;
            if (!IsHeaderBreadscrumbCorect(product.Name))
            {
                SpecificControls breadscrumb = new SpecificControls(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/ul/li[2]");
                CommonHelper.HighLightElement(breadscrumb);
                isPassed = false;
            }
            if (product.Name != Name_txt.GetValue())
            {
                CommonHelper.HighLightElement(Name_txt);
                isPassed = false;
            }
            if (product.Description != Description_txt.GetValue())
            {
                CommonHelper.HighLightElement(Description_txt);
                isPassed = false;
            }
            if (product.Notes != Note_txt.GetValue())
            {
                CommonHelper.HighLightElement(Name_txt);
                isPassed = false;
            }
            if (product.Unit != Unit_ddl.SelectedItemName)
            {
                CommonHelper.HighLightElement(Unit_ddl);
                isPassed = false;
            }
            if (product.RoundingUnit != RoundingUnit_txt.GetValue())
            {
                CommonHelper.HighLightElement(RoundingUnit_txt);
                isPassed = false;
            }
            var actual = CommonHelper.CastValueToList(RoundingRule.Rule);
            var expected = CommonHelper.CastValueToList(product.RoundingRule);
            if (!CommonHelper.IsEqual2List(actual, expected))
            {
                CommonHelper.HighLightElement(Name_txt);
                isPassed = false;
            }
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Open product detail page");
            string buildingPhaseName = product.BuildingPhase.Remove(0, product.BuildingPhase.IndexOf("-") + 1);
            if (!BuildingPhase_grid.IsItemOnCurrentPage("Name", buildingPhaseName))
            {
                SpecificControls buildingPhase = new SpecificControls(FindType.XPath, _buildingGrid);
                CommonHelper.MoveToElement(buildingPhase, true);
                CommonHelper.HighLightElement(buildingPhase);
                isPassed = false;
            }
            SpecificControls manuGrid = new SpecificControls(FindType.XPath, _ManuGrid);
            CommonHelper.MoveToElement(manuGrid, true);
            if (!Manufacture_grid.IsItemOnCurrentPage("Manufacturer", product.Manufacture))
            {
                CommonHelper.HighLightElement(manuGrid);
                isPassed = false;
            }
            if (!Manufacture_grid.IsItemOnCurrentPage("Style", product.Style))
            {
                CommonHelper.HighLightElement(manuGrid);
                isPassed = false;
            }
            if (!Manufacture_grid.IsItemOnCurrentPage("Product Code", product.Code))
            {
                CommonHelper.HighLightElement(manuGrid);
                isPassed = false;
            }
            if (!isPassed)
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), "The product is created with invalid data.");
            return isPassed;
        }

        public bool IsHeaderBreadscrumbCorect(string _name)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedName = $"{_name}";
            return SubHeadTitle().Equals(expectedName) && !CurrentURL.EndsWith("pid=0");
        }
        public bool IsSaveProductSuccessful(string ProductDetailName)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Equals(ProductDetailName) && !CurrentURL.EndsWith("cid=0");
        }
        public bool IsErrorMessageManufacturerModalCorrect()
        {
            bool isPass = ManufacturerError_lb.GetText().Trim().Contains("The Manufacturer Name must be unique");
            if (isPass == true)
            {
                ExtentReportsHelper.LogPass("<font color='green'>Manufacturer error message is correct</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Manufacturer error message is not correct</font>");
            }            
            return isPass;                
        }
        public bool IsErrorMessageStyleModalCorrect()
        {
            bool isPass = StyleError_lb.GetText().Trim().Contains("The Style Name in the same Manufacturer must be unique");
            if(isPass == true)
            {
                ExtentReportsHelper.LogPass("<font color='green'>Style error message is correct</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Style error message is not correct</font>");
            }
            return isPass;

        }
    }
}
