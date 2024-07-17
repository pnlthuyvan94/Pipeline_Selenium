using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;
using NUnit.Framework;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Assets.House.Import;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_J_PIPE_32981 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        HouseData _housedata; 
        ProductData  _productdata;
        private int totalItems;

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House04_Automation";

        private readonly string OPTION1_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION1_CODE_DEFAULT = "0100";
        private readonly string PARAMETER_DEFAULT = "SWG";

        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";
        private const string ImportType = "Pre-Import Modification";

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;


        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;

        private ProductToOptionData productToOption3;

        private ProductData productData_House_1;
        private ProductData productData_House_2;

        private ProductData productData_House_1_Style;
        private ProductData productData_House_2_Style;

        private ProductData productData_House_3;
        private ProductData productData_House_3_Style;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse1_Style;
        private ProductToOptionData productToHouse2_Style;
        private ProductToOptionData productToHouse3;
        private ProductToOptionData productToHouse3_Style;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;

        private ProductToOptionData productToHouseBOM3;

        private HouseQuantitiesData houseQuantities_DefaultCommunity;
        private HouseQuantitiesData houseQuantities_SpecificCommunity;

        private HouseQuantitiesData houseQuantities_HouseBOM_DefaultCommunity;
        private HouseQuantitiesData houseQuantities_HouseBOM_DefaultCommunity_Style;
        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunity;
        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunity_Style;

        [SetUp]
        public void GetTestData()
        {
            _housedata = new HouseData()
            {
                HouseName = "QA_RT_House04_Automation",
                SaleHouseName = "QA_RT_House04_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "400",
                BasePrice = "1000000",
                SQFTBasement = "1",
                SQFTFloor1 = "1",
                SQFTFloor2 = "2",
                SQFTHeated = "3",
                SQFTTotal = "7",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Test"
            };

            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_New_Product01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE"
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_New_Product02_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE"
            };

            productData_Option_3 = new ProductData()
            {
                Name = "QA_RT_New_Product03_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE"
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "120-QA_RT_Building_Phase_Automation",
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "120-QA_RT_Building_Phase_Automation",
                ProductList = new List<ProductData> { productData_Option_2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "120-QA_RT_Building_Phase_Automation",
                ProductList = new List<ProductData> { productData_Option_3 }
            };
            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1) { Quantities = "10.00" };

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2) { Quantities = "20.00" };

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1_Style = new ProductData(productData_Option_1) { Style = "QA_RT_Style2_Automation" };

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2_Style = new ProductData(productData_Option_2) { Style = "QA_RT_Style2_Automation" };

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3 = new ProductData(productData_Option_3) { Quantities = "30.00" };

            productData_House_3_Style = new ProductData(productData_Option_3) { Style = "QA_RT_Style2_Automation" };


            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse1_Style = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1_Style } };
            productToHouse2_Style = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2_Style } };

            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };
            productToHouse3_Style = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3_Style } };

            // There is no House quantities 
            houseQuantities_DefaultCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2 }
            };


            houseQuantities_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse3 }
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1) { Style = "QA_RT_Style_Automation" };
            ProductData productData_HouseBOM_2 = new ProductData(productData_Option_2) { Style = "QA_RT_Style_Automation" };
            ProductData productData_HouseBOM_3 = new ProductData(productData_Option_3) { Style = "QA_RT_Style_Automation" };

            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };

            productToHouseBOM3 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_3 } };

            houseQuantities_HouseBOM_DefaultCommunity = new HouseQuantitiesData(houseQuantities_DefaultCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2 }
            };

            houseQuantities_HouseBOM_DefaultCommunity_Style = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1_Style, productToHouse2_Style }
            };

            houseQuantities_HouseBOM_SpecificCommunity = new HouseQuantitiesData(houseQuantities_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM3 }
            };

            houseQuantities_HouseBOM_SpecificCommunity_Style = new HouseQuantitiesData(houseQuantities_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouse3_Style }
            };

        }
        [Test]
        [Category("Section_IV")]
        public void A04_J_Assets_DetailPage_Houses_ImportPage_HouseQuantitiesImport_Nolonger_Creates_Products_With_Default_Style()
        {

            //Group by Parameters settings is turned false
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Group by Parameters settings is turned false.</font><b>");
            //Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectGroupByParameter(false, PARAMETER_DEFAULT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);

            //Step 1: Import Products into a house with a blank style with a default community
            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Import Products into a house with a blank style with a default community.</font><b>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House default page.</font><b>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Hover over Assets  > click Houses then click a House that will be used for testing.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Hover over Assets  > click Houses then click a House that will be used for testing..</font><b>");
            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _housedata.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", _housedata.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(_housedata);
                string updateMsg = $"House {_housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _housedata.HouseName);

            }

            //Go to Assets/Houses/House detail/Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.1: Go to Assets/Houses/House detail/Import.font>");
            //Once navigated to House Details page click House BOM tab in the left nav panel.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Once navigated to House Details page click House BOM tab in the left nav panel.</b></font>");
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION1_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION1_NAME_DEFAULT + " - " + OPTION1_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT);
            }
            //Navigate To Import House Quantities
            //Step 1.2: Processing the import with specific community
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.2: Processing the import with specific community.</font>");
            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }
            //Processing the import with default community
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION1_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_A04_J_PIPE_32981.xml");

            //Step 1.2: Go to House quantities check data
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.3: Go to House quantities check data.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");

            foreach (ProductToOptionData housequantity in houseQuantities_DefaultCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_DefaultCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_DefaultCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //Navigate To House BOM
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            string HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            //Step 1.4: Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Generate House BOM and verify it.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_DefaultCommunity.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity.communityName);

            //Verify quantities In House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_DefaultCommunity);

            //Go to product detail/ change the default style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Go to product detail/ change the default style.</b></font>");
            //navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_1.Name);
                // b.Manufacturers and Style
                ExtentReportsHelper.LogInformation("<b>b. Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") is false)
                {
                    ProductDetailPage.Instance.AddManufacturersStyles("QA_RT_Manufacturer2_Automation", true, "QA_RT_Style2_Automation", string.Empty);
                }

            }
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_2.Name);
                // b.Manufacturers and Style
                ExtentReportsHelper.LogInformation("<b>b. Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") is false)
                {
                    ProductDetailPage.Instance.AddManufacturersStyles("QA_RT_Manufacturer2_Automation", true, "QA_RT_Style2_Automation", string.Empty);
                }
            }

            //Navigate To House BOM
            CommonHelper.OpenURL(HouseBOM_url);
            //Go to House BOM: Generate BOM again
            //Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6 :Generate House BOM.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_DefaultCommunity_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity_Style.communityName);

            //Verify quantities In House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM With Change Style.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_DefaultCommunity_Style);

            //Step 2 :Import Products into a house with a blank style with a specific community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2:Import Products into a house with a blank style with a specific community.</font>");
            //Import Products into a house with a blank style with a specific community
            //Step 2.1: Go to Assets/Houses/House detail/Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.1: Go to Assets/Houses/House detail/Import.</font>");

            // Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            //Processing the import with default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.2: Processing the import with default community.</font>");
            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION1_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_A04_J_PIPE_32981.xml");
            //Step 2.3: Go to House quantities and check data
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.3: Go to House quantities and check data.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }
            //Navigate To House BOM
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();

            //Generate House BOM and verify it
            //Step 2.4 :Go to House BOM: Generate BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4 :Go to House BOM: Generate BOM.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_SpecificCommunity.communityName);

            //Verify quantities In House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunity);

            //Step 2.5: Go to product detail/ change the default style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.5: Go to product detail/ change the default style.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_3.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_3.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_3.Name);
                // b.Manufacturers and Style
                ExtentReportsHelper.LogInformation("<b>Create Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") is false)
                {
                    ProductDetailPage.Instance.AddManufacturersStyles("QA_RT_Manufacturer2_Automation", true, "QA_RT_Style2_Automation", string.Empty);
                }

            }
            //Step 2.6: Go to House BOM: Generate BOM again
            //Navigate To House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6: Go to House BOM: Generate BOM again.</b></font>");
            CommonHelper.OpenURL(HouseBOM_url);

            //Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Generate House BOM.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_SpecificCommunity_Style.communityCode + "-" + houseQuantities_HouseBOM_SpecificCommunity_Style.communityName);

            //Verify quantities In House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM With Change Style.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunity_Style);

        }
        [TearDown]
        public void ClearData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_HouseBOM_SpecificCommunity_Style.communityCode + "-" + houseQuantities_HouseBOM_SpecificCommunity_Style.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Product Style .</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_1.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, "QA_RT_Style2_Automation");

            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_1.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", "QA_RT_Style2_Automation");
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_2.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, "QA_RT_Style2_Automation");
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_2.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", "QA_RT_Style2_Automation");
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_3.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, "QA_RT_Style2_Automation");
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_3.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_3.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", "QA_RT_Style2_Automation");
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }
        }
    }
}
