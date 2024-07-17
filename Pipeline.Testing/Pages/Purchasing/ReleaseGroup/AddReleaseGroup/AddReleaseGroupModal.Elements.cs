using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup.AddReleaseGroup
{
    public partial class AddReleaseGroupModal : ReleaseGroupPage
    {
        public AddReleaseGroupModal() : base()
        {
        }

        //private Row _name
        //   => ExcelFactory.GetRow(MetaData, 1);
        protected Textbox Name_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtAddRGName");

        //private Row _description
        //    => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Description_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtAddRGDescription");

        //private Row _sortOrder
        //   => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox SortOrder_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtAddRGSortOrder");

        //private Row _modalTitle
        //   => ExcelFactory.GetRow(MetaData, 8);
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='rg-modal']/section/header/h1");

        //private Row _save
        //     => ExcelFactory.GetRow(MetaData, 4);
        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertRG");

        //private Row _close
        //     => ExcelFactory.GetRow(MetaData, 5);
        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='rg-modal']/section/header/a");

    }

}
