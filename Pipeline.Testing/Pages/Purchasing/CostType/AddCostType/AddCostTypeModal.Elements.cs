using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.CostType.AddCostType
{
    public partial class AddCostTypeModal : CostTypePage
    {
        //private Row _modalTitle
        //    => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='ct-modal']/section/header/h1");

        //private Row _name
        //   => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddctName']");
        //private Row _description
        // => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox Description_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddctDescription']");

        //private Row _save
        //    => ExcelFactory.GetRow(MetaData, 4);
        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertct");

        //private Row _close
        //  => ExcelFactory.GetRow(MetaData, 5);
        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='ct-modal']/section/header/a");

    }

}
