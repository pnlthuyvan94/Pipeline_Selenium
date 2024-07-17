using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeUser.AddUserToTrade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeUser
{
    public partial class TradeBuilderUserPage : DetailsContentPage<TradeBuilderUserPage>
    {
        public readonly IQueryable<Row> MetaData;
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\BuildingTradeParams.xlsx";
        public TradeBuilderUserPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }

        public AddUserToTradeModal AddUserToTradeModal { get; private set; }

        private Row _grid => ExcelFactory.GetRow(MetaData, 33);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 35);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected Grid Users_Grid => new Grid(_grid, _gridLoading);

        private Row _add => ExcelFactory.GetRow(MetaData, 34);
        protected Button ShowModal_btn => new Button(_add);
    }
}
