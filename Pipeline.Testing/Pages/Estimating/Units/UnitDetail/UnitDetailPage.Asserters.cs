using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;

namespace Pipeline.Testing.Pages.Estimating.Units.UnitDetail
{
    public partial class UnitDetailPage
    {

        public bool IsDisplayDataCorrectly(UnitData data)
        {
            if (!SubHeadTitle().Equals(data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Subheader result: {data.Name}. Actual result: {SubHeadTitle()}");
                return false;
            }
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name result: {data.Name}. Actual result: {Name_txt.GetValue()}");
                return false;
            }
            if (data.Abbreviation != Abbreviation_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Abbreviation result: {data.Abbreviation}. Actual result: {Abbreviation_txt.GetValue()}");
                return false;
            }
            return true;
        }

        public bool IsDisplayAddButton()
        {
            return AddProductToUnit_btn.WaitForElementIsVisible(5);
        }

        public void VerifyHyperlinkToProduct(string productName)
        {
            string xpathProduct = $"//*[@id='ctl00_CPH_Content_rgProducts_ctl00']/tbody/tr/td[./a[text()='{productName}']]/a";
            IWebElement product = FindElementHelper.FindElement(FindType.XPath, xpathProduct);
            if (product != null)
            {
                CommonHelper.OpenLinkInNewTab(product.GetAttribute("href"));
                CommonHelper.SwitchTab(1);
                PageLoad();
                if (ProductDetailPage.Instance.IsHeaderBreadscrumbCorect(productName))
                    ExtentReportsHelper.LogPass($"Open Product: {productName} page successfully with URL: {ProductDetailPage.Instance.CurrentURL}");
                else
                    ExtentReportsHelper.LogFail($"Open Product: {productName} page Unsuccessfully.");
                CommonHelper.CloseCurrentTab();
                CommonHelper.SwitchTab(0);
            }
            else
                ExtentReportsHelper.LogFail($"Current screen.");
        }
    }
}
