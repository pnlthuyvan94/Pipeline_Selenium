using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseAssignment
{
    public partial  class UseAssignment : DetailsContentPage<UseAssignment>
    {
        protected Label HeaderUsesTitle_Lbl => new Label(FindType.XPath, "//*[@class='sub-tier1 subhead-list-item-1']");
        protected Label UsesName_Lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblTitle']");
        protected IGrid SubComponent_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSubcomponents_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSubcomponents']/div[1]");
        protected IGrid House_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgHouses_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");
        protected IGrid ActiveJobs_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgActiveJobs_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgActiveJobs']/div[1]");
        protected IGrid CloseJobs_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgClosedJobs_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgClosedJobs']/div[1]");
        protected IGrid Option_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptions_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        protected IGrid CustomOption_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCustomOptions_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomOptions']/div[1]");
        protected IGrid WorkSheet_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgWorksheets_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheets']/div[1]");
        protected IGrid ProductConversion_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProductSpecSets_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductSpecSets']/div[1]");
        protected IGrid StyleConversion_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgStyleSpecSets_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyleSpecSets']/div[1]");
    }
}
