using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddProductToPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddVendorToPhase;

namespace Pipeline.Testing.Pages.Purchasing.BuildingPhase.BuildingPhaseDetail
{
    public partial class BuildingPhaseDetailPage : DetailsContentPage<BuildingPhaseDetailPage>
    {
        public BuildingPhaseDetailPage() : base() { }

        public AddVendorToPhaseModal AddVendorToPhaseModal { get; private set; }

        protected Button AddVendorToBuildingPhase_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddVendor");

        protected Grid Products_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgProducts_ctl00", "");
        protected Grid Vendors_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgVendors_ctl00", "");

    }
}
