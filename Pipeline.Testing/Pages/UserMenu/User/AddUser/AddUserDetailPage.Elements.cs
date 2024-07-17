using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.User.AddUser
{
    public partial class AddUserDetailPage : DetailsContentPage<AddUserDetailPage>
    {
        //private static IQueryable<Row> MetaData;

        public AddUserDetailPage() : base()
        {
            // Sheet contains repository of Dashboard
            //MetaData = UserPage.Instance.MetaData;
        }

        //private Row _title
        // => ExcelFactory.GetRow(MetaData, 1);
        protected Label Title_lbl
            => new Label(FindType.Id, "ctl00_CPH_Content_user1_lblHeaderText");

        //private Row _userName
        //   => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox UserName_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtNewUserName");

        //private Row _password
        //    => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox Password_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtNewPassword");

        //private Row _confirmPass
        //   => ExcelFactory.GetRow(MetaData, 4);
        protected Textbox ConfirmPass_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtNewPassword2");

        //private Row _email
        //   => ExcelFactory.GetRow(MetaData, 5);
        protected Textbox Email_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtNewUserEmail");

        //private Row _role
        // => ExcelFactory.GetRow(MetaData, 6);
        protected DropdownList Role_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_user1_ddlRolesNew");

        //private Row _active
        // => ExcelFactory.GetRow(MetaData, 7);
        protected CheckBox Active_cbk
            => new CheckBox(FindType.Id, "ctl00_CPH_Content_user1_chkNewIsApproved");

        //private Row _firstName
        // => ExcelFactory.GetRow(MetaData, 8);
        protected Textbox FirstName_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtFName");

        //private Row _lastName 
        //    => ExcelFactory.GetRow(MetaData, 9);
        protected Textbox LastName_txt 
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtLName");

        //private Row _phone => ExcelFactory.GetRow(MetaData, 10);
        protected Textbox Phone_txt 
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtPhone");

        //private Row _ext 
        //    => ExcelFactory.GetRow(MetaData, 11);
        protected Textbox Ext_txt 
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtExtension");

        //private Row _cell 
        //    => ExcelFactory.GetRow(MetaData, 12);
        protected Textbox Cell_txt 
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtCell");

        //private Row _fax 
        //    => ExcelFactory.GetRow(MetaData, 13);
        protected Textbox Fax_txt 
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtFax");

        //private Row _address1 
        //    => ExcelFactory.GetRow(MetaData, 14);
        protected Textbox Address1_txt 
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtAddress1");

        //private Row _address2 
        //    => ExcelFactory.GetRow(MetaData, 15);
        protected Textbox Address2_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtAddress2");

        //private Row _city 
        //    => ExcelFactory.GetRow(MetaData, 16);
        protected Textbox City_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtCity");

        //private Row _state 
        //    => ExcelFactory.GetRow(MetaData, 17);
        protected Textbox State_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtState");

        //private Row _zip 
        //    => ExcelFactory.GetRow(MetaData, 18);
        protected Textbox Zip_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_user1_txtZip");

        //private Row _save 
        //    => ExcelFactory.GetRow(MetaData, 19);
        protected Button Save_btn 
            => new Button(FindType.Id, "ctl00_CPH_Content_user1_lnkCreate");
    }

}
