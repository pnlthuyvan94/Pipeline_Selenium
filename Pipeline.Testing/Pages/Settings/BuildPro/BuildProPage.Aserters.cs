using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;

namespace Pipeline.Testing.Pages.Settings.BuildPro
{
    public partial class BuildProPage
    {
        public bool VerifyCommunitySyncedSuccessfully(CommunityData data)
        {
            string xpath = $"//tbody/tr[./td['{data.Name}'] and ./td[.='{data.Code}'] ]";
            return CommonHelper.WaitUntilElementVisible(5, xpath);
        }

        public bool VerifyCommunitySyncedSuccessfullyOnOrgListPage(CommunityData data)
        {
            bool valid = true;
            string xpath = $"//a[contains(text(),'{data.Name}')]";
            Label item = new Label(FindType.XPath, xpath);
            item.WaitForElementIsVisible(5);
            item.JavaScriptClick();
            PageLoad();
            if (!data.Code.Equals(BP_OrgNumber_Txt.GetValue()))
            {
                valid = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BP_OrgNumber_Txt), $"The Community Code is not display as expected.<br>Actual {BP_OrgNumber_Txt.GetText()}<brExpected>: {data.Code}");
            }
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(BP_OrgNumber_Txt), $"The Community Code is displayed as expected.<br>Actual : {data.Code}");

            if (!data.Name.Equals(BP_OrgName_Txt.GetValue()))
            {
                valid = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BP_OrgName_Txt), $"The Community Name is not display as expected.<br>Actual {BP_OrgName_Txt.GetText()}<brExpected>: {data.Name}");
            }
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(BP_OrgName_Txt), $"The Community Name is displayed as expected.<br>Actual : {data.Name}");

            string state = string.Empty;
            switch (data.State)
            {
                case "TX":
                    state = "Texas";
                    break;
                case "AL":
                    state = "Alabama";
                    break;
                default:
                    break;
            }
            if (!state.Equals(BP_OrgState_Txt.SelectedItemName))
            {
                valid = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BP_OrgState_Txt), $"The Community State is not display as expected.<br>Actual {BP_OrgState_Txt.SelectedItemName}<brExpected>: {data.State}");
            }
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(BP_OrgState_Txt), $"The Community State is displayed as expected.<br>Actual : {data.State}");

            if (!data.Zip.Equals(BP_OrgZip_Txt.GetValue()))
            {
                valid = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BP_OrgZip_Txt), $"The Community Zip is not display as expected.<br>Actual {BP_OrgZip_Txt.GetValue()}<brExpected>: {data.Zip}");
            }
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(BP_OrgZip_Txt), $"The Community Zip is displayed as expected.<br>Actual : {data.Zip}");

            return valid;
        }

        public bool VerifyTheCommunitySyncToBuildProSuccessfully(string communityName)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults_ctl00']/tbody/tr/td[contains(.,'The Community \"{communityName}\"  has been received by BuildPro.')]";
            return CommonHelper.WaitUntilElementVisible(5, xpath);
        }

        public bool VerifyTheBuildingPhaseExistedOnBuildPro(BuildingPhaseData data)
        {
            if (BP_CGVisionCodeCost_Btn.IsExisted(false))
            {
                BP_CGVisionCodeCost_Btn.Click();
                PageLoad();
            }
            SpecificControls ele = new SpecificControls(FindType.XPath, $"//table/tbody/tr[./td[.='{data.Code}'] and ./td/input[@value='{data.AbbName}']]");
            if (ele.WaitForElementIsVisible(5, false))
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ele), $"The Building Phase with Name <font color='green'><b><i>{data.Name}</font></b></i> and Code <font color='green'><b><i>{data.Code}</font></b></i> is displayed on current screen");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"The Building Phase with Name <font color='green'><b><i>{data.Name}</font></b></i> and Code <font color='green'><b><i>{data.Code}</font></b></i> is <b>NOT</b> exist on current screen");
                return false;
            }
        }

        public bool VerifyTheVendorExistedOnBuildPro(VendorData data)
        {
            SearchVendor();
            string xpath = $"//*[@id='tblSuppliers']/tbody[2]/tr[./td/a[.='{data.Name}'] and ./td[contains(.,'{data.Code}')]]";
            SpecificControls ele = new SpecificControls(FindType.XPath, xpath);
            if (ele.WaitForElementIsVisible(5, false))
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ele), $"The Vendor with Name <font color='green'><b><i>{data.Name}</font></b></i> and Code <font color='green'><b><i>{data.Code}</font></b></i> is displayed on current screen");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"The Vendor with Name <font color='green'><b><i>{data.Name}</font></b></i> and Code <font color='green'><b><i>{data.Code}</font></b></i> is <b>NOT</b> exist on current screen");
                return false;
            }
        }

        public bool VerifyTheJobSyncToBuildPro(string POName, string vendorName)
        {
            string xpath = $"//*[@id='divGridview']/table/tbody/tr[./td/a[contains(.,'{POName}')] and ./td//a[.='{vendorName}']]";
            Label po_lbl = new Label(FindType.XPath, xpath);
            if (!po_lbl.IsExisted(false))
            {
                ExtentReportsHelper.LogFail($"The PO <font color='green'><b>{POName}</b></font> and Vendor Name <font color='green'><b>{vendorName}</b></font> could not be found.");
                return false;
            }
            po_lbl.UpdateValueToFind($"//*[@id='divGridview']/table/tbody/tr[./td/a[contains(.,'{POName}')] and ./td//a[.='{vendorName}']]/td/a");
            po_lbl.RefreshWrappedControl();
            ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(po_lbl), $"The PO <font color='green'><b>{POName}</b></font> and Vendor Name <font color='green'><b>{vendorName}</b></font> is displayed.");
            return true;
        }
    }
}
