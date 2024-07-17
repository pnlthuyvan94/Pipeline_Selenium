using NUnit.Framework;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.IO;
using System.Net;

namespace Pipeline.Testing.Pages.Resources
{
    public partial class ResourcePage
    {
        public void InsertResources()
        {
            InsertResources_btn.Click();
            PageLoad();
        }

        public void Cancel()
        {
            Cancel_btn.Click();
        }

        public bool UploadResource(ResourceData data)
        {
            // Can't upload resource with empty title
            if (string.Empty == data.Title)
                return false;

            // Populate data from excel file
            Type_ddl.SelectItem(data.Type, false);

            // Reload page after select Type
            PageLoad();

            Title_txt.SetText(data.Title);

            switch (data.Type)
            {
                case "Link":
                    // Can't upload resource with empty link
                    if (string.Empty == data.Link)
                        return false;

                    Link_txt.SetText(data.Link);
                    break;
                case "Document":
                    // Type is "Image" or "Document"
                    if (string.Empty == data.Source)
                        return false;
                    string sourcePathDocument = $"{data.Source}";
                    Source_txt.SendKeysWithoutClear(sourcePathDocument);
                    break;
                default:
                    // Type is "Image" or "Document"
                    if (string.Empty == data.Source)
                        return false;
                    string sourcePath = $"{data.Source}";
                    Source_txt.SendKeysWithoutClear(sourcePath);
                    break;
            }
            return true;
        }

        public bool UpdateResource(string resourceName, string updateTitle)
        {
            string UpdateTitle_xpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/a[starts-with(text(),'{resourceName}')]/../preceding::td/input[contains(@id,'txtTitle')]";
            Textbox NewTitle_txt = new Textbox(FindType.XPath, UpdateTitle_xpath);
            string AcceptButton_xpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/a[starts-with(text(),'{resourceName}')]/../following-sibling::td/input[contains(@src,'accept')]";
            Button Accept_btn = new Button(FindType.XPath, AcceptButton_xpath);

            if (Apply_btn == null || UpdateTitle_txt == null)
            {
                return false;
            }
            //string oldTitle = NewTitle_txt.Title;
            ResourcePage_Grid.ClickEditItemInGrid("Source", resourceName);
            //ResourcePage_Grid.WaitGridLoad();
            System.Threading.Thread.Sleep(7000);

            // Set new title and apply
            NewTitle_txt.SetText(updateTitle);
            System.Threading.Thread.Sleep(5000);
            Accept_btn.Click();

            // Wait loading grid
            WaitGridLoad();

            // Verify new resource title
            return ResourcePage_Grid.IsItemOnCurrentPage("Title", updateTitle);
        }

        public void DownloadFile(string href, string folderPath)
        {

            using (WebClient wc = new WebClient())
            {
                string file = Path.GetFileName(href);
                wc.DownloadFile(
                    // Param1 = Link of file
                    new Uri(href),
                   // Param2 = Path to save
                   folderPath
                );
            }
        }
        public void UploadResourceAndVerify(ResourceData data, params string[] List_Resource)
        {
            // Select and upload file
            foreach (string source in List_Resource)
            {
                data.Source = source;
                UploadResource(data);

                // Verify upload
                Assert.That(UploadResource(data), $"Can't upload resource with type {data.Type} and  any enpty value.");
                ExtentReportsHelper.LogPass(null, $"The Resource with type: {data.Type} and title: {data.Title} is uploaded.");

                //Click Save button and reload page
                InsertResources();
            }

            // Change view page to 100 items
            ExtentReportsHelper.LogInformation(null, $"Change page size to 100");
            ChangePageSizeView(100);

            foreach (string source in List_Resource)
            {
                string sourceName = Path.GetFileName(source);
                System.Threading.Thread.Sleep(3000);
                //Verify source item display on Gird
                if (IsItemInGrid("Source", sourceName) is true)
                    ExtentReportsHelper.LogPass(null, $"The {sourceName} resource upload successfully");
                else
                    ExtentReportsHelper.LogFail($"The {sourceName} resource upload unsuccessfully");
            }
        }

        public void CloseUploadResouceGrid()
        {
            Button cancel_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lnkCancelResource']");
            if (cancel_btn.IsDisplayed(false))
                cancel_btn.Click(false);
        }

        public void FilterAndRemoveResource(params string[] ItemDelete)
        {
            foreach (string item in ItemDelete)
            {
                FilterItemInGrid("Source", GridFilterOperator.Contains, item);
                if (!IsItemInGrid("Source", item))
                {
                    ExtentReportsHelper.LogFail($"Resource name: {item} doesn't display in the grid view");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"Resource name: {item} display in the grid view then delete it");

                    DeleteItemInGrid("Source", item);
                    WaitingLoadingGifByXpath("/html/body/div[1]/div[1]", 10);
                    //WaitGridLoad();

                    string expectedMess = $"{item.Split('.')[0]} was deleted successfully.";
                    string actualMsg = GetLastestToastMessage();
                    if (expectedMess.Equals(actualMsg))
                    {
                        ExtentReportsHelper.LogPass(null, $"Resource name: {item} deleted successfully!");
                        //CloseToastMessage();
                    }
                    else if (string.IsNullOrEmpty(actualMsg))
                    {
                        if (IsItemInGrid("Source", item))
                            ExtentReportsHelper.LogFail($"Resource name: {item} could not be deleted!");
                        else
                            ExtentReportsHelper.LogPass(null, $"Resource name: {item} deleted successfully!");
                    }
                }
            }
        }

        public string SourceFileHref { get; private set; }

        public string IsFileHref(string filename)
        {
            string xpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/a[text()='{filename}']//..//..//following-sibling::td[2]//a[contains(@id,'ypResource')]";
            Label item = new Label(FindType.XPath, xpath);
            // Wait grid loading from step 4
            System.Threading.Thread.Sleep(5000);

            if (item.IsNull())
                return null;
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Item is displayed on screen");
            
            // wait to get href
            System.Threading.Thread.Sleep(2000);
            SourceFileHref = item.GetAttribute("href");
            return SourceFileHref;
        }

        public void ChangePageSizeView(int size)
        {
            ResourcePage_Grid.ChangePageSize(size);
            ResourcePage_Grid.WaitGridLoad();
        }
    }
}
