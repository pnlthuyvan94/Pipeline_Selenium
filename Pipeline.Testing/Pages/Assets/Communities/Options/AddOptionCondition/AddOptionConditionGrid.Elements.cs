using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddOptionCondition
{
    public partial class AddOptionConditionGrid : CommunityOptionPage
    {
        string commonConditionGridXpath;
        string assignConditionGridXpath;
        string finalCondition;

        public AddOptionConditionGrid(string optionName) : base()
        {
            commonConditionGridXpath = $"//*[@id='ctl00_CPH_Content_rgHouseOptions_ctl00']/tbody/tr/td/a[starts-with(text(),'{optionName}')]/../../following-sibling::tr[1]/td[2]/table/thead/tr[2]";
            assignConditionGridXpath = $"//*[@id='ctl00_CPH_Content_rgHouseOptions_ctl00']/tbody/tr/td/a[starts-with(text(),'{optionName}')]/../../following-sibling::tr[1]/td[2]/table/thead/tr/th[text() = 'Condition']";
            finalCondition = $"//*[@id='ctl00_CPH_Content_rgHouseOptions_ctl00']/tbody/tr/td/a[starts-with(text(),'{optionName}')]/../../following-sibling::tr[1]/td[2]/table/tbody/tr";
        }

        Label AssignConditionGrid => new Label(FindType.XPath, assignConditionGridXpath);

        DropdownList OptionCondition_ddl => new DropdownList(FindType.XPath, commonConditionGridXpath + "/td[1]/select[1]");

        Button AddCondition_btn => new Button(FindType.XPath, commonConditionGridXpath + "/td[1]/a[1]");

        DropdownList Operator_ddl => new DropdownList(FindType.XPath, commonConditionGridXpath + "/td[1]/select[2]");

        Button AddOperator_btn => new Button(FindType.XPath, commonConditionGridXpath + "/td[1]/a[2]");

        Textbox OutputCondition_txt => new Textbox(FindType.XPath, commonConditionGridXpath + "/td[1]/div/input");

        Textbox ConditionPrice_txt => new Textbox(FindType.XPath, commonConditionGridXpath + "/td[2]/input");

        Button ApplyCondition_btn => new Button(FindType.XPath, commonConditionGridXpath + "/td[3]/input[contains(@src,'accept')]");

        Button CancelCondition_btn => new Button(FindType.XPath, commonConditionGridXpath + "/td[3]/input[contains(@src,'cross')]");

        // Final condition after appying
        Label FinalCondition_txt => new Label(FindType.XPath, finalCondition + "/td[1]");
        Label FinalConditionPrice_txt => new Label(FindType.XPath, finalCondition + "/td[2]");
    }

}
