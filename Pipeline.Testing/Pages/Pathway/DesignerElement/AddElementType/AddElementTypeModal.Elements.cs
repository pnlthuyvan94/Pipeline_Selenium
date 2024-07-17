using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Pathway.DesignerElement.AddElementType
{
    public partial class AddElementTypeModal : DesignerElementPage
    {

        public AddElementTypeModal() : base()
        {

        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _name
            => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox TypeName_txt
            => new Textbox(_name);

        private Row _save
            => ExcelFactory.GetRow(MetaData, 3);
        protected Button Save_btn
            => new Button(_save);

        private Row _close
           => ExcelFactory.GetRow(MetaData, 4);
        protected Button Close_btn
           => new Button(_close);
    }

}
