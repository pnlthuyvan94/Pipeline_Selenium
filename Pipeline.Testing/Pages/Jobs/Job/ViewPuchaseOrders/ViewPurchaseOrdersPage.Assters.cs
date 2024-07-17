using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders
{
    public partial class ViewPurchaseOrdersPage
    {
        public void VerifyViewPuchaseOrdersIsDisplayed(string url)
        {
            if (CurrentURL.EndsWith(url))
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>The View Purchase Orders Page is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The View Purchase Orders Page isn't displayed.</font>");
            }
        }
    }
}
