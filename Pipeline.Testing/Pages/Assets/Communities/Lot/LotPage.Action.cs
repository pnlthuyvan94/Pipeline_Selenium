using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther;
using Pipeline.Testing.Pages.Assets.Communities.Lot.AddLot;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Assets.Communities.Lot
{
    public partial class LotPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Lot_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            Lot_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Lot_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Lot_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            Lot_Grid.WaitGridLoad();
        }

        public void OpenAddLotModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddHouse']").Click();
            CommonHelper.VisibilityOfAllElementsLocatedBy(3, "//*[@id='lotinfomodal']/section/header/h1");
            AddLotModal = new AddLotModal();
        }

        public void WaitGridLoad()
        {
            Lot_Grid.WaitGridLoad();
        }

        public void OpenAssignPlanToLotModal(string lotNumber)
        {
            string AssignPlanToLotXpath = $"//*[@id='ctl00_CPH_Content_rgLots_ctl00']/tbody/tr[td/a[text()='{lotNumber}']]/td/input[contains(@src, 'Images/home')]";
            IWebElement assignPlanToLot = FindElementHelper.FindElement(FindType.XPath, AssignPlanToLotXpath);
            if (assignPlanToLot != null)
            {
                assignPlanToLot.Click();
                // Open asigned modal
                AssignedModal = new AssignLotOrPhaseOrHouseToEachOtherModal();
            }
        }
        public string ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case "Lot Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportLots']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportLots']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblStatusMessageLots']";
                    break;

                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return string.Empty;
            }

            // Upload file to corect grid
            Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
            Button LotImport_btn = new Button(FindType.XPath, importButtion_Xpath);

            // Get upload file location
            string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFileDir;

            // Upload
            Upload_txt.RefreshWrappedControl();
            Upload_txt.SendKeysWithoutClear(fileLocation);
            System.Threading.Thread.Sleep(500);
            LotImport_btn.Click(false);

            // Get message
            IWebElement message = FindElementHelper.FindElement(FindType.XPath, message_Xpath);
            return message.Displayed ? message.Text : string.Empty;

        }

        /// <summary>
        /// Remove lot from community by number
        /// </summary>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public  bool RemoveLotFromCommunity(string lotNumber)
        {
            if (IsItemInGrid("Number", lotNumber))
            {
                DeleteItemInGrid("Number", lotNumber);
                WaitGridLoad();
                bool isDeleteSuccessful = true;

                string expectedMess = "Lot deleted successfully!";
                if (expectedMess == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Lot: {lotNumber} deleted successfully!</b></font>");
                    CloseToastMessage();
                }
                else
                {
                    if (IsItemInGrid("Number", lotNumber))
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Lot: {lotNumber} could not be deleted!</font>");
                        isDeleteSuccessful = false;
                    }
                    else
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>Lot: {lotNumber} deleted successfully!</b></font>");
                    }
                }
                return isDeleteSuccessful;
            }
            return true;
        }

    }

}
