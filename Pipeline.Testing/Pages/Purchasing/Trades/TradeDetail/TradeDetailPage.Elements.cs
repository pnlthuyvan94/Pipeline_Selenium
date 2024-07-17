using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail
{
    public partial class TradeDetailPage : DetailsContentPage<TradeDetailPage>
    {
        public readonly IQueryable<Row> MetaData;
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\BuildingTradeParams.xlsx";
        public TradeDetailPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }

        private Row _name => ExcelFactory.GetRow(MetaData, 11);
        protected Textbox TradeName_txt => new Textbox(_name);

        private Row _code => ExcelFactory.GetRow(MetaData, 12);
        protected Textbox TradeCode_txt => new Textbox(_code);

        private Row _description => ExcelFactory.GetRow(MetaData, 13);
        protected Textbox TradeDescription_txt => new Textbox(_description);

        private Row _save => ExcelFactory.GetRow(MetaData, 14);
        protected Button Save_btn => new Button(_save);

    }
}
