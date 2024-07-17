using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.CustomField
{
    public partial class CustomFieldDetailPage : DetailsContentPage<CustomFieldDetailPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\CustomFieldDetail\CustomFieldDetailParams.xlsx";

        private const string cancelButton_Xpath = "//*[@id='ctl00_CPH_Content_lbRemove' and text()='Cancel']";
        private const string removeButton_Xpath = "//*[@id='ctl00_CPH_Content_lbRemove' and text()='Remove Custom Fields']";
        private const string saveButton_Id = "ctl00_CPH_Content_lbSave";
        private const string addBtn_Id = "ctl00_CPH_Content_lbAddNew";
        private const string insertBtn_Id = "ctl00_CPH_Content_lbInsert";
        private const string removeLoadingGif_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbRemove']/div[1]";
        private const string addLoadingGif_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNew']/div[1]";
        private const string modalTitile_Xpath = "//*[@id='addcategory-modal']/section/header/h1";
        private const string listCustomField_Xpath = "//*[@id='addcategory-modal']/section//div/ul/li//input[../span[text()='{0} - {1}']]";
        private const string noMoreCustomFieldsLabel_Xpath = "//*[@id='ctl00_CPH_Content_rlbAttributes']/div/div[1]";
        private const string listNameCustomFieldInModal_Xpah = "//*[@id='ctl00_CPH_Content_rlbAttributes']/div/ul/li/label/span";


        public IQueryable<Row> CustomOnHouse { get; set; }

        public CustomFieldDetailPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            CustomOnHouse = ExcelHelper.GetAllRows("RT_01228");
        }

        Label HeaderTitle_Lbl => new Label(FindType.XPath, "//header/h1/span");
        protected Button Cancel_Btn => new Button(FindType.XPath, cancelButton_Xpath);
        protected Button Remove_Btn => new Button(FindType.XPath, removeButton_Xpath);
        protected Button Save_Btn => new Button(FindType.Id, saveButton_Id);
        protected Button Add_Btn => new Button(FindType.Id, addBtn_Id);
        protected Button Insert_Btn => new Button(FindType.Id, insertBtn_Id);

        // Add Modal
        protected Label ModalTitle_lbl => new Label(FindType.XPath, modalTitile_Xpath);
        protected Label noMoreCustomField_lbl => new Label(FindType.XPath, noMoreCustomFieldsLabel_Xpath);
        protected CheckBox ModalCheckAll_ckb => new CheckBox(FindType.XPath, "//input[@class = 'rlbCheckAllItemsCheckBox']");

        //HOUSE CUSTOM FIELD
        protected Textbox TestNumberValue_Txb => new Textbox(FindType.XPath, "//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//input[@type = 'number']");
        protected CheckBox TestCheckboxValue_Ckb => new CheckBox(FindType.XPath, "//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//input[@type = 'checkbox']");
        protected Label StaticLabelValue_Lbl => new Label(FindType.XPath, "//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//section/span[contains(.,'Static')]/following-sibling::span");
        protected Button RemoveCustomField_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbRemove");
        protected Button TestNumberX_Btn => new Button(FindType.XPath, "//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//span[contains(.,'TestNumber')]/following-sibling::a");
        protected Button TestCheckBoxX_Btn => new Button(FindType.XPath, "//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//span[contains(.,'TestCheckBox')]/following-sibling::a");
        protected Button StaticX_Btn => new Button(FindType.XPath, "//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//span[contains(.,'Static')]/following-sibling::a");



    }
}
