using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders.ChangeStatus
{
    public partial class ChangeStatusModal
    {
        private ChangeStatusModal SelectStatus(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                Status_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        public void Save()
        {
            Save_btn.Click(true);
            ConfirmDialog(ConfirmType.OK);
            PO_Grid.WaitGridLoad();
            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = $"Successfully changed the status of the selected PO(s).";
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'></font>Failed to change PO status.</br></font>");
            }
        }

        public void ChangeStatus(string value)
        {
            SelectStatus(value)
            .Save();
        }
    }
}