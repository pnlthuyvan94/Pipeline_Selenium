using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Calculations;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.TestData.Section_IV
{
    class TestData_PIPE_19175 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }

        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_19175";
        ProductData productData, productData1;
        BuildingPhaseData buildingPhaseData;
        BuildingGroupData buildingGroupData;
        CalculationData calculationInfo;
        ManufacturerData manuData, manuData1;
        StyleData styleData, styleData1;
        SpecSetData specSet, specSetProductConversion, specSetStyleConversion;

        [SetUp]

        public void SetupData()
        {
            buildingGroupData = new BuildingGroupData()
            {
                Code = "BG_195",
                Name = "QA_RT_Automation_BG_19175"
            };
            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "1975",
                Name = "QA_RT_Automation_BP_19175",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };

            //Create style, manufacturer, use

            manuData = new ManufacturerData()
            {
                Name = "QA_RT_Automation_Manufacturer_19175"
            };
            manuData1 = new ManufacturerData()
            {
                Name = "RT_QA_Automation_Manufacturer_19175"
            };

            styleData = new StyleData()
            {
                Name = "QA_RT_Automation_Style_19175",
                Manufacturer = manuData.Name
            };
            styleData1 = new StyleData()
            {
                Name = "RT_QA_Automation_Style_19175",
                Manufacturer = manuData1.Name
            };

            productData = new ProductData()
            {
                Name = "QA_RT_Automation_Product_19175"
            };
            productData1 = new ProductData()
            {
                Name = "RT_QA_Automation_Product_19175"
            };
            //Specset name and group
            specSet = new SpecSetData()
            {
                GroupName = "Automation_RT_QA_Specset_Group",
                SpectSetName = "RT_QA_Automation_SpecSet",
            };

            specSetProductConversion = new SpecSetData()
            {
                OriginalPhase = "1975-QA_RT_Automation_BP_19175",
                OriginalProduct = "QA_RT_Automation_Product_19175",
                OriginalProductStyle = "QA_RT_Automation_Style_19175",
                OriginalProductUse = "ALL",
                NewPhase = "1975-QA_RT_Automation_BP_19175",
                NewProduct = "RT_QA_Automation_Product_19175",
                NewProductStyle = "RT_QA_Automation_Style_19175",
                NewProductUse = "NONE",
                ProductCalculation = "RT_QA_Automation_Calculation (QTY*195)",
            };

            specSetStyleConversion = new SpecSetData()
            {
                OriginalManufacture = "QA_RT_Automation_Manufacturer_19175",
                OriginalStyle = "QA_RT_Automation_Style_19175",
                OriginalUse = "ALL",
                NewManufacture = "RT_QA_Automation_Manufacturer_19175",
                NewStyle = "RT_QA_Automation_Style_19175",
                NewUse = "NONE",
                StyleCalculation = "RT_QA_Automation_Calculation (QTY*195)",
            };
            calculationInfo = new CalculationData()
            {
                Description = "RT_QA_Automation_Calculation",
                Calculation = "QTY*195"
            };

        }
        [Test]

        public void SetUpTestData_B08_A_Estimating_Detail_Pages_Calculations_Calculation()
        {
            
            //-------------Create Data----------------//
            //Prepare a building group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a building group</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Building group has been existed</font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            // Prepare a building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a building phase</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseData.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code).EnterPhaseName(buildingPhaseData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            //Prepare a new Manufacturer 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Manufacturer to import Product</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (!ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name))
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (!ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name))
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
            }

            //Prepare a style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (!StylePage.Instance.IsItemInGrid("Name", styleData.Name))
            {
                //If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData1.Name);
            if (!StylePage.Instance.IsItemInGrid("Name", styleData1.Name))
            {
                //If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData1);
            }

            //Create calculation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create calculation.</font>");
            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.Contains, calculationInfo.Description);
            if (!CalculationPage.Instance.IsItemInGrid("Description", calculationInfo.Description))
            {
                CalculationPage.Instance.CreateNewCalculation(calculationInfo);
            }

            // Import Product, before building phase has been created by UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = $@"{IMPORT_FOLDER}\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);
            
            //Go to Product/subcomponent page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Go to Product/subcomponent page</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData.Name);
            ProductPage.Instance.SelectItemInGrid("Product Name", productData.Name);
            ProductPage.Instance.LeftMenuNavigation("Subcomponents");
            if (!ProductSubcomponentPage.Instance.IsSubcomponentInGrid())
            {
                ProductSubcomponentPage.Instance.ClickAdd_btn();

                ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                           .SelectBuildingPhaseOfProduct(buildingPhaseData.Code + "-" + buildingPhaseData.Name)
                                           .SelectStyleOfProduct(styleData.Name)                                           
                                           .SelectChildBuildingPhaseOfSubComponent(buildingPhaseData.Code + "-" + buildingPhaseData.Name)
                                           .InputProductSubcomponentWithoutCategory(productData1.Name)
                                           .SelectStyleOfSubComponent(styleData1.Name)
                                           .SelectCalculationSubcomponent(calculationInfo.Description + " (" + calculationInfo.Calculation + ")")
                                           .ClickSaveProductSubcomponent();
            }          
            
            //Step 4: Now import product conversion and style conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 4: Import Product conversion.</font><b>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSet.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSet.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSet.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSet.GroupName);
                SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(specSet.SpectSetName, "Expand");
                if(!SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSet.SpectSetName, "Calculation", calculationInfo.Description + " (" + calculationInfo.Calculation + ")"))
                {
                    SpecSetDetailPage.Instance.AddStyleConversion(specSetStyleConversion);         
                    SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(specSetProductConversion);
                } 
            }
            
        }

    }
}
