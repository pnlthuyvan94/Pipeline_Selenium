using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.SpecSet.AddSpecSetGroup;

namespace Pipeline.Testing.Pages.Estimating.SpecSet
{
    public partial class SpecSetPage : DashboardContentPage<SpecSetPage>
    {
        public AddSpecSetGroupModal AddSpecSetModal { get; private set; }
        protected IGrid SpecSetGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSpecSetGroups_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']/div[1]");
        protected Textbox SpecSetGroupName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewGroupName']");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertGroup']");
    }
}
