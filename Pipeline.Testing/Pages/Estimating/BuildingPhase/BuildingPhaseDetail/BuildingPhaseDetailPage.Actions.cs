using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddProductToPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddVendorToPhase;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail
{
    public partial class BuildingPhaseDetailPage
    {
        public BuildingPhaseDetailPage EnterPhaseCode(string code)
        {
            Code_txt.SetText(code);
            return this;
        }

        public BuildingPhaseDetailPage EnterPhaseName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        public BuildingPhaseDetailPage EnterAbbName(string abbName)
        {
            Abbreviated_txt.SetText(abbName);
            return this;
        }
        public BuildingPhaseDetailPage EnterDescription(string description)
        {
            Description_txt.SetText(description);
            return this;
        }
        public string SelectBuildingGroup(string buildingGroup, int index)
        {
            return BuildingGroup_ddl.SelectItemByValueOrIndex(buildingGroup, index);
        }
        public string SelectType(string type, int index)
        {
            return Type_ddl.SelectItemByValueOrIndex(type, index);
        }
        public string SelectParent(string parent, int index)
        {
            return Parent_ddl.SelectItemByValueOrIndex(parent, index);
        }

        public string SelectTaskForPayment(string activity, int index)
        {
            return SchedulingTaskForPayment_ddl.SelectItemByValueOrIndex(activity, index);
        }

        public string SelectTaskForPO(string activity, int index)
        {
            return TaskForPoDisplay_ddl.SelectItemByValueOrIndex(activity, index);
        }
        public string SelectTrade(string trade, int index)
        {
            return Trade_ddl.SelectItemByValueOrIndex(trade, index);
        }
        public string SelectReleaseGroup(string releaseGroup, int index)
        {
            return ReleaseGroup_ddl.SelectItemByValueOrIndex(releaseGroup, index);
        }
        public string SelectCostCode(string costCode, int index)
        {
            return CostCode_ddl.SelectItemByValueOrIndex(costCode, index);
        }
        public string SelectCostCategory(string costCategory, int index)
        {
            return CostCategory_ddl.SelectItemByValueOrIndex(costCategory, index);
        }
        public string SelectPOView(string poView, int index)
        {
            return POView_ddl.SelectItemByValueOrIndex(poView, index);
        }
        public BuildingPhaseDetailPage IsBudgetOnly(bool check)
        {
            if (check) BudgetOnly_chkbox.Check();
            return this;
        }
        public BuildingPhaseDetailPage EnterPercentBilled(string percentBilled)
        {
            Percent_Billed_txt.SetText(percentBilled);
            return this;
        }
        public BuildingPhaseDetailPage ClickTaxableYes()
        {
            TaxableYes_rbtn.Click();
            return this;
        }

        public BuildingPhaseDetailPage ClickTaxableNo()
        {
            TaxableNo_rbtn.Click();
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlPhase']/div[1]");
        }

        public void ClickAddProductToPhaseModal()
        {
            AddProductToPhaseModal = new AddProductToPhaseModal();
            AddProductToBuildingPhase_btn.Click();
        }

        public void ClickAddVendorToPhaseModal()
        {
            AddVendorToPhaseModal = new AddVendorToPhaseModal();
            AddVendorToBuildingPhase_btn.Click();
        }

        public void DeleteItemInProductsGrid(string columnName, string value)
        {
            Products_grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']");
        }

        public void DeleteItemInVendorsGrid()
        {
            Vendors_grid.ClickDeleteFirstItem();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']");
        }
        public bool IsItemInGrid(string columnName, string value)
        {
            return Products_grid.IsItemOnCurrentPage(columnName, value);
        }
    }
}
