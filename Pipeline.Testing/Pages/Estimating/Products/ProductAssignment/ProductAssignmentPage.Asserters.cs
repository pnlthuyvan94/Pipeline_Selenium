using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductAssignment
{
    public partial class ProductAssignmentPage
    {
        /*
         * Verify head title and id of new Product
         */
        public void IsProductAssignmentPage()
        {
            if (!View_ddl.IsExisted())
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Can not open Product Assignment page</b></font>");
            }
            else
                ExtentReportsHelper.LogPass("<font color ='green'><b>Opened successfully Product Assignment page</b></font>");
        }

        public void VerifyViewProductAssignmentPage(string typeView)
        {
            View_ddl.SelectItem(typeView);
            //Waiting view dropdown list loading 
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]");
            
            if (!View_ddl.IsExisted())
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Can not open "+ typeView + " page</b></font>");
            }
            else
                ExtentReportsHelper.LogPass("<font color ='green'><b>Opened successfully the "+ typeView +" page</b></font>");
        }
        public void VerifyAddSpecSetConvertForm(ProductAssignmentData productAssignmentData)
        {
            
            if (AddbtnConvertFrom.WaitForElementIsVisible(5))
            {
                AddbtnConvertFrom.Click();
                System.Threading.Thread.Sleep(5000);
                if (ConvertFromModal.WaitForElementIsVisible(10))
                {
                    AddConvertFrom(productAssignmentData);
                    
                }
                else
                    ExtentReportsHelper.LogFail("<font color ='red'><b> Convert Form Modal not display </font></b>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'><b> Can not click Add button </font></b>");
        }

        public bool VerifyDeleteItemInConverFromGird()
        {
            if (ConvertFrom_Grid.GetTotalItems > 0)
            {
                return false;
            }
            else
                return true;
        }

        public void VerifyAddSpecSetConvertTo(ProductAssignmentData productAssignmentData)
        {

            if (AddbtnConvertTo.WaitForElementIsVisible(5))
            {
                AddbtnConvertTo.Click();
                System.Threading.Thread.Sleep(5000);
                if (ConvertToModal.WaitForElementIsVisible(10))
                {
                    AddConvertTo(productAssignmentData);
                }
                else
                    ExtentReportsHelper.LogFail("<font color ='red'><b> Convert To Modal not display </font></b>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'><b> Can not click Add button </font></b>");
        }
    }
}
