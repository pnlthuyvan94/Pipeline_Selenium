using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup
{
    public partial class AddTaxGroupModal : TaxGroupPage
    {
        public AddTaxGroupModal() : base()
        {

        }

        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='tg-modal']/section/header/h1");

        protected Textbox TaxGroupName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTaxGroupName']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertTG']");

        protected Button SeriesClose_btn
           => new Button(FindType.XPath, "//*[@id='tg-modal']/section/header/a");

    }

}
