
using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;
using System.Text;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorCurrentEstimate
{
    public partial class VendorCurrentEstimatePage
    {
        public void ExpandAllContent()
        {
            ExpandAll_btn.Click();
            WaitingLoadingGifByXpath(_loadingPage);
        }
    }
}
