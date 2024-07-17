using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders.Variance
{
    public partial class VarianceModal
    {
        public void ConfirmChanges()
        {
            ConfirmChanges_btn.Click(false);
        }

        public void Close()
        {
            Close_btn.Click(false);
        }
    }
}
