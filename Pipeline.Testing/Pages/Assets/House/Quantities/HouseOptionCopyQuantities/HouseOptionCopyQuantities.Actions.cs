using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;


namespace Pipeline.Testing.Pages.Assets.House.Quantities.HouseOptionCopyQuantities
{
    public partial class HouseOptionCopyQuantities : DetailsContentPage<HouseOptionCopyQuantities>
    {
        public void SelectHouseInListHouse(string valueHouse)
        {
            int maxRetries = 5;
            int retry = 0;
            string xPathHouseItem = $"//span[@id='ctl00_CPH_Content_ctl00_CPH_Content_rlbHousesPanel']//ul//span[text() = '{valueHouse}']/preceding-sibling::input";
            CheckBox iTemCkb = new CheckBox(FindType.XPath, xPathHouseItem);
            while (!iTemCkb.IsDisplayed() && retry < maxRetries)
            {
                try
                {                    
                    iTemCkb.SetCheck(true);
                }
                catch (StaleElementReferenceException)
                {
                    retry++;
                    iTemCkb = new CheckBox(FindType.XPath, xPathHouseItem);
                    iTemCkb.SetCheck(true);
                    System.Threading.Thread.Sleep(2000);
                    iTemCkb.RefreshWrappedControl();
                }
            }
            iTemCkb.SetCheck(true);
        }

        public void SelectOptionInListOption(string valueOption)
        {
            int maxRetries = 5;
            int retry = 0;
            string xPathOptionItem = $"//li[@id='ctl00_CPH_Content_rlbOptions_i0']//label//span[text()='{valueOption}']/preceding-sibling::input";
            CheckBox iTemCkb = new CheckBox(FindType.XPath, xPathOptionItem);           
            while (!iTemCkb.IsDisplayed() && retry < maxRetries)
            {
                try
                {
                    iTemCkb.SetCheck(true);
                }
                catch (StaleElementReferenceException)
                {                    
                    retry++;
                    iTemCkb = new CheckBox(FindType.XPath, xPathOptionItem);
                    iTemCkb.SetCheck(true);
                    System.Threading.Thread.Sleep(2000);
                    iTemCkb.RefreshWrappedControl();
                }                
            }
            iTemCkb.SetCheck(true);
        }

        public void SelectLeftOptioninOptList(string OptionValue)
        {
            int maxRetries = 5;
            int retry = 0;
            string xPathLeftOpt = $"//p[contains(.,'Select Option and Option(s) to Copy Quantities to:')]/parent::div//ul/li/span[text() = '{OptionValue}']";
            Button iTemCkb = new Button(FindType.XPath, xPathLeftOpt);
            while (!iTemCkb.IsDisplayed() && retry < maxRetries)
            {
                try
                {
                    iTemCkb.Click();
                }
                catch (StaleElementReferenceException)
                {
                    retry++;
                    iTemCkb = new Button(FindType.XPath, xPathLeftOpt);
                    iTemCkb.Click();
                    System.Threading.Thread.Sleep(2000);
                    iTemCkb.RefreshWrappedControl();
                }
            }
            iTemCkb.Click();
        }
        public void ClickCopyQuantitiesHouseToHouse()
        {
            Button CopyHouseBtn = new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbSendQty']");
            CopyHouseBtn.Click();        
        }
        public void ClickCopyQuantitiesOptionToOption()
        {
            Button CopyOptionBtn = new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbSendQtyToOptions']");
            CopyOptionBtn.Click();        
        }

        public void SelectMiddleCommunity(string communityValue)
        {
            Button middleComm = new Button(FindType.XPath, "//span[@class='rcbInner']");
            middleComm.Click();
            string xpathCom = $"//*[@id='ctl00_CPH_Content_rcbCopyQtys_DropDown']//ul/li[text() = '{communityValue}']";
            Button specificCom = new Button(FindType.XPath, xpathCom);
            specificCom.Click();
            middleComm.RefreshWrappedControl();
        }

        public bool IsListHouseAndOptionExist()
        {
        return (CopyToHouse != null) ? true : false;
        }

        public bool IsCopyQtyToOptExist()
        {
            return (CopyToOpt != null) ? true : false;
        }

        public void SelectOptionOptionToCpyQty(string optionName)
        {
            int maxRetries = 5;
            int retry = 0;
            string optionNameXP = $"//p[contains(.,'Select Option and Option(s) to Copy Quantities to:')]/parent::div/div[2]//ul/li//span[text() = '{optionName}']/preceding-sibling::input";            
            CheckBox iTemCkb = new CheckBox(FindType.XPath, optionNameXP);
            while (!iTemCkb.IsDisplayed() && retry < maxRetries)
            {
                try
                {
                    iTemCkb.SetCheck(true);
                }
                catch (StaleElementReferenceException)
                {
                    retry++;
                    iTemCkb = new CheckBox(FindType.XPath, optionNameXP);
                    iTemCkb.SetCheck(true);
                    System.Threading.Thread.Sleep(2000);
                    iTemCkb.RefreshWrappedControl();
                }
            }
            iTemCkb.SetCheck(true);
        }

        public void ClickModal(ConfirmType confirm)
        {
            switch (confirm)
            {
                case ConfirmType.No:
                    if (ModalNoBtn != null)
                    {
                        ModalNoBtn.Click();
                    }
                    break;
                case ConfirmType.Yes:
                    if (ModalYesBtn != null)
                    {
                        ModalYesBtn.Click();
                    }
                    break;
                case ConfirmType.X:
                    if (ModalXBtn != null)
                    {
                        ModalXBtn.Click();
                    }
                    break;
                default:
                    break;
            }         

        }
              
        public void ClickAlert(ConfirmType confirm)
        {
            bool alertAppear = CommonHelper.WaitUntilAlertAppears(driver, TimeSpan.FromSeconds(10));
            if (confirm == ConfirmType.OK)
            {              
                if (alertAppear)
                {
                    driver.SwitchTo().Alert().Accept();
                }
                else
                {
                    return;
                }                
            }
            if (confirm == ConfirmType.Cancel)
            {
                if (alertAppear)
                {
                    driver.SwitchTo().Alert().Dismiss();
                }
                else
                {
                    return;
                }
            }
        }
        public void WaitingForLoadingIconCpyHouseToHouse()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSendQty']/div[1]");
        }

        public void WaitingForLoadingIconSelectMiddleComm()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlCopyQty']/div[1]");
        }

        public void WaitingForLoadingIconClickCopyOptionToOption()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSendQtyToOptions']/div[1]", 5000);
        }
    }
}
