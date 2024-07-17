using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeUser.AddUserToTrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeUser
{
    public partial class TradeBuilderUserPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Users_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(_gridLoading, 500);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Users_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            PageLoad();
        }

        public void WaitBuildingPhaseGird()
        {
            Users_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Users_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void ShowAddUserToTradeModal()
        {
            ShowModal_btn.Click();
            AddUserToTradeModal = new AddUserToTradeModal();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
