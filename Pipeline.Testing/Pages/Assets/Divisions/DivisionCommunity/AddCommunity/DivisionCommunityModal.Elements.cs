using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity.AddCommunity
{
    public partial class DivisionCommunityModal : DivisionCommunityPage
    {
        protected Label AddCommunity_lbl
            => new Label(FindType.XPath, "//*[@id='community - modal']/section/header/h1");

        protected ListItem DivisionCommunity_lst
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbCommunities']/div/ul/li").ToList());

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbApplyCommunity']");

        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='community - modal']/section/header/a");

        private string _loadingGifApplyingCommunity => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbApplyCommunity']/div[1]";
    }

}
