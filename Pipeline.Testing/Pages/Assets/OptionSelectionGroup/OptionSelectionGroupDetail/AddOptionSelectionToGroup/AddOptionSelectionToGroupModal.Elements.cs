using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail.AddOptionSelectionToGroup
{
    public partial class AddOptionSelectionToGroupModal : DetailsContentPage<AddOptionSelectionToGroupModal>
    {
        public AddOptionSelectionToGroupModal() : base()
        {

        }

        // Add Building Phase to Building Group
        protected Label AddOptionSelectionToGroupTitle_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected Button AddOptioNSelectionToGroup_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSG']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

        string ListOfOptionSelectionXpath => "//*[@id='ctl00_CPH_Content_rlbSelections']/div/ul/li";
        protected ListItem ListOfOptionSelection_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, ListOfOptionSelectionXpath).ToList());
    }
}
