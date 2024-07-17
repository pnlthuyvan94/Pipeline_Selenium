using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors.AddVendor
{
    public partial class DivisionVendorModal
    {
        public DivisionVendorModal SelectDivisionVendor(string[] listDivisionVendor)
        {
            if (listDivisionVendor.Length != 0)
                DivisionVendor_lst.SetChecked(GridFilterOperator.Contains, listDivisionVendor);
            return this;
        }

        /// <summary>
        /// Select Vendor by Name
        /// </summary>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public DivisionVendorModal SelectDivisionVendorByName(string vendorName)
        {
            DivisionVendor_lst.SetChecked(GridFilterOperator.Contains, vendorName);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
        }

        public void Cancel()
        {
            Close_btn.Click();
        }
    }
}