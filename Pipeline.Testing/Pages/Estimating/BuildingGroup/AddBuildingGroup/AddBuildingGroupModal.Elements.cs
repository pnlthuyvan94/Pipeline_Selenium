using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.AddBuildingGroup
{
    public partial class AddBuildingGroupModal : BuildingGroupPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//h1[text() = 'Add Building Group']");

        protected Textbox BuildingGroupCode_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewBuildingGroupCode']");

        protected Textbox BuildingGroupName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewBuildingGroupName']");

        protected Textbox BuildingGroupDescription_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewBuildingGroupDescr']");

        private string _buildingTrade = "ctl00_CPH_Content_ddlNewBuildingTrade";
        protected DropdownList BuildingTrade_ddl => new DropdownList(FindType.Id, _buildingTrade);

        protected Button BuildingGroupSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");
        protected Button BuildingGroupCancel_btn => new Button(FindType.XPath, "//*[@id='lbCancel']");
        protected Button CloseModal_btn => new Button(FindType.XPath, "//*[@class='close']");
    }
}
