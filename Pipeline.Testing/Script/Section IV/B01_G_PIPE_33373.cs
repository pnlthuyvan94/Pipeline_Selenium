using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Calculations;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;


namespace Pipeline.Testing.Script.Section_IV
{
    class B01_G_PIPE_33373 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private CalculationData calculationData;

        private ProductData _product;
        private ProductData _product1;
        private ProductData _product2;
        private OptionData _option;
        private ProductData getNewproduct;

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private readonly string BUILDINGPHASE_SUBCOMPONENT_DEFAULT = "QA_2-QA_BuildingPhase_02_Automation";
        private readonly string BUILDINGPHASE_DEFAULT = "QA_1-QA_BuildingPhase_01_Automation";


        private readonly string BUILDINGPHASE_TO_DEFAULT = "QA_3-QA_BuildingPhase_03_Automation";

        private readonly string PRODUCT_SUBCOMPONENT_NAME_DEFAULT = "QA_Product_02_Automation";
        private readonly string STYLE_NAME_DEFAULT = "QA_Style_Automation";

        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION_CODE_DEFAULT = "0100";


        [SetUp]
        public void GetTestData()
        {
            _product = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent1_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation"
            };

            _product1 = new ProductData()
            {
                Name = "QA_Product_05_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "QA_3-QA_BuildingPhase_03_Automation"
            };

            _product2 = new ProductData()
            {
                Name = "QA_Product_02_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Testing",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation"
            };

