using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther;
using Pipeline.Testing.Pages.Assets.Communities.Lot.AddLot;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Communities.Lot
{
    public partial class LotPage : DetailsContentPage<LotPage>
    {     
        public IQueryable<Row> MetaData { get; set; }

        public AddLotModal AddLotModal { get; private set; }

        public AssignLotOrPhaseOrHouseToEachOtherModal AssignedModal { get; private set; }

        public LotPage() : base()
        {
        }
       
        public void GetInstance()
        {
        }


        private string _gridLoading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgLots']/div[1]";
        protected IGrid Lot_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgLots_ctl00']", _gridLoading);

        protected Button Add_btn
            => new Button(FindType.XPath, "//*[@id='lbAddLot']");

    }
}
