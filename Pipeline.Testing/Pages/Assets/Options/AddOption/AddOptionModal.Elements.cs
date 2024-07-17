using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Options.AddOption
{
    public partial class AddOptionModal : OptionPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath,"//*[@id='addoption-modal']/section/header/h1");

        protected Textbox OptionName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox OptionNumber_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNumber']");

        protected Textbox SquareFootage_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSize']");

        protected Textbox Description_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Textbox SaleDescription_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSalesDescription']");

        protected DropdownList OptionGroup_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlGroups']");

        protected DropdownList OptionRoom_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOptionRooms']");

        protected DropdownList CostGroup_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCostGroups']");

        protected DropdownList OptionType_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOptionTypes']");

        protected Textbox Price_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPrice']");

        private IList<CheckBox> TypeGroup_chkList
            => CommonHelper.CreateList<CheckBox>(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlContent']/section[10]/span/label").ToList());

        protected TypeOption Type => new TypeOption
        {
            Elevation = TypeGroup_chkList[0],
            AllowMultiples = TypeGroup_chkList[1],
            Global = TypeGroup_chkList[2]
        };

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertNewOption']");

        protected class TypeOption
        {
            public CheckBox Elevation { get; set; }
            public CheckBox AllowMultiples { get; set; }
            public CheckBox Global { get; set; }
        }
    }

}
