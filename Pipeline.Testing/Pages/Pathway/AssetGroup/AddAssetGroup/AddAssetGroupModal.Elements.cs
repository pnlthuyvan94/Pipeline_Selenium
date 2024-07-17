using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Pathway.AssetGroup.AddAssetGroup
{
    public partial class AddAssetGroupModal : AssetGroupPage
    {
        public IQueryable<Row> TestData_RT01099;
        public AddAssetGroupModal() : base()
        {
            // Sheet contains repository of Dashboard
            TestData_RT01099 = ExcelHelper.GetAllRows("RT_01099");
        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _name
            => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
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
