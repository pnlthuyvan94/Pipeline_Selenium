using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Options.Products
{
    public partial class ProductsToOptionPage : DashboardContentPage<ProductsToOptionPage>
    {
        Grid optionQuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00']", 
            "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
        Grid HouseOptionQuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00']",
    "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductsToHouses']/div[1]");
        protected DropdownList Community_list => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunities']");
        protected static string loadingIcon_Xpath => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected Button Generate_Bom_Button => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateGlobalBOM']");

        protected static string loadingIcon_GlobalBom => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgGlobalBOMReports']/div[1]";

        protected Grid GlobalBomQuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgGlobalBOMReports_ctl00']", loadingIcon_GlobalBom);

        protected Button HouseBulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions1']");
        protected Button OptionBulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");
        protected CheckBox AllOptionQuantities_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00']//input[contains(@id,'ctl00_CPH_Content_rgProducts_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox')]");
        protected CheckBox AllHouseQuantities_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00']//input[contains(@id,'ctl00_CPH_Content_rgProductsToHouses_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox')]");
    }

}
