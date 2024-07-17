using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.Budget
{
    public partial class JobBudgetPage : DashboardContentPage<JobBudgetPage>
    {
        string loadingGrid_xpath = "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected CheckBox SelectAllPhases_ckb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00_ctl02_ctl00_IBP_BuildingPhases_NameSelectCheckBox']");
        protected Button CreateBudgetForSelected_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSelected']");
        protected DropdownList ViewBy_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewByTypes");
        protected Grid BudgetViewByOptions_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgOptionsView_ctl00", loadingGrid_xpath);
        protected Grid BudgetViewByOptionsBPDetail_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10", loadingGrid_xpath);
        protected Grid BudgetViewByOptionsProductDetail_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10_ctl06_Detail10", loadingGrid_xpath);
        protected Grid BudgetViewByOptionsVendorDetail_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10_ctl06_Detail10_ctl06_Detail10", loadingGrid_xpath);
    }
}
