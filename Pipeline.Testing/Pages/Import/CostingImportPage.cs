using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Import
{
    public class CostingImportPage : DashboardContentPage<CostingImportPage>
    {
        public bool IsImportLabelDisplay(string gridTitle)
        {
            Label import_lbl = new Label(FindType.XPath, $"//h1[text()='{gridTitle}']");
            if (import_lbl.IsDisplayed() is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>{gridTitle} grid view displays successfully.</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find {gridTitle} grid view on the current page.</font>");
                return false;
            }
        }

        public string ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case "Option (Tier 1)":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHistoricBidCostsOption']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHistoricBidCostsOption']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHistoricBidCostsOption']";
                    break;
                case "House (Tier 2)":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHistoricBidCostsHouse']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHistoricBidCostsHouse']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHistoricBidCostsHouse']";
                    break;
                case "Community (Tier 3)":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHistoricBidCostsCommunity']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHistoricBidCostsCommunity']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHistoricBidCostsCommunity']";
                    break;
                case "Community House Option (Tier 4)":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHistoricBidCostsCommunityOptionHouse']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHistoricBidCostsCommunityOptionHouse']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHistoricBidCostsCommunityOptionHouse']";
                    break;
                case "Vendors Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendors']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportVendors']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendors']";
                    break;
                case "Vendors To Building Phases Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendorsToBuildingPhases']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbUploadVendorsToBuildingPhases']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendorsToBuildingPhases']";
                    break;
                case "Vendor Option Bid Costs - House/Option Cost Tier 2 Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuTier2VendorOptionBidCosts']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbUploadTier2VendorOptionBidCosts']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblTier2VendorOptionBidCosts']";
                    break;
                case "Product Base Costs Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendorBaseCosts']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportVendorBaseCosts']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendorBaseCosts']";
                    break;
                case "Vendors To Community Overrides Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendorsToCommunityOverrides']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbUploadVendorsToCommunityOverrides']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendorsToCommunityOverrides']";
                    break;
                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return string.Empty;
            }
            try
            {
                // Upload file to corect grid
                Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
                Button Import_btn = new Button(FindType.XPath, importButtion_Xpath);

                // Get upload file location
                string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFileDir;

                // Upload
                Upload_txt.RefreshWrappedControl();
                Upload_txt.SendKeysWithoutClear(fileLocation);
                System.Threading.Thread.Sleep(500);
                Import_btn.Click(false);
                PageLoad();

                // Get message
                Label message = new Label(FindType.XPath, message_Xpath);
                return message.IsDisplayed() ? message.GetText() : string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
