using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.CustomField
{
    public partial class CustomFieldModal : DetailsContentPage<CustomFieldModal>
    {
        // Xpath repository
        private string titleModal_Xpath = "//div[@aria-hidden='false']/section/header/h1[text()='Create New Custom Field']";
        private string formatField_Xpath = "//div[@aria-hidden='false']/section[header/h1[text()='Create New Custom Field']]/section[@class='card-section form']/section/span[./preceding-sibling::label[starts-with(text(),'{0}')]]/input";
        private string formatErrorField_Xpath = "(//div[@aria-hidden='false']/section[header/h1[text()='Create New Custom Field']]/section[@class='card-section form']/section/span[./preceding-sibling::label[starts-with(text(),'{0}')] and not(contains(@style,'none'))])[last()]";
        private string titleField_Xpath => string.Format(formatField_Xpath, "Title");
        private string descriptionField_Xpath => string.Format(formatField_Xpath, "Description");
        private string tagField_Xpath => string.Format(formatField_Xpath, "Tag");
        private string typeField_Xpath = "//div[@aria-hidden='false']/section[header/h1[text()='Create New Custom Field']]/section[@class='card-section form']/section/select[./preceding-sibling::label[starts-with(text(),'Field Type')]]";
        private string defaultField_Xpath = "//div[@aria-hidden='false']/section[header/h1[text()='Create New Custom Field']]/section[@class='card-section form']/section/*[./preceding-sibling::label[starts-with(text(),'Default')]]//input[@type='radio']";
        private string save_Xpath = "//div[@aria-hidden='false']/section[header/h1[text()='Create New Custom Field']]//a[text()='Save']";
        private string closeModal_Xpath = "//div[@aria-hidden='false']/section[header/h1[text()='Create New Custom Field']]//a[@class='close']";

        // Error message - use the last position
        private string titleErrorField_Xpath => string.Format(formatErrorField_Xpath, "Title");
        private string tagErrorField_Xpath => string.Format(formatErrorField_Xpath, "Tag");

        // Grid edit mode
        private string defaultEdit_Xpath = "//table[contains(@id,'rgCustomFields')]/tbody//tr[./td/input[contains(@id,'Title')]]/td/input[contains(@id,'IsRequired')]";
        private string editModeFormat = "//table[contains(@id,'rgCustomFields')]/tbody//{0}[contains(@id,'{1}')]";
        private string titleEdit_Xpath => string.Format(editModeFormat, "input", "Title");
        private string descriptionEdit_Xpath => string.Format(editModeFormat, "input", "Description");
        private string tagEdit_Xpath => string.Format(editModeFormat, "input", "Tag");
        private string typeEdit_Xpath => string.Format(editModeFormat, "select", "Type");
        private string updateBtn_Xpath => string.Format(editModeFormat, "input", "UpdateButton");

        // Grid 
        const string Xp_DefaultHead = "(//input[contains(@id,'IsRequired')])[1]";
        const string Xp_FilterButton = "(//input[contains(@id,'IsRequired')])[2]";


        public CustomFieldModal() : base() { }

        protected Label Title_lbl => new Label(FindType.XPath, titleModal_Xpath);
        protected Label TitleErrorField => new Label(FindType.XPath, titleErrorField_Xpath);
        protected Label TagErrorField => new Label(FindType.XPath, tagErrorField_Xpath);
        protected Textbox Title_Txt => new Textbox(FindType.XPath, titleField_Xpath);
        protected Textbox Description_Txt => new Textbox(FindType.XPath, descriptionField_Xpath);
        protected Textbox Tag_Txt => new Textbox(FindType.XPath, tagField_Xpath);
        protected DropdownList FieldType_Dll => new DropdownList(FindType.XPath, typeField_Xpath);
        protected Button Save_btn => new Button(FindType.XPath, save_Xpath);
        protected Button CloseModal_btn => new Button(FindType.XPath, closeModal_Xpath);

        // Edit mode
        protected Textbox TitleEdit_Txt => new Textbox(FindType.XPath, titleEdit_Xpath);
        protected Textbox DescriptionEdit_Txt => new Textbox(FindType.XPath, descriptionEdit_Xpath);
        protected Textbox TagEdit_Txt => new Textbox(FindType.XPath, tagEdit_Xpath);
        protected DropdownList FieldTypeEdit_Dll => new DropdownList(FindType.XPath, typeEdit_Xpath);
        protected CheckBox DefaultEdit_Chk => new CheckBox(FindType.XPath, defaultEdit_Xpath);
        protected Button Update_Btn => new Button(FindType.XPath, updateBtn_Xpath);

        // Grid
        protected CheckBox DefaultHeader_Chk => new CheckBox(FindType.XPath, Xp_DefaultHead);
        protected Button FilterPanel_Btn => new Button(FindType.XPath, Xp_FilterButton);

    }
}
