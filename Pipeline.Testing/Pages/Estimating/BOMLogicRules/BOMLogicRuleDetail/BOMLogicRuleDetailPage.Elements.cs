
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.BOMLogicRuleDetail
{
    public partial class BOMLogicRuleDetailPage : DetailsContentPage<BOMLogicRuleDetailPage>
    {
        protected IGrid Condition_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBOMLogicRuleConditions_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRuleConditions']/div[1]");

        Button Condition_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbConditionModal']");
        Button Action_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbActionModal']");
        Label AddCondition_lbl => new Label(FindType.XPath, "//*[@id='conditionTitle']");
        Label AddAction_lbl => new Label(FindType.XPath, "//*[@id='actionTitle']");
        DropdownList ConditionKey_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlConditionKey']");
        DropdownList ConditionKeyAttribute_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlConditionAttribute']");
        DropdownList Operator_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOperator']");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblSaveCondition']");
        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblCloseCondition']");
        protected Button UpdateSave_btn => new Button(FindType.XPath, "//*[contains(@id,'UpdateButton')]");
        DropdownList ActionKey_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlActionType']");
        DropdownList ActionValue_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlConditionAttribute']");
        protected Button ActionSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveAction']");
    }
}
