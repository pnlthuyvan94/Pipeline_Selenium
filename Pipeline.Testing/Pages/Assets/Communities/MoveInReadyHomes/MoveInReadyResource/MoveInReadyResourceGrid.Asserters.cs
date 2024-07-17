
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes.MoveInReadyResource
{
    public partial class MoveInReadyResourceGrid
    {
        public bool IsAddMoveInReadyResourceGridDisplay()
        {
            //Scroll to down
            System.Threading.Thread.Sleep(100);
            CommonHelper.MoveToElement(AddMoveInReadyResource_lbl, true);
            System.Threading.Thread.Sleep(300);

            // Wait to title visible
            AddMoveInReadyResource_lbl.WaitForElementIsVisible(3);
            return !AddMoveInReadyResource_lbl.IsNull() && AddMoveInReadyResource_lbl.GetText() == "Move-In Ready Resources";
        }

        public bool IsUploadResourceGridDisplay()
        {
            SelectImage_txt.WaitForElementIsVisible(2);
            // Display current tab is Image with Select and Upload button
            return !SelectImage_txt.IsNull() && !UploadImage_btn.IsNull();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            MoveInReadyResourcePage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            MoveInReadyResourcePage_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return MoveInReadyResourcePage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            MoveInReadyResourcePage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            MoveInReadyResourcePage_Grid.WaitGridLoad();
        }
    }
}
