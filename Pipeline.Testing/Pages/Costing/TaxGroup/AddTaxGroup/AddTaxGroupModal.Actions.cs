using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup
{
    public partial class AddTaxGroupModal
    {
        public AddTaxGroupModal EnterTaxGroupName(string name)
        {
            TaxGroupName_txt.SetText(name);
            return this;
        }

        public string Save()
        {
            Save_btn.Click();
            //CommonHelper.WaitUntilElementVisible(1, GetLastestToastMessage());
            System.Threading.Thread.Sleep(2000);
            var message = GetLastestToastMessage();
            return message;
        }

        public void CloseModal()
        {
            SeriesClose_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }
    }
}
