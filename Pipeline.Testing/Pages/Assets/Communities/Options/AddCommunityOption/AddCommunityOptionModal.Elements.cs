using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityOption
{
    public partial class AddCommunityOptionModal : CommunityOptionPage
    {
        public AddCommunityOptionModal() : base()
        {
        }

        protected Label AddCommunityOptionTitle_lbl => new Label(FindType.XPath, "//*[@id='comm-opts-modal']/section/header/h1");

        protected ListItem AllHouseOptions_lst
            => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbAllCommunityOptions']/div/ul/li").ToList());

        protected ListItem OtherMasterOptions_lst
            => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbCommunityOptions']/div/ul/li").ToList());

        protected Textbox Option_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_RadTextBox1']");


        protected Textbox SalePrice_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewCommunityOptionCost']");

        protected Button Standard_btn => new Button(FindType.XPath, "//*[@for='ctl00_CPH_Content_ckbNewCommunityOptionIsStandard']");

        protected Button AvailableToAllHouse_btn => new Button(FindType.XPath, "//*[@for='ctl00_CPH_Content_ckbNewCommunityOptionIsAvailableToAllHouses']");

        protected Button AddCommunityOptionToCommunity_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertCommunityOption']");

        protected Button Close_btn => new Button(FindType.XPath, "//*[@id='comm-opts-modal']/section/header/a");
    }

}
