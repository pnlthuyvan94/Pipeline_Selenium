using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityVendor.AddVendor
{
    public partial class CommunityVendorModal : CommunityVendorPage
    {
        protected Label Modal_Lbl
            => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected ListItem CommunityVendor_lst
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbVendors']/div/ul/li").ToList());

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertVendor']");

        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

    }

}
