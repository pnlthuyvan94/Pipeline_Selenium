using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Estimating.SpecSet
{
    public partial class SpecSetPage
    {
        public void ClickAddToShowCategoryModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddSpecSetModal = new AddSpecSetGroup.AddSpecSetGroupModal();
        }

        public SpecSetPage FindItemInGridWithTextContains(string columnName, string value)
        {
            int numberOfPage = SpecSetGroup_Grid.GetTotalPages;
            for (int i = 1; i <= numberOfPage; i++)
            {
                if (!i.Equals(1))
                {
                    SpecSetGroup_Grid.NavigateToPage(i);
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']");
                }
                if (SpecSetGroup_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value))
                    break;
            }
            return this;
        }

        public void Navigatepage(int i)
        {
            SpecSetGroup_Grid.NavigateToPage(i);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']/div[1]");
        }

        public void ChangeSpecSetPageSize(int size)
        {
            SpecSetGroup_Grid.ChangePageSize(size);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']/div[1]");
            System.Threading.Thread.Sleep(2000);
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            //SpecSetGroup_Grid.ClickItemInGrid(columnName, value);
            Button item = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSpecSetGroups_ctl00']//tbody/tr/td/a[contains(text(),'{value}')]");
            item.Click();
            PageLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return SpecSetGroup_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            SpecSetGroup_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']/div[1]");
        }
        public void DeleteItemInGridSpecSet(string columnName, string value)
        {
            Button delete = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSpecSetGroups_ctl00']//tbody/tr/td/a[contains(text(),'{value}')]//..//following-sibling::td//input[@title='Delete']");
            delete.Click();
            ConfirmDialog(ConfirmType.OK);
            SpecSetGroup_Grid.WaitGridLoad();
        }

        public void ImportExporOnSpecSetGroup(string item)
        {
            ImportExporFromMoreMenu(item);
        }

        private void SelectView(string view)
        {
            DropdownList View_ddl = new DropdownList(FindType.XPath, "//*[@id='ddlViewType']");

            if (View_ddl.SelectedValue == view)
                return;

            View_ddl.SelectItem(view, false);
            PageLoad();
        }

        public void ImportFile(string ViewName, string importTitle, string uploadFileName)
        {
            // Select view from drop down list
            SelectView(ViewName);

            string textboxUpload_Xpath, importButtion_Xpath, label_Xpath;
            switch (importTitle)
            {
                case "Spec Sets Product Import":
                    {
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSpecSets']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSpecSets']";
                        label_Xpath = "//*[@id='ctl00_CPH_Content_lblSpecSetGroupsToSpecSetCreation']";
                        break;
                    }
                case "Spec Set Group / Spec Set Creation Import":
                    {
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSpecSetGroupsToSpecSetCreation']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSpecSetGroupsToSpecSetCreation']";
                        label_Xpath = "//*[@id='ctl00_CPH_Content_lblImportSpecSets']";
                        break;
                    }

                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle} on menu {ViewName}.</font>");
                    return;
            }

            Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
            Button ProductImport_btn = new Button(FindType.XPath, importButtion_Xpath);

            // Get upload file location
            string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + uploadFileName;

            // Upload
            Upload_txt.SendKeysWithoutClear(fileLocation);
            System.Threading.Thread.Sleep(500);
            ProductImport_btn.Click();
            Label textbox = new Label(FindType.XPath, label_Xpath);

            // Verify the upload file name on Job Quantities File text box
            if (textbox.WaitForElementIsInVisible(10, false) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>SpecSet Group file {uploadFileName} is uploaded successfully.</b></font>");
            PageLoad();
        }

        public void DeleteSpecSet(string groupName)
        {
            DeleteItemInGridSpecSet("Name", groupName);
            string successfulMess = $"Spec Set Group deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "Spec Set Group deleted successfully!");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", groupName))
                    ExtentReportsHelper.LogFail("Spec Set Group could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(null, "Spec Set Group deleted successfully!");
            }
        }

        /// <summary>
        /// Get total item on the Category page
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return SpecSetGroup_Grid.GetTotalItems;
        }

        public void ImportErrorFile(string errorMsg, string viewName, string importTitle, string uploadFileName)
        {
            // Select view from drop down list
            SelectView(viewName);

            string textboxUpload_Xpath, importButtion_Xpath, label_Xpath;
            switch (importTitle)
            {
                case "Spec Sets Product Import":
                    {
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSpecSets']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSpecSets']";
                        label_Xpath = "//*[@id='ctl00_CPH_Content_lblImportSpecSets']";
                        break;
                    }
                case "Spec Set Group / Spec Set Creation Import":
                    {
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSpecSetGroupsToSpecSetCreation']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSpecSetGroupsToSpecSetCreation']";
                        label_Xpath = "//*[@id='ctl00_CPH_Content_lblSpecSetGroupsToSpecSetCreation']";
                        break;
                    }

                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle} on menu {viewName}.</font>");
                    return;
            }

            Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
            Button ProductImport_btn = new Button(FindType.XPath, importButtion_Xpath);

            // Get upload file location
            string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + uploadFileName;

            // Upload
            Upload_txt.SendKeysWithoutClear(fileLocation);
            System.Threading.Thread.Sleep(500);
            ProductImport_btn.Click();
            PageLoad();
            if (errorMsg != string.Empty)
            {
                Label textbox = new Label(FindType.XPath, label_Xpath);

                // Verify the toast message
                string actualMessage = textbox.GetText().ToString();
                string expectedToastMess = $"{errorMsg}";
                if (expectedToastMess.Equals(actualMessage) is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The toast error message is same as the expectation.</b></font>" +
                                            $"<br>Actual Msg: <font color='lavender'>{actualMessage}</font>");
                else
                    ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>The toast message is NOT same as the expectation.</b></font>" +
                                            $"<br>Expected: {expectedToastMess}" +
                                            $"<br>Actual: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Import Empty File is uploaded With Empty Message</b></font>");
            }
        }

        public void CreateNewSpecSetGroup(string specSetGroupName)
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            SpecSetGroupName_txt.SetText(specSetGroupName);
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']/div[1]");
            string successfulMess = $"Spec Set Group added successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Spec Set Group added successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Spec Set Group could not be added!</font>");
            }
        }

    }
}
