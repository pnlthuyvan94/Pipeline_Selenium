

using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseAssignment
{
    public partial class UseAssignment
    {
        public bool IsItemInSubcomponentGrid(string columnName, string value)
        {
            return SubComponent_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public bool IsItemInHouseGrid(string columnName, string value)
        {
            return House_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public bool IsItemInActiveJobsGrid(string columnName, string value)
        {
            return ActiveJobs_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public bool IsItemInCloseJobsGrid(string columnName, string value)
        {
            return CloseJobs_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public bool IsItemInOptionGrid(string columnName, string value)
        {
            return Option_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemInCustomOptionGrid(string columnName, string value)
        {
            return CustomOption_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemInWorkSheetGrid(string columnName, string value)
        {
            return WorkSheet_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemInProductConversionGrid(string columnName, string value)
        {
            return ProductConversion_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemInStyleConversionGrid(string columnName, string value)
        {
            return StyleConversion_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void FilterItemInHouseGrid(string columnName, GridFilterOperator GridFilterOperator, string value)
        {
            House_Grid.FilterByColumn(columnName, GridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");
        }
    }
}
