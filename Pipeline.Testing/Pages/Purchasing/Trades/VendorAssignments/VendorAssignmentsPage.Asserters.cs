using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments
{
    public partial class VendorAssignmentsPage
    {
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return VendorAssignmentsHeader_grid.IsColumnFoundInGrid(columnName);
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
