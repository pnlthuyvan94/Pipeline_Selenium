using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeUser.AddUserToTrade
{
    public partial class AddUserToTradeModal : DetailsContentPage<TradeBuilderUserPage>
    {
        public AddUserToTradeModal():base()
        { }

        protected Label ModalTitle_lbl
           => new Label(FindType.XPath, "//*[@id='sg-bu-modal']/section/header/h1");

        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertBuilderUsers");
        protected DropdownList Users_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_lstBuilderUsers");

    }
}
