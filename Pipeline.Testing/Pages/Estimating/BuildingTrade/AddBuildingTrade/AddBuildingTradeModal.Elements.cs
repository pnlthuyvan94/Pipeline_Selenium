using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Estimating.BuildingTrade.AddBuildingTrade
{
    public partial class AddBuildingTradeModal : BuildingTradePage
    {
        public IQueryable<Row> TestData_RT01157;

        public AddBuildingTradeModal() : base()
        {
            // Sheet contains repository of Dashboard
            TestData_RT01157 = ExcelHelper.GetAllRows("RT_01157");
        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 7);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _name
           => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
            => new Textbox(_name);

        private Row _code
            => ExcelFactory.GetRow(MetaData, 1);
        protected Textbox Code_txt
            => new Textbox(_code);

        private Row _description
         => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox Description
            => new Textbox(_description);

        private Row _save
      => ExcelFactory.GetRow(MetaData, 4);
        protected Button Save_btn
            => new Button(_save);

    }

}
