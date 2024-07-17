using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail
{
    public partial class SpecSetDetailPage : DetailsContentPage<SpecSetDetailPage>
    {
        protected Button ExpandAll_btn
             => new Button(FindType.XPath, "//input[@Title='Expand']");

        private string _loadingOnDetailPage
            => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]";

        protected IGrid SpecSet_Grid
            => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");

        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewSetName']");

        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='houses']/header/h1");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSet']");

        protected Button CloseModal_btn
            => new Button(FindType.XPath, "//*[@id='houses']/header/a");

        protected IGrid Style_Grid
            => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10']", _loadingOnDetailPage);

        protected IGrid Product_Grid
             => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11']", _loadingOnDetailPage);

        protected Button AddNewStyle_Btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl00_lbInsertNewStyle']");

        protected Button PerformAddNewStyle_Btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_PerformInsertButton']");

        protected Button AddNewProduct_Btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl00_lbInsertNewProduct']");

        protected Button PerformAddNewProduct_Btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_PerformInsertButton']");


        protected DropdownList OriManufacture_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlOriginalManufacturer']");

        protected DropdownList OriStyle_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlOriginalStyle']");

        protected DropdownList OriUse_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlOriginalUse']");


        protected DropdownList NewManufacture_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlNewManufacturer']");

        protected DropdownList NewStyle_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlNewStyle']");

        protected DropdownList NewUse_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlNewUse']");

        protected DropdownList StyleCalculation_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl02_ctl04_ddlCalculation']");


        protected DropdownList OriginalBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlOriginalBuildingPhase']");

        protected DropdownList OriginalProduct_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlOriginalProduct']");

        protected DropdownList OriginalProductStyle_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlOriginalProductStyles']");

        protected DropdownList OriProductUse_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlOriginalProductUse']");

        protected DropdownList NewBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlNewBuildingPhase']");

        protected DropdownList NewProduct_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlNewProduct']");

        protected DropdownList NewProductStyle_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlNewProductStyles']");

        protected DropdownList NewProductUse_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlNewProductUse']");

        protected DropdownList ProductCalculation_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlCalculationProds']");

        protected DropdownList OriginalCategory_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlOriginalCategory']");

        protected DropdownList NewCategory_ddl
        => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_ddlNewCategory']");


        /*--------Edit Product Conversion----*/
        protected DropdownList UpdateOriginalBuildingPhase_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlOriginalBuildingPhase");
        protected DropdownList UpdateOriginalProduct_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlOriginalProduct");
        protected DropdownList UpdateOriginalProductStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlOriginalProductStyles");
        protected DropdownList UpdateOriProductUse_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlOriginalProductUse");
        protected DropdownList UpdateNewBuildingPhase_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlNewBuildingPhase");
        protected DropdownList UpdateNewProduct_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlNewProduct");
        protected DropdownList UpdateNewProductStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlNewProductStyles");
        protected DropdownList UpdateNewProductUse_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlNewProductUse");
        protected DropdownList UpdateProductCalculation_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_ddlCalculationProds");
        protected Button UpdateProduct_Btn => new Button(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_UpdateButton");
        
        /*--------Edit Style Conversion----*/
        protected DropdownList UpdateOriManufactureStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlOriginalManufacturer");
        protected DropdownList UpdateOriStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlOriginalStyle");
        protected DropdownList UpdateOriUseStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlOriginalUse");
        protected DropdownList UpdateNewManufactureStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlNewManufacturer");
        protected DropdownList UpdateNewStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlNewStyle");
        protected DropdownList UpdateNewUseStyle_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlNewUse");
        protected DropdownList UpdateStyleCalculation_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_ddlCalculation");
        protected Button UpdateStyle_Btn => new Button(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11_ctl04_UpdateButton");

        protected IGrid Divisions_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgSets_Divisions_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Divisions']/div[1]");
        protected IGrid Communities_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgSets_Communities_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Communities']/div[1]");
        protected IGrid Houses_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgSets_Houses_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Houses']/div[1]");
        protected IGrid Jobs_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgSets_Jobs_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Jobs']/div[1]");
        protected IGrid Options_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgSets_Options_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Options']/div[1]");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='hypUtils']");

        protected Button AddNewProduct_Btn2 => new Button(FindType.Id, "ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl00_lbInsertNewProduct");
        protected DropdownList OriginalBuildingPhase2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlOriginalBuildingPhase']");
        protected DropdownList NewBuildingPhase2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlNewBuildingPhase']");
        protected DropdownList OriginalProductStyle2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlOriginalProductStyles']");
        protected DropdownList OriProductUse2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlOriginalProductUse']");
        protected DropdownList NewProductUse2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlNewProductUse']");
        protected DropdownList NewProductStyle2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlNewProductStyles']");
        protected DropdownList ProductCalculation2_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_ddlCalculationProds']");
        protected Button PerformAddNewProduct2_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_PerformInsertButton']");

    }

}
