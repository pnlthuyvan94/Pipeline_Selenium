using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask.AddSchedulingTaskToTrade
{
    public partial class AddSchedulingTaskToTradeModal : DetailsContentPage<Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask.TradeSchedulingTaskPage>
    {
        public AddSchedulingTaskToTradeModal() : base()
        {
        }


        protected Textbox Search_text => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSearchSchedulingTasks");
        protected ListItem Tasks_list => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbSchedulingTask']/div[1]/ul/li"));
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertSchedulingTask");
        protected Label ModalTitle_lbl
           => new Label(FindType.XPath, "//*[@id='sg-bd-modal']/section/header/h1");

    }
}