            _option = new OptionData()
            {
                Name = "QA_RT_Option01_Automation",
                Number = "0100",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
            };
            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }

                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(_option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { _option.Name} and Number {_option.Number}.");
                    Assert.Fail($"Could not create Option.");
                }
                BasePage.PageLoad();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { _option.Name} is displayed in grid.</font>");
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CALCULATION_URL);

            calculationData = new CalculationData()
            {
                Description = "10",
                Calculation = "QTY +10"
            };
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.EqualTo, calculationData.Description);

            // Create a new Calculation if not existing.
            if (CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create Calation {calculationData.Description}.</b></font>");
                CalculationPage.Instance.CreateNewCalculation(calculationData);
            }


            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);
            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_Manu_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StyleData styleData = new StyleData()
            {
                Name = "QA_Style_Automation",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "_0001",
                Name = "QA_Building_Group_Automation"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }
            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_33373\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_33373\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);


            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();

        }
        [Test]
        [Category("Section_IV")]
        public void B01_G_Estimating_DetailPage_Products_Subcomponents_CopyModal()
        {
            //Show Category on Add Spec Set Product Conversion Modal - TURN OFF
            // Navigate setting/product
            ExtentReportsHelper.LogInformation(null, "<b> Show Category on Add Spec Set Product Conversion Modal - TURN OFF </b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);

            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);

            //Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            //The options displayed on copy modal 
            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
            }
            else
            {

                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedURL), "Product detail page isn't displayed");

                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproduct = ProductDetailPage.Instance.CreateAProduct(_product);

                // Verify new Product in header
                Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct), "Create new Product unsuccessfully");
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            //Navigate To Subcomponents
            ExtentReportsHelper.LogInformation(null, "<b> Navigate To Subcomponents</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Add subcomponent with type is Basic 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add subcomponent with type is Basic</b></font color>");
            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(_product.BuildingPhase)
                                            .SelectStyleOfProduct(_product.Style)
                                            .SelectChildBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(STYLE_NAME_DEFAULT)
                                            .SelectCalculationSubcomponent(calculationData.Description +" "+ $"({calculationData.Calculation})")
                                            .SelectOptionSubcomponent(_option.Name + " - " + _option.Number)
                                            .ClickSaveProductSubcomponent();

            //Verify add subcomponent
            string expectedMess = "Successfully added new subcomponent!";
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
            ProductSubcomponentPage.Instance.VerifyItemInGrid("Calculation", calculationData.Description + " " + $"({calculationData.Calculation})");
            //Verify Options List In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Options List In Grid</b></font color>");
            ProductSubcomponentPage.Instance.VerifyOptionsListInGrid(BUILDINGPHASE_SUBCOMPONENT_DEFAULT, OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);

            //Click on the “Copy Subcomponent” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click on the “Copy Subcomponent” button</b></font color>");
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
                                            .CopySubcomponentModal_SelectBuildingPhaseFrom(BUILDINGPHASE_DEFAULT)
                                            .CopySubcomponentModal_SelectSubcomponentToCopyFrom(BUILDINGPHASE_SUBCOMPONENT_DEFAULT);

            //Subcomponents: Filter the data and check the option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Subcomponents: Filter the data and check the option</b></font color>");
            ProductSubcomponentPage.Instance.VerifyOptionsListInCopySubcomponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT, OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            //On the copy Subcomponents: Filter the data and select he product to “Copy to”     
            ExtentReportsHelper.LogInformation(null, "<b>On the copy Subcomponents: Filter the data and select he product to “Copy to”     </b>");
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectBuildingPhaseTo(BUILDINGPHASE_TO_DEFAULT)
                                                .CopySubcomponentModal_SelectProduct("QA_Product_05_Automation")
                                                .CopySubcomponentModal_ClickSave();
            string act_CopyMessage = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            string expected_CopyMessage = "1 subcomponent(s) successfully copied.";
            if (act_CopyMessage == expected_CopyMessage)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b> Subcomponent was Copied successfully </b></font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>{act_CopyMessage}</font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

            //Go to the product copy to and check:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product1.Name);
            }
            else
            {
                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedURL), "Product detail page isn't displayed");

                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproduct = ProductDetailPage.Instance.CreateAProduct(_product1);

                // Verify new Product in header
                Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct), "Create new Product unsuccessfully");
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            //Navigate To Subcomponents
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
            //Verify Options List In Grid
            ProductSubcomponentPage.Instance.VerifyOptionsListInGrid(BUILDINGPHASE_SUBCOMPONENT_DEFAULT, OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            // //The description product show on the copy modal
            //Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product2.Name);
            }
            else
            {
                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                if (ProductDetailPage.Instance.IsPageDisplayed(expectedURL) is true)
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Product detail page is displayed.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Product detail page isn't displayed.</font>");
                }
                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproduct = ProductDetailPage.Instance.CreateAProduct(_product2);

                // Verify new Product in header
                if (ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct) is true)
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Create new Product successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create new Product unsuccessfully.</font>");
                }
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            //Show Subcomponent Description is turned false
            //The description product show on the copy modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Show Subcomponent Description is turned false.</font><b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            string EstimatingSetting_url = EstimatingPage.Instance.CurrentURL;
            EstimatingPage.Instance.VerifySettingEstimatingPageIsDisplayed();
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(false);
            

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product1.Name);
            }
            //Navigate To Subcomponent Page
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            //Verify Subcomponent Name In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Subcomponent Name In Grid.</font><b>");
            ProductSubcomponentPage.Instance.VerifySubcomponentNameOnGrid("QA_Product_02_Automation", string.Empty);
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
            .CopySubcomponentModal_SelectBuildingPhaseFrom(BUILDINGPHASE_TO_DEFAULT);
            ProductSubcomponentPage.Instance.VerifySubcomponentNameInCopySubcomponentModal("QA_Product_02_Automation" + " - " + "QA Testing");

            //Show Subcomponent Description is turned false
            //.Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Show Subcomponent Description is turned true.</font><b>");
            CommonHelper.OpenURL(EstimatingSetting_url);
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(true);

            //Go to Product Detail Page
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product1.Name);
            }
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            //Verify Subcomponent Name With show Description
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Subcomponent Name With show Description.</font><b>");
            ProductSubcomponentPage.Instance.VerifySubcomponentNameOnGrid("QA_Product_02_Automation", " - " + "QA Testing");
            //Click on the “Copy Subcomponent” button
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
            .CopySubcomponentModal_SelectBuildingPhaseFrom(BUILDINGPHASE_TO_DEFAULT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Subcomponent Name In Copy SubComponent Modal With show Description.</font><b>");
            ProductSubcomponentPage.Instance.VerifySubcomponentNameInCopySubcomponentModal("QA_Product_02_Automation" + " - " + "QA Testing");
        }

        [TearDown]
        public void DeleteData()
        {

            //Refresh Page
            CommonHelper.RefreshPage();
            //.Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change Subcomponent Description is turned false.</font><b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(false);

            //Delete SubComponent 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete SubComponent Name {BUILDINGPHASE_SUBCOMPONENT_DEFAULT}  .</font><b>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

            //Delete SubComponent 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete SubComponent Name {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} .</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL); 
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product1.Name);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

        }

        private void VerifyToastMessage(string expectedMess, string columnToVerify, string value)
        {
            string act_Message = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_Message == expectedMess)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Successfully added new subcomponent</b></font color>");
            }
            else
            {
                // Can't get toast message then verify the item on the grid view
                if (ProductSubcomponentPage.Instance.VerifyItemInGrid(columnToVerify, value) is false)
                {
                    ExtentReportsHelper.LogFail("Failed add new subcomponent");
                    ProductSubcomponentPage.Instance.CloseToastMessage();
                }
            }
        }
    }

}
