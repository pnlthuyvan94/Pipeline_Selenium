using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityHouseOption
{
    public partial class AddCommunityHouseOptionModal : CommunityOptionPage
    {
        protected Label AddCommunityHouseOption_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblModalHeader']");

        protected Button IsAddOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbViewType']/label[1]");

        protected Button IsAddConditionOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbViewType']/label[2]");

        protected DropdownList OptionGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlGroups']");

        protected DropdownList CostGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCostGroups']");

        protected ListItem AllHouseOptions_lst
           => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbHouseOptions']/div/ul/li").ToList());

        protected ListItem OtherMasterOptions_lst
            => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbAllHouseOptions']/div/ul/li").ToList());

        protected Textbox SalePrice_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewHouseOptionCost']");

        protected Button Standard_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddOption']/section/div/span[1]/label");

        protected Button AvailableToAllHouse_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddOption']/section/div/span[2]/label");

        protected Button AddCommunityHouseOptionToCommunity_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertHouseOption']");

        protected Button Close_btn => new Button(FindType.XPath, "//*[@id='house - opts - modal']/section/header/a");
    }
}
