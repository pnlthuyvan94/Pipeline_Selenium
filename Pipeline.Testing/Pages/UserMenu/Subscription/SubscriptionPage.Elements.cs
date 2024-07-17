using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.UserMenu.Subscription.AddSubscription;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.Subscription
{
    public partial class SubscriptionPage : DashboardContentPage<SubscriptionPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UserMenu\SubscriptionParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }

        //public IQueryable<Row> TestData_RT01105 { get; set; }

        public AddSubscriptionModal AddSubscriptionModal { get; private set; }

        public SubscriptionPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("Subscription_OR");
            //TestData_RT01105 = ExcelHelper.GetAllRows("RT_01105");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 5);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 6);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbEvents']/div[1]";

        protected IGrid Subscription_Grid => new Grid(FindType.Id,"ctl00_CPH_Content_rgSubscriptions_ctl00", _gridLoading);

    }
}
