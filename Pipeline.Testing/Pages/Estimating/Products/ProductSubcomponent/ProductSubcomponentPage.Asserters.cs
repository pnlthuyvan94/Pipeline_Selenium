using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent
{
    public partial class ProductSubcomponentPage
    {
        /*
         * Verify head title and id of new Product
         */
        public void IsProductSubcomponentPage()
        {
            if (!Subcomponent_Title.IsExisted())
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Can not open Product Subcomponent page</b></font>");
            }
            else
                ExtentReportsHelper.LogPass("<font color ='green'><b>Opened successfully Product Subcomponent page</b></font>");
        }

        public void VerifyViewProductAssignmentPage(string typeView)
        {
            View_ddl.SelectItem(typeView);
            //Waiting view dropdown list loading 
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]");

            if (!View_ddl.IsExisted())
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Can not open " + typeView + " page</b></font>");
            }
            else
                ExtentReportsHelper.LogPass("<font color ='green'><b>Opened successfully the " + typeView + " page</b></font>");
        }
        public void VerifyAddSpecSetConvertForm()
        {

            if (AddbtnConvertFrom.WaitForElementIsVisible(5))
            {
                AddbtnConvertFrom.Click();
                System.Threading.Thread.Sleep(1000);
                if (ConvertFromModal.WaitForElementIsVisible(10))
                {
                    //SavebtnConvertFrom.Click();
                    WaitConvertFromGridLoad();
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

        public void VerifyAddSpecSetConvertTo()
        {

            if (AddbtnConvertTo.WaitForElementIsVisible(5))
            {
                AddbtnConvertTo.Click();
                System.Threading.Thread.Sleep(1000);
                if (ConvertToModal.WaitForElementIsVisible(10))
                {
                    SavebtnConvertTo.Click();
                    WaitConvertFromGridLoad();
                }
                else
                    ExtentReportsHelper.LogFail("<font color ='red'><b> Convert To Modal not display </font></b>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'><b> Can not click Add button </font></b>");
        }

        public void VerifyOptionsListInGrid(string BuildingPhase, string Option)
        {
            Label Option_lbl = new Label(FindType.XPath, $"//*[@class='rtRelativeWrapper']/div[@class='rtContent']//a[contains(text(),'{Option}')]");
            Button SubCompomentOption_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlSubcomponents']//span[contains(text(),'{BuildingPhase}')]//ancestor::td//../following-sibling::td/section[contains(@id,'Options')]");
            SubCompomentOption_btn.HoverMouse();
            if (Option_lbl.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> SubComponent Option with Name: {Option} is display in grid </font></b>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'><b> SubComponent Option with Name: {Option} is not display in grid</font></b>");
            }
        }

        public void VerifyOptionsListInCopySubcomponent(string BuildingPhase, string Option)
        {
            Label Option_lbl = new Label(FindType.XPath, $"//*[@id='RadToolTipWrapper_ctl00_CPH_Content_rgSubToCopy_ctl00_ctl04_RadToolTip2']//*[@class='rtRelativeWrapper']/div[@class='rtContent']//a[contains(text(),'{Option}')]");
            Button SubCompomentOption_lbl = new Button(FindType.XPath, $"(//*[@id='ctl00_CPH_Content_rgSubToCopy']//td[contains(text(),'{BuildingPhase}')]//../following-sibling::td/section//*)[1]");
            SubCompomentOption_lbl.HoverMouse();
            if (Option_lbl.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> SubComponent Option with Name: {Option} is display in Copy Subcomponents Modal.</font></b>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'><b> SubComponent Option with Name: {Option} is not display in Copy Subcomponents Modal.</font></b>");
            }
        }

        public void VerifySubcomponentNameOnGrid(string SubcomponentName, string Description)
        {
            if (Description == string.Empty)
            {
                Label SubcompomentName_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlSubcomponents']//td/a[contains(text(),'{SubcomponentName}')]");
                if (SubcompomentName_lbl.IsDisplayed() && SubcompomentName_lbl.GetText().Equals(SubcomponentName) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> SubComponent Name with Name: {SubcompomentName_lbl.GetText()} is display in On Grid </font></b>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color ='red'><b> SubComponent Name with Name: {SubcompomentName_lbl.GetText()} is not display in On Grid</font></b>");
                }
            }
            else
            {
                Label SubcompomentName_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlSubcomponents']//td/a[contains(text(),'{SubcomponentName}')]");

                if (SubcompomentName_lbl.IsDisplayed() && (SubcompomentName_lbl.GetText()).Equals(SubcomponentName + Description) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> SubComponent Name with Name: {SubcompomentName_lbl.GetText()} is display in Copy Subcomponents </font></b>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color ='red'><b> SubComponent Name with Name: {SubcompomentName_lbl.GetText()} is not display in Copy Subcomponents</font></b>");
                }
            }

        }
        public void VerifySubcomponentNameInCopySubcomponentModal(string SubcomponentName)
        {
            Label SubcompomentName_btn = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSubToCopy_ctl00']//td/a[contains(text(),'{SubcomponentName}')]");
            if (SubcompomentName_btn.IsDisplayed() && SubcompomentName_btn.GetText().Equals(SubcomponentName) is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> SubComponent Name with Name: {SubcompomentName_btn.GetText()} is display in Copy Subcomponents Modal.</font></b>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'><b> SubComponent Name with Name: {SubcompomentName_btn.GetText()} is not display in Copy Subcomponents Modal.</font></b>");
            }
        }


        public void VerifyUseDataInSubcomponent(string expectdata)
        {
            UseSubcomponent_txt.WaitForElementIsVisible(10);
            if (UseSubcomponent_txt.IsDisplayed() && UseSubcomponent_txt.GetText().Equals(expectdata))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(UseSubcomponent_txt), $"The Use Data of SubcomponentPage is displayed with value <font color='green'><b>{UseSubcomponent_txt.GetText()}</b></font>");//highlights green
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(UseSubcomponent_txt), $"The The Use Data of SubcomponentPage is wrong displayed with value <font color='green'><b>{UseSubcomponent_txt.GetText()}</b></font>");//highlights green
            }

        }

        public bool IsStyleModalDisplayed()
        {
            if (!AddStyleModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Style & Add Manufacture Group\" modal is not displayed after 10s.");
            return (AddStyleModalTitle_lbl.GetText() == "Add Manufacturer and Style");
        }

        public void VerifyAddNewStyleInSubcomponent()
        {
            Button addStyle_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddStyle']");
            if (addStyle_btn.IsDisplayed())
            {
                addStyle_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddStyle']/div[1]");
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Add Style Button is not display in Product Subcomponent");
            }

            if (IsStyleModalDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Add Manufacturer and Style modal isn't displayed or title is incorrect." +
                    $"<br>Expected title: 'Add Manufacturer and Style'</font>");
            else
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Add Manufacturer and Style modal displayed successfully.</b></font>");
        }

    }
}
