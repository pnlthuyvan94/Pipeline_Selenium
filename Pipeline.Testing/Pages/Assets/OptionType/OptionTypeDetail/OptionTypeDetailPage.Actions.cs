using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail.AddOptionToOptionType;

namespace Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail
{
    public partial class OptionTypeDetailPage
    {
        private OptionTypeDetailPage EnterOptionType(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        private OptionTypeDetailPage EnterSortOrder(string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
                SortOrder_txt.SetText(sortOrder);
            return this;
        }

        private OptionTypeDetailPage EnterDisplayName(string displayName)
        {
            DisplayName_txt.SetText(displayName);
            return this;
        }

        private OptionTypeDetailPage IsPathwayVisible(bool isPathwayVisible)
        {
            IsPathwayVisible_ckb.SetCheck(isPathwayVisible);
            return this;
        }

        private OptionTypeDetailPage IsFlexPlan(bool isFlexPlan)
        {
            IsFlexPlan_ckb.SetCheck(isFlexPlan);
            return this;
        }

        private void Save()
        {
            Save_btn.Click();
            // Loading grid next to Save button
            WaitingLoadingGifByXpath(_loadingXpath, 5);
        }

        public OptionTypeDetailPage UpdateOptionType(OptionTypeData data)
        {
            EnterOptionType(data.Name).EnterSortOrder(data.SortOrder);//.EnterDisplayName(data.DisplayName)
                //.IsPathwayVisible(data.IsPathwayVisible).IsFlexPlan(data.IsFlexPlan);
            Save();
            return this;
        }

        public OptionTypeDetailPage AddOptionToOptionType()
        {
            AddOption_btn.Click();
            AddOptionToOptionTypeModal = new AddOptionToOptionTypeModal();
            return this;
        }

        public void WaitGridLoad()
        {
            Option_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Option_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Option_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Option_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void DeleteAllItem()
        {
            // Select all items
            SelectAllItems_btn.Check();

            // Click Delete All button
            BulkAction_btn.Click();
            DeleteSelectedItem_btn.JavaScriptClick();
            ConfirmDialog(ConfirmType.OK);

            // Reloading page
            PageLoad();
        }

    }
}
