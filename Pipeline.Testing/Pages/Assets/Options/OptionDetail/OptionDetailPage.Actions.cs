using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Options.OptionDetail
{
    public partial class OptionDetailPage
    {
        public OptionDetailPage EnterOptionName(string data)
        {
            OptionName_txt.SetText(data);
            return this;
        }

        public OptionDetailPage EnterOptionNumber(string data)
        {
            OptionNumber_txt.SetText(data);
            return this;
        }

        public OptionDetailPage EnterOptionSquareFootage(string data)
        {
            SquareFootage_txt.SetText(data);
            return this;
        }

        public OptionDetailPage EnterPrice(string data)
        {
            Price_txt.SetText(data);
            return this;
        }

        public OptionDetailPage EnterOptionDescription(string data)
        {
            Description_txt.SetText(data);
            return this;
        }

        public OptionDetailPage EnterOptionSaleDescription(string data)
        {
            SaleDescription_txt.SetText(data);
            return this;
        }

        public string EnterOptionGroup(string data)
        {

            return OptionGroup_ddl.SelectItemByValueOrIndex(data, 1);

        }

        public string EnterOptionRoom(string data)
        {
            return OptionRoom_ddl.SelectItemByValueOrIndex(data, 0);
        }

        public string EnterCostGroup(string data)
        {
            return CostGroup_ddl.SelectItemByValueOrIndex(data, 0);
        }

        public string EnterOptionType(string data)
        {
            return OptionType_ddl.SelectItemByValueOrIndex(data, 0);
        }

        public OptionDetailPage SelectTypeOfOption(bool elevation, bool allowMultiples, bool global)
        {
            string xpathAllow = "//*[@id='ctl00_CPH_Content_pnlContent']/section[11]/span[./label[text()='Allow Multiples']]";
            string xpathEle = "//*[@id='ctl00_CPH_Content_pnlContent']/section[11]/span[./label[text()='Elevation']]";
            string xpathGlobal = "//*[@id='ctl00_CPH_Content_pnlContent']/section[11]/span[./label[text()='Global']]";
            if (Type.AllowMultiples.IsChecked)
            {
                if (!allowMultiples)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, xpathAllow));
            }
            else
            {
                if (allowMultiples)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, xpathAllow));
            }
            if (Type.Elevation.IsChecked)
            {
                if (!elevation)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, xpathEle));
            }
            else
            {
                if (elevation)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, xpathEle));
            }
            if (Type.Global.IsChecked)
            {
                if (!global)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, xpathGlobal));
            }
            else
            {
                if (global)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, xpathGlobal));
            }
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");
        }

        public OptionData UpdateOption(OptionData Option)
        {
            EnterOptionName(Option.Name)
           .EnterOptionNumber(Option.Number)
           .EnterOptionSquareFootage(Option.SquareFootage.ToString())
           .EnterOptionDescription(Option.Description)
           .EnterOptionSaleDescription(Option.SaleDescription);
            Option.OptionGroup = EnterOptionGroup(Option.OptionGroup);
            Option.CostGroup = EnterCostGroup(Option.CostGroup);
            Option.OptionType = EnterOptionType(Option.OptionType);
            EnterPrice(Option.Price.ToString())
                //.SelectTypeOfOption(Option.Types[0], Option.Types[1], Option.Types[2])
                .Save();
            OptionData newoption = new OptionData(Option)
            {
                OptionGroup = Option.OptionGroup,
                CostGroup = Option.CostGroup,
                OptionType = Option.OptionType
            };

            return newoption;
        }

        public void ClickAddToShowAddSelectionModal()
        {
            AddSelection_Btn.Click();
            if (!HeaderTitleOfModal_Lbl.WaitForElementIsVisible(20))
                ExtentReportsHelper.LogFail("The Add Option Selection Modal is NOT displayed after 5s.");
        }

        public OptionDetailPage SelectSelectionGroup(string selectionGroupName)
        {
            SelectionGroup_ddl.SelectItem(selectionGroupName, true, false);
            WaitingLoadingGifByXpath(loadingGifOfSelectionList);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(SelectionGroup_ddl), $"The page was selected item <font color='green'><b>{selectionGroupName}</b></font> with the left mouse dropdown list.");
            return this;
        }

        public void InsertSelectionToThisOption()
        {
            InsertSelection_Btn.Click();
            Selection_Grid.WaitGridLoad();
            //InsertSelection_Btn.WaitForElementIsInVisible(10);
            System.Threading.Thread.Sleep(2000);
        }

        public void CloseAddSelectionModal()
        {
            if (CloseSelectionModal_Btn.WaitForElementIsVisible(5)==true)
                CloseSelectionModal_Btn.Click();
                HeaderTitleOfModal_Lbl.WaitForElementIsInVisible(20);
        }

        public OptionDetailPage SelectSelection(params string[] itemName)
        {
            IList<IWebElement> items = new List<IWebElement>();
            foreach (var item in itemName)
            {
                var temps = Selection_lst.GetAllItems().Where(p => p.GetAttribute("textContent") == item).ToList();
                foreach (var temp in temps)
                    items.Add(temp);
            }
            foreach (var item in items)
            {
                var temp = item.FindElement(By.TagName("input"));
                temp.Click();
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(temp), $"The Option Selection with Name {temp.GetAttribute("textContent")} is selected.");
                System.Threading.Thread.Sleep(200);
            }
            return this;
        }

        public OptionDetailPage SelectSelection(params int[] itemIndex)
        {
            foreach (var item in itemIndex)
            {
                var temp = Selection_lst.GetAllItems()[item].FindElement(By.TagName("input"));
                temp.Click();
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(temp), $"The Option Selection with Name {temp.GetAttribute("textContent")} is selected.");
                System.Threading.Thread.Sleep(200);
            }
            return this;
        }

        /// <summary>
        /// Select number first item from list
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public IList<string> SelectTopSelection(int numberOfItems)
        {
            var items = Selection_lst.GetAllItems().Take(numberOfItems);
            var itemNames = new List<string>();
            foreach (var item in items)
            {
                var temp = item.FindElement(By.TagName("input"));
                itemNames.Add(item.GetAttribute("textContent"));
                System.Threading.Thread.Sleep(1000);
                temp.Click();
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(temp), $"The Option Selection with Name {temp.GetAttribute("textContent")} is selected.");
                System.Threading.Thread.Sleep(200);
            }
            return itemNames;
        }

        public void DeleteItemOnGrid(string columnName, string valueToFind)
        {
            Selection_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionsToSelections']/div[1]");
        }
		public void DeleteAllAssignmentToOption()
        {
            Button DeltBtn = new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_btnDelAllAssignment']");
            DeltBtn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionsToSelections']/div[1]");
        }

        public bool IsCutOffPhasePanelDisplayed()
        {
            Label import_lbl = new Label(FindType.XPath, $"//*[@id=\"ctl00_CPH_Content_pnlCutOffPhases\"]/section/label");
            if (import_lbl.IsDisplayed() is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
