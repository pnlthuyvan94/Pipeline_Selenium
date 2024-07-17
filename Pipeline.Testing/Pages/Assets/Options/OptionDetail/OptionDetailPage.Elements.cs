using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Collections.Generic;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Options.OptionDetail
{
    public partial class OptionDetailPage : DetailsContentPage<OptionDetailPage>
    {
        protected Textbox OptionName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox OptionNumber_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNumber']");

        protected Textbox SquareFootage_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSize']");

        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Textbox SaleDescription_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSalesDescription']");

        protected DropdownList OptionGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlGroups']");

        protected DropdownList OptionRoom_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOptionRooms']");

        protected DropdownList CostGroup_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCostGroups']");

        protected DropdownList OptionType_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOptionTypes']");

        protected Textbox Price_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPrice']");

        private IList<CheckBox> TypeGroup_chkList => CommonHelper.CreateList<CheckBox>(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlContent']/section[11]/span/input").ToList());

        protected TypeOption Type => new TypeOption
        {
            Elevation = TypeGroup_chkList[0],
            AllowMultiples = TypeGroup_chkList[1],
            Global = TypeGroup_chkList[2]
        };

        protected class TypeOption
        {
            public CheckBox Elevation { get; set; }
            public CheckBox AllowMultiples { get; set; }
            public CheckBox Global { get; set; }
        }

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        protected Button AddSelection_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewOptionToSelection']");

        protected Button InsertSelection_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertNewSelection']");
        protected Button CloseSelectionModal_Btn => new Button(FindType.XPath, "//*[@id='addoptiontoselection - modal']/section/header/a");

        protected Label HeaderTitleOfModal_Lbl => new Label(FindType.XPath, "//*[@id='addoptiontoselection - modal']/section/header/h1");
        protected string loadingGifOfSelectionList => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlSelections']/div[1]";
        protected DropdownList SelectionGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSelectionGroups']");
        protected ListItem Selection_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSelections']/div/ul/li/label").ToList());
        protected IGrid Selection_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionsToSelections_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionsToSelections']/div[1]");
    }
}
