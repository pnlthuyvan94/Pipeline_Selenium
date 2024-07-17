using Pipeline.Common.BaseClass;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup;

namespace Pipeline.Testing.Pages.Costing.TaxGroup
{
    public partial class TaxGroupPage
    {
        public void ClickAddToOpenTaxGroupModal()
        {
            AddTaxGroup_btn.Click();
            AddTaxGroupModal = new AddTaxGroupModal();
            AddTaxGroupModal.IsModalDisplayed();
        }

        public string DeleteItemInGrid(string columnName, string value)
        {
            var item = FindElementHelper.FindElement(Common.Enums.FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlTaxGroupsPanel']//option[contains(text(),'{value}')]", 10);
            CommonHelper.MoveToElement(item, true);
            //deleteButton = item.FindElement(FindType.XPath, ".//*[@title='Delete' or contains(@src,'delete.png')]");

            //CommonHelper.MoveToElementWithoutCaptureAndCenter(deleteButton);

            //SpecificControls del = new SpecificControls(deleteButton);
            //if (del.IsNull())
            //    throw new NoSuchElementException(string.Format("The item with name - {0} - is not contains Delete function.", valueToFind));

            //del.JavaScriptClick();
            Button Delete_btn = new Button(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgTaxGroups_ctl00']//tbody//tr//td/a/span[contains(text(),'{value}')]//..//..//following-sibling::td//*[contains(@title,'Delete')]");
            Delete_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            //CommonHelper.WaitUntilElementVisible(5, GetLastestToastMessage());
            System.Threading.Thread.Sleep(2000);
            return GetLastestToastMessage();
        }

        public TaxGroupPage GoToLastPage()
        {
            this.ChangePageSize(10);

            TaxGroup_Grid.NavigateToPage(TaxGroup_Grid.GetTotalPages);
            return this;
        }

        public TaxGroupPage ChangePageSize(int size)
        {
            TaxGroup_Grid.ChangePageSize(size);
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(2000);
            return this;
        }

        public TaxGroupPage FindItemInGridWithTextContains(string columnName, string value)
        {
            int numberOfPage = TaxGroup_Grid.GetTotalPages;
            for (int i = 1; i <= numberOfPage; i++)
            {
                if (!i.Equals(1))
                {
                    TaxGroup_Grid.NavigateToPage(i);
                    System.Threading.Thread.Sleep(4000);
                }
                if (TaxGroup_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value))
                    break;
            }
            return this;
        }
    }
}