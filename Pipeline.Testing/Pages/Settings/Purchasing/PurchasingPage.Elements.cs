using Pipeline.Common.Pages;
using Pipeline.Common.Enums;
using Pipeline.Common.Controls;

namespace Pipeline.Testing.Pages.Settings.Purchasing
{
    public partial class PurchasingPage : DetailsContentPage<PurchasingPage>
    {
        protected CheckBox HideZeroCostItems_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_cbHideZeroCostItem']");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
        protected DropdownList DefaultJobBudgetView_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlBudgetViews");
    }
}
