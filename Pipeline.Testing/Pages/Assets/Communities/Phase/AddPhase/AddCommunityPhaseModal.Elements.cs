using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.Phase.AddPhase
{
    public partial class AddCommunityPhaseModal : CommunityPhasePage
    {
        public AddCommunityPhaseModal() : base()
        {
        }

        protected Label AddPhaseTitle_lbl
            => new Label(FindType.XPath, "//*[@id='CommunityPhase-modal']/section/header/h1");

        protected Textbox Name_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox Code_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtCode']");

        protected DropdownList Status_ddl =>
            new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStatusTypes']");

        protected Textbox SortOrder_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtOrder']");

        protected Textbox Description_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Button AddPhase_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertCommunityPhase']");

        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='CommunityPhase modal']/section/header/a");

    }

}
