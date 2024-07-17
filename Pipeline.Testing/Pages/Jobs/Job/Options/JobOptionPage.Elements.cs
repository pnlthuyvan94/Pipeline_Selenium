using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Jobs.Job.Options
{
    public partial class JobOptionPage : DashboardContentPage<JobOptionPage>
    {
        protected Label OptionTitle_lbl => new Label(FindType.XPath, "//*[@class='card-content']//h1[text()='Options']");
        protected Button AprroveConfig_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbApproveConfig' and not (contains(@class, 'Disabled'))]");
        protected Button AddConfig_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddConfig']");
        protected DropdownList Configuration_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlConfigurations']");


        /******************************************** Option ************************************************/
        protected Button AddOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewOptions']");
        protected Button SaveOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertOption']");
        protected Button CloseOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelOption']");
        string optionLoadingGrid_xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]";
        private const string optionGrid_Xpath = "//*[@id='ctl00_CPH_Content_rgOptions_ctl00']";
        protected IGrid OptionPage_Grid => new Grid(FindType.XPath, optionGrid_Xpath, optionLoadingGrid_xpath);



        /******************************************** Custom Option ************************************************/
        protected Button AddCustomOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewCustomOptions']");
        protected Button SaveCustomOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertCustomOption']");
        protected Button CloseCustomOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelCustomOption']");
        private const string customOptLoadingGrid_xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomOptions']";
        private const string cusOptGrid_Xpath = "//*[@id='ctl00_CPH_Content_rgCustomOptions_ctl00']";
        protected IGrid CustomOptionPage_Grid => new Grid(FindType.XPath, cusOptGrid_Xpath, customOptLoadingGrid_xpath);
    }
}
