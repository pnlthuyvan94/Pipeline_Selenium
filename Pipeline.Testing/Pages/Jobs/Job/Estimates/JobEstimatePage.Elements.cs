using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.Estimates
{
    public partial class JobEstimatePage : DashboardContentPage<JobEstimatePage>
    {
        string loadingGrid_xpath = "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected Button GenerateBomAndEstimates_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCalculate']");

        protected Pipeline.Common.Controls.Image imgDifferentPhaseValues_img => new Pipeline.Common.Controls.Image(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00_ctl04_imgDifferentPhaseValues']");
        string grid_Xpath = "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']";

        protected IGrid JobEstimatesPhase_Grid => new Grid(FindType.XPath, grid_Xpath, loadingGrid_xpath);
        protected DropdownList ViewBy_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewTypes");
    }
}
