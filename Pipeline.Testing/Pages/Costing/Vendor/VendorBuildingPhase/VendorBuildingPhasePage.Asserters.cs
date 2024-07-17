using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase
{
    public partial class VendorBuildingPhasePage
    {
        /*
         * Verify head title and id of new Job
         */
       

        public bool IsHeaderBreadscrumbCorect(string _name)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedName = $"{_name}";
            return SubHeadTitle().Equals(expectedName) && !CurrentURL.EndsWith("vid=0");
        }
    }
}
