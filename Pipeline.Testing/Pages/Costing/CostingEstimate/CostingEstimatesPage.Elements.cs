using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.CostingEstimate
{
    public partial class CostingEstimatesPage : DashboardContentPage<CostingEstimatesPage>
    {
        protected RadioButton JobEstimate_rbtn => new RadioButton(FindType.Id, "ctl00_CPH_Content_rbEstimate_0");
        protected RadioButton HouseEstimate_rbtn => new RadioButton(FindType.Id, "ctl00_CPH_Content_rbEstimate_1");

        //House Estimates
        protected DropdownList House_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlHouses");
        protected DropdownList Community_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlCommunities");
        private string _houseGridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgReport']/div[1]";

        protected Grid HouseEstimates_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReport_ctl00']", _houseGridLoading);

        //Job Estimates
        protected DropdownList Job_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlJobs");
        protected DropdownList GeneratedBOM_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlGeneratedBOMs");
        protected DropdownList ViewType_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewTypes");
        private string _jobGridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]";
        protected Grid JobEstimates_Grid => new Grid(FindType.XPath, "ctl00_CPH_Content_rgPhasesView_ctl00", _jobGridLoading);
    }
}
