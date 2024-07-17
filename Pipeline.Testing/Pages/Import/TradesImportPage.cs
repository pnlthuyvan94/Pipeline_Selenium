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
    public class TradesImportPage : DashboardContentPage<TradesImportPage>
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
                case "Trade Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fileTradeImport']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_btnImportTrade']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblTrades']";
                    break;
                case "Trade to Building Phase Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fileTradesToPhases']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_btnImportTradeToPhase']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblTradesToPhases']";
                    break;
                case "Trade to Vendor Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fileTradesToVendor']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_btnImportTradeToVendor']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblTradesToVendor']";
                    break;
                case "Trade to Scheduling Task Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fileTradesToSchedulingTask']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_btnImportTradeToSchedulingTask']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblTradesToSchedulingTask']";
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
