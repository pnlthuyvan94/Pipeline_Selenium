using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Diagnostics;

namespace Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes.MoveInReadyResource
{
    public partial class MoveInReadyResourceGrid
    {
        public void DeleteAllItemInGrid()
        {
            Label totalItems = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSpecResources_ctl00']/tfoot/tr/td/table/tbody/tr/td/div[5]/strong[1]");
            int.TryParse(totalItems.GetText(), out int total);
            Button item = new Button(FindType.XPath, "//input[@Title='Delete']");
            while (item.IsExisted() && total > 0)
            {
                item.Click();
                ConfirmDialog(ConfirmType.OK);
                MoveInReadyResourcePage_Grid.WaitGridLoad();
                CloseToastMessage();
                System.Threading.Thread.Sleep(500);
                item.RefreshWrappedControl();
                total -= 1;
            }
        }

        public MoveInReadyResourceGrid AddNewMoveInReadyResource()
        {
            //Scroll to down
            ScrollDown();
            AddMoveInReadyResource_btn.Click();
            string tabXpath = $"//*[@id='tabs']/li[1]/a";
            Button switchResourceTab_btn = new Button(FindType.XPath, tabXpath);
            switchResourceTab_btn.WaitForElementIsVisible(3, false);
            return this;
        }

        public MoveInReadyResourceGrid SwitchResourceTab(string tabName)
        {
            string tabXpath = "//*[@id='tabs']/li[{0}]/a";
            Button SwitchResourceTab_btn = new Button(FindType.XPath, tabXpath);
            switch (tabName)
            {
                case "Images":
                    tabXpath = string.Format(tabXpath, "1");
                    break;
                case "Features":
                    tabXpath = string.Format(tabXpath, "2");
                    break;
                default:
                    // Documents
                    tabXpath = string.Format(tabXpath, "3");
                    break;
            }
            SwitchResourceTab_btn.UpdateValueToFind(tabXpath);
            SwitchResourceTab_btn.WaitForElementIsVisible(3, false);
            SwitchResourceTab_btn.Click();

            return this;
        }

        /********************* Imgae  *****************************/
        public MoveInReadyResourceGrid SelectImage(string imageLink)
        {
            if (!string.IsNullOrEmpty(imageLink))
            {
                SelectImage_txt.SendKeysWithoutClear(imageLink);
            }
            var item = FindElementHelper.FindElements(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_AsyncUpload')]/input[@value='Cancel']", 1).Count;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (item > 0 && timer.Elapsed.TotalSeconds < 60)
            {
                try
                {
                    var elements = FindElementHelper.FindElements(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_AsyncUpload')]/input[@value='Cancel']", 1);
                    item = elements.Count;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
                System.Threading.Thread.Sleep(1000);
            }
            timer.Stop();
            return this;
        }

        public MoveInReadyResourceGrid UploadImage()
        {
            if (!UploadImage_btn.IsNull())
                UploadImage_btn.Click();
            PageLoad();
            return this;
        }

        /********************* Features  *****************************/
        public MoveInReadyResourceGrid EnterFeatures(string features)
        {
            Feature_txt.WaitForElementIsVisible(5);
            if (!string.IsNullOrEmpty(features))
                Feature_txt.SetText(features);
            return this;
        }

        public MoveInReadyResourceGrid EnterFeatureDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                FeatureDescription_txt.SetText(description);
            return this;
        }

        public void SaveFeature()
        {
            SaveFeature_btn.Click();
            string expectedMsg = "Resource added successfully!";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("The feature added successfully!");
                CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail("The feature is NOT add successfully! Actual message: " + actualMsg);
                CloseToastMessage();
            }
        }

        /********************* Documents  *****************************/
        public MoveInReadyResourceGrid SelectDocument(string documentLink)
        {
            Label fakeInnput = new Label(FindType.Id, "ctl00_CPH_Content_RadAsyncUploadDocsfakeInput0");
            if (!fakeInnput.WaitForElementIsVisible(5))
            {
                SwitchResourceTab("Documents");
                fakeInnput.RefreshWrappedControl();
                fakeInnput.WaitForElementIsVisible(5);
            }
            if (!string.IsNullOrEmpty(documentLink))
                SelectDocument_txt.AppendKeys(documentLink);
            var item = FindElementHelper.FindElements(FindType.XPath, "//*[contains(@id,'AsyncUpload')]/input[@value='Cancel']", 1).Count;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (item > 0 && timer.Elapsed.TotalSeconds < 60)
            {
                try
                {
                    var elements = FindElementHelper.FindElements(FindType.XPath, "//*[contains(@id,'AsyncUpload')]/input[@value='Cancel']", 1);
                    item = elements.Count;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
                System.Threading.Thread.Sleep(1000);
            }
            timer.Stop();
            
            return this;
        }

        public MoveInReadyResourceGrid UploadDocument()
        {
            if (!UploadDocument_btn.IsNull())
                UploadDocument_btn.Click();
            PageLoad();
            return this;
        }

        public void ScrollDown()
        {
            System.Threading.Thread.Sleep(100);
            CommonHelper.MoveToElement(AddMoveInReadyResource_btn, true);
            System.Threading.Thread.Sleep(300);
        }
    }
}
