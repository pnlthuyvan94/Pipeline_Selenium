using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Products.ProductResources.AddProductResource;
using System.IO;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources
{
    public partial class ProductResourcePage
    {
        public void InsertResources(ProductResourceData data, params string[] List_Resource)
        {
            // Upload all sourceFile
            foreach (string source in List_Resource)
            {
                InsertResource_btn.Click();
                AddProductResourceModal = new AddProductResourceModal();

                if (AddProductResourceModal.IsModalDisplayed())
                {
                    ExtentReportsHelper.LogPass($"Add Resource modal displayed correctly.");
                    // Upload resource
                    AddProductResourceModal.UploadResourceToProduct(data, source);
                }
            }

            ExtentReportsHelper.LogInformation($"Change page size to 100");
            ChangePageSizeView(100);

            // Verify each source name on the grid view
            foreach (string source in List_Resource)
            {
                string sourceName = Path.GetFileName(source);
                //Verify source item display on Gird
                if (IsItemInGrid("Source", sourceName) is true)
                    ExtentReportsHelper.LogPass($"The {sourceName} resource upload successfully");
                else
                    ExtentReportsHelper.LogFail($"The {sourceName} resource upload unsuccessfully");
            }
        }


        public bool UpdateResources(ProductResourceData data, string editResourceName)
        {
            // Click edit button for any resource
            Resource_Grid.ClickEditItemInGrid("Source", editResourceName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductResources']/div[1]");
            string UpdateTitle_xpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/span[text()='{editResourceName}']/../preceding-sibling::td/input[contains(@id, 'txtTitle')]";
            Textbox NewTitle_txt = new Textbox(FindType.XPath, UpdateTitle_xpath);
            string AcceptButton_xpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/span[text()='{editResourceName}']/../following-sibling::td/ input[@alt='Update']";
            Button Accept_btn = new Button(FindType.XPath, AcceptButton_xpath);
            // Set new title and apply
            NewTitle_txt.SetText(data.UpdatedTitle);
            Accept_btn.Click();
            // Wait loading grid
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductResources']/div[1]");
            // Verify new resource title
            return Resource_Grid.IsItemOnCurrentPage("Title", data.UpdatedTitle);
        }

        public void DeleteResource(params string[] deletedItems)
        {
            ChangePageSizeView(100);
            foreach (string item in deletedItems)
            {
                if (!IsItemInGrid("Source", item))
                {
                    ExtentReportsHelper.LogFail($"Resource name: {item} doesn't display in the grid view");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Resource name: {item} display in the grid view then delete it");

                    DeleteItemInGrid("Source", item);
                    WaitGridLoad();

                    string expectedMess = $"{item.Split('.')[0]} was deleted successfully.";
                    string actualMsg = GetLastestToastMessage();
                    if (expectedMess.Equals(actualMsg))
                    {
                        ExtentReportsHelper.LogPass($"Resource name: {item} deleted successfully!");
                        CloseToastMessage();
                    }
                    else if (!string.IsNullOrEmpty(actualMsg))
                    {
                        if (IsItemInGrid("Source", item))
                            ExtentReportsHelper.LogFail($"Resource name: {item} could not be deleted!");
                        else
                            ExtentReportsHelper.LogPass($"Resource name: {item} deleted successfully!");
                    }
                }
            }
        }

        public void VerifyHyperLink(params string[] resourceNameList)
        {
            var currentUrl = CurrentURL;
            foreach (string source in resourceNameList)
            {
                // Wait until Add button displayed.
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lbAddResource']", 2);

                // Click image icon of source
                string image_xpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/span[text()='{source}']/../../td/a/img[contains(@id, 'imgSpec')]";
                Button Image_btn = new Button(FindType.XPath, image_xpath);

                // Open page and capture - Manual verify
               // Image_btn.Click();
               // WaitGridLoad();
               // PageLoad();

                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Capture and log for manual testing purpose, Verify '{source}' image.");

                // Back to Product/ resource page
                CommonHelper.OpenURL(currentUrl);
            }
        }

        private void ChangePageSizeView(int size)
        {
            Resource_Grid.ChangePageSize(size);
            Resource_Grid.WaitGridLoad();
        }
    }
}
