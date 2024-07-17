using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail.AddBuildingPhaseToBuildingGroup;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail
{
    public partial class BuildingGroupDetailPage : DetailsContentPage<BuildingGroupDetailPage>
    {
        public AddPhaseToGroupModal AddBuildingGroupModal { get; private set; }
        // Building Group Detail
        protected Label BuildingGroupTitle_lbl => new Label(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[2]/div/article/section[2]/div[1]/article/section/article/header/h1");

        protected Textbox BuildingGroupCode_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGroupCode']");

        protected Textbox BuildingGroupName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGroupName']");

        protected Textbox BuildingGropDescription_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGroupDescr']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");


        // Building Phase
        protected Label BuildingPhaseTitle_lbl => new Label(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[2]/div/article/section[2]/div[2]/article/section/article/header/h1");

        protected Button AddBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddBuildingPhase']");

        protected IGrid BuildingGroupDetail_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingPhase_ctl00']", "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rlbBuildingPhase']/div[1]");

    }
}
