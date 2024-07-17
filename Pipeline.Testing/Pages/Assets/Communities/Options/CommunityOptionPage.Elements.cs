using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityHouseOption;
using Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityOption;
using Pipeline.Testing.Pages.Assets.Communities.Options.AddOptionCondition;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Communities.Options
{
    public partial class CommunityOptionPage : DetailsContentPage<CommunityOptionPage>
    {
        public AddCommunityOptionModal AddCommunityOptionModal { get; private set; }

        public AddCommunityHouseOptionModal AddCommunityHouseOptionModal { get; private set; }

        public AddOptionConditionGrid AddOptionCondition { get; private set; }

        public CommunityOptionPage() : base()
        {

        }

        /* ---------------------- Community Option ------------------------------*/

        protected IGrid CommunityOption_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptions_ctl00']"
            , "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");

        protected Button AddCommunityOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewCommunityOption']");

        /* ---------------------- Community House Option ------------------------------*/

        protected IGrid CommunityHouseOption_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgHouseOptions_ctl00']"
            , "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouseOptions']/div[1]");

        protected Button AddCommunityHouseOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewHouseOption']");

    }
}
