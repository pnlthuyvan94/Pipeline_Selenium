using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Assets.Options.OptionImport
{
        public partial class OptionImportPage 
        {

            /******************************* View = Option Attribute *********************************/

            /// <summary>
            /// Verify import grid displays or not
            /// </summary>
            /// <param name="gridTitle"></param>
            /// <returns></returns>
            public bool IsImportGridDisplay(string view, string gridTitle)
            {
                DropdownList view_ddl = new DropdownList(FindType.XPath, "//*[@id='ddlViewType']");
                if (view_ddl.IsDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find 'View' drop down list on the current page.</font>");
                    return false;
                }

                if (view.Equals(view_ddl.SelectedItemName) is false)
                {
                    // Current view is different from expected one, then re-select it
                    view_ddl.SelectItem(view, true);
                    PageLoad();
                }


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

            /// <summary>
            /// Import file in Product Import page
            /// </summary>
            /// <param name="importTitle"></param>
            /// <param name="importFileDir"></param>
            /// <returns></returns>
            public string ImportFile(string importTitle, string importFileDir)
            {
                string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
                switch (importTitle)
                {
                    case "Option Import":
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptions']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptions']";
                        message_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptions']";
                        break;
                    case "Option Group Import":
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionGroups']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptionGroups']";
                        message_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionGroups']";
                        break;
                    case "Option Cost Group Import":
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionCostGroups']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptionCostGroups']";
                        message_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionCostGroups']";
                        break;
                    case "Option Custom Fields Import":
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionCustomFields']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbOptionCustomFields']";
                        message_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionCustomFields']";
                        break;
                    case "Advanced Update Option Import":
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionAdvanced']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportAdvancedOption']";
                        message_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionAdvanced']";
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
                catch (Exception)
                {
                    return string.Empty;
                }

            }
        }
    }
