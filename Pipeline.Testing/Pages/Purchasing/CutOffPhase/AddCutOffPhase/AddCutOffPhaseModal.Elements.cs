using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase.AddCutoffPhase
{
    public partial class AddCutoffPhaseModal : CutoffPhasePage
    {
        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 7);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _code
           => ExcelFactory.GetRow(MetaData, 1);
        protected Textbox Code_txt
            => new Textbox(_code);

        private Row _name
           => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
            => new Textbox(_name);

        private Row _description
            => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox SortOrder_txt
            => new Textbox(_description);

        private Row _save
      => ExcelFactory.GetRow(MetaData, 4);
        protected Button Save_btn
            => new Button(_save);

    }

}
