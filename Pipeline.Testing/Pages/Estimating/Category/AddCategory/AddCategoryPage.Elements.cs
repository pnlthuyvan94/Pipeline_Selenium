using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Category.AddCategory
{
    public partial class AddCategoryPage : CategoryPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='options-modal']/div/article/section/article/header/h1");

        protected Textbox CategoryName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected DropdownList CategoryParent_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlParent']");


        protected Button CategorySave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");

        protected Button CategoryClose_btn
           => new Button(FindType.XPath, "//*[@id='options-modal']/div/article/section/article/header/a");
    }
}
