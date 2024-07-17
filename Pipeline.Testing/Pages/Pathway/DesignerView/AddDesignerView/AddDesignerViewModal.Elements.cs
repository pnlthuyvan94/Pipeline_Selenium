using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Pathway.DesignerView.AddDesignerView
{
    public partial class AddDesignerViewModal : DesignerViewPage
    {
        public IQueryable<Row> TestData_RT01100;
        public AddDesignerViewModal() : base()
        {
            // Sheet contains repository of Dashboard
            TestData_RT01100 = ExcelHelper.GetAllRows("RT_01100");
        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _viewName
            => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox ViewName_txt
            => new Textbox(_viewName);

        private Row _viewLocation
           => ExcelFactory.GetRow(MetaData, 3);
        protected DropdownList ViewLocation_ddl
            => new DropdownList(_viewLocation);

        private Row _save
            => ExcelFactory.GetRow(MetaData, 4);
        protected Button Save_btn
            => new Button(_save);

        private Row _close
           => ExcelFactory.GetRow(MetaData, 5);
        protected Button Close_btn
           => new Button(_close);



    }

}
