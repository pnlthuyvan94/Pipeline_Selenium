using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther;
using Pipeline.Testing.Pages.Assets.Communities.Phase.AddPhase;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Communities.Phase
{
    public partial class CommunityPhasePage : DetailsContentPage<CommunityPhasePage>
    {
        public AddCommunityPhaseModal AddPhaseModal { get; private set; }

        public AssignLotOrPhaseOrHouseToEachOtherModal AssignedModal { get; private set; }

        public CommunityPhasePage() : base()
        {
        }

        private string _gridLoading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityPhases']/div[1]";

        protected IGrid Phase_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCommunityPhases_ctl00']", _gridLoading);

        protected Button Add_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddCommunityPhase']");

    }
}
