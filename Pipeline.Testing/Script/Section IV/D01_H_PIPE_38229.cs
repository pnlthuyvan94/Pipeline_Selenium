using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.BaseCosts;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class D01_H_PIPE_38229 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private VendorData vendorData;
        private const string NewVendorName = "RT_QA_New_Vendor_D01H";
        private const string NewVendorCode = "D01H";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_D01H";
        private const string NewBuildingGroupCode = "D01H";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_D01H";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_D01H";
        private const string NewBuildingPhaseCode = "D01H";

        private ProductData newProductData;
        private const string productDataName = "RT_QA_New_Product_D01H";
        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "D01H";

        [SetUp]
        public void Setup()
        {
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            System.Threading.Thread.Sleep(2000);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase.Code)
                                          .EnterPhaseName(newBuildingPhase.Name)
                                          .EnterAbbName(newBuildingPhase.AbbName)
                                          .EnterDescription(newBuildingPhase.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            vendorData = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode,
                Trade = "",
                Contact = "Contact",
                Email = "D01H@test.com",
                Address1 = "address1",
                Address2 = "address2",
                Address3 = "address3",
                City = "city",
                State = "state",
                Zip = "zip",
                Phone = "phone1",
                AltPhone = "phone2",
                MobilePhone = "00000000000",
                Fax = "fax",
                Url = "url@url.com",
                EnablePrecision = true
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add new vendor data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData);
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhase.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                    System.Threading.Thread.Sleep(2000);
                }
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new Product test data.</b></font>");
            newProductData = new ProductData()
            {
                Name = productDataName,
                Description = productDataName,
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
            };
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, newProductData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", newProductData.Name) is true)
            {
                ProductPage.Instance.DeleteProduct(newProductData.Name, false);
            }
            ProductPage.Instance.CreateNewProduct(newProductData);
            ProductDetailPage.Instance.AddManufacturersStyles(genericManufacturer, true, genericStyle, productCode);
            System.Threading.Thread.Sleep(2000);
            ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Manufacturer", "_00000000000000000000000000");
        }

        [Test]
        public void D01_H_Costing_Detail_Pages_Vendors_Base_Cost()
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Go to Vendor Base Cost page.</b></font>");
                VendorDetailPage.Instance.LeftMenuNavigation("Base Costs");

                if (BaseCostPage.Instance.IsProductBaseCostInGrid("Code", productCode) is true)
                {
                    BaseCostPage.Instance.DeleteItemInGrid(productCode);
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Click the Add Cost button to show the modal.</b></font>");
                BaseCostPage.Instance.ClickAddNewBaseCost();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Add new product base cost.</b></font>");
                BaseCostPage.Instance.AddNewBaseCost(productDataName + " / " + productDataName, genericStyle, "50", "60");

                if (BaseCostPage.Instance.IsProductBaseCostInGrid("Code", productCode) is true)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Product Base Cost was added successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Product Base Cost was not added.</b></font>");
                }
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Edit product base cost.</b></font>");
                BaseCostPage.Instance.EditBaseCost(productCode, "20", "30");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Delete product base cost.</b></font>");
                BaseCostPage.Instance.DeleteItemInGrid(productCode);

                if (BaseCostPage.Instance.IsProductBaseCostInGrid("Code", productCode) is false)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Product Base Cost was deleted successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Product Base Cost was not deleted.</b></font>");
                }
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Add new product base cost via Add All Available Styles button.</b></font>");
                BaseCostPage.Instance.ClickAddNewBaseCost();
                BaseCostPage.Instance.AddAllAvailableStyles(productDataName + " / " + productDataName, genericStyle, "70", "80");
                if (BaseCostPage.Instance.IsProductBaseCostInGrid("Code", productCode) is true)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Product Base Cost was added successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Product Base Cost was not added.</b></font>");
                }

            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete product.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, newProductData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", newProductData.Name) is true)
            {
                ProductPage.Instance.DeleteProduct(productDataName);
            }
            //delete bp from vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2 Delete building phase from vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhase.Code) is true)
                {
                    VendorBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
                }
            }
            //delete bp
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 Delete building phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
            }
            //delete bg
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4 Delete building group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteBuildingGroup(newBuildingGroup);
            }
        }
    }
}