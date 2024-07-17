using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    class B01_I_PIPE_45580 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string PRODUCT_NAME = "QA_RT_Automation_Product_195";
        private const string STYLE_NAME = "QA_RT_Automation_Style_195";
        private const string MANUFACTURER_NAME = "QA_RT_Automation_Manufacturer_195";
        private const string MANUFACTURE_STYLE_ELEMENT_ID = "ctl00_CPH_Content_Label1";
        private const string ADD_MANUFACTURER_BUTTON_ELEMENT_ID = "ctl00_CPH_Content_lbAddMfg2";
        private const string ADD_STYLE_BTN_ID = "ctl00_CPH_Content_lbAddStyle2";
        private const string ATTRIBUTE_NAME = "class";
        private const string ATRIBUTE_VALUE_TO_MATCH = "aspNetDisabled";

     

        [Test]
        [Category("Section_IV")]
        public void B01_I_Estimating_Detail_Page_Products_Add_Manufacturer_Add_Style_In_Products_Detail_Page()
        {
            //Step I: I. Verify when add Manufacturer
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I Verify the adding to Manufacturer</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            //Step I.1 Go to tab Estimating/Product, pick a product and go to its details
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I.1 Go to tab Estimating/Product, pick a product and go to its details</font>");
            ProductPage.Instance.EnterProductNameToFilter("Product Name", PRODUCT_NAME);
            ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT_NAME);

            //Step I.2 Scroll down to section "Manufacturer and styles" and click "Add"
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I.2 Scroll down to section 'Manufacturer and styles' and click 'Add'</font>");
            CommonHelper.ScrollToElement(MANUFACTURE_STYLE_ELEMENT_ID);
            ProductDetailPage.Instance.ClickManufacturerStyleAddBtn();            
            ProductDetailPage.Instance.ClickAddManufacturerButton();
            //Step I.3 Verify the button 'Add manufacturer' is now disabled
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I.3 Verify button 'Add manufacturer' disabled</font>");
            CommonHelper.IsElementDisabled(ADD_MANUFACTURER_BUTTON_ELEMENT_ID, ATTRIBUTE_NAME, ATRIBUTE_VALUE_TO_MATCH);

            //Step I.4 Verify their behavior/act here, the name of manufacturer must be unique
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I.4 Verify their behavior/act here, the name of manufacturer must be unique</font>");
            ProductDetailPage.Instance.InputTextToManufacturer(MANUFACTURER_NAME);
            ProductDetailPage.Instance.ClickInsertManufacturerButton();
            ProductDetailPage.Instance.IsErrorMessageManufacturerModalCorrect();
            //Step I.5 Click Cancel button to cancel the text box area
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I.5 Click Cancel button to cancel the textbox area </font>");
            ProductDetailPage.Instance.ClickManufactCancelButton();
            //Step II: II.Verify when add Style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step II Verify when add Style</font>");
            //Step II.1 Click on add style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step II.1 Click on 'add' style </font>");
            ProductDetailPage.Instance.ClickAddStyleButton();
            //Step II.2 Verify now the button Add style now is disabled
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step I.2 Verify button 'Add style' disabled</font>");
            CommonHelper.IsElementDisabled(ADD_STYLE_BTN_ID, ATTRIBUTE_NAME, ATRIBUTE_VALUE_TO_MATCH);

            //Step II.3 Verify the logic of style, the name of style must be unique
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step II.3 Verify the logic of style, the name of style must be unique</font>");
            ProductDetailPage.Instance.InputTextToStyle(STYLE_NAME);
            ProductDetailPage.Instance.ClickInsertStyleButton();
            ProductDetailPage.Instance.IsErrorMessageStyleModalCorrect();
            
        }
    }
}
