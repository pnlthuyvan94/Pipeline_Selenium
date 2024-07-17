using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using System;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Estimating.Products
{
    public partial class ProductPage
    {
        public void ClickAddToProductIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            PageLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ProductPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public ProductPage DeleteItemInGrid(string columnName, string value)
        {
            ProductPage_Grid.ClickDeleteItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
            return this;
        }

        public void DeleteProductByBuildingPhase()
        {
            DeleteByBuildingPhase_btn.JavaScriptClick();
            ConfirmDialog(ConfirmType.OK);
            _waitingLoadingWhileDelete();
        }

        public void DeleteProductByStyle()
        {

            Specific_Product.Click();
            StyleDelete_chk.Check();
            Delete_btn.JavaScriptClick();
            ConfirmDialog(ConfirmType.OK);
            _waitingLoadingWhileDelete();
        }

        public void DeleteByAll()
        {
            EntireProduct_btn.JavaScriptClick();
            //DeleteByAll_btn.JavaScriptClick();
            Delete_btn.JavaScriptClick();
            _waitingLoadingWhileDelete();
        }

        private void _waitingLoadingWhileDelete()
        {
            if (!string.IsNullOrEmpty(_loadingWhileDeleting.ToString()))
            {
                int iTime = 0;
                while (FindElementHelper.FindElement(FindType.XPath, _loadingWhileDeleting.ToString(), 1) != null)
                {
                    // Wait until timeout
                    System.Threading.Thread.Sleep(500);
                    iTime++;
                    if (iTime > 120)
                        throw new TimeoutException("The grid is loaded over 60s.");
                }
            }
            else
                // Default value wait
                System.Threading.Thread.Sleep(1000);
        }
        public void WaitGridLoad()
        {
            ProductPage_Grid.WaitGridLoad();
        }

        public ProductPage EnterProductNameToFilter(string columnName, string houseName)
        {
            ProductPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, houseName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
            return this;
        }

        public void SelectItemInGrid(int columIndex, int rowIndex)
        {
            ProductPage_Grid.ClickItemInGrid(columIndex, rowIndex);
            PageLoad();
        }

        public void ChangePageSize(int size)
        {
            ProductPage_Grid.ChangePageSize(size);
            WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string valueToFind)
        {
            ProductPage_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ProductPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(_gridLoading,8000);
        }

        public void ImportExportProduct(string item)
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

            string textboxUpload_Xpath, importButtion_Xpath;
            switch (importTitle)
            {
                case "Products Import":
                    {
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportProducts']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportProducts']";
                        break;
                    }

                case "Subcomponents Import":
                    {
                        textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSubcomponents']";
                        importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSubcomponents']";
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
            PageLoad();
        }

        public void DeleteProduct(string productName, bool is_test_step = true)
        {
            DeleteItemInGrid("Product Name", productName);
            DeleteByAll();

            string successfulMess = $"Successfully deleted product/phase combination.";
            if (successfulMess == GetLastestToastMessage())
            {
                if (is_test_step) ExtentReportsHelper.LogPass("Product deleted successfully!");
                CloseToastMessage();
            }
            else
            {
                CloseDeleteModal();

                if (is_test_step)
                {
                    if (IsItemInGrid("Product Name", productName))
                        ExtentReportsHelper.LogFail("Product could not be deleted!");
                    else
                        ExtentReportsHelper.LogPass("Product deleted successfully!");
                }
            }
        }

        public void CreateNewProduct(ProductData product)
        {
            ClickAddToProductIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
            Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedURL), "Product detail page isn't displayed");

            ExtentReportsHelper.LogInformation("Populate all values and save new product");
            // Select the 'Save' button on the modal;
            ProductDetailPage.Instance.CreateAProduct(product);

            // Verify new Product in header
            Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(product), "Create new Product unsuccessfully");
            ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
        }
        /// <summary>
        /// Get total number on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return ProductPage_Grid.GetTotalItems;
        }
        /// <summary>
        /// Click on (...) more item to select import/export function
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isCaptured"></param>
        public void SelectItemProductExport(string item, bool isCaptured = true)
        {
            try
            {
                Button moreItem = new Button(FindType.XPath,"//*[@data-original-title='Utilities']");
                moreItem.Click();

                Button Export_btn = new Button(FindType.XPath, "//*[@data-target='#ProductsExportModal']");
                Export_btn.Click();

                string itemXpath = $"//*[@id='exportOption']";
                DropdownList ExportType_btn = new DropdownList(FindType.XPath, itemXpath);
                CommonHelper.WaitUntilElementVisible(5, itemXpath, false);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportType_btn),
                        $"Click <font color='green'><b><i>{item:g}</i></b></font> button.");
                ExportType_btn.SelectItem(item);
                Button continue_btn = new Button(FindType.XPath, "//*[@id='exportContinue']");
                continue_btn.Click();
                System.Threading.Thread.Sleep(4000);
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {item} on Utilities menu"));
            }
        }

        /// <summary>
        /// Download basline file to compare
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        public void DownloadBaseLineProuductFile(string exportType, string exportFileName)
        {
            // Download baseline file to report folder
            SelectItemProductExport(exportType, false);
            System.Threading.Thread.Sleep(3000);
            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(exportType, exportFileName);
        }

        /// <summary>
        /// Export file from More menu
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        /// <param name="expectedTotalNumber"></param>
        public void ExportProductFile(string exportType, string exportFileName, int expectedTotalNumber, string expectedExportTitle)
        {
            bool isCaptured = false;
            TableType format;
            if (exportType.ToLower().Contains("csv"))
                format = TableType.CSV;
            else if (exportType.ToLower().Contains("excel"))
                format = TableType.XLSX;
            else
                format = TableType.XML;

            string fileName = ValidationEngine.GetFullExportFileName(exportFileName, format);
            string filePath = CommonHelper.GetFullDownLoadFilePath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            // Download file
            SelectItemProductExport(exportType, isCaptured);
            System.Threading.Thread.Sleep(4000);

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportFileName, expectedExportTitle, expectedTotalNumber);
        }

        /// <summary>
        /// Delete Relationship Product By Delete Type
        /// </summary>
        /// <param name="deletetype"></param>
        public void DeleteRelationshipsItemInProduct(string deletetype)
        {
            Button Deletetype_btn = new Button(FindType.XPath, $"//a[@id = 'bulk-actions']/parent::div/ul/li/a[contains(.,'{deletetype}')]");
            CheckAll_btn.Check(true);
            BulkActions_btn.Click();
            Deletetype_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts'']/div[1]");
        }
    }
}
