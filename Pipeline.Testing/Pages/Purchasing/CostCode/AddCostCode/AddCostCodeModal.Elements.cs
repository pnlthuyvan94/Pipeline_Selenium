using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.CostCode.AddCostCode
{
    public partial class AddCostCodeModal : CostCodePage
    {
        public AddCostCodeModal() : base()
        {
        }

        //private Row _modalTitle
        //    => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='cc-modal']/section/header/h1");

        //private Row _name
        //   => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtAddccName");

        //private Row _description
        //   => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox Description_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtAddccDescription");

        //private Row _save
        //    => ExcelFactory.GetRow(MetaData, 4);
        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertcc");

        //private Row _close
        //  => ExcelFactory.GetRow(MetaData, 5);
        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='cc-modal']/section/header/a");

    }

}
