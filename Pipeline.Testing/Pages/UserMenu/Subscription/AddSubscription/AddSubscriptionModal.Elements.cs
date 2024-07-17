using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.UserMenu.Subscription.AddSubscription
{
    public partial class AddSubscriptionModal : SubscriptionPage
    {
        public AddSubscriptionModal() : base()
        {
        }

        protected Label subdescription_load => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbEvents']/div[1]");
        //private Row _modalTitle
        //    => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1");

        protected ListItem Event_lt
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbEvents']/div/ul/li").ToList());

        //private Row _insert
        //    => ExcelFactory.GetRow(MetaData, 3);
        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertSubscriptions");

        //private Row _close
        //  => ExcelFactory.GetRow(MetaData, 4);
        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='options-modal']/section/header/a");
    }

}
