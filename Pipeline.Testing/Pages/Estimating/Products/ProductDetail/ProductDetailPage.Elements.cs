using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;


namespace Pipeline.Testing.Pages.Estimating.Products.ProductDetail
{
    public partial class ProductDetailPage : DetailsContentPage<ProductDetailPage>
    {
        // Element of Building Phases
        protected Button AddBuildingPhase_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddNewBuildingPhase");
        protected Button SaveBuildingPhase_btn => new Button(FindType.XPath, "//*[contains(@id,'lbInsertBuildingPhase')]");
        protected DropdownList BuildingPhases_ddl => new DropdownList(FindType.XPath, "//*[contains(@name,'ddlNewBuildingPhase')]");
        protected DropdownList TaxStatus_ddl => new DropdownList(FindType.XPath, "//*[contains(@id,'ddlNewProductInPhaseTaxStatus')]");
        protected CheckBox NewPhaseDefault_cb => new CheckBox(FindType.XPath, "//*[contains(@id,'ckbNewPhaseIsDefault')]");

        // Element of Manufacturers and Styles Modal
        protected Button AddManufacturer_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbNewStyle");
        protected Button SaveManufacturer_btn => new Button(FindType.XPath, "//*[contains(@id,'lbInsertNewStyle')]");
        protected DropdownList Manufacturer_ddl => new DropdownList(FindType.XPath, "//*[contains(@name,'ddlNewManufacturers')]");
        protected DropdownList StyleManufacturer_ddl => new DropdownList(FindType.XPath, "//*[contains(@name,'ddlNewStyles')]");
        protected CheckBox NewManufacturerDefault_cb => new CheckBox(FindType.XPath, "//*[contains(@id,'ckbNewIsDefault')]");
        protected Textbox ProductCodeManufacturer_txt => new Textbox(FindType.XPath, "//*[contains(@id,'txtNewProductCode')]");

        protected Button AddNewManufacturerButton_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddMfg2");
        protected Button ManufacInsert_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertMfg2");
        protected Button ManufacCancel_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbCancelMfg2");
        protected Textbox ManufacturerInputText_tbx => new Textbox(FindType.Id, "ctl00_CPH_Content_txtMfgInsert2");
        protected Label ManufacturerError_lb => new Label(FindType.Id, "error-manufacturer");
        protected Button AddStyle_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddStyle2");
        protected Button StyleInsert_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertStyle2");
        protected Button StyleCancel_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbCancelStyle2");
        protected Textbox StyleInputText_tbx => new Textbox(FindType.Id, "ctl00_CPH_Content_txtStyleInsert2");
        protected Label StyleError_lb => new Label(FindType.Id, "error-style");


        // Element of Catelories
        protected Button AddCatelories_btn => new Button(FindType.XPath, "//*[contains(@id,'lbNewCategory')]");
        protected Button SaveCatelories_btn => new Button(FindType.XPath, "//*[contains(@id,'lbInsertCategory')]");

        // Element of House Note Overrides
        protected Button AddHouseNote_btn => new Button(FindType.XPath, "//*[contains(@id,'lbAddHouseNotes')]");
        protected Button SaveHouseNote_btn => new Button(FindType.XPath, "//*[contains(@id,'lbSaveHouseNoteOverride')]");
        protected Textbox HouseNotes_txt => new Textbox(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_txtAddHouseNotes')]");

        // Element of Job Note Overrides

        protected Button JobNotes_btn => new Button(FindType.XPath, "//*[contains(@id,'lbAddJobNotes')]");
        protected Button SaveJobNotes_btn => new Button(FindType.XPath, "//a[contains(@id,'lbSaveJobNoteOverride')]");
        protected Textbox JobNotes_txt => new Textbox(FindType.XPath, "//textarea[contains(@id,'txtAddJobNotes')]");

        // Table
        protected IGrid HouseNoteOverrides_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgNotesHouse_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesHouse']/div[1]");
        protected IGrid JobNoteOverrides_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgNotesJob_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesJob']/div[1]");
        protected IGrid Category_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgCategories_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCategories']/div[1]");



        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesHouse"]/div[1]

        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected DropdownList Manufactur_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlMfg']");

        protected DropdownList Style_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyle']");

        protected Textbox ProductCode_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddNewProductCode']");

        protected Textbox Description_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Textbox Note_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNotes']");

        protected DropdownList Unit_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlUnit']");

        protected Textbox SKU_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSKU']");

        protected RoundingRule RoundingRule => new RoundingRule();

        protected Textbox RoundingUnit_txt

            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAccuracy']");

        protected Textbox Waste_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtWaste']");

        protected DropdownList BuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhase']");

        protected CheckBox Supplemental_chk
            => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbSupToBid']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        protected string _ManuGrid => "//*[@id='ctl00_CPH_Content_rgStyles_ctl00']";
        protected string _buildingGrid => "//*[@id='ctl00_CPH_Content_rgPhases_ctl00']";
        protected IGrid Manufacture_grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgStyles_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");

        protected IGrid Categories_grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCategories_ctl00']", "");

        protected IGrid BuildingPhase_grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhases_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
    }

    public class RoundingRule
    {
        public CheckBox StandardRounding_chk
            => new CheckBox(
                    FindType.XPath,
                    (string.Format("//*[@id='ctl00_CPH_Content_rbRoundingType']/tbody/tr/td/input[../label[text()='{0}']]", "Standard Rounding"))
                );

        public CheckBox AlwaysRoundUp_chk
            => new CheckBox(
                    FindType.XPath,
                    (string.Format("//*[@id='ctl00_CPH_Content_rbRoundingType']/tbody/tr/td/input[../label[text()='{0}']]", "Always Round Up"))
                );

        public CheckBox AlwaysRoundDown_chk
            => new CheckBox(
                    FindType.XPath,
                    (string.Format("//*[@id='ctl00_CPH_Content_rbRoundingType']/tbody/tr/td/input[../label[text()='{0}']]", "Always Round Down"))
                );


        public string Rule
        {
            get
            {
                string _rule = string.Empty;
                if (StandardRounding_chk.IsChecked)
                    _rule += FindElementHelper.Instance().FindElement(FindType.XPath, "//label[@for='ctl00_CPH_Content_rbRoundingType_0']").Text + ";";
                if (AlwaysRoundUp_chk.IsChecked)
                    _rule += FindElementHelper.Instance().FindElement(FindType.XPath, "//label[@for='ctl00_CPH_Content_rbRoundingType_1']").Text + ";";
                if (AlwaysRoundDown_chk.IsChecked)
                    _rule += FindElementHelper.Instance().FindElement(FindType.XPath, "//label[@for='ctl00_CPH_Content_rbRoundingType_2']").Text + ";";
                return _rule;
            }
        }
    }
}
