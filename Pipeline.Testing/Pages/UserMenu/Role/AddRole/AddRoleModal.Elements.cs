using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.Role.AddRole
{
    public partial class AddRoleModal : RolePage
    {
        public AddRoleModal() : base()
        {
        }

        //private Row _modalTitle
        //    => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1");

        //private Row _name
        //   => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtRoleName");

        //private Row _save
        //    => ExcelFactory.GetRow(MetaData, 3);
        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertRole");

        //private Row _close
        //  => ExcelFactory.GetRow(MetaData, 4);
        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='options-modal']/section/header/a");

    }

}
