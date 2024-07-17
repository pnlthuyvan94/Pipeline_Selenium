using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using System;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.Role.RolePermission
{
    public partial class RolePermissionPage : DetailsContentPage<RolePermissionPage>
    {

        //private IQueryable<Row> MetaData;
        //private IQueryable<Row> TestData_RT01107;
        public RolePermissionPage() : base()
        {
            // Sheet contains repository of Dashboard
            //TestData_RT01107 = RolePage.Instance.TestData_RT01107;
            //MetaData = RolePage.Instance.MetaData;
        }

        //private Row _permissionTitle => ExcelFactory.GetRow(MetaData, 7);
        protected Label Permission_lbl => new Label(FindType.XPath, "//*[@id='aspnetForm']/section/div[3]/article/section/article/header/h1");

        //private Row _permissionType => ExcelFactory.GetRow(MetaData, 8);
        protected DropdownList PermissionType_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewType");

        //private Row _savePermission => ExcelFactory.GetRow(MetaData, 9);
        protected Button SavePermission_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSavePermissions");

        //private Row _gridTitle => ExcelFactory.GetRow(MetaData, 10);
        protected Label GridTitle_lbl => new Label(FindType.XPath, "//*[@id='houses']/header/h1");

        //private Row _selectAll => ExcelFactory.GetRow(MetaData, 11);
        protected Button SelectAll_btn => new Button(FindType.XPath, "//*[contains(@id, 'lbCheckAll')]");

        //private Row _selectNone => ExcelFactory.GetRow(MetaData, 12);
        protected Button SelectNone_btn => new Button(FindType.XPath, "//*[contains(@id, 'lbCheckNone')]");

        String LoadingSelectPermission => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]";
        String LoadingSave => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]";
    }

}
