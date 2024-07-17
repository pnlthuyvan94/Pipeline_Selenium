using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Pathway.DesignerElement.AddElement
{
    public partial class AddElementModal : DesignerElementPage
    {
        public AddElementModal() : base()
        {
        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 15);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _name
            => ExcelFactory.GetRow(MetaData, 6);
        protected Textbox ElementName_txt
            => new Textbox(_name);

        private Row _save
            => ExcelFactory.GetRow(MetaData, 9);
        protected Button Save_btn
            => new Button(_save);

        private Row _close
           => ExcelFactory.GetRow(MetaData, 10);
        protected Button Close_btn
           => new Button(_close);

        private Row _des
          => ExcelFactory.GetRow(MetaData, 7);
        protected Textbox Description_txt
            => new Textbox(_des);

        private Row _type
            => ExcelFactory.GetRow(MetaData, 5);
        protected DropdownList ElementType_ddl
            => new DropdownList(_type);

        private Row _style
            => ExcelFactory.GetRow(MetaData, 8);
        protected DropdownList Style_ddl
            => new DropdownList(_style);
    }

}
