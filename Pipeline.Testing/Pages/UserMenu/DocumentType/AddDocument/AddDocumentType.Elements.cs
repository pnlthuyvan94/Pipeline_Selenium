using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;


namespace Pipeline.Testing.Pages.UserMenu.DocumentType.AddDocument
{
    public partial class AddDocumentType : DocumentTypePage
    {
        public AddDocumentType() : base()
        {
        }

        //private Row _name
        //    => ExcelFactory.GetRow(MetaData, 1);
        protected Textbox Name_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtName");

        protected ListItem SelectedCategory_lst
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbCategoriesIncluded']/div/ul/li"));

        //private Row _toLeftCategory
        //   => ExcelFactory.GetRow(MetaData, 3);
        protected Button CategoryToLeft_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbCategoriesIncluded']/table/tbody/tr/td/a[2]/span/span/span/span/span");

        protected ListItem AvailableCategory_lst
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbCategoriesAvailable']/div/ul/li"));

        protected ListItem SelectedRole_lst
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbRolesIncluded']/div/ul/li"));

        //private Row _toLeftRole
        //   => ExcelFactory.GetRow(MetaData, 6);
        protected Button RoleToLeft_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbRolesIncluded']/table/tbody/tr/td/a[2]/span/span/span/span/span");

        protected ListItem AvailableRole_lst
            => new ListItem(FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbRolesAvailable']/div/ul/li"));

        //private Row _insert
        //    => ExcelFactory.GetRow(MetaData, 8);
        protected Button Insert_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbAddNewType");

        //private Row _cancel
        //  => ExcelFactory.GetRow(MetaData, 9);
        protected Button Cancel_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbCancelAddEditType");

        //private Row _categoryModal
        //   => ExcelFactory.GetRow(MetaData, 13);
        protected Label CategoryModal_lbl
            => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddEditType']/section/table[2]/tbody/tr[1]/td/b");

        //private Row _roleVisibilityModal
        //   => ExcelFactory.GetRow(MetaData, 14);
        protected Label RoleVisibilityModal_lbl
            => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddEditType']/section/table[3]/tbody/tr[1]/td/b");
    }

}
