using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct
{
    public partial class CustomOptionProduct
    {
        public bool IsSaveCustomOptionSuccessful(string CustomOptionName)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Equals(CustomOptionName) && !CurrentURL.EndsWith("did=0");
        }
    }
}
