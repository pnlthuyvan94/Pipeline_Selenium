using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail.AddBuildingPhaseToBuildingGroup
{
    public partial class AddPhaseToGroupModal : DetailsContentPage<AddPhaseToGroupModal>
    {

        // Add Building Phase to Building Group
        protected Label AddPhaseToGroupTitle_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected Button AddPhaseToGroup_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSO']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

        string ListOfPhaseXpath => "//*[@id='ctl00_CPH_Content_rlbBuildingPhase']/div/ul/li/label";
        protected ListItem ListOfPhase_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, ListOfPhaseXpath).ToList());
    }
}
