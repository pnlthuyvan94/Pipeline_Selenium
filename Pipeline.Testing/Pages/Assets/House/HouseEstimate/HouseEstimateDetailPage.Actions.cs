using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Common.Controls;
using Pipeline.Testing.Pages.Estimating.Products;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House.HouseEstimate
{
    public partial class HouseEstimateDetailPage
    {
        public void SelectCommunity(string selectedCommunity)
        {
            if (selectedCommunity != string.Empty)
            {
                Community_ddl.SelectItem(selectedCommunity);
                WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
            }
        }

        public void SelectOrUnSelectAllItems(bool status)
        {
            Check_All_chk.SetCheck(status);
        }

        public void GenerateHouseBOMAndEstimate(string community)
        {
            // Select community
            SelectCommunity(community);

            // Click BOM & Estimate Generation button
            if (BomGeneration_btn.IsDisplayed(false) is true)
            {
                SelectOrUnSelectAllItems(true);
                System.Threading.Thread.Sleep(2000);

                // Click generate BOM button
                BomGeneration_btn.Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath);
                string actualToastMess = GetLastestToastMessage();
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>"  + actualToastMess +  "</b></font>");
             }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'BOM Generation'</b> button.</font>");
            }
        }


        public void SendAllVendorEstimatesToTheQueueBOMAndEstimate()
        {
            if (QueueBomCosts_btn.IsDisplayed(false) is true)
            {
                QueueBomCosts_btn.Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath);
            }
        }

        public void SendAllVendorEstimatesToTheQueueEstimateOnly()
        {
            if (BomQueueCosts_btn.IsDisplayed(false) is true)
            {
                BomQueueCosts_btn.Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath);
            }
        }

    }
}

