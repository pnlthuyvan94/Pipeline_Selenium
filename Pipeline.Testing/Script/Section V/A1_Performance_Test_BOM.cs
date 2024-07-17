using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
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
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Assigments;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Calculations;
using Pipeline.Testing.Pages.Assets.Communities.Products;

namespace Pipeline.Testing.Script.Section_V
{
    class A1_Performance_Test_BOM : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        [SetUp]
        public void GetData()
        {
           
        }

        [Test]
        [Category("Section_V")]
        public void A1_PipelineBOM_Performance()
        {
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.NavigateURL("BuilderBom/HouseBom/HouseBom.aspx?hid=266");
            // HouseBOMDetailPage.Instance.SelectCommunity("HN-hainguyenCommunity");
            HouseBOMDetailPage.Instance.SelectCollection("ALL");
            HouseBOMDetailPage.Instance.GenerateHouseBOM("HN-hai nguyen community");
            HouseBOMDetailPage.Instance.RefreshPage();
            HouseBOMDetailPage.Instance.GenerateHouseBOM("Automation_01-QA_RT_Community01_Automation");

            //==== House BOM 2 =======//
            HousePage.Instance.NavigateURL("BuilderBom/HouseBom/HouseBom.aspx?hid=1969");
            HouseBOMDetailPage.Instance.SelectCollection("ALL");
            HouseBOMDetailPage.Instance.GenerateHouseBOM("_001-HN-QA-Community");

            //==== House BOM 3 =======//
            HousePage.Instance.NavigateURL("BuilderBom/HouseBom/HouseBom.aspx?hid=616");
            HouseBOMDetailPage.Instance.SelectCollection("ALL");
            HouseBOMDetailPage.Instance.GenerateHouseBOM("0005-Master Community 1");

        }
        [TearDown]
        public void DeleteData()
        {
            
        }

    }
}

